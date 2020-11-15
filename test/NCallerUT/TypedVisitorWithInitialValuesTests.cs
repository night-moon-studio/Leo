using System;
using System.Collections.Generic;
using NMS.Leo;
using NMS.Leo.Typed;
using Xunit;

namespace NCallerUT
{
    [Trait("TypedVisitorWithInitialValue", "LeoVisitorWithInitialValue")]
    public class TypedVisitorWithInitialValuesTests : Prepare
    {
        [Fact(DisplayName = "直接类型测试")]
        public void DirectFutureTest()
        {
            var d = new Dictionary<string, object>();
            d["Name"] = "Haha";
            d["Age"] = 505;
            d["Country"] = Country.China;
            d["Birthday"] = DateTime.Today.AddDays(9);
            d["IsValid"] = true;

            var type = typeof(NiceAct);
            var v = LeoVisitorFactory.Create(type, d);

            Assert.False(v.IsStatic);
            Assert.Equal(typeof(NiceAct), v.SourceType);
            Assert.Equal(AlgorithmKind.Precision, v.AlgorithmKind);

            Assert.Equal("Haha", v.GetValue<string>("Name"));
            Assert.Equal(505, v.GetValue<int>("Age"));
            Assert.Equal(Country.China, v.GetValue<Country>("Country"));
            Assert.Equal(DateTime.Today.AddDays(9), v.GetValue<DateTime>("Birthday"));
            Assert.True(v.GetValue<bool>("IsValid"));

            v.SetValue("Name", "Du");
            v.SetValue("Age", 55);
            v.SetValue("Country", Country.USA);
            v.SetValue("Birthday", DateTime.Today.AddDays(-1));
            v.SetValue("IsValid", true);

            Assert.Equal("Du", v.GetValue<string>("Name"));
            Assert.Equal(55, v.GetValue<int>("Age"));
            Assert.Equal(Country.USA, v.GetValue<Country>("Country"));
            Assert.Equal(DateTime.Today.AddDays(-1), v.GetValue<DateTime>("Birthday"));
            Assert.True(v.GetValue<bool>("IsValid"));

            v["Name"] = "Au";
            v["Age"] = 77;
            v["Country"] = Country.China;
            v["Birthday"] = DateTime.Today.AddDays(1);
            v["IsValid"] = false;

            Assert.Equal("Au", v.GetValue<string>("Name"));
            Assert.Equal(77, v.GetValue<int>("Age"));
            Assert.Equal(Country.China, v.GetValue<Country>("Country"));
            Assert.Equal(DateTime.Today.AddDays(1), v.GetValue<DateTime>("Birthday"));
            Assert.False(v.GetValue<bool>("IsValid"));

            v.SetValue<NiceAct>(x => x.Name, "Zu");
            v.SetValue<NiceAct>(x => x.Age, 99);
            v.SetValue<NiceAct>(x => x.Country, Country.USA);
            v.SetValue<NiceAct>(x => x.Birthday, DateTime.Today.AddDays(2));
            v.SetValue<NiceAct>(x => x.IsValid, true);

            Assert.Equal("Zu", v.GetValue<NiceAct>(x => x.Name));
            Assert.Equal(99, v.GetValue<NiceAct>(x => x.Age));
            Assert.Equal(Country.USA, v.GetValue<NiceAct>(x => x.Country));
            Assert.Equal(DateTime.Today.AddDays(2), v.GetValue<NiceAct>(x => x.Birthday));
            Assert.True((bool) v.GetValue<NiceAct>(x => x.IsValid));

            v.SetValue<NiceAct, string>(x => x.Name, "Xu");
            v.SetValue<NiceAct, int>(x => x.Age, 199);
            v.SetValue<NiceAct, Country>(x => x.Country, Country.China);
            v.SetValue<NiceAct, DateTime>(x => x.Birthday, DateTime.Today.AddDays(-2));
            v.SetValue<NiceAct, bool>(x => x.IsValid, false);

            Assert.Equal("Xu", v.GetValue<NiceAct, string>(x => x.Name));
            Assert.Equal(199, v.GetValue<NiceAct, int>(x => x.Age));
            Assert.Equal(Country.China, v.GetValue<NiceAct, Country>(x => x.Country));
            Assert.Equal(DateTime.Today.AddDays(-2), v.GetValue<NiceAct, DateTime>(x => x.Birthday));
            Assert.False(v.GetValue<NiceAct, bool>(x => x.IsValid));

            d["Name"] = "Ax";
            d["Age"] = 10086;
            d["Country"] = Country.China;
            d["Birthday"] = DateTime.Today.AddDays(10);
            d["IsValid"] = true;

            v.SetValue(d);

            Assert.Equal("Ax", d["Name"]);
            Assert.Equal(10086, d["Age"]);
            Assert.Equal(Country.China, d["Country"]);
            Assert.Equal(DateTime.Today.AddDays(10), d["Birthday"]);
            Assert.True((bool) d["IsValid"]);
        }

