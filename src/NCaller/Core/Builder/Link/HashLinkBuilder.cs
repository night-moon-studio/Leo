using BTFindTree;
using Natasha;
using Natasha.CSharp;
using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;

namespace NCaller.Builder
{

    public class HashLinkBuilder
    {


        static HashLinkBuilder()
        {

            _type_cache = new ConcurrentDictionary<Type, string>();
            _str_cache = new ConcurrentDictionary<string, string>();
            StrTypeCache = new ConcurrentDictionary<string, Type>();
            _hdc = new HDC<Type, string, LinkBase>();

        }

        private static readonly ConcurrentDictionary<Type, string> _type_cache;
        private static readonly ConcurrentDictionary<string, string> _str_cache;
        public static readonly ConcurrentDictionary<string, Type> StrTypeCache;
        private static readonly HDC<Type, string, LinkBase> _hdc;




        public static Func<string, LinkBase> Ctor(Type type)
        {

            StrTypeCache[type.GetDevelopName()] = type;
            //获得动态生成的类型
            Type result = LinkBuilder.InitType(type, Core.Model.FindTreeType.Fuzzy);
            //加入缓存
            string script = $"return new {result.GetDevelopName()}();";
            _str_cache[type.GetDevelopName()] = script;
            _type_cache[type] = script;


            //生成脚本
            HDC<Type, string, LinkBase> handler = default;
            if (_hdc.BuilderInfo != null)
            {
                handler = _hdc | _str_cache;
            }
            else
            {
                handler = (_hdc | _str_cache | HashLinkOperator.CreateFromString | Ctor) % CallerManagement.GetTypeFunc;
            }
            return NDelegate.UseDomain(type.GetDomain()).UnsafeFunc<string, LinkBase>(handler.ToString(), _type_cache.Keys.ToArray(), "NCallerDynamic", "NCaller.Builder");

        }

    }

}
