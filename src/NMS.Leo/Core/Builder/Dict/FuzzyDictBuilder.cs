using BTFindTree;
using Natasha.CSharp;
using System;
using System.Collections.Concurrent;
using System.Linq;

namespace NMS.Leo.Builder
{
    public class FuzzyDictBuilder
    {
        static FuzzyDictBuilder()
        {
            _type_cache = new ConcurrentDictionary<Type, string>();
            _str_cache = new ConcurrentDictionary<string, string>();
        }

        private static readonly ConcurrentDictionary<Type, string> _type_cache;
        private static readonly ConcurrentDictionary<string, string> _str_cache;
        
        public static unsafe DictBase Ctor(Type type)
        {
            //获得动态生成的类型
            Type proxyType = DictBuilder.InitType(type, Core.Model.FindTreeType.Fuzzy);

            //加入缓存
            string script = $"return new {proxyType.GetDevelopName()}();";
            _str_cache[type.GetDevelopName()] = script;
            _type_cache[type] = script;

            string newFindTree = "var str = arg.GetDevelopName();";
            newFindTree += BTFTemplate.GetPrecisionPointBTFScript(_str_cache, "str");
            newFindTree += $"return PrecisionDictBuilder.Ctor(arg);";


            //生成脚本
            var newAction = NDelegate
                            .UseDomain(type.GetDomain(), builder => builder.LogCompilerError())
                            .UnsafeFunc<Type, DictBase>(newFindTree, _type_cache.Keys.ToArray(), "NMS.Leo.NCallerDynamic");

            FuzzyDictOperator.CreateFromString = (delegate * managed<Type, DictBase>)(newAction.Method.MethodHandle.GetFunctionPointer());
            return (DictBase) Activator.CreateInstance(proxyType);
        }
    }
}