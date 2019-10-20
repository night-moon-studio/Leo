using Natasha;
using Natasha.Operator;
using NCaller.Builder;
using NCaller.Constraint;
using System;

namespace NCaller.Operator
{

    public class HashDictOperator
    {

        public static Func<string, DictBase> CreateFromString;
        static HashDictOperator()
        {

            CreateFromString = HashDictBuilder.Ctor(typeof(NullClass));

        }

        public static DictBase CreateFromType(Type type)
        {
            return CreateFromString(type.GetDevelopName());
        }

    }




    public static class HashDictOperator<T>
    {

        public readonly static Func<DictBase> Create;

        static HashDictOperator()
        {
            Type dynamicType = DictBuilder.InitType(typeof(T), Core.Model.FindTreeType.Hash);
            Create = (Func<DictBase>)CtorOperator.NewDelegate(dynamicType);
        }

    }


}
