using System;
using System.Collections.Concurrent;

namespace NCaller.Builder
{
    public class HashDictBuilder
    {

        public static readonly ConcurrentDictionary<Type, Func<DictBase>> TypeCreatorMapping;
        static HashDictBuilder() => TypeCreatorMapping = new ConcurrentDictionary<Type, Func<DictBase>>();


        public static DictBase Ctor(Type type)
        {

            if (!TypeCreatorMapping.ContainsKey(type))
            {

                TypeCreatorMapping[type] = DictBuilder.InitType(type, Core.Model.FindTreeType.Hash);

            }


            return TypeCreatorMapping[type]();

        }

    }

}
