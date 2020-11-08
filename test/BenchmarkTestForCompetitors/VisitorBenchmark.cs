using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Mathematics;
using BenchmarkDotNet.Order;
using BenchmarkTestForCompetitors.Models;
using Newbe.ObjectVisitor;
using NMS.Leo;
using NMS.Leo.Typed;

namespace BenchmarkTestForCompetitors
{
    [MemoryDiagnoser, MarkdownExporter, RPlotExporter]
    [MinColumn, MaxColumn, MeanColumn, MedianColumn]
    [Orderer(SummaryOrderPolicy.FastestToSlowest)]
    [GroupBenchmarksBy(BenchmarkLogicalGroupRule.ByCategory)]
    [RankColumn(NumeralSystem.Arabic)]
    [CategoriesColumn]
    //[Config(typeof(Config))]
    public class VisitorBenchmark
    {
        private readonly NiceLemon _lemon;
        private readonly PropertyInfo _propertyForAge;
        private readonly DictBase _leoPrecisionHandler;
        private readonly DictBase _leoHashHandler;
        private readonly DictBase _leoFuzzyHandler;
        private readonly ILeoVisitor<NiceLemon> _leoPrecisionVisitor;
        private readonly ILeoVisitor<NiceLemon> _leoHashVisitor;
        private readonly ILeoVisitor<NiceLemon> _leoFuzzyVisitor;
        private IOvBuilderContext<NiceLemon> _ovVisitor;

        public VisitorBenchmark()
        {
            _lemon = new NiceLemon
            {
                Name = "Hu",
                Age = 22,
                Country = Country.China,
                Birthday = DateTime.Today,
                IsValid = true,
            };

            _propertyForAge = typeof(NiceLemon).GetProperty(nameof(NiceLemon.Age));

            _leoPrecisionHandler = NMS.Leo.FuzzyDictOperator.CreateFromType(typeof(NiceLemon));
            _leoHashHandler = NMS.Leo.HashDictOperator.CreateFromType(typeof(NiceLemon));
            _leoFuzzyHandler = NMS.Leo.FuzzyDictOperator.CreateFromType(typeof(NiceLemon));

            _leoPrecisionVisitor = _lemon.ToLeoVisitor(LeoType.Precision);
            _leoHashVisitor = _lemon.ToLeoVisitor(LeoType.Hash);
            _leoFuzzyVisitor = _lemon.ToLeoVisitor(LeoType.Fuzzy);

            _ovVisitor = _lemon.V();

            _leoPrecisionHandler.SetObjInstance(_lemon);
            _leoHashHandler.SetObjInstance(_lemon);
            _leoFuzzyHandler.SetObjInstance(_lemon);
        }

        public int N = 10000000;

        [Benchmark(Description = "System Original Getter")]
        [BenchmarkCategory("Getter")]
        public void OriginalGetter()
        {
            for (var i = 0; i < N; i++)
            {
                var z = _lemon.Age;
            }
        }

        [Benchmark(Description = "System Reflect Getter")]
        [BenchmarkCategory("Getter")]
        public void ReflectGetter()
        {
            for (var i = 0; i < N; i++)
            {
                var z = _propertyForAge.GetValue(_lemon);
            }
        }

        [Benchmark(Description = "NCC Natasha Leo Precision Direct Getter")]
        [BenchmarkCategory("Getter")]
        public void LeoDirectPrecisionGetter()
        {
            for (var i = 0; i < N; i++)
            {
                var z = _leoPrecisionHandler["Age"];
            }
        }

        [Benchmark(Description = "NCC Natasha Leo Hash Direct Getter")]
        [BenchmarkCategory("Getter")]
        public void LeoDirectHashGetter()
        {
            for (var i = 0; i < N; i++)
            {
                var z = _leoHashHandler["Age"];
            }
        }

        [Benchmark(Description = "NCC Natasha Leo Fuzzy Direct Getter")]
        [BenchmarkCategory("Getter")]
        public void LeoDirectFuzzyGetter()
        {
            for (var i = 0; i < N; i++)
            {
                var z = _leoFuzzyHandler["Age"];
            }
        }

