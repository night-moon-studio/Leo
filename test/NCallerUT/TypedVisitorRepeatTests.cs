using System;
using System.Collections.Generic;
using NCallerUT.Model;
using NMS.Leo.Typed;
using Xunit;

namespace NCallerUT
{
    [Trait("TypedVisitor.Repeat", "LeoRepeater")]
    public class TypedVisitorRepeatTests : Prepare
    {
        [Fact(DisplayName = "直接实例重复操作测试")]
        public void DirectInstanceRepeatTest()
        {
            var act = new NiceAct()
            {
                Name = "Hu",
                Age = 22,
                Country = Country.China,
                Birthday = DateTime.Today
            };

            var type = typeof(NiceAct);
            var v = LeoVisitorFactory.Create(type, act);

            v.SetValue("Name", "Du");
            v.SetValue("Age", 55);
            v.SetValue("Country", Country.USA);
            v.SetValue("Birthday", DateTime.Today.AddDays(-1));
            v.SetValue("IsValid", true);

            var n1 = new NiceAct();
            var b1 = v.TryRepeat(n1, out var o1);
            var to1 = (NiceAct) o1;

            Assert.True(b1);
            Assert.Equal("Du", to1.Name);
            Assert.Equal(55, to1.Age);
            Assert.Equal(Country.USA, to1.Country);
            Assert.Equal(DateTime.Today.AddDays(-1), to1.Birthday);
            Assert.True(to1.IsValid);

            v["Name"] = "Au";
            v["Age"] = 77;
            v["Country"] = Country.China;
            v["Birthday"] = DateTime.Today.AddDays(1);
            v["IsValid"] = false;

            var n2 = new NiceAct();
            var b2 = v.TryRepeat(n2, out var o2);
            var to2 = (NiceAct) o2;

            var b3 = v.TryRepeat(n1, out var o3);
            var to3 = (NiceAct) o3;

            var b4 = v.TryRepeat(out var o4);
            var to4 = (NiceAct) o4;

            var b5 = v.TryRepeatAs<NiceAct>(out var o5);

            var n6 = new NiceAct();
            var b6 = v.TryRepeatAs<NiceAct>(n6, out var o6);

            Assert.True(b2);
            Assert.Equal("Au", to2.Name);
            Assert.Equal(77, to2.Age);
            Assert.Equal(Country.China, to2.Country);
            Assert.Equal(DateTime.Today.AddDays(1), to2.Birthday);
            Assert.False(to2.IsValid);

            Assert.True(b3);
            Assert.Equal("Au", to3.Name);
            Assert.Equal(77, to3.Age);
            Assert.Equal(Country.China, to3.Country);
            Assert.Equal(DateTime.Today.AddDays(1), to3.Birthday);
            Assert.False(to3.IsValid);

            Assert.True(b4);
            Assert.Equal("Au", to4.Name);
            Assert.Equal(77, to4.Age);
            Assert.Equal(Country.China, to4.Country);
            Assert.Equal(DateTime.Today.AddDays(1), to4.Birthday);
            Assert.False(to4.IsValid);

            Assert.True(b5);
            Assert.Equal("Au", o5.Name);
            Assert.Equal(77, o5.Age);
            Assert.Equal(Country.China, o5.Country);
            Assert.Equal(DateTime.Today.AddDays(1), o5.Birthday);
            Assert.False(o5.IsValid);

            Assert.True(b6);
            Assert.Equal("Au", o6.Name);
            Assert.Equal(77, o6.Age);
            Assert.Equal(Country.China, o6.Country);
            Assert.Equal(DateTime.Today.AddDays(1), o6.Birthday);
            Assert.False(o6.IsValid);

            var repeater = v.ForRepeat();
            var n7 = new NiceAct();
            var o7 = (NiceAct) repeater.Play(n7);
            var o8 = (NiceAct) repeater.NewAndPlay();

            Assert.Equal("Au", o7.Name);
            Assert.Equal(77, o7.Age);
            Assert.Equal(Country.China, o7.Country);
            Assert.Equal(DateTime.Today.AddDays(1), o7.Birthday);
            Assert.False(o7.IsValid);

            Assert.Equal("Au", o8.Name);
            Assert.Equal(77, o8.Age);
            Assert.Equal(Country.China, o8.Country);
            Assert.Equal(DateTime.Today.AddDays(1), o8.Birthday);
            Assert.False(o8.IsValid);

            var d = new Dictionary<string, object>();
            d["Name"] = "AuX";
            d["Age"] = 707;
            d["Country"] = Country.USA;
            d["Birthday"] = DateTime.Today.AddDays(10);
            d["IsValid"] = true;

            var b10 = v.TryRepeat(d, out var o10);
            var to10 = (NiceAct) o10;

            var b11 = v.TryRepeatAs<NiceAct>(d, out var o11);

            var o12 = (NiceAct) repeater.Play(d);

            Assert.True(b10);
            Assert.Equal("Au", to10.Name);
            Assert.Equal(77, to10.Age);
            Assert.Equal(Country.China, to10.Country);
            Assert.Equal(DateTime.Today.AddDays(1), to10.Birthday);
            Assert.False(to10.IsValid);

            Assert.True(b11);
            Assert.Equal("Au", o11.Name);
            Assert.Equal(77, o11.Age);
            Assert.Equal(Country.China, o11.Country);
            Assert.Equal(DateTime.Today.AddDays(1), o11.Birthday);
            Assert.False(o11.IsValid);

            Assert.Equal("Au", o12.Name);
            Assert.Equal(77, o12.Age);
            Assert.Equal(Country.China, o12.Country);
            Assert.Equal(DateTime.Today.AddDays(1), o12.Birthday);
            Assert.False(o12.IsValid);
        }

