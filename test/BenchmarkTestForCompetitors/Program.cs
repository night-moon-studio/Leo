using System;
using BenchmarkDotNet.Running;

namespace BenchmarkTestForCompetitors
{
    class Program
    {
        static void Main(string[] args)
        {
            NatashaInitializer.InitializeAndPreheating();
            BenchmarkRunner.Run<VisitorBenchmark>();
            Console.ReadKey();
        }
    }
}
