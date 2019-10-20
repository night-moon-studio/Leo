using BTFindTree;
using Natasha;
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

        }

        private static readonly ConcurrentDictionary<Type, string> _type_cache;
        private static readonly ConcurrentDictionary<string, string> _str_cache;
        public static readonly ConcurrentDictionary<string, Type> StrTypeCache;


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
            StringBuilder builder = new StringBuilder();
            builder.Append(BTFTemplate.GetPrecisionPointBTFScript(_str_cache));
            builder.Append($"HashLinkOperator.CreateFromString = HashLinkBuilder.Ctor(CallerManagement.Cache[arg]);");
            builder.Append("return HashLinkOperator.CreateFromString(arg);");
            return NFunc<string, LinkBase>.UnsafeDelegate(builder.ToString(), _type_cache.Keys.ToArray(), "NCallerDynamic", "NCaller.Builder");

        }

    }

}
