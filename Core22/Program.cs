using NCaller;
using System;
using System.Diagnostics;

namespace Core22
{
    class Program
    {
        public static DateTime TempTime;
        static void Main(string[] args)
        {
            TempTime = DateTime.Now;
            Stopwatch stopwatch = new Stopwatch();
            for (int j = 0; j < 20; j++)
            {
                Console.WriteLine("=========================================");


                stopwatch.Restart();
                for (int i = 0; i < 50000; i++)
                {
                    var tEntity = new TestB();
                    if (tEntity.Name == "111")
                    {
                        //调用动态委托赋值
                        tEntity.Name = "222";
                    }
                }
                stopwatch.Stop();
                Console.WriteLine("原生调用:\t\t" + stopwatch.Elapsed);



                var entity = DynamicCaller.Create(typeof(TestB));
                stopwatch.Restart();
                for (int i = 0; i < 50000; i++)
                {
                    entity.New();
                    if (entity.Get<string>("Name") == "111")
                    {
                        //调用动态委托赋值
                        entity.Set("Name", "222");
                    }
                }
                stopwatch.Stop();
                Console.WriteLine("NCaller SimpleCaller:\t" + stopwatch.Elapsed);


                stopwatch.Restart();
                for (int i = 0; i < 50000; i++)
                {
                    RunDynamic(new TestB());
                }
                stopwatch.Stop();
                Console.WriteLine("Dynamic :\t\t" + stopwatch.Elapsed);


                stopwatch.Restart();
                for (int i = 0; i < 50000; i++)
                {
                    var tEntity = (new TestB()).Caller();
                    if (tEntity.Get<string>("Name") == "111")
                    {
                        //调用动态委托赋值
                        tEntity.Set("Name", "222");
                    }
                }
                stopwatch.Stop();
                Console.WriteLine("NCaller Extension:\t" + stopwatch.Elapsed);


                entity = DynamicCaller.Create(typeof(TestB));
                stopwatch.Restart();
                for (int i = 0; i < 50000; i++)
                {
                    entity.New();
                    if (entity.Get<DateTime>("Time") != TempTime)
                    {
                        //调用动态委托赋值
                        entity.Set("Time", TempTime);
                    }
                }
                stopwatch.Stop();
                Console.WriteLine("NCaller SimpleCaller:\t" + stopwatch.Elapsed);

                stopwatch.Restart();
                for (int i = 0; i < 50000; i++)
                {
                    RunDynamicTime(new TestB());
                }
                stopwatch.Stop();
                Console.WriteLine("Dynamic :\t\t" + stopwatch.Elapsed);

                entity = DynamicCaller.Create(typeof(TestB));
                stopwatch.Restart();
                for (int i = 0; i < 50000; i++)
                {
                    entity.New();
                    if (entity.Get<DateTime>("Time") != TempTime)
                    {
                        //调用动态委托赋值
                        entity.Set("Time", TempTime);
                    }
                }
                stopwatch.Stop();
                Console.WriteLine("NCaller SimpleCaller:\t" + stopwatch.Elapsed);
                Console.WriteLine("=========================================");
            }
            Console.ReadKey();
        }
        public static void RunDynamic(dynamic tEntity)
        {
            if (tEntity.Name == "111")
            {
                //调用动态委托赋值
                tEntity.Name = "222";
            }
        }

        public static void RunDynamicTime(dynamic tEntity)
        {
            if (tEntity.Time != TempTime)
            {
                //调用动态委托赋值
                tEntity.Time = TempTime;
            }
        }
    }

    public class TestB
    {
        public TestB()
        {
            Time = DateTime.Now;
        }
        public int Age;
        public int Age1;
        public int Age2;
        public int Age3;
        public int Age4;
        public int Age5;
        public int Age6;

        public int Age7;
        public int Age8;
        public int Age9;
        public int Age10;
        public int Age11;
        public int Age12;
        public int Age13;
        public int Age21;
        public int Age31;
        public int Age41;
        public int Age51;
        public int Age61;

        public int Age71;
        public int Age81;
        public int Age91;
        public int Age211;
        public int Age311;
        public int Age411;
        public int Age511;
        public int Age611;

        public int Age711;
        public int Age811;
        public int Age911;

        public string Name;
        public DateTime Time;
    }
}
