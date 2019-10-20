using Natasha;
using NCaller.Constraint;
using System;
using System.Collections.Concurrent;

namespace NCaller
{
    public static class CallerManagement
    {

        public static readonly ConcurrentDictionary<string, Type> Cache;
        static CallerManagement() 
        { 
            Cache = new ConcurrentDictionary<string, Type>();
            Cache["NullClass"] = typeof(NullClass);
        }




        public static void AddType(Type type)
        {

            Cache[type.GetDevelopName()] = type;

        }

    }

}
