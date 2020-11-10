using System;
using Leo.Typed.Core;

namespace NMS.Leo.Typed.Core
{
    internal static class LeoVisitorFactoryCore
    {
        public static InstanceLeoVisitor CreateForInstance(Type type, object instance, AlgorithmKind kind, bool repeatable)
        {
            var handler = SafeLeoHandleSwitcher.Switch(kind)(type);
            return new InstanceLeoVisitor(handler, type, instance, kind, repeatable);
        }

        public static InstanceLeoVisitor<T> CreateForInstance<T>(T instance, AlgorithmKind kind, bool repeatable)
        {
            var handler = UnsafeLeoHandleSwitcher.Switch<T>(kind)().With<T>(); ;
            return new InstanceLeoVisitor<T>(handler, instance, kind, repeatable);
        }

        public static FutureInstanceLeoVisitor CreateForFutureInstance(Type type, AlgorithmKind kind, bool repeatable)
        {
            var handler = SafeLeoHandleSwitcher.Switch(kind)(type);
            return new FutureInstanceLeoVisitor(handler, type, kind, repeatable);
        }

        public static FutureInstanceLeoVisitor<T> CreateForFutureInstance<T>(AlgorithmKind kind, bool repeatable)
        {
            var handler = UnsafeLeoHandleSwitcher.Switch<T>(kind)().With<T>();
            return new FutureInstanceLeoVisitor<T>(handler, kind, repeatable);
        }

        public static StaticTypeLeoVisitor CreateForStaticType(Type type, AlgorithmKind kind)
        {
            var handler = SafeLeoHandleSwitcher.Switch(kind)(type);
            return new StaticTypeLeoVisitor(handler, type, kind);
        }

        public static StaticTypeLeoVisitor<T> CreateForStaticType<T>(AlgorithmKind kind)
        {
            var handler = UnsafeLeoHandleSwitcher.Switch<T>(kind)().With<T>();
            return new StaticTypeLeoVisitor<T>(handler, kind);
        }
    }
}