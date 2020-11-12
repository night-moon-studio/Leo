using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Mathematics;
using BenchmarkDotNet.Order;
using BenchmarkTest.Model;

namespace BenchmarkTest
{

    public unsafe static class SetterProxy<S,T>
    {

        public static delegate* managed<CallModel, T, void> Set;

    }


    public unsafe static class SetterProxyName
    {
        static SetterProxyName()
        {
            SetterProxy<CallModel, string>.Set = &Set;
        }
        public static void Set(CallModel model,string value)
        {
            model.Name = value;
        }

        public static  void Test()
        {

        }
    }

    


    public class SCaller
    {
        private CallModel model;


        public SCaller()
        {
            SetterProxyName.Test();
            model = new CallModel();
            
        }

        public void Direct(string value)
        {
            model.Name = value;
        }
        public void Set(string name,object value)
        {
            model.Name = (string)value;
        }
        public unsafe void SetValue<T>(string name, T value)
        {
            SetterProxy<CallModel, T>.Set(model, value);
        }
    }

    [MemoryDiagnoser, MarkdownExporter, RPlotExporter]
    [MinColumn, MaxColumn, MeanColumn, MedianColumn]
    [GroupBenchmarksBy(BenchmarkLogicalGroupRule.ByCategory)]
    [Orderer(SummaryOrderPolicy.FastestToSlowest)]
    [RankColumn(NumeralSystem.Arabic)]
    [CategoriesColumn]
    public class TestProxy
    {
        private readonly SCaller sCaller;
        public TestProxy()
        {
            sCaller = new SCaller();
        }



        [Benchmark]
        public void SetObjectTest()
        {
            sCaller.Set("Name", "hello");
        }

        [Benchmark]
        public void SetValueTest()
        {
            sCaller.SetValue("Name", "hello");
        }

        [Benchmark]
        public void SetDirectTest()
        {
            sCaller.Direct("hello");
        }

       


    }
}
