using System;
using NMS.Leo.Typed.Core;

namespace NMS.Leo.Typed
{
    public static class LeoVisitorFactory
    {
        public static ILeoVisitor Create(Type type, object instance, LeoType leoType = LeoType.Precision, bool repeatable = true)
        {
            return LeoVisitorFactoryCore.CreateForInstance(type, instance, leoType, repeatable);
        }

        public static ILeoVisitor Create(Type type, LeoType leoType = LeoType.Precision, bool repeatable = true)
        {
            if (type.IsAbstract && type.IsSealed)
                return LeoVisitorFactoryCore.CreateForStaticType(type, leoType);
            return LeoVisitorFactoryCore.CreateForFutureInstance(type, leoType, repeatable);
        }

        public static ILeoVisitor<T> Create<T>(T instance, LeoType leoType = LeoType.Precision, bool repeatable = true)
        {
            return LeoVisitorFactoryCore.CreateForInstance<T>(instance, leoType, repeatable);
        }

        public static ILeoVisitor<T> Create<T>(LeoType leoType = LeoType.Precision, bool repeatable = true)
        {
            var type = typeof(T);
            if (type.IsAbstract && type.IsSealed)
                return LeoVisitorFactoryCore.CreateForStaticType<T>(leoType);
            return LeoVisitorFactoryCore.CreateForFutureInstance<T>(leoType, repeatable);
        }
    }
}