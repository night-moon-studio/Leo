using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Mathematics;
using BenchmarkDotNet.Order;
using BenchmarkTest.Model;
using Natasha;
using Natasha.CSharp;
using NMS.Leo;
using System;
using System.Collections.Generic;
using System.Text;

namespace BenchmarkTest
{
    [MemoryDiagnoser, MarkdownExporter, RPlotExporter]
    [MinColumn, MaxColumn, MeanColumn, MedianColumn]
    [GroupBenchmarksBy(BenchmarkLogicalGroupRule.ByCategory)]
    [Orderer(SummaryOrderPolicy.FastestToSlowest)]
    [RankColumn(NumeralSystem.Arabic)]
    [CategoriesColumn]
    public class TestCaller
    {
        public DictBase PrecisionDict;
        public DictBase FuzzyDict;
        public DictBase HashDict;

        //public LinkBase LinkHandler;
        //public LinkBase FuzzyLinkHandler;
        //public LinkBase HashLinkHandler;

        public dynamic Dynamic;
        public CallModel Model;
        public CallModel DictModel;
        public Dictionary<string, Action<CallModel, object>> Dict;
        public TestCaller()
        {
            NatashaInitializer.Preheating();
            string temp = "Hello";
            Dict = new Dictionary<string, Action<CallModel, object>>();
            Type type = typeof(CallModel);
            PrecisionDict = PrecisionDictOperator.CreateFromType(type);
            FuzzyDict = FuzzyDictOperator.CreateFromType(type);
            HashDict = HashDictOperator.CreateFromType(type);
            //LinkHandler = LinkOperator.CreateFromType(type);
            //FuzzyLinkHandler = FuzzyLinkOperator.CreateFromType(type);
            //HashLinkHandler = HashLinkOperator.CreateFromType(type);
            Model = new CallModel();
            Dynamic = new CallModel();
            DictModel = new CallModel();
            PrecisionDict.New();
            FuzzyDict.New();
            HashDict.New();
            Dict["Name"] = NDelegate.DefaultDomain().Action<CallModel, object>("arg1.Name=(string)arg2;");
            for (int i = 0; i < 3000; i++)
            {
                Model.Name = "Hello";
                Dynamic.Name = "Hello";
                Dict["Name"](DictModel, "Hello");
                PrecisionDict.Set("Name", "Hello");
            }
        }


        [BenchmarkCategory("Write", "String"), Benchmark(Baseline =true, Description = "Origin")]
        public void OriginSetStringTest()
        {
            Model.Name = "Hello";
        }
        [BenchmarkCategory("Write", "String"), Benchmark(Description = "Dynamic")]
        public void DynamicSetStringTest()
        {
            Dynamic.Name = "Hello";
        }
        [BenchmarkCategory("Write", "String"), Benchmark(Description = "Dictionary")]
        public void DictionarySetStringTest()
        {
            Dict["Name"](DictModel, "Hello");
        }
        //[BenchmarkCategory("Write", "String"), Benchmark(Description = "Dict With SetMethod")]
        //public void DictSetMethodStringTest()
        //{
        //    DictHandler.Set("Name", "Hello");
        //}
        //[BenchmarkCategory("Write", "String"), Benchmark(Description = "HashDict With SetMethod")]
        //public void HashDictSetMethodStringTest()
        //{
        //    HashDictHandler.Set("Name", "Hello");
        //}
        //[BenchmarkCategory("Write", "String"), Benchmark(Description = "FuzzyDict With SetMethod")]
        //public void FuzzyDictSetMethodStringTest()
        //{
        //    FuzzyDictHandler.Set("Name", "Hello");
        //}
        [BenchmarkCategory("Write", "String"), Benchmark(Description = "PrecisionDict")]
        public void DictSetStringTest()
        {
            PrecisionDict.Set("Name" , "Hello");
        }
        //[BenchmarkCategory("Write", "String"), Benchmark(Description = "HashDict")]
        //public void HashDictSetStringTest()
        //{
        //    HashDict.Set("Name", "Hello");
        //}
        [BenchmarkCategory("Write", "String"), Benchmark(Description = "FuzzyDict")]
        public void FuzzyDictSetStringTest()
        {
            FuzzyDict.Set("Name", "Hello");
        }
        //[BenchmarkCategory("Write", "String"), Benchmark(Description = "Link")]
        //public void LinkSetStringTest()
        //{
        //    LinkHandler["Name"].Set("Hello");
        //}
        //[BenchmarkCategory("Write", "String"), Benchmark(Description = "HashLink")]
        //public void HashLinkSetStringTest()
        //{
        //    HashLinkHandler["Name"].Set("Hello");
        //}
        //[BenchmarkCategory("Write", "String"), Benchmark(Description = "FuzzyLink")]
        //public void FuzzyLinkSetStringTest()
        //{
        //    FuzzyLinkHandler["Name"].Set("Hello");
        //}
    }
}
