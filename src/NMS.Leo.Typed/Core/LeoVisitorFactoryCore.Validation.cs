using NMS.Leo.Typed.Core.Extensions;
using NMS.Leo.Typed.Validation;

namespace NMS.Leo.Typed.Core;

internal static partial class LeoVisitorFactoryCore
{
    public static InstanceVisitor CreateForInstance<TStrategy>(Type type, object instance, AlgorithmKind kind, bool repeatable, bool liteMode, TStrategy strategy, bool strictMode)
        where TStrategy : class, ILeoValidationStrategy, new()
    {
        var handler = SafeLeoHandleSwitcher.Switch(kind)(type);
        var visitor = new InstanceVisitor(handler, type, instance, kind, repeatable, liteMode, strictMode);
        visitor.ValidationEntry.SetStrategy(strategy);
        return visitor;
    }

    public static InstanceVisitor<T> CreateForInstance<T, TStrategy>(T instance, AlgorithmKind kind, bool repeatable, bool liteMode, bool strictMode)
        where TStrategy : class, ILeoValidationStrategy<T>, new()
    {
        var handler = UnsafeLeoHandleSwitcher.Switch<T>(kind)().With<T>();
        var visitor = new InstanceVisitor<T>(handler, instance, kind, repeatable, liteMode, strictMode);
        visitor.ValidationEntry.SetStrategy<TStrategy>();
        return visitor;
    }

    public static FutureInstanceVisitor CreateForFutureInstance<TStrategy>(Type type, AlgorithmKind kind, bool repeatable, bool liteMode, TStrategy strategy, bool strictMode, IDictionary<string, object> initialValues = null)
        where TStrategy : class, ILeoValidationStrategy, new()
    {
        var handler = SafeLeoHandleSwitcher.Switch(kind)(type);
        var visitor = new FutureInstanceVisitor(handler, type, kind, repeatable, initialValues, liteMode, strictMode);
        visitor.ValidationEntry.SetStrategy(strategy);
        return visitor;
    }

    public static FutureInstanceVisitor<T> CreateForFutureInstance<T, TStrategy>(AlgorithmKind kind, bool repeatable, bool liteMode, bool strictMode, IDictionary<string, object> initialValues = null)
        where TStrategy : class, ILeoValidationStrategy<T>, new()
    {
        var handler = UnsafeLeoHandleSwitcher.Switch<T>(kind)().With<T>();
        var visitor = new FutureInstanceVisitor<T>(handler, kind, repeatable, initialValues, liteMode, strictMode);
        visitor.ValidationEntry.SetStrategy<TStrategy>();
        return visitor;
    }

    public static StaticTypeLeoVisitor CreateForStaticType<TStrategy>(Type type, AlgorithmKind kind, bool liteMode, TStrategy strategy, bool strictMode)
        where TStrategy : class, ILeoValidationStrategy, new()
    {
        var handler = SafeLeoHandleSwitcher.Switch(kind)(type);
        var visitor = new StaticTypeLeoVisitor(handler, type, kind, liteMode, strictMode);
        visitor.ValidationEntry.SetStrategy(strategy);
        return visitor;
    }

    public static StaticTypeLeoVisitor<T> CreateForStaticType<T, TStrategy>(AlgorithmKind kind, bool liteMode, bool strictMode)
        where TStrategy : class, ILeoValidationStrategy<T>, new()
    {
        var handler = UnsafeLeoHandleSwitcher.Switch<T>(kind)().With<T>();
        var visitor = new StaticTypeLeoVisitor<T>(handler, kind, liteMode, strictMode);
        visitor.ValidationEntry.SetStrategy<TStrategy>();
        return visitor;
    }
}