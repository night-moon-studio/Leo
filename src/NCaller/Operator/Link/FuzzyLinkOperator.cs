using NCaller.Builder;
using System;
using System.Collections.Generic;
using System.Text;

namespace NCaller.Operator
{

    public class FuzzyLinkOperator
    {
        public static LinkBase Create(Type type) => FuzzyLinkBuilder.Ctor(type);
    }




    public class FuzzyLinkOperator<T>
    {
        public readonly static Func<LinkBase> Create;
        static FuzzyLinkOperator() => Create = LinkBuilder.InitType(typeof(T), Core.Model.FindTreeType.Fuzzy);
    }

}
