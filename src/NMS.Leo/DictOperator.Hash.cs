using Natasha.CSharp;
using NMS.Leo.Builder;
using System;
using NMS.Leo.Core;

namespace NMS.Leo
{
    public static unsafe class HashDictOperator
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

    public static unsafe class HashDictOperator<T>
    {
        public static readonly delegate* managed<DictBase> Create;

        static HashDictOperator()
        {
            var dynamicType = DictBuilder.InitType(typeof(T), AlgorithmKind.Hash);
            Create = (delegate * managed<DictBase>)(NInstance.Creator(dynamicType).Method.MethodHandle.GetFunctionPointer());
        }
    }
}