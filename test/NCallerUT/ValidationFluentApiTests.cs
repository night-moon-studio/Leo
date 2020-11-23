using NCallerUT.Model;
using NMS.Leo.Typed;
using Xunit;

namespace NCallerUT
{
    [Trait("Validation.FluentApi", "Validation")]
    public class ValidationFluentApiTests: Prepare
    {
        [Fact(DisplayName = "直接实例自定义消息验证测试")]
        public void DirectFutureWithFluentApiTest()
        {
            var type = typeof(NiceAct);
            var v = LeoVisitorFactory.Create(type);

            v["Name"] = "Balala";

            v.ValidationEntry.ForMember("Name", c => c.Must(NameMustBeBalala).WithMessage("MustBala!"));

            var r = v.Verify();
            
            Assert.True(r.IsValid);

            v["Name"] = "Bong";
            
            r = v.Verify();
            
            Assert.False(r.IsValid);
            Assert.Single(r.Errors);
            Assert.Single(r.Errors[0].Details);
            Assert.Equal("MustBala!",r.Errors[0].Details[0].ErrorMessage);
        }

        public bool NameMustBeBalala(object value)
        {
            if (value is string stringVal)
            {
                return stringVal == "Balala";
            }

            return false;
        }
    }
}