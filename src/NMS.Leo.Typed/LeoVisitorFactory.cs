using System;
using NMS.Leo.Typed.Core;

namespace NMS.Leo.Typed
{
    public static class LeoVisitorFactory
    {
        public static ILeoVisitor Create(Type type, object instance, AlgorithmKind kind = AlgorithmKind.Precision, bool repeatable = true)
        {
            return LeoVisitorFactoryCore.CreateForInstance(type, instance, kind, repeatable);
        }

        public static ILeoVisitor Create(Type type, AlgorithmKind kind = AlgorithmKind.Precision, bool repeatable = true)
        {
            if (type.IsAbstract && type.IsSealed)
                return LeoVisitorFactoryCore.CreateForStaticType(type, kind);
            return LeoVisitorFactoryCore.CreateForFutureInstance(type, kind, repeatable);
        }

        public static ILeoVisitor<T> Create<T>(T instance, AlgorithmKind kind = AlgorithmKind.Precision, bool repeatable = true)
        {
            return LeoVisitorFactoryCore.CreateForInstance<T>(instance, kind, repeatable);
        }

        public static ILeoVisitor<T> Create<T>(AlgorithmKind kind = AlgorithmKind.Precision, bool repeatable = true)
        {
            var type = typeof(T);
            if (type.IsAbstract && type.IsSealed)
                return LeoVisitorFactoryCore.CreateForStaticType<T>(kind);
            return LeoVisitorFactoryCore.CreateForFutureInstance<T>(kind, repeatable);
        }
    }
}