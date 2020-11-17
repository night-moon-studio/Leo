using System;
using NMS.Leo.Typed.Core;

namespace NMS.Leo.Typed
{
    public static class LeoSetter
    {
        public static ILeoSetter For(Type type, object instance, AlgorithmKind kind = AlgorithmKind.Precision)
        {
            return LeoVisitorFactoryCore.CreateForInstance(type, instance, kind, false);
        }

        public static ILeoSetter For(Type type, AlgorithmKind kind = AlgorithmKind.Precision)
        {
            if (type.IsAbstract && type.IsSealed)
                return LeoVisitorFactoryCore.CreateForStaticType(type, kind);
            return LeoVisitorFactoryCore.CreateForFutureInstance(type, kind, false);
        }

        public static ILeoSetter<T> For<T>(T instance, AlgorithmKind kind = AlgorithmKind.Precision)
        {
            return LeoVisitorFactoryCore.CreateForInstance<T>(instance, kind, false);
        }

        public static ILeoSetter<T> For<T>(AlgorithmKind kind = AlgorithmKind.Precision)
        {
            var type = typeof(T);
            if (type.IsAbstract && type.IsSealed)
                return LeoVisitorFactoryCore.CreateForStaticType<T>(kind);
            return LeoVisitorFactoryCore.CreateForFutureInstance<T>(kind, false);
        }
    }
}