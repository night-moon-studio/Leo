﻿using BTFindTree;
using System.Collections.Concurrent;
#if NETCOREAPP3_1_OR_GREATER
using Natasha.CSharp.MultiDomain.Extension;
#endif

// ReSharper disable once CheckNamespace
namespace NMS.Leo.Builder;

public class PrecisionDictBuilder
{
    static PrecisionDictBuilder()
    {
        _type_cache = new ConcurrentDictionary<Type, string>();
        _str_cache = new ConcurrentDictionary<string, string>();
    }

    private static readonly ConcurrentDictionary<Type, string> _type_cache;
    private static readonly ConcurrentDictionary<string, string> _str_cache;

    public static unsafe DictBase Ctor(Type type)
    {
        //获得动态生成的类型
        var proxyType = DictBuilder.InitType(type, AlgorithmKind.Fuzzy);

        //加入缓存
        var script = $"return new {proxyType.GetDevelopName()}();";
        _str_cache[type.GetDevelopName()] = script;
        _type_cache[type] = script;

        var newFindTree = "var str = arg.GetDevelopName();";
        newFindTree += BTFTemplate.GetGroupPrecisionPointBTFScript(_str_cache, "str");
        newFindTree += $"return PrecisionDictBuilder.Ctor(arg);";


        //生成脚本
        var newAction = NDelegate
#if NETCOREAPP3_1_OR_GREATER
            .UseDomain(type.GetDomain())
#else
            .DefaultDomain()
#endif
                        .ConfigUsing(_type_cache.Keys.ToArray(), "NMS.Leo.NCallerDynamic")
                        .UnsafeFunc<Type, DictBase>(newFindTree);

        PrecisionDictOperator.CreateFromString = (delegate * managed<Type, DictBase>)(newAction.Method.MethodHandle.GetFunctionPointer());
        return (DictBase) Activator.CreateInstance(proxyType);
    }
}