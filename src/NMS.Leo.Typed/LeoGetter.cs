using System;
using System.Collections.Generic;
using NMS.Leo.Typed.Core;

namespace NMS.Leo.Typed
{
    public static class LeoGetter
    {
        public static ILeoGetter For(Type type, object instance, AlgorithmKind kind = AlgorithmKind.Precision)
        {
            return LeoVisitorFactoryCore.CreateForInstance(type, instance, kind, false);
        }

        public static ILeoGetter For(Type type, IDictionary<string, object> initialValues, AlgorithmKind kind = AlgorithmKind.Precision)
        {
            if (type.IsAbstract && type.IsSealed)
                return LeoVisitorFactoryCore.CreateForStaticType(type, kind);
            return LeoVisitorFactoryCore.CreateForFutureInstance(type, kind, false, initialValues);
        }

        public static ILeoGetter<T> For<T>(T instance, AlgorithmKind kind = AlgorithmKind.Precision)
        {
            return LeoVisitorFactoryCore.CreateForInstance<T>(instance, kind, false);
        }

        public static ILeoGetter<T> For<T>(IDictionary<string, object> initialValues, AlgorithmKind kind = AlgorithmKind.Precision)
        {
            var type = typeof(T);
            if (type.IsAbstract && type.IsSealed)
                return LeoVisitorFactoryCore.CreateForStaticType<T>(kind);
            return LeoVisitorFactoryCore.CreateForFutureInstance<T>(kind, false, initialValues);
        }
    }
}