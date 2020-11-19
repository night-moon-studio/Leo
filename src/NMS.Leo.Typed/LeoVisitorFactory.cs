using System;
using System.Collections.Generic;
using NMS.Leo.Typed.Core;

namespace NMS.Leo.Typed
{
    public static class LeoVisitorFactory
    {
        public static ILeoVisitor Create(Type type, object instance, AlgorithmKind kind = AlgorithmKind.Precision, bool repeatable = VisitorMode.REPEATABLE)
        {
            return LeoVisitorFactoryCore.CreateForInstance(type, instance, kind, repeatable, VisitorMode.FULL);
        }

        public static ILeoVisitor Create(Type type, AlgorithmKind kind = AlgorithmKind.Precision, bool repeatable = VisitorMode.REPEATABLE)
        {
            if (type.IsAbstract && type.IsSealed)
                return LeoVisitorFactoryCore.CreateForStaticType(type, kind, VisitorMode.FULL);
            return LeoVisitorFactoryCore.CreateForFutureInstance(type, kind, repeatable, VisitorMode.FULL);
        }

        public static ILeoVisitor Create(Type type, IDictionary<string, object> initialValues, AlgorithmKind kind = AlgorithmKind.Precision, bool repeatable = VisitorMode.REPEATABLE)
        {
            if (type.IsAbstract && type.IsSealed)
                return LeoVisitorFactoryCore.CreateForStaticType(type, kind, VisitorMode.FULL);
            return LeoVisitorFactoryCore.CreateForFutureInstance(type, kind, repeatable, VisitorMode.FULL, initialValues);
        }

        public static ILeoVisitor<T> Create<T>(T instance, AlgorithmKind kind = AlgorithmKind.Precision, bool repeatable = VisitorMode.REPEATABLE)
        {
            return LeoVisitorFactoryCore.CreateForInstance<T>(instance, kind, repeatable, VisitorMode.FULL);
        }

        public static ILeoVisitor<T> Create<T>(AlgorithmKind kind = AlgorithmKind.Precision, bool repeatable = VisitorMode.REPEATABLE)
        {
            var type = typeof(T);
            if (type.IsAbstract && type.IsSealed)
                return LeoVisitorFactoryCore.CreateForStaticType<T>(kind, VisitorMode.FULL);
            return LeoVisitorFactoryCore.CreateForFutureInstance<T>(kind, repeatable, VisitorMode.FULL);
        }

        public static ILeoVisitor<T> Create<T>(IDictionary<string, object> initialValues, AlgorithmKind kind = AlgorithmKind.Precision, bool repeatable = VisitorMode.REPEATABLE)
        {
            var type = typeof(T);
            if (type.IsAbstract && type.IsSealed)
                return LeoVisitorFactoryCore.CreateForStaticType<T>(kind, VisitorMode.FULL);
            return LeoVisitorFactoryCore.CreateForFutureInstance<T>(kind, repeatable, VisitorMode.FULL, initialValues);
        }
    }
}