using Natasha.CSharp;
using NCaller;
using NCallerUT.Model;
using System;
using Xunit;

namespace NCallerUT
{
    [Trait("DictOperator", "普通类")]
    public class DictNormalTest
    {

        [Fact(DisplayName = "动态类")]
        public void TestCall4()
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
            Pp = 10;
            Rp=""aa"";
        }
        private long Pp;
        private readonly string Rp;
        public string Name;
        public int Age{get;set;}
    }
}";
            //根据脚本创建动态类
            
            var oop = new AssemblyCSharpBuilder();
            oop.Syntax.Add(text);
            Type type = oop.GetTypeFromShortName("Test");
            CallerManagement.AddType(type);
            //创建动态类实例代理
            var instance = DictOperator.CreateFromType(type);
            instance.New();
            //Get动态调用
            Assert.Equal("111", (string)instance["Name"]);
            Assert.Equal("aa", (string)instance["Rp"]);
            Assert.Equal(10, (long)instance["Pp"]);
            //调用动态委托赋值
            instance.Set("Name", "222");

            Assert.Equal("222", (string)instance["Name"]);

        }




        [Fact(DisplayName = "普通类")]
        public void TestCall5()
        {
            //创建动态类实例代理
            var instance = DictOperator<TestB>.Create();
            instance.New();
            Assert.Equal("111", (string)instance["Name"]);

            //调用动态委托赋值
            instance.Set("Name", "222");

            Assert.Equal("222", (string)instance["Name"]);
        }




        [Fact(DisplayName = "复杂类")]
        public void TestCall6()
        {
            //创建动态类实例代理
            var instance = DictOperator<TestB>.Create();
            instance.New();
            Assert.Equal("111", (string)instance["Name"]);

            //调用动态委托赋值
            instance.Set("Name", "222");

            Assert.Equal("222", (string)instance["Name"]);


            var c = (TestC)instance["InstanceC"];
            Assert.Equal("abc", c.Name);


            instance["InstanceC"] = (new TestC() { Name = "bbca" });
            Assert.Equal("bbca", ((TestC)instance["InstanceC"]).Name);


        }
    }
}
