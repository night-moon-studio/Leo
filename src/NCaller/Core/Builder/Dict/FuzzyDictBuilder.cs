using BTFindTree;
using Natasha;
using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;

namespace NCaller.Builder
{
    public static class FuzzyDictBuilder
    {
        static FuzzyDictBuilder()
        {

            _type_cache = new ConcurrentDictionary<Type, string>();
            _str_cache = new ConcurrentDictionary<string, string>();
            StrTypeCache = new ConcurrentDictionary<string, Type>();
            _fdc = new FDC<Type, DictBase>();

        }

        private static readonly ConcurrentDictionary<Type, string> _type_cache;
        private static readonly ConcurrentDictionary<string, string> _str_cache;
        public static readonly ConcurrentDictionary<string, Type> StrTypeCache;
        private static readonly FDC<Type,DictBase> _fdc;



        public static Func<string, DictBase> Ctor(Type type)
        {

            StrTypeCache[type.GetDevelopName()] = type;
            //获得动态生成的类型
            Type result = DictBuilder.InitType(type, Core.Model.FindTreeType.Fuzzy);
            //加入缓存
            string script = $"return new {result.GetDevelopName()}();";
            _str_cache[type.GetDevelopName()] = script;
            _type_cache[type] = script;


            //生成脚本
            FDC<Type, DictBase> handler = default;
            if (_fdc.BuilderInfo!=null)
            {
                handler = _fdc | _str_cache;
            }
            else
            {
                handler = (_fdc | _str_cache | FuzzyDictOperator.CreateFromString | FuzzyDictBuilder.Ctor) % CallerManagement.GetTypeFunc;
            }
            return NDomain.Create(type.GetDomain()).UnsafeFunc<string, DictBase>(handler.ToString(), _type_cache.Keys.ToArray(), "NCallerDynamic", "NCaller.Builder");

        }

    }

}
