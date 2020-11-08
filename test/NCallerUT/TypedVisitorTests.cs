using System;
using NMS.Leo.Typed;
using Xunit;

namespace NCallerUT
{
    [Trait("TypedVisitor", "LeoVisitor")]
    public class TypedVisitorTests : Prepare
    {
        [Fact(DisplayName = "实例扩展方法测试")]
        public void DirectInstanceExtensionsTest()
        {
            var act = new NiceAct()
            {
                Name = "Hu",
                Age = 22,
                Country = Country.China,
                Birthday = DateTime.Today
            };

            var v = act.ToLeoVisitor();

            Assert.False(v.IsStatic);
            Assert.Equal(typeof(NiceAct), v.TargetType);
            Assert.Equal(LeoType.Precision, v.AlgorithmType);

            Assert.Equal("Hu", v.GetValue<string>("Name"));
            Assert.Equal(22, v.GetValue<int>("Age"));
            Assert.Equal(Country.China, v.GetValue<Country>("Country"));
            Assert.Equal(DateTime.Today, v.GetValue<DateTime>("Birthday"));
            Assert.False(v.GetValue<bool>("IsValid"));

            v.SetValue("Name", "Du");
            v.SetValue("Age", 55);
            v.SetValue("Country", Country.USA);
            v.SetValue("Birthday", DateTime.Today.AddDays(-1));
            v.SetValue("IsValid", true);

            Assert.Equal("Du", v.GetValue<string>("Name"));
            Assert.Equal(55, v.GetValue<int>("Age"));
            Assert.Equal(Country.USA, v.GetValue<Country>("Country"));
            Assert.Equal(DateTime.Today.AddDays(-1), v.GetValue<DateTime>("Birthday"));
            Assert.True(v.GetValue<bool>("IsValid"));

            v["Name"] = "Au";
            v["Age"] = 77;
            v["Country"] = Country.China;
            v["Birthday"] = DateTime.Today.AddDays(1);
            v["IsValid"] = false;

            Assert.Equal("Au", v.GetValue<string>("Name"));
            Assert.Equal(77, v.GetValue<int>("Age"));
            Assert.Equal(Country.China, v.GetValue<Country>("Country"));
            Assert.Equal(DateTime.Today.AddDays(1), v.GetValue<DateTime>("Birthday"));
            Assert.False(v.GetValue<bool>("IsValid"));
        }

        [Fact(DisplayName = "类型扩展方法测试")]
        public void DirectTypeExtensionsTest()
        {
            var type = typeof(NiceAct);
            var v = type.ToLeoVisitor();

            Assert.False(v.IsStatic);
            Assert.Equal(typeof(NiceAct), v.TargetType);
            Assert.Equal(LeoType.Precision, v.AlgorithmType);

            v.SetValue("Name", "Du");
            v.SetValue("Age", 55);
            v.SetValue("Country", Country.USA);
            v.SetValue("Birthday", DateTime.Today.AddDays(-1));
            v.SetValue("IsValid", true);

            Assert.Equal("Du", v.GetValue<string>("Name"));
            Assert.Equal(55, v.GetValue<int>("Age"));
            Assert.Equal(Country.USA, v.GetValue<Country>("Country"));
            Assert.Equal(DateTime.Today.AddDays(-1), v.GetValue<DateTime>("Birthday"));
            Assert.True(v.GetValue<bool>("IsValid"));

            v["Name"] = "Au";
            v["Age"] = 77;
            v["Country"] = Country.China;
            v["Birthday"] = DateTime.Today.AddDays(1);
            v["IsValid"] = false;

            Assert.Equal("Au", v.GetValue<string>("Name"));
            Assert.Equal(77, v.GetValue<int>("Age"));
            Assert.Equal(Country.China, v.GetValue<Country>("Country"));
            Assert.Equal(DateTime.Today.AddDays(1), v.GetValue<DateTime>("Birthday"));
            Assert.False(v.GetValue<bool>("IsValid"));
        }

