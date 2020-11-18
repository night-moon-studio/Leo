using System;
using NMS.Leo.Typed.Core;

namespace NMS.Leo.Typed
{
    public static class LeoSetter
    {
        public static IFluentSetter Type(Type type, AlgorithmKind kind = AlgorithmKind.Precision) => new FluentSetterBuilder(type, kind);

        public static IFluentSetter<T> Type<T>(AlgorithmKind kind = AlgorithmKind.Precision) => new FluentSetterBuilder<T>(kind);
    }
}