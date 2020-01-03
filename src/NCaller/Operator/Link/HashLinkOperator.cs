using Natasha.Operator;
using NCaller.Builder;
using NCaller.Constraint;
using System;

namespace NCaller
{

    public class HashLinkOperator
    {

        public static Func<string, LinkBase> CreateFromString;
        static HashLinkOperator()
        {

            CreateFromString = item => default;
            CreateFromString = HashLinkBuilder.Ctor(typeof(NullClass));

        }

        public static LinkBase CreateFromType(Type type)
        {
            return CreateFromString(type.GetDevelopName());
        }

    }




    public class HashLinkOperator<T>
    {

        public readonly static Func<LinkBase> Create;

        static HashLinkOperator()
        {
            Type dynamicType = LinkBuilder.InitType(typeof(T), Core.Model.FindTreeType.Hash);
            Create = (Func<LinkBase>)(CtorOperator.Default().NewDelegate(dynamicType));
        }

    }

}