        [Benchmark(Description = "NCC Natasha Leo Typed Precision Visitor Getter")]
        [BenchmarkCategory("Getter")]
        public void LeoTypedPrecisionGetter()
        {
            for (var i = 0; i < N; i++)
            {
                var z = _leoPrecisionVisitor.GetValue<int>("Age");
            }
        }

        [Benchmark(Description = "NCC Natasha Leo Typed Hash Visitor Getter")]
        [BenchmarkCategory("Getter")]
        public void LeoTypedHashGetter()
        {
            for (var i = 0; i < N; i++)
            {
                var z = _leoHashVisitor.GetValue<int>("Age");
            }
        }

        [Benchmark(Description = "NCC Natasha Leo Typed Fuzzy Visitor Getter")]
        [BenchmarkCategory("Getter")]
        public void LeoTypedFuzzyGetter()
        {
            for (var i = 0; i < N; i++)
            {
                var z = _leoFuzzyVisitor.GetValue<int>("Age");
            }
        }

        [Benchmark(Description = "Newbe ObjectVisitor Getter")]
        [BenchmarkCategory("Getter")]
        public void ObjectVisitorGetter()
        {
            for (var i = 0; i < N; i++)
            {
                var z = ValueGetter<NiceLemon, int, int>.GetGetter(_propertyForAge).Invoke(_lemon);
            }
        }

        [Benchmark(Description = "System Original Setter")]
        [BenchmarkCategory("Setter")]
        public void OriginalSetter()
        {
            for (var i = 0; i < N; i++)
            {
                _lemon.Age = 55;
            }
        }

        [Benchmark(Description = "System Reflect Setter")]
        [BenchmarkCategory("Setter")]
        public void ReflectSetter()
        {
            for (var i = 0; i < N; i++)
            {
                _propertyForAge.SetValue(_lemon, 55);
            }
        }

        [Benchmark(Description = "NCC Natasha Leo Precision Direct Setter")]
        [BenchmarkCategory("Setter")]
        public void LeoDirectPrecisionSetter()
        {
            for (var i = 0; i < N; i++)
            {
                _leoPrecisionHandler["Age"] = 55;
            }
        }

        [Benchmark(Description = "NCC Natasha Leo Hash Direct Setter")]
        [BenchmarkCategory("Setter")]
        public void LeoDirectHashSetter()
        {
            for (var i = 0; i < N; i++)
            {
                _leoHashHandler["Age"] = 55;
            }
        }

        [Benchmark(Description = "NCC Natasha Leo Fuzzy Direct Setter")]
        [BenchmarkCategory("Setter")]
        public void LeoDirectFuzzySetter()
        {
            for (var i = 0; i < N; i++)
            {
                _leoFuzzyHandler["Age"] = 55;
            }
        }

        [Benchmark(Description = "NCC Natasha Leo Typed Precision Visitor Setter")]
        [BenchmarkCategory("Setter")]
        public void LeoTypedPrecisionSetter()
        {
            for (var i = 0; i < N; i++)
            {
                _leoPrecisionVisitor.SetValue("Age", 55);
            }
        }

        [Benchmark(Description = "NCC Natasha Leo Typed Hash Visitor Setter")]
        [BenchmarkCategory("Setter")]
        public void LeoTypedHashSetter()
        {
            for (var i = 0; i < N; i++)
            {
                _leoHashVisitor.SetValue("Age", 55);
            }
        }

        [Benchmark(Description = "NCC Natasha Leo Typed Fuzzy Visitor Setter")]
        [BenchmarkCategory("Setter")]
        public void LeoTypedFuzzySetter()
        {
            for (var i = 0; i < N; i++)
            {
                _leoFuzzyVisitor.SetValue("Age", 55);
            }
        }

        [Benchmark(Description = "Newbe ObjectVisitor Setter")]
        [BenchmarkCategory("Setter")]
        public void ObjectVisitorSetter()
        {
            for (var i = 0; i < N; i++)
            {
                ValueSetter<NiceLemon, int, int>.GetSetter(_propertyForAge).Invoke(_lemon, 55);
            }
        }
    }
}
