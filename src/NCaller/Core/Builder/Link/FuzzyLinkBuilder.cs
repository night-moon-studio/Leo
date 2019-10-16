using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace NCaller.Builder
{
    public static  class FuzzyLinkBuilder
    {

        public static readonly ConcurrentDictionary<Type, Func<LinkBase>> TypeCreatorMapping;
        static FuzzyLinkBuilder() => TypeCreatorMapping = new ConcurrentDictionary<Type, Func<LinkBase>>();


        public static LinkBase Ctor(Type type)
        {

            if (!TypeCreatorMapping.ContainsKey(type))
            {

                TypeCreatorMapping[type] = LinkBuilder.InitType(type, Core.Model.FindTreeType.Fuzzy);

            }

            return TypeCreatorMapping[type]();

        }

    }
}
