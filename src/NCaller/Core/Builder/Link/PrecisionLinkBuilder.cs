using BTFindTree;
using Natasha;
using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;

namespace NCaller.Builder
{

    public class PrecisionLinkBuilder
    {

        static PrecisionLinkBuilder()
        {

            _type_cache = new ConcurrentDictionary<Type, string>();
            _str_cache = new ConcurrentDictionary<string, string>();
            StrTypeCache = new ConcurrentDictionary<string, Type>();
            _pdc = new PDC<Type, LinkBase>();

        }

        private static readonly ConcurrentDictionary<Type, string> _type_cache;
        private static readonly ConcurrentDictionary<string, string> _str_cache;
        public static readonly ConcurrentDictionary<string, Type> StrTypeCache;
        private static readonly PDC<Type, LinkBase> _pdc;



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
            PDC<Type, LinkBase> handler = default;
            if (_pdc.BuilderInfo != null)
            {
                handler = _pdc | _str_cache;
            }
            else
            {
                handler = (_pdc | _str_cache | LinkOperator.CreateFromString | Ctor) % CallerManagement.GetTypeFunc;
            }
            return NDomain.Default.UnsafeFunc<string, LinkBase>(handler.ToString(), _type_cache.Keys.ToArray(), "NCallerDynamic", "NCaller.Builder");

        }

    }

}
