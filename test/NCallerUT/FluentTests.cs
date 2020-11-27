using System;
using System.Collections.Generic;
using NCallerUT.Model;
using NMS.Leo.Typed;
using Xunit;

namespace NCallerUT
{
    [Trait("TypedVisitor.Fluent", "Fluent")]
    public class FluentTests : Prepare
    {
        [Fact(DisplayName = "Fluent Getter")]
        public void FluentGetterTest()
        {
            var type = typeof(NiceAct);
            var act = new NiceAct()
            {
                Name = "Hu",
                Age = 22,
                Country = Country.China,
                Birthday = DateTime.Today
            };

            var d = new Dictionary<string, object>();
            d["Name"] = "Ax";
            d["Age"] = 10086;
            d["Country"] = Country.China;
            d["Birthday"] = DateTime.Today.AddDays(10);
            d["IsValid"] = true;

            var g1 = LeoGetter.Type(type).Instance(act);
            var g2 = LeoGetter.Type(type).InitialValues(d);
            var g3 = LeoGetter.Type<NiceAct>().Instance(act);
            var g4 = LeoGetter.Type<NiceAct>().InitialValues(d);

            var g5 = LeoGetter.Type(type).Value("Name");
            var g6 = LeoGetter.Type<NiceAct>().Value("Name");
            var g7 = LeoGetter.Type<NiceAct>().Value(t => t.Name);
            var g8 = LeoGetter.Type<NiceAct>().Value<string>(t => t.Name);

            Assert.Equal("Hu", g1.GetValue<string>("Name"));
            Assert.Equal("Ax", g2.GetValue<string>("Name"));
            Assert.Equal("Hu", g3.GetValue<string>("Name"));
            Assert.Equal("Ax", g4.GetValue<string>("Name"));

            Assert.Equal("Hu", g5.Instance(act).Value);
            Assert.Equal("Hu", g6.Instance(act).Value);
            Assert.Equal("Hu", g7.Instance(act).Value);
            Assert.Equal("Hu", g8.Instance(act).Value);
        }

        [Fact(DisplayName = "Fluent Setter")]
        public void FluentSetterTest()
        {
            var type = typeof(NiceAct);
            var act = new NiceAct()
            {
                Name = "Hu",
                Age = 22,
                Country = Country.China,
                Birthday = DateTime.Today
            };

            var d = new Dictionary<string, object>();
            d["Name"] = "Ax";
            d["Age"] = 10086;
            d["Country"] = Country.China;
            d["Birthday"] = DateTime.Today.AddDays(10);
            d["IsValid"] = true;

            var s1 = LeoSetter.Type(type).Instance(act);
            var s2 = LeoSetter.Type(type).InitialValues(d);
            var s3 = LeoSetter.Type(type).NewInstance();
            var s4 = LeoSetter.Type<NiceAct>().Instance(act);
            var s5 = LeoSetter.Type<NiceAct>().InitialValues(d);
            var s6 = LeoSetter.Type<NiceAct>().NewInstance();

            var s7 = LeoSetter.Type(type).Value("Name");
            var s8 = LeoSetter.Type<NiceAct>().Value("Name");
            var s9 = LeoSetter.Type<NiceAct>().Value(t => t.Name);
            var s10 = LeoSetter.Type<NiceAct>().Value<string>(t => t.Name);

            s1.SetValue("Name", "LL0");
            Assert.Equal("LL0", ((NiceAct) s1.Instance).Name);
            Assert.Equal("LL0", act.Name);

            s2.SetValue("Name", "LL1");
            Assert.Equal("LL1", ((NiceAct) s2.Instance).Name);
            var act2 = (NiceAct) s2.Instance;
            Assert.Equal("LL1", act2.Name);

            s3.SetValue("Name", "LL2");
            Assert.Equal("LL2", ((NiceAct) s3.Instance).Name);
            var act3 = (NiceAct) s3.Instance;
            Assert.Equal("LL2", act3.Name);

            s4.SetValue("Name", "LL3");
            Assert.Equal("LL3", ((NiceAct) s4.Instance).Name);
            Assert.Equal("LL3", act.Name);

            s5.SetValue("Name", "LL4");
            Assert.Equal("LL4", ((NiceAct) s5.Instance).Name);
            var act5 = (NiceAct) s5.Instance;
            Assert.Equal("LL4", act5.Name);

            s6.SetValue("Name", "LL5");
            Assert.Equal("LL5", ((NiceAct) s6.Instance).Name);
            var act6 = (NiceAct) s6.Instance;
            Assert.Equal("LL5", act6.Name);

            s7.Instance(act).Value("LL6");
            Assert.Equal("LL6", act.Name);

            s8.Instance(act).Value("LL7");
            Assert.Equal("LL7", act.Name);

            s9.Instance(act).Value("LL8");
            Assert.Equal("LL8", act.Name);

            s10.Instance(act).Value("LL9");
            Assert.Equal("LL9", act.Name);
        }
    }
}