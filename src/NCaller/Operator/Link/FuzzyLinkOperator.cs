using Natasha;
using Natasha.Operator;
using NCaller.Builder;
using NCaller.Constraint;
using System;

namespace NCaller
{

    public static class FuzzyLinkOperator
    {

        public static Func<string, LinkBase> CreateFromString;
        static FuzzyLinkOperator() 
        {

            CreateFromString = item => default;
            CreateFromString = FuzzyLinkBuilder.Ctor(typeof(NullClass));

        }

        public static LinkBase CreateFromType(Type type)
        {
            return CreateFromString(type.GetDevelopName());
        }

    }




    public static class FuzzyLinkOperator<T>
    {

        public readonly static Func<LinkBase> Create;
        static FuzzyLinkOperator() 
        { 
            Type dynamicType = LinkBuilder.InitType(typeof(T), Core.Model.FindTreeType.Fuzzy);
            Create = (Func<LinkBase>)CtorOperator.NewDelegate(dynamicType);
        }

    }

}
