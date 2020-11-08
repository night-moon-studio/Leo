using System;

namespace NCallerUT.Model
{
    public static class StaticTestModel
    {
        public static int Age;
        public static string Name { get; set; }

        public static DateTime Temp;

        public static float Money;
    }

    public class FakeStaticTestModel
    {
        public static int Age;
        public static string Name { get; set; }

        public static DateTime Temp;

        public static float Money;
    }

    public static class StaticTestModel1
    {
        public static int Age;
        public static string Name { get; set; }

        public static DateTime Temp;

        public static float Money;
    }

    public static class StaticTestModel2
    {
        public static int Age;
        public static string Name { get; set; }

        public static DateTime Temp;

        public static float Money;
    }


    public class FakeStaticTestModel1
    {
        public static int Age;
        public static int Age1;
        public static int Age2;
        public static int AgeAge1;
        public static int AgeAge12;
        public static int AgeAge13;
        public static string Name { get; set; }

        public static DateTime Temp;

        public static float Money;
        public static float Money11;
        public static float Money12;
        public static float Money123;
        public static float Money124;
    }


    public class FakeStaticTestModel2
    {
        public static int Age;
        public static string Name { get; set; }

        public static DateTime Temp;

        public static float Money;
    }


    public class TestB
    {
        public TestB()
        {
            Name = "111";
            InstanceC = new TestC
            {
                Name = "abc"
            };
        }

        public string Name { get; set; }
        public int Age;
        public TestC InstanceC;
    }

    public struct TestC
    {
        public string Name;
    }
}