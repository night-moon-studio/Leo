using NCaller.Builder;
using System;
using System.Collections.Generic;
using System.Text;

namespace NCaller.Operator
{

    public class HashLinkOperator
    {
        public static LinkBase Create(Type type) => HashLinkBuilder.Ctor(type);
    }




    public class HashLinkOperator<T>
    {
        public readonly static Func<LinkBase> Create;
        static HashLinkOperator() => Create = LinkBuilder.InitType(typeof(T), Core.Model.FindTreeType.Hash);
    }

}
