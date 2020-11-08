using System;
using NMS.Leo.Typed.Core;

namespace NMS.Leo.Typed
{
    public static class LeoVisitorFactory
    {
        public static ILeoVisitor Create(Type type, object instance, LeoType leoType = LeoType.Precision)
        {
            return LeoVisitorFactoryCore.CreateForInstance(type, instance, leoType);
        }

        public static ILeoVisitor Create(Type type, LeoType leoType = LeoType.Precision)
        {
            if (type.IsAbstract && type.IsSealed)
                return LeoVisitorFactoryCore.CreateForStaticType(type, leoType);
            return LeoVisitorFactoryCore.CreateForFutureInstance(type, leoType);
        }

        public static ILeoVisitor<T> Create<T>(T instance, LeoType leoType = LeoType.Precision)
        {
            return LeoVisitorFactoryCore.CreateForInstance<T>(instance, leoType);
        }

        public static ILeoVisitor<T> Create<T>(LeoType leoType = LeoType.Precision)
        {
            var type = typeof(T);
            if (type.IsAbstract && type.IsSealed)
                return LeoVisitorFactoryCore.CreateForStaticType<T>(leoType);
            return LeoVisitorFactoryCore.CreateForFutureInstance<T>(leoType);
        }
    }
}