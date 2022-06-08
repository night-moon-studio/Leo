namespace NMS.Leo.Typed.Validation;

public interface ILeoValidationContext
{
    bool StrictMode { get; set; }

    ILeoValidationContext SetStrategy<TStrategy>(StrategyMode mode = StrategyMode.OverallOverwrite)
        where TStrategy : class, ILeoValidationStrategy, new();

    ILeoValidationContext SetStrategy<TStrategy>(TStrategy strategy, StrategyMode mode = StrategyMode.OverallOverwrite)
        where TStrategy : class, ILeoValidationStrategy, new();

    ILeoValidationContext ForMember(string name, Func<ILeoValueRuleBuilder, ILeoValueRuleBuilder> func);

    ILeoValidationContext ForMember(PropertyInfo propertyInfo, Func<ILeoValueRuleBuilder, ILeoValueRuleBuilder> func);

    ILeoValidationContext ForMember(FieldInfo fieldInfo, Func<ILeoValueRuleBuilder, ILeoValueRuleBuilder> func);
}

public interface ILeoValidationContext<T> : ILeoValidationContext
{
    new ILeoValidationContext<T> SetStrategy<TStrategy>(StrategyMode mode = StrategyMode.OverallOverwrite)
        where TStrategy : class, ILeoValidationStrategy<T>, new();

    new ILeoValidationContext<T> SetStrategy<TStrategy>(TStrategy strategy, StrategyMode mode = StrategyMode.OverallOverwrite)
        where TStrategy : class, ILeoValidationStrategy<T>, new();

    ILeoValidationContext<T> ForMember(string name, Func<ILeoValueRuleBuilder<T>, ILeoValueRuleBuilder<T>> func);

    ILeoValidationContext<T> ForMember(PropertyInfo propertyInfo, Func<ILeoValueRuleBuilder<T>, ILeoValueRuleBuilder<T>> func);

    ILeoValidationContext<T> ForMember(FieldInfo fieldInfo, Func<ILeoValueRuleBuilder<T>, ILeoValueRuleBuilder<T>> func);

    ILeoValidationContext<T> ForMember<TVal>(Expression<Func<T, TVal>> expression, Func<ILeoValueRuleBuilder<T, TVal>, ILeoValueRuleBuilder<T, TVal>> func);
}