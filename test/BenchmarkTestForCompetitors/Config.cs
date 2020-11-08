using System;
using System.Collections.Generic;
using System.Text;
using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Environments;
using BenchmarkDotNet.Exporters;
using BenchmarkDotNet.Exporters.Csv;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Mathematics;

namespace BenchmarkTestForCompetitors
{
    public class Config : ManualConfig
    {
        public Config()
        {
            AddColumn(
                TargetMethodColumn.Method,
                new CategoriesColumn(),
                StatisticColumn.Mean,
                StatisticColumn.Error,
                StatisticColumn.StdDev,
                new RankColumn(NumeralSystem.Arabic));
            AddExporter(CsvMeasurementsExporter.Default);
            AddExporter(RPlotExporter.Default);
            AddExporter(MarkdownExporter.GitHub);

            AddJob(Job.Default
                    .WithId("netcoreapp31")
                    .WithRuntime(CoreRuntime.Core31));
        }
    }
}