        [Fact(DisplayName = "泛型类型测试")]
        public void GenericFutureTest()
        {
            var d = new Dictionary<string, object>();
            d["Name"] = "Haha";
            d["Age"] = 505;
            d["Country"] = Country.China;
            d["Birthday"] = DateTime.Today.AddDays(9);
            d["IsValid"] = true;

            var v = LeoVisitorFactory.Create<NiceAct>(d);

            Assert.False(v.IsStatic);
            Assert.Equal(typeof(NiceAct), v.SourceType);
            Assert.Equal(AlgorithmKind.Precision, v.AlgorithmKind);

            Assert.Equal("Haha", v.GetValue<string>("Name"));
            Assert.Equal(505, v.GetValue<int>("Age"));
            Assert.Equal(Country.China, v.GetValue<Country>("Country"));
            Assert.Equal(DateTime.Today.AddDays(9), v.GetValue<DateTime>("Birthday"));
            Assert.True(v.GetValue<bool>("IsValid"));

            v.SetValue("Name", "Du");
            v.SetValue("Age", 55);
            v.SetValue("Country", Country.USA);
            v.SetValue("Birthday", DateTime.Today.AddDays(-1));
            v.SetValue("IsValid", true);

            Assert.Equal("Du", v.GetValue<string>("Name"));
            Assert.Equal(55, v.GetValue<int>("Age"));
            Assert.Equal(Country.USA, v.GetValue<Country>("Country"));
            Assert.Equal(DateTime.Today.AddDays(-1), v.GetValue<DateTime>("Birthday"));
            Assert.True(v.GetValue<bool>("IsValid"));

            v["Name"] = "Au";
            v["Age"] = 77;
            v["Country"] = Country.China;
            v["Birthday"] = DateTime.Today.AddDays(1);
            v["IsValid"] = false;

            Assert.Equal("Au", v.GetValue<string>("Name"));
            Assert.Equal(77, v.GetValue<int>("Age"));
            Assert.Equal(Country.China, v.GetValue<Country>("Country"));
            Assert.Equal(DateTime.Today.AddDays(1), v.GetValue<DateTime>("Birthday"));
            Assert.False(v.GetValue<bool>("IsValid"));

            v.SetValue<NiceAct>(x => x.Name, "Zu");
            v.SetValue<NiceAct>(x => x.Age, 99);
            v.SetValue<NiceAct>(x => x.Country, Country.USA);
            v.SetValue<NiceAct>(x => x.Birthday, DateTime.Today.AddDays(2));
            v.SetValue<NiceAct>(x => x.IsValid, true);

            Assert.Equal("Zu", v.GetValue<NiceAct>(x => x.Name));
            Assert.Equal(99, v.GetValue<NiceAct>(x => x.Age));
            Assert.Equal(Country.USA, v.GetValue<NiceAct>(x => x.Country));
            Assert.Equal(DateTime.Today.AddDays(2), v.GetValue<NiceAct>(x => x.Birthday));
            Assert.True((bool) v.GetValue<NiceAct>(x => x.IsValid));

            v.SetValue<NiceAct, string>(x => x.Name, "Xu");
            v.SetValue<NiceAct, int>(x => x.Age, 199);
            v.SetValue<NiceAct, Country>(x => x.Country, Country.China);
            v.SetValue<NiceAct, DateTime>(x => x.Birthday, DateTime.Today.AddDays(-2));
            v.SetValue<NiceAct, bool>(x => x.IsValid, false);

            Assert.Equal("Xu", v.GetValue<NiceAct, string>(x => x.Name));
            Assert.Equal(199, v.GetValue<NiceAct, int>(x => x.Age));
            Assert.Equal(Country.China, v.GetValue<NiceAct, Country>(x => x.Country));
            Assert.Equal(DateTime.Today.AddDays(-2), v.GetValue<NiceAct, DateTime>(x => x.Birthday));
            Assert.False(v.GetValue<NiceAct, bool>(x => x.IsValid));

            v.SetValue<string>(x => x.Name, "Lu");
            v.SetValue<int>(x => x.Age, 11);
            v.SetValue<Country>(x => x.Country, Country.USA);
            v.SetValue<DateTime>(x => x.Birthday, DateTime.Today);
            v.SetValue<bool>(x => x.IsValid, true);

            Assert.Equal("Lu", v.GetValue(x => x.Name));
            Assert.Equal(11, v.GetValue(x => x.Age));
            Assert.Equal(Country.USA, v.GetValue(x => x.Country));
            Assert.Equal(DateTime.Today, v.GetValue(x => x.Birthday));
            Assert.True(v.GetValue(x => x.IsValid));

            d["Name"] = "Ax";
            d["Age"] = 10086;
            d["Country"] = Country.China;
            d["Birthday"] = DateTime.Today.AddDays(10);
            d["IsValid"] = true;

            v.SetValue(d);

            Assert.Equal("Ax", d["Name"]);
            Assert.Equal(10086, d["Age"]);
            Assert.Equal(Country.China, d["Country"]);
            Assert.Equal(DateTime.Today.AddDays(10), d["Birthday"]);
            Assert.True((bool) d["IsValid"]);
        }
    }
}