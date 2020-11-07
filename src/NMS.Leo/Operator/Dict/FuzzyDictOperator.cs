using Natasha.CSharp;
using NMS.Leo.Builder;
using NMS.Leo.Constraint;
using System;

namespace NMS.Leo
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

    public static unsafe class FuzzyDictOperator<T>
    {
        public static readonly delegate* managed<DictBase> Create;

        static FuzzyDictOperator()
        {
            var dynamicType = DictBuilder.InitType(typeof(T), Core.Model.FindTreeType.Fuzzy);
            Create = (delegate * managed<DictBase>)(NInstance.Creator(dynamicType).Method.MethodHandle.GetFunctionPointer());
        }
    }
}