using System;
using NCallerUT.Model;
using NMS.Leo.Typed;
using Xunit;

namespace NCallerUT
{
    [Trait("Validation.Strategy/Rule Overwrite", "Validation")]
    public class ValidationOverwriteTests : Prepare
    {
        [Fact(DisplayName = "直接实例属性规则验证复写测试")]
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
        }

        [Fact(DisplayName = "直接类型属性规则验证复写测试")]
        public void DirectFutureWithValueApiValidTest()
        {
            var type = typeof(NiceAct);
            var v = LeoVisitorFactory.Create(type);

            v["Name"] = "Hulu";

            var context = v.ValidationEntry;

            Assert.NotNull(context);

            context.ForMember("Name",
                c => c.Required().MinLength(4).MaxLength(15));

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
        }

        [Fact(DisplayName = "泛型实例属性规则验证复写测试")]
        public void GenericInstanceWithValueApiValidTest()
        {
            var act = new NiceAct()
            {
                Name = "Hulu",
                Age = 22,
                Country = Country.China,
                Birthday = DateTime.Today
            };

            var v = LeoVisitorFactory.Create<NiceAct>(act);

            var context = v.ValidationEntry;

            Assert.NotNull(context);

            context.ForMember(x => x.Name,
                c => c.Required().MinLength(4).MaxLength(15));

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
        }

        [Fact(DisplayName = "泛型类型属性规则验证复写测试")]
        public void GenericFutureWithValueApiValidTest()
        {
            var v = LeoVisitorFactory.Create<NiceAct>();

            v["Name"] = "Hulu";

            var context = v.ValidationEntry;

            Assert.NotNull(context);

            context.ForMember(x => x.Name,
                c => c.Required().MinLength(4).MaxLength(15));

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
        }
    }
}