        [Fact(DisplayName = "直接类型重复操作测试")]
        public void DirectFutureRepeatTest()
        {
            var type = typeof(NiceAct);
            var v = LeoVisitorFactory.Create(type);

            v.SetValue("Name", "Du");
            v.SetValue("Age", 55);
            v.SetValue("Country", Country.USA);
            v.SetValue("Birthday", DateTime.Today.AddDays(-1));
            v.SetValue("IsValid", true);

            var n1 = new NiceAct();
            var b1 = v.TryRepeat(n1, out var o1);
            var to1 = (NiceAct) o1;

            Assert.True(b1);
            Assert.Equal("Du", to1.Name);
            Assert.Equal(55, to1.Age);
            Assert.Equal(Country.USA, to1.Country);
            Assert.Equal(DateTime.Today.AddDays(-1), to1.Birthday);
            Assert.True(to1.IsValid);

            v["Name"] = "Au";
            v["Age"] = 77;
            v["Country"] = Country.China;
            v["Birthday"] = DateTime.Today.AddDays(1);
            v["IsValid"] = false;

            var n2 = new NiceAct();
            var b2 = v.TryRepeat(n2, out var o2);
            var to2 = (NiceAct) o2;

            var b3 = v.TryRepeat(n1, out var o3);
            var to3 = (NiceAct) o3;

            var b4 = v.TryRepeat(out var o4);
            var to4 = (NiceAct) o4;

            var b5 = v.TryRepeatAs<NiceAct>(out var o5);

            var n6 = new NiceAct();
            var b6 = v.TryRepeatAs<NiceAct>(n6, out var o6);

            Assert.True(b2);
            Assert.Equal("Au", to2.Name);
            Assert.Equal(77, to2.Age);
            Assert.Equal(Country.China, to2.Country);
            Assert.Equal(DateTime.Today.AddDays(1), to2.Birthday);
            Assert.False(to2.IsValid);

            Assert.True(b3);
            Assert.Equal("Au", to3.Name);
            Assert.Equal(77, to3.Age);
            Assert.Equal(Country.China, to3.Country);
            Assert.Equal(DateTime.Today.AddDays(1), to3.Birthday);
            Assert.False(to3.IsValid);

            Assert.True(b4);
            Assert.Equal("Au", to4.Name);
            Assert.Equal(77, to4.Age);
            Assert.Equal(Country.China, to4.Country);
            Assert.Equal(DateTime.Today.AddDays(1), to4.Birthday);
            Assert.False(to4.IsValid);

            Assert.True(b5);
            Assert.Equal("Au", o5.Name);
            Assert.Equal(77, o5.Age);
            Assert.Equal(Country.China, o5.Country);
            Assert.Equal(DateTime.Today.AddDays(1), o5.Birthday);
            Assert.False(o5.IsValid);

            Assert.True(b6);
            Assert.Equal("Au", o6.Name);
            Assert.Equal(77, o6.Age);
            Assert.Equal(Country.China, o6.Country);
            Assert.Equal(DateTime.Today.AddDays(1), o6.Birthday);
            Assert.False(o6.IsValid);

            var repeater = v.ForRepeat();
            var n7 = new NiceAct();
            var o7 = (NiceAct) repeater.Play(n7);
            var o8 = (NiceAct) repeater.NewAndPlay();

            Assert.Equal("Au", o7.Name);
            Assert.Equal(77, o7.Age);
            Assert.Equal(Country.China, o7.Country);
            Assert.Equal(DateTime.Today.AddDays(1), o7.Birthday);
            Assert.False(o7.IsValid);

            Assert.Equal("Au", o8.Name);
            Assert.Equal(77, o8.Age);
            Assert.Equal(Country.China, o8.Country);
            Assert.Equal(DateTime.Today.AddDays(1), o8.Birthday);
            Assert.False(o8.IsValid);

            var d = new Dictionary<string, object>();
            d["Name"] = "AuX";
            d["Age"] = 707;
            d["Country"] = Country.USA;
            d["Birthday"] = DateTime.Today.AddDays(10);
            d["IsValid"] = true;

            var b10 = v.TryRepeat(d, out var o10);
            var to10 = (NiceAct) o10;

            var b11 = v.TryRepeatAs<NiceAct>(d, out var o11);

            var o12 = (NiceAct) repeater.Play(d);

            Assert.True(b10);
            Assert.Equal("Au", to10.Name);
            Assert.Equal(77, to10.Age);
            Assert.Equal(Country.China, to10.Country);
            Assert.Equal(DateTime.Today.AddDays(1), to10.Birthday);
            Assert.False(to10.IsValid);

            Assert.True(b11);
            Assert.Equal("Au", o11.Name);
            Assert.Equal(77, o11.Age);
            Assert.Equal(Country.China, o11.Country);
            Assert.Equal(DateTime.Today.AddDays(1), o11.Birthday);
            Assert.False(o11.IsValid);

            Assert.Equal("Au", o12.Name);
            Assert.Equal(77, o12.Age);
            Assert.Equal(Country.China, o12.Country);
            Assert.Equal(DateTime.Today.AddDays(1), o12.Birthday);
            Assert.False(o12.IsValid);
        }