        [Fact(DisplayName = "直接实例测试")]
        public void DirectInstanceTest()
        {
            var act = new NiceAct()
            {
                Name = "Hu",
                Age = 22,
                Country = Country.China,
                Birthday = DateTime.Today
            };

            var type = typeof(NiceAct);
            var v = LeoVisitorFactory.Create(type, act);

            Assert.False(v.IsStatic);
            Assert.Equal(typeof(NiceAct), v.TargetType);
            Assert.Equal(LeoType.Precision, v.AlgorithmType);

            Assert.Equal("Hu", v.GetValue<string>("Name"));
            Assert.Equal(22, v.GetValue<int>("Age"));
            Assert.Equal(Country.China, v.GetValue<Country>("Country"));
            Assert.Equal(DateTime.Today, v.GetValue<DateTime>("Birthday"));
            Assert.False(v.GetValue<bool>("IsValid"));

            v.SetValue("Name", "Du");
            v.SetValue("Age", 55);
            v.SetValue("Country", Country.USA);
            v.SetValue("Birthday", DateTime.Today.AddDays(-1));
            v.SetValue("IsValid", true);

            Assert.Equal("Du", v.GetValue<string>("Name"));
            Assert.Equal(55, v.GetValue<int>("Age"));
            Assert.Equal(Country.USA, v.GetValue<Country>("Country"));
            Assert.Equal(DateTime.Today.AddDays(-1), v.GetValue<DateTime>("Birthday"));
            Assert.True(v.GetValue<bool>("IsValid"));

            v["Name"] = "Au";
            v["Age"] = 77;
            v["Country"] = Country.China;
            v["Birthday"] = DateTime.Today.AddDays(1);
            v["IsValid"] = false;

            Assert.Equal("Au", v.GetValue<string>("Name"));
            Assert.Equal(77, v.GetValue<int>("Age"));
            Assert.Equal(Country.China, v.GetValue<Country>("Country"));
            Assert.Equal(DateTime.Today.AddDays(1), v.GetValue<DateTime>("Birthday"));
            Assert.False(v.GetValue<bool>("IsValid"));
        }

        [Fact(DisplayName = "直接类型测试")]
        public void DirectFutureTest()
        {
            var type = typeof(NiceAct);
            var v = LeoVisitorFactory.Create(type);

            Assert.False(v.IsStatic);
            Assert.Equal(typeof(NiceAct), v.TargetType);
            Assert.Equal(LeoType.Precision, v.AlgorithmType);

            v.SetValue("Name", "Du");
            v.SetValue("Age", 55);
            v.SetValue("Country", Country.USA);
            v.SetValue("Birthday", DateTime.Today.AddDays(-1));
            v.SetValue("IsValid", true);

            Assert.Equal("Du", v.GetValue<string>("Name"));
            Assert.Equal(55, v.GetValue<int>("Age"));
            Assert.Equal(Country.USA, v.GetValue<Country>("Country"));
            Assert.Equal(DateTime.Today.AddDays(-1), v.GetValue<DateTime>("Birthday"));
            Assert.True(v.GetValue<bool>("IsValid"));

            v["Name"] = "Au";
            v["Age"] = 77;
            v["Country"] = Country.China;
            v["Birthday"] = DateTime.Today.AddDays(1);
            v["IsValid"] = false;

            Assert.Equal("Au", v.GetValue<string>("Name"));
            Assert.Equal(77, v.GetValue<int>("Age"));
            Assert.Equal(Country.China, v.GetValue<Country>("Country"));
            Assert.Equal(DateTime.Today.AddDays(1), v.GetValue<DateTime>("Birthday"));
            Assert.False(v.GetValue<bool>("IsValid"));
        }

