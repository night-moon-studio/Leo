using System;

namespace NMS.Leo.Typed.Core
{
    internal static class SafeLeoHandleSwitcher
    {
        public static Func<Type, DictBase> Switch(AlgorithmKind kind)
        {
            switch (kind)
            {
                case AlgorithmKind.Precision:
                    return PrecisionDictOperator.CreateFromType;
                case AlgorithmKind.Hash:
                    return HashDictOperator.CreateFromType;
                case AlgorithmKind.Fuzzy:
                    return FuzzyDictOperator.CreateFromType;
                default:
                    throw new InvalidOperationException("Unknown AlgorithmKind.");
            }
        }
    }

    internal static unsafe class UnsafeLeoHandleSwitcher
    {
        public static Func<DictBase> Switch<T>(AlgorithmKind kind)
        {
            switch (kind)
            {
                case AlgorithmKind.Precision:
                    return () => PrecisionDictOperator<T>.Create();
                case AlgorithmKind.Hash:
                    return () => HashDictOperator<T>.Create();
                case AlgorithmKind.Fuzzy:
                    return () => FuzzyDictOperator<T>.Create();
                default:
                    throw new InvalidOperationException("Unknown AlgorithmKind.");
            }
        }
    }
}