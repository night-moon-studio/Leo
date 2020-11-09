using System;

namespace NMS.Leo.Typed.Core
{
    internal static class SafeLeoHandleSwitcher
    {
        public static Func<Type, DictBase> Switch(AlgorithmType algorithmType)
        {
            switch (algorithmType)
            {
                case AlgorithmType.Precision:
                    return PrecisionDictOperator.CreateFromType;
                case AlgorithmType.Hash:
                    return HashDictOperator.CreateFromType;
                case AlgorithmType.Fuzzy:
                    return FuzzyDictOperator.CreateFromType;
                default:
                    throw new InvalidOperationException("Unknown AlgorithmType.");
            }
        }
    }

    internal static unsafe class UnsafeLeoHandleSwitcher
    {
        public static Func<DictBase> Switch<T>(AlgorithmType algorithmType)
        {
            switch (algorithmType)
            {
                case AlgorithmType.Precision:
                    return () => PrecisionDictOperator<T>.Create();
                case AlgorithmType.Hash:
                    return () => HashDictOperator<T>.Create();
                case AlgorithmType.Fuzzy:
                    return () => FuzzyDictOperator<T>.Create();
                default:
                    throw new InvalidOperationException("Unknown AlgorithmType.");
            }
        }
    }
}