        [Fact(DisplayName = "泛型实例测试")]
        public void GenericInstanceTest()
        {
            var act = new NiceAct()
            {
                Name = "Hu",
                Age = 22,
                Country = Country.China,
                Birthday = DateTime.Today
            };

            var v = LeoVisitorFactory.Create<NiceAct>(act);

            Assert.False(v.IsStatic);
            Assert.Equal(typeof(NiceAct), v.TargetType);
            Assert.Equal(LeoType.Precision, v.AlgorithmType);

            Assert.Equal("Hu", v.GetValue<string>("Name"));
            Assert.Equal(22, v.GetValue<int>("Age"));
            Assert.Equal(Country.China, v.GetValue<Country>("Country"));
            Assert.Equal(DateTime.Today, v.GetValue<DateTime>("Birthday"));
            Assert.False(v.GetValue<bool>("IsValid"));

            v.SetValue("Name", "Du");
            v.SetValue("Age", 55);
            v.SetValue("Country", Country.USA);
            v.SetValue("Birthday", DateTime.Today.AddDays(-1));
            v.SetValue("IsValid", true);

            Assert.Equal("Du", v.GetValue<string>("Name"));
            Assert.Equal(55, v.GetValue<int>("Age"));
            Assert.Equal(Country.USA, v.GetValue<Country>("Country"));
            Assert.Equal(DateTime.Today.AddDays(-1), v.GetValue<DateTime>("Birthday"));
            Assert.True(v.GetValue<bool>("IsValid"));

            v["Name"] = "Au";
            v["Age"] = 77;
            v["Country"] = Country.China;
            v["Birthday"] = DateTime.Today.AddDays(1);
            v["IsValid"] = false;

            Assert.Equal("Au", v.GetValue<string>("Name"));
            Assert.Equal(77, v.GetValue<int>("Age"));
            Assert.Equal(Country.China, v.GetValue<Country>("Country"));
            Assert.Equal(DateTime.Today.AddDays(1), v.GetValue<DateTime>("Birthday"));
            Assert.False(v.GetValue<bool>("IsValid"));
        }

        [Fact(DisplayName = "泛型类型测试")]
        public void GenericFutureTest()
        {
            var v = LeoVisitorFactory.Create<NiceAct>();

            Assert.False(v.IsStatic);
            Assert.Equal(typeof(NiceAct), v.TargetType);
            Assert.Equal(LeoType.Precision, v.AlgorithmType);

            v.SetValue("Name", "Du");
            v.SetValue("Age", 55);
            v.SetValue("Country", Country.USA);
            v.SetValue("Birthday", DateTime.Today.AddDays(-1));
            v.SetValue("IsValid", true);

            Assert.Equal("Du", v.GetValue<string>("Name"));
            Assert.Equal(55, v.GetValue<int>("Age"));
            Assert.Equal(Country.USA, v.GetValue<Country>("Country"));
            Assert.Equal(DateTime.Today.AddDays(-1), v.GetValue<DateTime>("Birthday"));
            Assert.True(v.GetValue<bool>("IsValid"));

            v["Name"] = "Au";
            v["Age"] = 77;
            v["Country"] = Country.China;
            v["Birthday"] = DateTime.Today.AddDays(1);
            v["IsValid"] = false;

            Assert.Equal("Au", v.GetValue<string>("Name"));
            Assert.Equal(77, v.GetValue<int>("Age"));
            Assert.Equal(Country.China, v.GetValue<Country>("Country"));
            Assert.Equal(DateTime.Today.AddDays(1), v.GetValue<DateTime>("Birthday"));
            Assert.False(v.GetValue<bool>("IsValid"));
        }

