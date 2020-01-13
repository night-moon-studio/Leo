using Natasha;
using Natasha.Log;
using NCaller;
using NCallerUT.Model;
using System;
using Xunit;

namespace NCallerUT
{
    [Trait("LinkOperator", "普通类")]
    public class LinkNormalTest
    {
        [Fact(DisplayName = "动态类")]
        public void TestCall1()
        {
            //ScriptComplier.Init();
            string text = @"using System;
using System.Collections;
using System.Linq;
using System.Text;
 
namespace HelloWorld
{
    public class Test2
    {
        public Test2(){
            Name=""111"";
        }

        public string Name;
        public int Age{get;set;}
    }
}";

            //根据脚本创建动态类
            var oop = new AssemblyComplier();
            oop.Add(text);
            var type = oop.GetType("Test2");
            NErrorLog.Enabled = true;
            //创建动态类实例代理
            CallerManagement.AddType(type);
            var instance = LinkOperator.CreateFromType(type);
            instance.New();
            //Get动态调用
            Assert.Equal("111", instance["Name"].Get<string>());
            //调用动态委托赋值
            instance.Set("Name", "222");

            Assert.Equal("222", instance["Name"].Get<string>());

        }




        [Fact(DisplayName = "普通类")]
        public void TestCall2()
        {
            //创建动态类实例代理
            var instance = LinkOperator<TestB>.Create();
            instance.New();
            Assert.Equal("111", instance["Name"].Get<string>());

            //调用动态委托赋值
            instance.Set("Name", "222");

            Assert.Equal("222", instance["Name"].Get<string>());
        }




        [Fact(DisplayName = "复杂类")]
        public void TestCall3()
        {
            //创建动态类实例代理
            var instance = LinkOperator<TestB>.Create();
            instance.New();
            Assert.Equal("111", instance["Name"].Get<string>());

            //调用动态委托赋值
            instance.Set("Name", "222");

            Assert.Equal("222", instance["Name"].Get<string>());


            var c = instance["InstanceC"].Get<TestC>();
            Assert.Equal("abc", c.Name);


            instance["InstanceC"].Set(new TestC() { Name = "bbca" });
            Assert.Equal("bbca", instance["InstanceC"].Get<TestC>().Name);


        }
    }
}
