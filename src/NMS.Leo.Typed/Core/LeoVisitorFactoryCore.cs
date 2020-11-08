using System;

namespace NMS.Leo.Typed.Core
{
    internal static class LeoVisitorFactoryCore
    {
        public static InstanceLeoVisitor CreateForInstance(Type type, object instance, LeoType leoType)
        {
            var handler = SafeLeoHandleSwitcher.Switch(leoType)(type);
            return new InstanceLeoVisitor(handler, type, instance, leoType);
        }

        public static InstanceLeoVisitor<T> CreateForInstance<T>(T instance, LeoType leoType)
        {
            var handler = (DictBase<T>) UnsafeLeoHandleSwitcher.Switch<T>(leoType)();
            return new InstanceLeoVisitor<T>(handler, instance, leoType);
        }

        public static FutureInstanceLeoVisitor CreateForFutureInstance(Type type, LeoType leoType)
        {
            var handler = SafeLeoHandleSwitcher.Switch(leoType)(type);
            return new FutureInstanceLeoVisitor(handler, type, leoType);
        }

        public static FutureInstanceLeoVisitor<T> CreateForFutureInstance<T>(LeoType leoType)
        {
            var handler = (DictBase<T>) UnsafeLeoHandleSwitcher.Switch<T>(leoType)();
            return new FutureInstanceLeoVisitor<T>(handler, leoType);
        }

        public static StaticTypeLeoVisitor CreateForStaticType(Type type, LeoType leoType)
        {
            var handler = SafeLeoHandleSwitcher.Switch(leoType)(type);
            return new StaticTypeLeoVisitor(handler, type, leoType);
        }

        public static StaticTypeLeoVisitor<T> CreateForStaticType<T>(LeoType leoType)
        {
            var handler = (DictBase<T>) UnsafeLeoHandleSwitcher.Switch<T>(leoType)();
            return new StaticTypeLeoVisitor<T>(handler, leoType);
        }
    }
}