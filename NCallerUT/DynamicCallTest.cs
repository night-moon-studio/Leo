using Natasha;
using NCaller;
using System;
using Xunit;

namespace NCallerUT
{
    [Trait("动态调用2", "普通类")]
    public class DynamicCallTest
    {
        [Fact(DisplayName = "动态类的动态操作测试")]
        public void TestCall1()
        {
            //ScriptComplier.Init();
            string text = @"using System;
using System.Collections;
using System.Linq;
using System.Text;
 
namespace HelloWorld
{
    public class Test
    {
        public Test(){
            Name=""111"";
        }

        public string Name;
        public int Age{get;set;}
    }
}";
            //根据脚本创建动态类
            Type type = RuntimeComplier.GetType(text);
            //创建动态类实例代理
            var instance = DictCaller.Create(type);
            instance.New();
            //Get动态调用
            Assert.Equal("111", instance["Name"].Get<string>());
            //调用动态委托赋值
            instance["Name"].Set("222");

            Assert.Equal("222", instance["Name"].Get<string>());
           
        }



        [Fact(DisplayName = "普通类的动态操作测试")]
        public void TestCall2()
        {
            //创建动态类实例代理
            var instance = DictCaller.Create(typeof(TestB));
            instance.New();
            Assert.Equal("111", instance["Name"].Get<string>());

            //调用动态委托赋值
            instance["Name"].Set("222");

            Assert.Equal("222", instance["Name"].Get<string>());
        }
    }
    public class TestB
    {
        public TestB()
        {
            Name = "111";
            InstanceC = new TestC
            {
                Name = "abc"
            };
        }
        public string Name { get; set; }
        public int Age;
        public TestC InstanceC;
    }

    public class TestC
    {
        public string Name;
    }
}
