using System;
using System.Collections.Generic;

namespace NMS.Leo.Typed.Core
{
    internal static class LeoVisitorFactoryCore
    {
        public static InstanceVisitor CreateForInstance(Type type, object instance, AlgorithmKind kind, bool repeatable, bool liteMode)
        {
            var handler = SafeLeoHandleSwitcher.Switch(kind)(type);
            return new InstanceVisitor(handler, type, instance, kind, repeatable, liteMode);
        }

        public static InstanceVisitor<T> CreateForInstance<T>(T instance, AlgorithmKind kind, bool repeatable, bool liteMode)
        {
            var handler = UnsafeLeoHandleSwitcher.Switch<T>(kind)().With<T>();
            return new InstanceVisitor<T>(handler, instance, kind, repeatable, liteMode);
        }

        public static FutureInstanceVisitor CreateForFutureInstance(Type type, AlgorithmKind kind, bool repeatable, bool liteMode, IDictionary<string, object> initialValues = null)
        {
            var handler = SafeLeoHandleSwitcher.Switch(kind)(type);
            return new FutureInstanceVisitor(handler, type, kind, repeatable, initialValues, liteMode);
        }

        public static FutureInstanceVisitor<T> CreateForFutureInstance<T>(AlgorithmKind kind, bool repeatable, bool liteMode, IDictionary<string, object> initialValues = null)
        {
            var handler = UnsafeLeoHandleSwitcher.Switch<T>(kind)().With<T>();
            return new FutureInstanceVisitor<T>(handler, kind, repeatable, initialValues, liteMode);
        }

        public static StaticTypeLeoVisitor CreateForStaticType(Type type, AlgorithmKind kind, bool liteMode)
        {
            var handler = SafeLeoHandleSwitcher.Switch(kind)(type);
            return new StaticTypeLeoVisitor(handler, type, kind, liteMode);
        }

        public static StaticTypeLeoVisitor<T> CreateForStaticType<T>(AlgorithmKind kind, bool liteMode)
        {
            var handler = UnsafeLeoHandleSwitcher.Switch<T>(kind)().With<T>();
            return new StaticTypeLeoVisitor<T>(handler, kind, liteMode);
        }
    }
}