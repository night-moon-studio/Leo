using System;
using System.Collections.Concurrent;

namespace NCaller.Builder
{
    public static class FuzzyDictBuilder
    {
        public static readonly ConcurrentDictionary<Type, Func<DictBase>> TypeCreatorMapping;
        static FuzzyDictBuilder() => TypeCreatorMapping = new ConcurrentDictionary<Type, Func<DictBase>>();


        public static DictBase Ctor(Type type)
        {

            if (!TypeCreatorMapping.ContainsKey(type))
            {

                TypeCreatorMapping[type] = DictBuilder.InitType(type,Core.Model.FindTreeType.Fuzzy);

            }


            return TypeCreatorMapping[type]();

        }
    }
}