        [Fact(DisplayName = "泛型实例重复操作测试")]
        public void GenericInstanceTest()
        {
            var act = new NiceAct()
            {
                Name = "Hu",
                Age = 22,
                Country = Country.China,
                Birthday = DateTime.Today
            };

            var v = LeoVisitorFactory.Create<NiceAct>(act);

            v.SetValue("Name", "Du");
            v.SetValue("Age", 55);
            v.SetValue("Country", Country.USA);
            v.SetValue("Birthday", DateTime.Today.AddDays(-1));
            v.SetValue("IsValid", true);

            var n1 = new NiceAct();
            var b1 = v.TryRepeat(n1, out var o1);
            var to1 = (NiceAct) o1;

            Assert.True(b1);
            Assert.Equal("Du", to1.Name);
            Assert.Equal(55, to1.Age);
            Assert.Equal(Country.USA, to1.Country);
            Assert.Equal(DateTime.Today.AddDays(-1), to1.Birthday);
            Assert.True(to1.IsValid);

            v["Name"] = "Au";
            v["Age"] = 77;
            v["Country"] = Country.China;
            v["Birthday"] = DateTime.Today.AddDays(1);
            v["IsValid"] = false;

            var n2 = new NiceAct();
            var b2 = v.TryRepeat(n2, out var o2);

            var b3 = v.TryRepeat(n1, out var o3);

            var b4 = v.TryRepeat(out var o4);

            var b5 = v.TryRepeatAs<NiceAct>(out var o5);

            var n6 = new NiceAct();
            var b6 = v.TryRepeatAs<NiceAct>(n6, out var o6);

            Assert.True(b2);
            Assert.Equal("Au", o2.Name);
            Assert.Equal(77, o2.Age);
            Assert.Equal(Country.China, o2.Country);
            Assert.Equal(DateTime.Today.AddDays(1), o2.Birthday);
            Assert.False(o2.IsValid);

            Assert.True(b3);
            Assert.Equal("Au", o3.Name);
            Assert.Equal(77, o3.Age);
            Assert.Equal(Country.China, o3.Country);
            Assert.Equal(DateTime.Today.AddDays(1), o3.Birthday);
            Assert.False(o3.IsValid);

            Assert.True(b4);
            Assert.Equal("Au", o4.Name);
            Assert.Equal(77, o4.Age);
            Assert.Equal(Country.China, o4.Country);
            Assert.Equal(DateTime.Today.AddDays(1), o4.Birthday);
            Assert.False(o4.IsValid);

            Assert.True(b5);
            Assert.Equal("Au", o5.Name);
            Assert.Equal(77, o5.Age);
            Assert.Equal(Country.China, o5.Country);
            Assert.Equal(DateTime.Today.AddDays(1), o5.Birthday);
            Assert.False(o5.IsValid);

            Assert.True(b6);
            Assert.Equal("Au", o6.Name);
            Assert.Equal(77, o6.Age);
            Assert.Equal(Country.China, o6.Country);
            Assert.Equal(DateTime.Today.AddDays(1), o6.Birthday);
            Assert.False(o6.IsValid);

            var repeater = v.ForRepeat();
            var n7 = new NiceAct();
            var o7 = repeater.Play(n7);
            var o8 = repeater.NewAndPlay();

            Assert.Equal("Au", o7.Name);
            Assert.Equal(77, o7.Age);
            Assert.Equal(Country.China, o7.Country);
            Assert.Equal(DateTime.Today.AddDays(1), o7.Birthday);
            Assert.False(o7.IsValid);

            Assert.Equal("Au", o8.Name);
            Assert.Equal(77, o8.Age);
            Assert.Equal(Country.China, o8.Country);
            Assert.Equal(DateTime.Today.AddDays(1), o8.Birthday);
            Assert.False(o8.IsValid);

            var d = new Dictionary<string, object>();
            d["Name"] = "AuX";
            d["Age"] = 707;
            d["Country"] = Country.USA;
            d["Birthday"] = DateTime.Today.AddDays(10);
            d["IsValid"] = true;

            var b10 = v.TryRepeat(d, out var to10);

            var b11 = v.TryRepeatAs<NiceAct>(d, out var o11);

            var o12 =  repeater.Play(d);

            Assert.True(b10);
            Assert.Equal("Au", to10.Name);
            Assert.Equal(77, to10.Age);
            Assert.Equal(Country.China, to10.Country);
            Assert.Equal(DateTime.Today.AddDays(1), to10.Birthday);
            Assert.False(to10.IsValid);

            Assert.True(b11);
            Assert.Equal("Au", o11.Name);
            Assert.Equal(77, o11.Age);
            Assert.Equal(Country.China, o11.Country);
            Assert.Equal(DateTime.Today.AddDays(1), o11.Birthday);
            Assert.False(o11.IsValid);

            Assert.Equal("Au", o12.Name);
            Assert.Equal(77, o12.Age);
            Assert.Equal(Country.China, o12.Country);
            Assert.Equal(DateTime.Today.AddDays(1), o12.Birthday);
            Assert.False(o12.IsValid);
        }

