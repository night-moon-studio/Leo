using Natasha.CSharp;
using NMS.Leo.Builder;
using NMS.Leo.Constraint;
using System;

namespace NMS.Leo
{
    public static unsafe class PrecisionDictOperator
    {
        public static delegate* managed<Type, DictBase> CreateFromString;

        static PrecisionDictOperator()
        {
            PrecisionDictBuilder.Ctor(typeof(NullClass));
        }

        public static DictBase CreateFromType(Type type)
        {
            return CreateFromString(type);
        }
    }

    public static unsafe class PrecisionDictOperator<T>
    {
        public static readonly delegate* managed<DictBase> Create;

        static PrecisionDictOperator()
        {
            var dynamicType = DictBuilder.InitType(typeof(T), AlgorithmKind.Precision);
            Create = (delegate * managed<DictBase>)(NInstance.Creator(dynamicType).Method.MethodHandle.GetFunctionPointer());
        }
    }
}