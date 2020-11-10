using System;
using NMS.Leo.Typed;
using Xunit;

namespace NCallerUT
{
    [Trait("TypedVisitor.Loop", "LeoLooper")]
    public class TypedVisitorLoopTests : Prepare
    {
        [Fact(DisplayName = "直接实例循环操作测试")]
        public void DirectInstanceLoopTest()
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

            string s0 = "", s1 = "", s2 = "";
            int i2 = 0;
            var ss = "NameAgeBirthdayCountryIsValid";
            var ii = 0 + 1 + 2 + 3 + 4;

            var l0 = v.ForEach((s, o) => { s0 += s; });
            var l1 = v.ForEach((s, o, m) => { s1 += s; });
            var l2 = v.ForEach(c =>
            {
                s2 += c.Name;
                i2 += c.Index;
            });

            Assert.Equal("", s0);
            Assert.Equal("", s1);
            Assert.Equal("", s2);
            Assert.Equal(0, i2);

            l0.Fire();
            l1.Fire();
            l2.Fire();

            Assert.Equal(ss, s0);
            Assert.Equal(ss, s1);
            Assert.Equal(ss, s2);
            Assert.Equal(ii, i2);

            l2.Fire();
            Assert.Equal($"{ss}{ss}", s2);
            Assert.Equal(ii + ii, i2);
        }

        [Fact(DisplayName = "直接类型循环操作测试")]
        public void DirectFutureLoopTest()
        {
            var type = typeof(NiceAct);
            var v = LeoVisitorFactory.Create(type);

            v.SetValue("Name", "Du");
            v.SetValue("Age", 55);
            v.SetValue("Country", Country.USA);
            v.SetValue("Birthday", DateTime.Today.AddDays(-1));
            v.SetValue("IsValid", true);

            string s0 = "", s1 = "", s2 = "";
            int i2 = 0;
            var ss = "NameAgeBirthdayCountryIsValid";
            var ii = 0 + 1 + 2 + 3 + 4;

            var l0 = v.ForEach((s, o) => { s0 += s; });
            var l1 = v.ForEach((s, o, m) => { s1 += s; });
            var l2 = v.ForEach(c =>
            {
                s2 += c.Name;
                i2 += c.Index;
            });

            Assert.Equal("", s0);
            Assert.Equal("", s1);
            Assert.Equal("", s2);
            Assert.Equal(0, i2);

            l0.Fire();
            l1.Fire();
            l2.Fire();

            Assert.Equal(ss, s0);
            Assert.Equal(ss, s1);
            Assert.Equal(ss, s2);
            Assert.Equal(ii, i2);

            l2.Fire();
            Assert.Equal($"{ss}{ss}", s2);
            Assert.Equal(ii + ii, i2);
        }

        [Fact(DisplayName = "泛型实例循环操作测试")]
        public void GenericInstanceLoopTest()
        {
            var act = new NiceAct()
            {
                Name = "Hu",
                Age = 22,
                Country = Country.China,
                Birthday = DateTime.Today
            };

            var v = LeoVisitorFactory.Create<NiceAct>(act);

            string s0 = "", s1 = "", s2 = "";
            int i2 = 0;
            var ss = "NameAgeBirthdayCountryIsValid";
            var ii = 0 + 1 + 2 + 3 + 4;

            var l0 = v.ForEach((s, o) => { s0 += s; });
            var l1 = v.ForEach((s, o, m) => { s1 += s; });
            var l2 = v.ForEach(c =>
            {
                s2 += c.Name;
                i2 += c.Index;
            });

            Assert.Equal("", s0);
            Assert.Equal("", s1);
            Assert.Equal("", s2);
            Assert.Equal(0, i2);

            l0.Fire();
            l1.Fire();
            l2.Fire();

            Assert.Equal(ss, s0);
            Assert.Equal(ss, s1);
            Assert.Equal(ss, s2);
            Assert.Equal(ii, i2);

            l2.Fire();
            Assert.Equal($"{ss}{ss}", s2);
            Assert.Equal(ii + ii, i2);
        }

        [Fact(DisplayName = "泛型类型循环操作测试")]
        public void GenericFutureLoopTest()
        {
            var v = LeoVisitorFactory.Create<NiceAct>();

            v.SetValue("Name", "Du");
            v.SetValue("Age", 55);
            v.SetValue("Country", Country.USA);
            v.SetValue("Birthday", DateTime.Today.AddDays(-1));
            v.SetValue("IsValid", true);

            string s0 = "", s1 = "", s2 = "";
            int i2 = 0;
            var ss = "NameAgeBirthdayCountryIsValid";
            var ii = 0 + 1 + 2 + 3 + 4;

            var l0 = v.ForEach((s, o) => { s0 += s; });
            var l1 = v.ForEach((s, o, m) => { s1 += s; });
            var l2 = v.ForEach(c =>
            {
                s2 += c.Name;
                i2 += c.Index;
            });

            Assert.Equal("", s0);
            Assert.Equal("", s1);
            Assert.Equal("", s2);
            Assert.Equal(0, i2);

            l0.Fire();
            l1.Fire();
            l2.Fire();

            Assert.Equal(ss, s0);
            Assert.Equal(ss, s1);
            Assert.Equal(ss, s2);
            Assert.Equal(ii, i2);

            l2.Fire();
            Assert.Equal($"{ss}{ss}", s2);
            Assert.Equal(ii + ii, i2);
        }
    }
}