        [Fact(DisplayName = "泛型类型重复操作测试")]
        public void GenericFutureTest()
        {
            var v = LeoVisitorFactory.Create<NiceAct>();

            v.SetValue("Name", "Du");
            v.SetValue("Age", 55);
            v.SetValue("Country", Country.USA);
            v.SetValue("Birthday", DateTime.Today.AddDays(-1));
            v.SetValue("IsValid", true);

            var n1 = new NiceAct();
            var b1 = v.TryRepeat(n1, out var o1);
            var to1 = (NiceAct) o1;

            Assert.True(b1);
            Assert.Equal("Du", to1.Name);
            Assert.Equal(55, to1.Age);
            Assert.Equal(Country.USA, to1.Country);
            Assert.Equal(DateTime.Today.AddDays(-1), to1.Birthday);
            Assert.True(to1.IsValid);

            v["Name"] = "Au";
            v["Age"] = 77;
            v["Country"] = Country.China;
            v["Birthday"] = DateTime.Today.AddDays(1);
            v["IsValid"] = false;

            var n2 = new NiceAct();
            var b2 = v.TryRepeat(n2, out var o2);

            var b3 = v.TryRepeat(n1, out var o3);

            var b4 = v.TryRepeat(out var o4);

            var b5 = v.TryRepeatAs<NiceAct>(out var o5);

            var n6 = new NiceAct();
            var b6 = v.TryRepeatAs<NiceAct>(n6, out var o6);

            Assert.True(b2);
            Assert.Equal("Au", o2.Name);
            Assert.Equal(77, o2.Age);
            Assert.Equal(Country.China, o2.Country);
            Assert.Equal(DateTime.Today.AddDays(1), o2.Birthday);
            Assert.False(o2.IsValid);

            Assert.True(b3);
            Assert.Equal("Au", o3.Name);
            Assert.Equal(77, o3.Age);
            Assert.Equal(Country.China, o3.Country);
            Assert.Equal(DateTime.Today.AddDays(1), o3.Birthday);
            Assert.False(o3.IsValid);

            Assert.True(b4);
            Assert.Equal("Au", o4.Name);
            Assert.Equal(77, o4.Age);
            Assert.Equal(Country.China, o4.Country);
            Assert.Equal(DateTime.Today.AddDays(1), o4.Birthday);
            Assert.False(o4.IsValid);

            Assert.True(b5);
            Assert.Equal("Au", o5.Name);
            Assert.Equal(77, o5.Age);
            Assert.Equal(Country.China, o5.Country);
            Assert.Equal(DateTime.Today.AddDays(1), o5.Birthday);
            Assert.False(o5.IsValid);

            Assert.True(b6);
            Assert.Equal("Au", o6.Name);
            Assert.Equal(77, o6.Age);
            Assert.Equal(Country.China, o6.Country);
            Assert.Equal(DateTime.Today.AddDays(1), o6.Birthday);
            Assert.False(o6.IsValid);

            var repeater = v.ForRepeat();
            var n7 = new NiceAct();
            var o7 = repeater.Play(n7);
            var o8 = repeater.NewAndPlay();

            Assert.Equal("Au", o7.Name);
            Assert.Equal(77, o7.Age);
            Assert.Equal(Country.China, o7.Country);
            Assert.Equal(DateTime.Today.AddDays(1), o7.Birthday);
            Assert.False(o7.IsValid);

            Assert.Equal("Au", o8.Name);
            Assert.Equal(77, o8.Age);
            Assert.Equal(Country.China, o8.Country);
            Assert.Equal(DateTime.Today.AddDays(1), o8.Birthday);
            Assert.False(o8.IsValid);

            var d = new Dictionary<string, object>();
            d["Name"] = "AuX";
            d["Age"] = 707;
            d["Country"] = Country.USA;
            d["Birthday"] = DateTime.Today.AddDays(10);
            d["IsValid"] = true;

            var b10 = v.TryRepeat(d, out var to10);

            var b11 = v.TryRepeatAs<NiceAct>(d, out var o11);

            var o12 = repeater.Play(d);

            Assert.True(b10);
            Assert.Equal("Au", to10.Name);
            Assert.Equal(77, to10.Age);
            Assert.Equal(Country.China, to10.Country);
            Assert.Equal(DateTime.Today.AddDays(1), to10.Birthday);
            Assert.False(to10.IsValid);

            Assert.True(b11);
            Assert.Equal("Au", o11.Name);
            Assert.Equal(77, o11.Age);
            Assert.Equal(Country.China, o11.Country);
            Assert.Equal(DateTime.Today.AddDays(1), o11.Birthday);
            Assert.False(o11.IsValid);

            Assert.Equal("Au", o12.Name);
            Assert.Equal(77, o12.Age);
            Assert.Equal(Country.China, o12.Country);
            Assert.Equal(DateTime.Today.AddDays(1), o12.Birthday);
            Assert.False(o12.IsValid);
        }
    }
}