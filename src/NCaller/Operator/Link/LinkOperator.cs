using Natasha;
using Natasha.Operator;
using NCaller.Builder;
using NCaller.Constraint;
using System;


namespace NCaller
{

    public class LinkOperator
    {

        public static Func<string, LinkBase> CreateFromString;
        static LinkOperator()
        {

            CreateFromString = PrecisionLinkBuilder.Ctor(typeof(NullClass));

        }

        public static LinkBase CreateFromType(Type type)
        {
            return CreateFromString(type.GetDevelopName());
        }

    }




    public class LinkOperator<T>
    {

        public readonly static Func<LinkBase> Create;
        static LinkOperator()
        {
            Type dynamicType = LinkBuilder.InitType(typeof(T), Core.Model.FindTreeType.Precision);
            Create = (Func<LinkBase>)(CtorOperator.Default.NewDelegate(dynamicType));
        }

    }

}
