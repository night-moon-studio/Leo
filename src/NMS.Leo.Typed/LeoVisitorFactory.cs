using System;
using NMS.Leo.Typed.Core;

namespace NMS.Leo.Typed
{
    public static class LeoVisitorFactory
    {
        public static ILeoVisitor Create(Type type, object instance, AlgorithmType algorithmType = AlgorithmType.Precision, bool repeatable = true)
        {
            return LeoVisitorFactoryCore.CreateForInstance(type, instance, algorithmType, repeatable);
        }

        public static ILeoVisitor Create(Type type, AlgorithmType algorithmType = AlgorithmType.Precision, bool repeatable = true)
        {
            if (type.IsAbstract && type.IsSealed)
                return LeoVisitorFactoryCore.CreateForStaticType(type, algorithmType);
            return LeoVisitorFactoryCore.CreateForFutureInstance(type, algorithmType, repeatable);
        }

        public static ILeoVisitor<T> Create<T>(T instance, AlgorithmType algorithmType = AlgorithmType.Precision, bool repeatable = true)
        {
            return LeoVisitorFactoryCore.CreateForInstance<T>(instance, algorithmType, repeatable);
        }

        public static ILeoVisitor<T> Create<T>(AlgorithmType algorithmType = AlgorithmType.Precision, bool repeatable = true)
        {
            var type = typeof(T);
            if (type.IsAbstract && type.IsSealed)
                return LeoVisitorFactoryCore.CreateForStaticType<T>(algorithmType);
            return LeoVisitorFactoryCore.CreateForFutureInstance<T>(algorithmType, repeatable);
        }
    }
}