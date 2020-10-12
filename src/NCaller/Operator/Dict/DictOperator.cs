using Natasha.CSharp;
using Natasha.CSharp.Builder;
using NCaller.Builder;
using NCaller.Constraint;
using System;


namespace NCaller
{

    public static unsafe class DictOperator
    {

        public static delegate* managed<Type, DictBase> CreateFromString;
        static DictOperator()
        {

            PrecisionDictBuilder.Ctor(typeof(NullClass));

        }

        public static DictBase CreateFromType(Type type)
        {
            return CreateFromString(type);
        }

    }




    public unsafe static class DictOperator<T>
    {

        public readonly static delegate* managed<DictBase> Create;
        static DictOperator()
        {
            Type dynamicType = DictBuilder.InitType(typeof(T), Core.Model.FindTreeType.Precision);
            Create = (delegate* managed<DictBase>)(NInstance.Creator(dynamicType).Method.MethodHandle.GetFunctionPointer());
        }

    }

}
