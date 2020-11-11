using System;
using System.Linq;
using NMS.Leo;
using Xunit;

namespace NCallerUT
{
    [Trait("LeoMember", "Metadata")]
    public unsafe class MemberMetadataTests : Prepare
    {
        [Fact(DisplayName = "类型元数据测试")]
        public void DirectMetadataTest()
        {
            var instance = PrecisionDictOperator.CreateFromType(typeof(NiceAct));
            
            var members0 = instance.GetMembers();
            var members1 = instance.GetCanReadMembers();
            var members2 = instance.GetCanReadMembers();
            
            Assert.Equal(5, members0.Count());
            Assert.Equal(5, members1.Count());
            Assert.Equal(5, members2.Count());
            
            Assert.NotNull(instance.GetMember("Name"));
            Assert.NotNull(instance.GetMember("Age"));
            Assert.NotNull(instance.GetMember("Birthday"));
            Assert.NotNull(instance.GetMember("Country"));
            Assert.NotNull(instance.GetMember("IsValid"));
            
            Assert.Equal(typeof(string), instance.GetMember("Name").MemberType);
            Assert.Equal(typeof(int), instance.GetMember("Age").MemberType);
            Assert.Equal(typeof(DateTime), instance.GetMember("Birthday").MemberType);
            Assert.Equal(typeof(Country), instance.GetMember("Country").MemberType);
            Assert.Equal(typeof(bool), instance.GetMember("IsValid").MemberType);
        }
        
        [Fact(DisplayName = "泛型元数据测试")]
        public unsafe void GenericMetadataTest()
        {
            var instance = PrecisionDictOperator<NiceAct>.Create();
            
            var members0 = instance.GetMembers();
            var members1 = instance.GetCanReadMembers();
            var members2 = instance.GetCanReadMembers();
            
            Assert.Equal(5, members0.Count());
            Assert.Equal(5, members1.Count());
            Assert.Equal(5, members2.Count());
            
            Assert.NotNull(instance.GetMember("Name"));
            Assert.NotNull(instance.GetMember("Age"));
            Assert.NotNull(instance.GetMember("Birthday"));
            Assert.NotNull(instance.GetMember("Country"));
            Assert.NotNull(instance.GetMember("IsValid"));
            
            Assert.Equal(typeof(string), instance.GetMember("Name").MemberType);
            Assert.Equal(typeof(int), instance.GetMember("Age").MemberType);
            Assert.Equal(typeof(DateTime), instance.GetMember("Birthday").MemberType);
            Assert.Equal(typeof(Country), instance.GetMember("Country").MemberType);
            Assert.Equal(typeof(bool), instance.GetMember("IsValid").MemberType);
        }
    }
}