using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Mathematics;
using BenchmarkDotNet.Order;
using BenchmarkTest.Model;
using NCaller;
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
        public DictBase DictHandler;
        public DictBase FuzzyDictHandler;
        public DictBase HashDictHandler;

        public LinkBase LinkHandler;
        public LinkBase FuzzyLinkHandler;
        public LinkBase HashLinkHandler;

        public dynamic Dynamic;
        public CallModel Model;

        public TestCaller()
        {
            string temp = "Hello";
            Type type = typeof(CallModel);
            CallerManagement.AddType(type);
            DictHandler = DictOperator.CreateFromType(type);
            FuzzyDictHandler = FuzzyDictOperator.CreateFromType(type);
            HashDictHandler = HashDictOperator.CreateFromType(type);
            LinkHandler = LinkOperator.CreateFromType(type);
            FuzzyLinkHandler = FuzzyLinkOperator.CreateFromType(type);
            HashLinkHandler = HashLinkOperator.CreateFromType(type);
            Model = new CallModel();
            Dynamic = new CallModel();
            DictHandler.New();
            FuzzyDictHandler.New();
            HashDictHandler.New();
            LinkHandler.New();
            FuzzyLinkHandler.New();
            HashLinkHandler.New();
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
        [BenchmarkCategory("Write", "String"), Benchmark(Description = "Dict With SetMethod")]
        public void DictSetMethodStringTest()
        {
            DictHandler.Set("Name", "Hello");
        }
        [BenchmarkCategory("Write", "String"), Benchmark(Description = "HashDict With SetMethod")]
        public void HashDictSetMethodStringTest()
        {
            HashDictHandler.Set("Name", "Hello");
        }
        [BenchmarkCategory("Write", "String"), Benchmark(Description = "FuzzyDict With SetMethod")]
        public void FuzzyDictSetMethodStringTest()
        {
            FuzzyDictHandler.Set("Name", "Hello");
        }
        [BenchmarkCategory("Write", "String"), Benchmark(Description = "Dict")]
        public void DictSetStringTest()
        {
            DictHandler["Name"] = "Hello";
        }
        [BenchmarkCategory("Write", "String"), Benchmark(Description = "HashDict")]
        public void HashDictSetStringTest()
        {
            HashDictHandler["Name"] = "Hello";
        }
        [BenchmarkCategory("Write", "String"), Benchmark(Description = "FuzzyDict")]
        public void FuzzyDictSetStringTest()
        {
            FuzzyDictHandler["Name"] = "Hello";
        }
        [BenchmarkCategory("Write", "String"), Benchmark(Description = "Link")]
        public void LinkSetStringTest()
        {
            LinkHandler["Name"].Set("Hello");
        }
        [BenchmarkCategory("Write", "String"), Benchmark(Description = "HashLink")]
        public void HashLinkSetStringTest()
        {
            HashLinkHandler["Name"].Set("Hello");
        }
        [BenchmarkCategory("Write", "String"), Benchmark(Description = "FuzzyLink")]
        public void FuzzyLinkSetStringTest()
        {
            FuzzyLinkHandler["Name"].Set("Hello");
        }
    }
}
