using System;

namespace NMS.Leo.Typed.Core
{
    internal static class SafeLeoHandleSwitcher
    {
        public static Func<Type, DictBase> Switch(LeoType leoType)
        {
            switch (leoType)
            {
                case LeoType.Precision:
                    return PrecisionDictOperator.CreateFromType;
                case LeoType.Hash:
                    return HashDictOperator.CreateFromType;
                case LeoType.Fuzzy:
                    return FuzzyDictOperator.CreateFromType;
                default:
                    throw new InvalidOperationException("Unknown LeoType.");
            }
        }
    }

    internal static unsafe class UnsafeLeoHandleSwitcher
    {
        public static Func<DictBase> Switch<T>(LeoType leoType)
        {
            switch (leoType)
            {
                case LeoType.Precision:
                    return () => PrecisionDictOperator<T>.Create();
                case LeoType.Hash:
                    return () => HashDictOperator<T>.Create();
                case LeoType.Fuzzy:
                    return () => FuzzyDictOperator<T>.Create();
                default:
                    throw new InvalidOperationException("Unknown LeoType.");
            }
        }
    }
}