using Natasha;
using NCaller.Constraint;
using System;
using System.Collections.Concurrent;

namespace NCaller
{
    public static class CallerManagement
    {

        public static readonly ConcurrentDictionary<string, Type> Cache;
        public static readonly Func<string, string> GetTypeFunc;
        static CallerManagement() 
        { 
            Cache = new ConcurrentDictionary<string, Type>();
            Cache["NullClass"] = typeof(NullClass);
            GetTypeFunc = item => "CallerManagement.Cache[arg]";
        }




        public static void AddType(Type type)
        {

            Cache[type.GetDevelopName()] = type;

        }

    }

}
