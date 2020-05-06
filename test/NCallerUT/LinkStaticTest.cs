using Natasha;
using Natasha.CSharp;
using NCaller;
using NCallerUT.Model;
using System;
using Xunit;

namespace NCallerUT
{
    [Trait("LinkOperator", "静态类")]
    public class LinkStaticTest
    {
        [Fact(DisplayName = "动态生成类")]
        public void TestCall1()
        {
            //ScriptComplier.Init();
            string text = @"using System;
using System.Collections;
using System.Linq;
using System.Text;
 
namespace HelloWorld
{
    public static class StaticTest1
    {
        static StaticTest1(){
            Name=""111"";
        }

        public static string Name;
        public static int Age{get;set;}
    }
}";
            //根据脚本创建动态类
            var oop = new AssemblyCSharpBuilder();
            oop.Syntax.Add(text);
            var type = oop.GetTypeFromShortName("StaticTest1");
            CallerManagement.AddType(type);
            //创建动态类实例代理
            var instance = LinkOperator.CreateFromType(type);
            //Get动态调用
            Assert.Equal("111", instance["Name"].Get<string>());
            //调用动态委托赋值
            instance["Name"].Set("222");

            Assert.Equal("222", instance.Get<string>("Name"));

        }



        [Fact(DisplayName = "运行时静态类")]
        public void TestCall2()
        {
            //创建动态类实例代理
            CallerManagement.AddType(typeof(StaticTestModel1));
            var instance = LinkOperator.CreateFromType(typeof(StaticTestModel1));
            StaticTestModel1.Name = "111";
            Assert.Equal("111", instance["Name"].Get<string>());
            instance["Name"].Set("222");
            Assert.Equal("222", instance["Name"].Get<string>());
            StaticTestModel1.Age = 1001;
            Assert.Equal(1001, instance.Get<int>("Age"));
            StaticTestModel1.Temp = DateTime.Now;
            instance["Temp"].Set(StaticTestModel1.Temp);
            Assert.Equal(StaticTestModel1.Temp, instance["Temp"].Get<DateTime>());

        }

        [Fact(DisplayName = "运行时伪静态类")]
        public void TestCall3()
        {
            //创建动态类实例代理
            CallerManagement.AddType(typeof(FakeStaticTestModel1));
            var instance = LinkOperator.CreateFromType(typeof(FakeStaticTestModel1));
            FakeStaticTestModel1.Name = "111";
            Assert.Equal("111", instance["Name"].Get<string>());
            instance["Name"].Set("222");
            Assert.Equal("222", instance["Name"].Get<string>());
            FakeStaticTestModel1.Age = 1001;
            Assert.Equal(1001, instance.Get<int>("Age"));
            FakeStaticTestModel1.Temp = DateTime.Now;
            instance["Temp"].Set(FakeStaticTestModel1.Temp);
            Assert.Equal(FakeStaticTestModel1.Temp, instance["Temp"].Get<DateTime>());
            FakeStaticTestModel1.Money123 = 123321;
            Assert.Equal(123321, instance.Get<float>("Money123"));
            FakeStaticTestModel1.AgeAge13 = 123321;
            Assert.Equal(123321, instance.Get<int>("AgeAge13"));

        }
    }
}
