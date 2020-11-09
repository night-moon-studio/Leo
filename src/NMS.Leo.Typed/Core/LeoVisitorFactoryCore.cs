using System;
using Leo.Typed.Core;

namespace NMS.Leo.Typed.Core
{
    internal static class LeoVisitorFactoryCore
    {
        public static InstanceLeoVisitor CreateForInstance(Type type, object instance, AlgorithmType algorithmType, bool repeatable)
        {
            var handler = SafeLeoHandleSwitcher.Switch(algorithmType)(type);
            return new InstanceLeoVisitor(handler, type, instance, algorithmType, repeatable);
        }

        public static InstanceLeoVisitor<T> CreateForInstance<T>(T instance, AlgorithmType algorithmType, bool repeatable)
        {
            var handler = UnsafeLeoHandleSwitcher.Switch<T>(algorithmType)().With<T>(); ;
            return new InstanceLeoVisitor<T>(handler, instance, algorithmType, repeatable);
        }

        public static FutureInstanceLeoVisitor CreateForFutureInstance(Type type, AlgorithmType algorithmType, bool repeatable)
        {
            var handler = SafeLeoHandleSwitcher.Switch(algorithmType)(type);
            return new FutureInstanceLeoVisitor(handler, type, algorithmType, repeatable);
        }

        public static FutureInstanceLeoVisitor<T> CreateForFutureInstance<T>(AlgorithmType algorithmType, bool repeatable)
        {
            var handler = UnsafeLeoHandleSwitcher.Switch<T>(algorithmType)().With<T>();
            return new FutureInstanceLeoVisitor<T>(handler, algorithmType, repeatable);
        }

        public static StaticTypeLeoVisitor CreateForStaticType(Type type, AlgorithmType algorithmType)
        {
            var handler = SafeLeoHandleSwitcher.Switch(algorithmType)(type);
            return new StaticTypeLeoVisitor(handler, type, algorithmType);
        }

        public static StaticTypeLeoVisitor<T> CreateForStaticType<T>(AlgorithmType algorithmType)
        {
            var handler = UnsafeLeoHandleSwitcher.Switch<T>(algorithmType)().With<T>();
            return new StaticTypeLeoVisitor<T>(handler, algorithmType);
        }
    }
}