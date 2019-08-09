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
                for (int i = 0; i < 400000; i++)
                {
                    var tEntity = new TestB();
                    if (tEntity.A2ge712 == "111")
                    {
                        //调用动态委托赋值
                        tEntity.A2ge712 = "222";
                    }
                }
                stopwatch.Stop();
                Console.WriteLine("原生调用:\t\t" + stopwatch.Elapsed);



                var entity = LinkOperator.Create(typeof(TestB));
                stopwatch.Restart();
                for (int i = 0; i < 400000; i++)
                {
                    entity.New();
                    if (entity.Get<string>("A2ge712") == "111")
                    {
                        //调用动态委托赋值
                        entity.Set("A2ge712", "222");
                    }
                }
                stopwatch.Stop();
                Console.WriteLine("NCaller SimpleCaller:\t" + stopwatch.Elapsed);

                var dict = DictOperator.Create(typeof(TestB));
                stopwatch.Restart();
                for (int i = 0; i < 400000; i++)
                {
                    dict.New();
                    if ((string)(dict["A2ge712"]) == "111")
                    {
                        //调用动态委托赋值
                        dict["A2ge712"] = "222";
                    }
                }
                stopwatch.Stop();
                Console.WriteLine("NCaller DictCaller:\t" + stopwatch.Elapsed);

                stopwatch.Restart();
                for (int i = 0; i < 400000; i++)
                {
                    RunDynamic(new TestB());
                }
                stopwatch.Stop();
                Console.WriteLine("Dynamic :\t\t" + stopwatch.Elapsed);


                stopwatch.Restart();
                for (int i = 0; i < 400000; i++)
                {
                    var tEntity = (new TestB()).LinkCaller();
                    if (tEntity.Get<string>("A2ge712") == "111")
                    {
                        //调用动态委托赋值
                        tEntity.Set("A2ge712", "222");
                    }
                }
                stopwatch.Stop();
                Console.WriteLine("NCaller Extension:\t" + stopwatch.Elapsed);


                //entity = DynamicCaller.Create(typeof(TestB));
                //stopwatch.Restart();
                //for (int i = 0; i < 400000; i++)
                //{
                //    entity.New();
                //    if (entity.Get<DateTime>("Time") != TempTime)
                //    {
                //        //调用动态委托赋值
                //        entity.Set("Time", TempTime);
                //    }
                //}
                //stopwatch.Stop();
                //Console.WriteLine("NCaller SimpleCaller:\t" + stopwatch.Elapsed);

                //stopwatch.Restart();
                //for (int i = 0; i < 400000; i++)
                //{
                //    RunDynamicTime(new TestB());
                //}
                //stopwatch.Stop();
                //Console.WriteLine("Dynamic :\t\t" + stopwatch.Elapsed);

                //entity = DynamicCaller.Create(typeof(TestB));
                //stopwatch.Restart();
                //for (int i = 0; i < 400000; i++)
                //{
                //    entity.New();
                //    if (entity.Get<DateTime>("Time") != TempTime)
                //    {
                //        //调用动态委托赋值
                //        entity.Set("Time", TempTime);
                //    }
                //}
                //stopwatch.Stop();
                //Console.WriteLine("NCaller SimpleCaller:\t" + stopwatch.Elapsed);
                Console.WriteLine("=========================================");
            }

            //var dict = DictOperator<TestB>.Create();
            //dict["Name"] = "Hello";
            //dict["Age"] = 100;
            //dict["Time"] = DateTime.Now;

            Console.ReadKey();
        }

      
        public static void RunDynamic(dynamic tEntity)
        {
            if (tEntity.A2ge712 == "111")
            {
                //调用动态委托赋值
                tEntity.A2ge712 = "222";
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

        public int Age2111;
        public int Age3111;
        public int Age4111;
        public int Age5111;
        public int Age6111;

        public int Age7111;
        public int Age8111;
        public int Age9111;


        public int A2ge;
        public int A2ge1;
        public int A2ge2;
        public int A2ge3;
        public int A2ge4;
        public int A2ge5;
        public int A2ge6;

        public int A2ge7;
        public int A2ge8;
        public int A2ge9;
        public int A2ge10;
        public int A2ge11;
        public int A2ge12;
        public int A2ge13;
        public int Ag2e21;
        public int Ag2e31;
        public int A2ge41;
        public int A2ge51;
        public int A2ge61;

        public int A2ge71;
        public int A2ge81;
        public int A2ge91;
        public int A2ge211;
        public int A2ge311;
        public int A2ge411;
        public int A2ge511;
        public int A2ge611;

        public int A2ge711;
        public int A2ge811;
        public int A2ge911;

        public int A2ge2111;
        public int A2ge3111;
        public int A2ge4111;
        public int A2ge5111;
        public int A2ge6111;

        public int A2ge7111;
        public int A2ge8111;
        public int A2ge9111;


        public string A2ge712;
        public DateTime Time;
    }
}
