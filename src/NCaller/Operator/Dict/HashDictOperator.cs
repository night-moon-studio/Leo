using Natasha.CSharp;
using NCaller.Builder;
using NCaller.Constraint;
using System;

namespace NCaller
{

    public unsafe static class HashDictOperator
    {

        public static delegate* managed<Type, DictBase> CreateFromString;
        static HashDictOperator()
        {

           HashDictBuilder.Ctor(typeof(NullClass));

        }

        public static DictBase CreateFromType(Type type)
        {
            return CreateFromString(type);
        }

    }




    public unsafe static class HashDictOperator<T>
    {

        public readonly static delegate* managed<DictBase> Create;
        static HashDictOperator()
        {
            Type dynamicType = DictBuilder.InitType(typeof(T), Core.Model.FindTreeType.Hash);
            Create = (delegate* managed<DictBase>)(NInstance.Creator(dynamicType).Method.MethodHandle.GetFunctionPointer());
        }

    }


}
