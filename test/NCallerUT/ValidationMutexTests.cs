using System;
using NCallerUT.Model;
using NMS.Leo.Typed;
using Xunit;

namespace NCallerUT
{
    [Trait("Validation.Strategy/Rule Mutex", "Validation")]
    public class ValidationMutexTests : Prepare
    {
        [Fact(DisplayName = "直接实例互斥属性规则验证测试")]
        public void DirectInstanceWithValueApiValidTest()
        {
            var act = new NiceAct()
            {
                Name = "Hulu",
                Age = 22,
                Country = Country.China,
                Birthday = DateTime.Today
            };

            var type = typeof(NiceAct);
            var v = LeoVisitorFactory.Create(type, act);

            v.ValidationEntry
             .ForMember("Name", c => c.Required().MinLength(4).MaxLength(15));

            var r1 = v.Verify();
            Assert.True(r1.IsValid);

            v.ValidationEntry
             .ForMember("Name", c => c.RequiredNull().OverwriteRule());

            var r2 = v.Verify();
            Assert.False(r2.IsValid);
            Assert.Single(r2.Errors);
            Assert.Single(r2.Errors[0].Details);

            v["Name"] = "";

            var r3 = v.Verify();
            Assert.True(r3.IsValid);

            v["Name"] = null;

            var r4 = v.Verify();
            Assert.True(r4.IsValid);

            // NotEqual is not mutually exclusive, so these three tokens can be added. The default is AppendRule mode.
            v.ValidationEntry.ForMember("Name", c => c.Length(4, 15).NotEqual("Huhuhu").NotEqual("Lululu").NotEqual("Hu"));

            r1 = v.Verify();
            Assert.True(r1.IsValid);

            // NotEqual is not mutually exclusive, so these three tokens can be added.
            v.ValidationEntry.ForMember("Name", c => c.Length(4, 15).NotEqual("Huhuhu").NotEqual("Lululu").NotEqual("Hu").OverwriteRule());

            v["Name"] = "Hululu";
            r1 = v.Verify();
            Assert.True(r1.IsValid);

            v["Name"] = "Huhuhu";
            r1 = v.Verify();
            Assert.False(r1.IsValid);
            Assert.Single(r1.Errors);
            Assert.Single(r1.Errors[0].Details);

            v["Name"] = "Lululu";
            r1 = v.Verify();
            Assert.False(r1.IsValid);
            Assert.Single(r1.Errors);
            Assert.Single(r1.Errors[0].Details);

            v["Name"] = "Hu";
            r1 = v.Verify();
            Assert.False(r1.IsValid);
            Assert.Single(r1.Errors);
            Assert.Equal(2, r1.Errors[0].Details.Count);

            // Revert to RequiredNull token to test the mutual exclusion under AppendRule
            v.ValidationEntry
             .ForMember("Name", c => c.RequiredNull().OverwriteRule());

            // Due to mutual exclusion, these conditions will all fail to take effect (because of the AppendRule mode).
            v.ValidationEntry
             .ForMember("Name", c => c.Required().MinLength(4).MaxLength(15).AppendRule());

            v["Name"] = "";

            r1 = v.Verify();
            Assert.True(r1.IsValid);

            v["Name"] = null;

            r1 = v.Verify();
            Assert.True(r1.IsValid);
        }
    }
}