using System;
using System.Collections.Concurrent;

namespace NCaller.Builder
{
    public class HashLinkBuilder
    {

        public static readonly ConcurrentDictionary<Type, Func<LinkBase>> TypeCreatorMapping;
        static HashLinkBuilder() => TypeCreatorMapping = new ConcurrentDictionary<Type, Func<LinkBase>>();


        public static LinkBase Ctor(Type type)
        {

            if (!TypeCreatorMapping.ContainsKey(type))
            {

                TypeCreatorMapping[type] = LinkBuilder.InitType(type, Core.Model.FindTreeType.Hash);

            }

            return TypeCreatorMapping[type]();

        }

    }
}
