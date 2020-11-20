using BenchmarkDotNet.Running;
using System;

namespace BenchmarkTest
{
    class Program
    {
        static void Main(string[] args)
        {
            BenchmarkRunner.Run<TestCaller>();
            Console.ReadKey();
        }
    }
}
