using System;
using System.Collections.Generic;
using NMS.Leo.Typed.Core.Extensions;

namespace NMS.Leo.Typed.Core
{
    internal static class LeoVisitorFactoryCore
    {
        public static InstanceVisitor CreateForInstance(Type type, object instance, AlgorithmKind kind, bool repeatable, bool liteMode, bool strictMode)
        {
            var handler = SafeLeoHandleSwitcher.Switch(kind)(type);
            return new InstanceVisitor(handler, type, instance, kind, repeatable, liteMode, strictMode);
        }

        public static InstanceVisitor<T> CreateForInstance<T>(T instance, AlgorithmKind kind, bool repeatable, bool liteMode, bool strictMode)
        {
            var handler = UnsafeLeoHandleSwitcher.Switch<T>(kind)().With<T>();
            return new InstanceVisitor<T>(handler, instance, kind, repeatable, liteMode, strictMode);
        }

        public static FutureInstanceVisitor CreateForFutureInstance(Type type, AlgorithmKind kind, bool repeatable, bool liteMode, bool strictMode, IDictionary<string, object> initialValues = null)
        {
            var handler = SafeLeoHandleSwitcher.Switch(kind)(type);
            return new FutureInstanceVisitor(handler, type, kind, repeatable, initialValues, liteMode, strictMode);
        }

        public static FutureInstanceVisitor<T> CreateForFutureInstance<T>(AlgorithmKind kind, bool repeatable, bool liteMode, bool strictMode, IDictionary<string, object> initialValues = null)
        {
            var handler = UnsafeLeoHandleSwitcher.Switch<T>(kind)().With<T>();
            return new FutureInstanceVisitor<T>(handler, kind, repeatable, initialValues, liteMode, strictMode);
        }

        public static StaticTypeLeoVisitor CreateForStaticType(Type type, AlgorithmKind kind, bool liteMode, bool strictMode)
        {
            var handler = SafeLeoHandleSwitcher.Switch(kind)(type);
            return new StaticTypeLeoVisitor(handler, type, kind, liteMode, strictMode);
        }

        public static StaticTypeLeoVisitor<T> CreateForStaticType<T>(AlgorithmKind kind, bool liteMode, bool strictMode)
        {
            var handler = UnsafeLeoHandleSwitcher.Switch<T>(kind)().With<T>();
            return new StaticTypeLeoVisitor<T>(handler, kind, liteMode, strictMode);
        }
    }
}