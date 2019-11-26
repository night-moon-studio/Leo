using Natasha;
using Natasha.Operator;
using NCaller.Builder;
using NCaller.Constraint;
using System;

namespace NCaller
{


    public class FuzzyDictOperator
    {

        public static Func<string, DictBase> CreateFromString;
        static FuzzyDictOperator()
        {

            CreateFromString = item => default;
            CreateFromString = FuzzyDictBuilder.Ctor(typeof(NullClass));

        }

        public static DictBase CreateFromType(Type type)
        {
            return CreateFromString(type.GetDevelopName());
        }

    }




    public static class FuzzyDictOperator<T>
    {

        public readonly static Func<DictBase> Create;
        static FuzzyDictOperator()
        {
            Type dynamicType = DictBuilder.InitType(typeof(T), Core.Model.FindTreeType.Fuzzy);
            Create = (Func<DictBase>)CtorOperator.NewDelegate(dynamicType);
        }

    }

}
