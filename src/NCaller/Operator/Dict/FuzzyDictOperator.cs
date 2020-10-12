using Natasha.CSharp;
using NCaller.Builder;
using NCaller.Constraint;
using System;

namespace NCaller
{


    public static unsafe class FuzzyDictOperator
    {

        public static delegate* managed<Type, DictBase> CreateFromString;
        static FuzzyDictOperator()
        {

            FuzzyDictBuilder.Ctor(typeof(NullClass));

        }

        public static DictBase CreateFromType(Type type)
        {
            return CreateFromString(type);
        }

    }




    public unsafe static class FuzzyDictOperator<T>
    {

        public readonly static delegate* managed<DictBase> Create;
        static FuzzyDictOperator()
        {
            Type dynamicType = DictBuilder.InitType(typeof(T), Core.Model.FindTreeType.Fuzzy);
            Create = (delegate* managed<DictBase>)(NInstance.Creator(dynamicType).Method.MethodHandle.GetFunctionPointer());
        }

    }

}
