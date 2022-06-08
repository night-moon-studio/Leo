using NMS.Leo.Typed.Validation;

namespace NMS.Leo.Typed;

public static class LeoVisitorFactory
{
    public static ILeoVisitor Create(Type type, object instance, AlgorithmKind kind = AlgorithmKind.Precision, bool repeatable = RpMode.REPEATABLE, bool strictMode = StMode.NORMALE)
    {
        return LeoVisitorFactoryCore.CreateForInstance(type, instance, kind, repeatable, LvMode.FULL, strictMode);
    }

    public static ILeoVisitor Create(Type type, AlgorithmKind kind = AlgorithmKind.Precision, bool repeatable = RpMode.REPEATABLE, bool strictMode = StMode.NORMALE)
    {
        if (type.IsAbstract && type.IsSealed)
            return LeoVisitorFactoryCore.CreateForStaticType(type, kind, LvMode.FULL, strictMode);
        return LeoVisitorFactoryCore.CreateForFutureInstance(type, kind, repeatable, LvMode.FULL, strictMode);
    }

    public static ILeoVisitor Create(Type type, IDictionary<string, object> initialValues, AlgorithmKind kind = AlgorithmKind.Precision, bool repeatable = RpMode.REPEATABLE, bool strictMode = StMode.NORMALE)
    {
        if (type.IsAbstract && type.IsSealed)
            return LeoVisitorFactoryCore.CreateForStaticType(type, kind, LvMode.FULL, strictMode);
        return LeoVisitorFactoryCore.CreateForFutureInstance(type, kind, repeatable, LvMode.FULL, strictMode, initialValues);
    }

    public static ILeoVisitor<T> Create<T>(T instance, AlgorithmKind kind = AlgorithmKind.Precision, bool repeatable = RpMode.REPEATABLE, bool strictMode = StMode.NORMALE)
    {
        return LeoVisitorFactoryCore.CreateForInstance<T>(instance, kind, repeatable, LvMode.FULL, strictMode);
    }

    public static ILeoVisitor<T> Create<T>(AlgorithmKind kind = AlgorithmKind.Precision, bool repeatable = RpMode.REPEATABLE, bool strictMode = StMode.NORMALE)
    {
        var type = typeof(T);
        if (type.IsAbstract && type.IsSealed)
            return LeoVisitorFactoryCore.CreateForStaticType<T>(kind, LvMode.FULL, strictMode);
        return LeoVisitorFactoryCore.CreateForFutureInstance<T>(kind, repeatable, LvMode.FULL, strictMode);
    }

    public static ILeoVisitor<T> Create<T>(IDictionary<string, object> initialValues, AlgorithmKind kind = AlgorithmKind.Precision, bool repeatable = RpMode.REPEATABLE, bool strictMode = StMode.NORMALE)
    {
        var type = typeof(T);
        if (type.IsAbstract && type.IsSealed)
            return LeoVisitorFactoryCore.CreateForStaticType<T>(kind, LvMode.FULL, strictMode);
        return LeoVisitorFactoryCore.CreateForFutureInstance<T>(kind, repeatable, LvMode.FULL, strictMode, initialValues);
    }

    public static ILeoVisitor Create<TStrategy>(Type type, object instance, TStrategy validationStrategy, AlgorithmKind kind = AlgorithmKind.Precision, bool repeatable = RpMode.REPEATABLE, bool strictMode = StMode.NORMALE)
        where TStrategy : class, ILeoValidationStrategy, new()
    {
        return LeoVisitorFactoryCore.CreateForInstance(type, instance, kind, repeatable, LvMode.FULL, validationStrategy, strictMode);
    }

    public static ILeoVisitor Create<TStrategy>(Type type, TStrategy validationStrategy, AlgorithmKind kind = AlgorithmKind.Precision, bool repeatable = RpMode.REPEATABLE, bool strictMode = StMode.NORMALE)
        where TStrategy : class, ILeoValidationStrategy, new()
    {
        if (type.IsAbstract && type.IsSealed)
            return LeoVisitorFactoryCore.CreateForStaticType(type, kind, LvMode.FULL, validationStrategy, strictMode);
        return LeoVisitorFactoryCore.CreateForFutureInstance(type, kind, repeatable, LvMode.FULL, validationStrategy, strictMode);
    }

    public static ILeoVisitor Create<TStrategy>(Type type, IDictionary<string, object> initialValues, TStrategy validationStrategy, AlgorithmKind kind = AlgorithmKind.Precision, bool repeatable = RpMode.REPEATABLE,
        bool strictMode = StMode.NORMALE)
        where TStrategy : class, ILeoValidationStrategy, new()
    {
        if (type.IsAbstract && type.IsSealed)
            return LeoVisitorFactoryCore.CreateForStaticType(type, kind, LvMode.FULL, validationStrategy, strictMode);
        return LeoVisitorFactoryCore.CreateForFutureInstance(type, kind, repeatable, LvMode.FULL, validationStrategy, strictMode, initialValues);
    }

    public static ILeoVisitor<T> Create<T, TStrategy>(T instance, AlgorithmKind kind = AlgorithmKind.Precision, bool repeatable = RpMode.REPEATABLE, bool strictMode = StMode.NORMALE)
        where TStrategy : class, ILeoValidationStrategy<T>, new()
    {
        return LeoVisitorFactoryCore.CreateForInstance<T, TStrategy>(instance, kind, repeatable, LvMode.FULL, strictMode);
    }

    public static ILeoVisitor<T> Create<T, TStrategy>(AlgorithmKind kind = AlgorithmKind.Precision, bool repeatable = RpMode.REPEATABLE, bool strictMode = StMode.NORMALE)
        where TStrategy : class, ILeoValidationStrategy<T>, new()
    {
        var type = typeof(T);
        if (type.IsAbstract && type.IsSealed)
            return LeoVisitorFactoryCore.CreateForStaticType<T, TStrategy>(kind, LvMode.FULL, strictMode);
        return LeoVisitorFactoryCore.CreateForFutureInstance<T, TStrategy>(kind, repeatable, LvMode.FULL, strictMode);
    }

    public static ILeoVisitor<T> Create<T, TStrategy>(IDictionary<string, object> initialValues, AlgorithmKind kind = AlgorithmKind.Precision, bool repeatable = RpMode.REPEATABLE, bool strictMode = StMode.NORMALE)
        where TStrategy : class, ILeoValidationStrategy<T>, new()
    {
        var type = typeof(T);
        if (type.IsAbstract && type.IsSealed)
            return LeoVisitorFactoryCore.CreateForStaticType<T, TStrategy>(kind, LvMode.FULL, strictMode);
        return LeoVisitorFactoryCore.CreateForFutureInstance<T, TStrategy>(kind, repeatable, LvMode.FULL, strictMode, initialValues);
    }
}