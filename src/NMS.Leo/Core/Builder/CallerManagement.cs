using NMS.Leo.Constraint;
using System;
using System.Collections.Concurrent;
using Natasha.CSharp;

namespace NMS.Leo
{
    internal static class CallerManagement
    {

        public static readonly ConcurrentDictionary<string, Type> Cache;
        public static readonly Func<string, string> GetTypeFunc;

        static CallerManagement()
        {
            Cache = new ConcurrentDictionary<string, Type> { ["NullClass"] = typeof(NullClass) };
            GetTypeFunc = item => "CallerManagement.Cache[arg]";
        }

        public static void AddType(Type type, Type runtimeProxyType)
        {
            Cache[type.GetDevelopName()] = runtimeProxyType;
        }

        public static bool TryGetRuntimeType(Type type, out Type runtimeProxyType)
        {
            return Cache.TryGetValue(type.GetDevelopName(), out runtimeProxyType);
        }
    }

}
