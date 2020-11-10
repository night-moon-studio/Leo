using System.Linq;
using NMS.Leo;
using Xunit;

namespace NCallerUT
{
    [Trait("LeoMember", "Metadata")]
    public class MemberMetadataTests : Prepare
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
        }
    }
}