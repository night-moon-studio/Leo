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
        public static string Name { get; set; }

        public static DateTime Temp;

        public static float Money;
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