        [Fact(DisplayName = "静态类型测试")]
        public void DirectStaticTypeTest()
        {
            StaticNiceAct1.Name = "Hu";
            StaticNiceAct1.Age = 22;
            StaticNiceAct1.Country = Country.China;
            StaticNiceAct1.Birthday = DateTime.Today;

            var type = typeof(StaticNiceAct1);

            var v = LeoVisitorFactory.Create(type);

            Assert.True(v.IsStatic);
            Assert.Equal(typeof(NiceAct), v.TargetType);
            Assert.Equal(LeoType.Precision, v.AlgorithmType);

            Assert.Equal("Hu", v.GetValue<string>("Name"));
            Assert.Equal(22, v.GetValue<int>("Age"));
            Assert.Equal(Country.China, v.GetValue<Country>("Country"));
            Assert.Equal(DateTime.Today, v.GetValue<DateTime>("Birthday"));
            Assert.False(v.GetValue<bool>("IsValid"));

            v.SetValue("Name", "Du");
            v.SetValue("Age", 55);
            v.SetValue("Country", Country.USA);
            v.SetValue("Birthday", DateTime.Today.AddDays(-1));
            v.SetValue("IsValid", true);

            Assert.Equal("Du", v.GetValue<string>("Name"));
            Assert.Equal(55, v.GetValue<int>("Age"));
            Assert.Equal(Country.USA, v.GetValue<Country>("Country"));
            Assert.Equal(DateTime.Today.AddDays(-1), v.GetValue<DateTime>("Birthday"));
            Assert.True(v.GetValue<bool>("IsValid"));

            v["Name"] = "Au";
            v["Age"] = 77;
            v["Country"] = Country.China;
            v["Birthday"] = DateTime.Today.AddDays(1);
            v["IsValid"] = false;

            Assert.Equal("Au", v.GetValue<string>("Name"));
            Assert.Equal(77, v.GetValue<int>("Age"));
            Assert.Equal(Country.China, v.GetValue<Country>("Country"));
            Assert.Equal(DateTime.Today.AddDays(1), v.GetValue<DateTime>("Birthday"));
            Assert.False(v.GetValue<bool>("IsValid"));
        }

        [Fact(DisplayName = "伪静态类型测试")]
        public void FackStaticTypeTest()
        {
            var act = new StaticNiceAct2();

            StaticNiceAct2.Name = "Hu";
            StaticNiceAct2.Age = 22;
            StaticNiceAct2.Country = Country.China;
            StaticNiceAct2.Birthday = DateTime.Today;

            var type = typeof(StaticNiceAct2);

            var v = LeoVisitorFactory.Create(type, act);

            Assert.False(v.IsStatic);
            Assert.Equal(typeof(StaticNiceAct2), v.TargetType);
            Assert.Equal(LeoType.Precision, v.AlgorithmType);

            Assert.Equal("Hu", v.GetValue<string>("Name"));
            Assert.Equal(22, v.GetValue<int>("Age"));
            Assert.Equal(Country.China, v.GetValue<Country>("Country"));
            Assert.Equal(DateTime.Today, v.GetValue<DateTime>("Birthday"));
            Assert.False(v.GetValue<bool>("IsValid"));

            v.SetValue("Name", "Du");
            v.SetValue("Age", 55);
            v.SetValue("Country", Country.USA);
            v.SetValue("Birthday", DateTime.Today.AddDays(-1));
            v.SetValue("IsValid", true);

            Assert.Equal("Du", v.GetValue<string>("Name"));
            Assert.Equal(55, v.GetValue<int>("Age"));
            Assert.Equal(Country.USA, v.GetValue<Country>("Country"));
            Assert.Equal(DateTime.Today.AddDays(-1), v.GetValue<DateTime>("Birthday"));
            Assert.True(v.GetValue<bool>("IsValid"));

            v["Name"] = "Au";
            v["Age"] = 77;
            v["Country"] = Country.China;
            v["Birthday"] = DateTime.Today.AddDays(1);
            v["IsValid"] = false;

            Assert.Equal("Au", v.GetValue<string>("Name"));
            Assert.Equal(77, v.GetValue<int>("Age"));
            Assert.Equal(Country.China, v.GetValue<Country>("Country"));
            Assert.Equal(DateTime.Today.AddDays(1), v.GetValue<DateTime>("Birthday"));
            Assert.False(v.GetValue<bool>("IsValid"));
        }
    }

    public class NiceAct
    {
        public string Name { get; set; }

        public int Age { get; set; }

        public DateTime Birthday { get; set; }

        public Country Country { get; set; }

        public bool IsValid { get; set; }
    }

    public enum Country
    {
        China,
        USA
    }

    public static class StaticNiceAct1
    {
        public static string Name { get; set; }

        public static int Age { get; set; }

        public static DateTime Birthday { get; set; }

        public static Country Country { get; set; }

        public static bool IsValid { get; set; }
    }

    public class StaticNiceAct2
    {
        public static string Name { get; set; }

        public static int Age { get; set; }

        public static DateTime Birthday { get; set; }

        public static Country Country { get; set; }

        public static bool IsValid { get; set; }
    }
}