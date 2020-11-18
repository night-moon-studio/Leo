using System;
using NMS.Leo.Typed.Core;

namespace NMS.Leo.Typed
{
    public static class LeoGetter
    {
        public static IFluentGetter Type(Type type, AlgorithmKind kind = AlgorithmKind.Precision) => new FluentGetterBuilder(type, kind);

        public static IFluentGetter<T> Type<T>(AlgorithmKind kind = AlgorithmKind.Precision) => new FluentGetterBuilder<T>(kind);
    }
}