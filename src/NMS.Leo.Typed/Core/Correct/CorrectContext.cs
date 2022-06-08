using NMS.Leo.Typed.Core.Extensions;
using NMS.Leo.Typed.Core.Members;
using NMS.Leo.Typed.Validation;

namespace NMS.Leo.Typed.Core.Correct;

internal class CorrectContext : ILeoValidationContext
{
    private readonly ICoreVisitor _visitor;
    protected readonly List<CorrectValueRule> _correctValueRules;
    protected readonly object _valueRuleLockObj = new object();

    protected CorrectContext(bool strictMode)
    {
        _visitor = null;
        _correctValueRules = new List<CorrectValueRule>();
        StrictMode = strictMode;
    }

    public CorrectContext(ICoreVisitor visitor, bool strictMode)
    {
        _visitor = visitor ?? throw new ArgumentNullException(nameof(visitor));
        _correctValueRules = new List<CorrectValueRule>();
        StrictMode = strictMode;
    }

    public bool StrictMode { get; set; }

    public ILeoValidationContext SetStrategy<TStrategy>(StrategyMode mode = StrategyMode.OverallOverwrite)
        where TStrategy : class, ILeoValidationStrategy, new()
    {
        var rel = (ICorrectStrategy) new TStrategy();
        AddOrUpdateValueRules(rel.GetValueRuleBuilders().Select(builder => builder.Build()), mode);
        return this;
    }

    public ILeoValidationContext SetStrategy<TStrategy>(TStrategy strategy, StrategyMode mode = StrategyMode.OverallOverwrite)
        where TStrategy : class, ILeoValidationStrategy, new()
    {
        if (strategy is null) throw new ArgumentNullException(nameof(strategy));
        var rel = (ICorrectStrategy) strategy;
        AddOrUpdateValueRules(rel.GetValueRuleBuilders().Select(builder => builder.Build()), mode);
        return this;
    }

    public ILeoValidationContext ForMember(string name, Func<ILeoValueRuleBuilder, ILeoValueRuleBuilder> func)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentNullException(nameof(name));
        var handler = _visitor.ExposeLazyMemberHandler().Value;
        var builder = new CorrectValueRuleBuilder(handler.GetMember(name), () => handler.GetInstanceObject(), () => handler.GetValueObject(name));
        var rule = ((CorrectValueRuleBuilder) func(builder)).Build();
        AddOrUpdateValueRule(rule);
        return this;
    }

    public ILeoValidationContext ForMember(PropertyInfo propertyInfo, Func<ILeoValueRuleBuilder, ILeoValueRuleBuilder> func)
    {
        if (propertyInfo is null)
            throw new ArgumentNullException(nameof(propertyInfo));
        return ForMember(propertyInfo.Name, func);
    }

    public ILeoValidationContext ForMember(FieldInfo fieldInfo, Func<ILeoValueRuleBuilder, ILeoValueRuleBuilder> func)
    {
        if (fieldInfo is null)
            throw new ArgumentNullException(nameof(fieldInfo));
        return ForMember(fieldInfo.Name, func);
    }

    protected void AddOrUpdateValueRules(IEnumerable<CorrectValueRule> rules, StrategyMode mode)
    {
        lock (_valueRuleLockObj)
        {
            if (mode == StrategyMode.OverallOverwrite)
                _correctValueRules.Clear();

            foreach (var rule in rules)
            {
                var target = _correctValueRules.FirstOrDefault(r => r.Name == rule.Name);

                if (target is null)
                {
                    // If the value of StrategyMode is OverallOverwrite,
                    // this branch must be entered.
                    _correctValueRules.Add(rule);
                }

                else if (mode == StrategyMode.ItemOverwrite)
                {
                    _correctValueRules.Remove(target);
                    _correctValueRules.Add(rule);
                }

                else if (mode == StrategyMode.Append)
                {
                    target.Merge(rule);
                }
            }
        }
    }

    protected void AddOrUpdateValueRule(CorrectValueRule rule)
    {
        lock (_valueRuleLockObj)
        {
            var target = _correctValueRules.FirstOrDefault(r => r.Name == rule.Name);

            if (target is null)
            {
                // Regardless of the value of ValueRuleMode, as long as the rule does not exist,
                // this branch is always entered.
                _correctValueRules.Add(rule);
            }

            else if (rule.Mode == CorrectValueRuleMode.Overwrite)
            {
                _correctValueRules.Remove(target);
                _correctValueRules.Add(rule);
            }

            else if (rule.Mode == CorrectValueRuleMode.Append)
            {
                target.Merge(rule);
            }
        }
    }

    // ReSharper disable once InconsistentlySynchronizedField
    public IReadOnlyList<CorrectValueRule> GetCorrectValueRules() => _correctValueRules;

    public LeoVerifyResult ValidValue()
    {
        // ReSharper disable once InconsistentlySynchronizedField
        return InternalValidator.Valid(_visitor.ExposeLazyMemberHandler().Value, _correctValueRules);
    }

    public new LeoVerifyResult ValidOne(string name)
    {
        // ReSharper disable once InconsistentlySynchronizedField
        return InternalValidator.ValidOne(_visitor.ExposeLazyMemberHandler().Value, _correctValueRules, name);
    }

    public LeoVerifyResult ValidOne(string name, object value)
    {
        // ReSharper disable once InconsistentlySynchronizedField
        return InternalValidator.ValidOne(name, value, _correctValueRules);
    }

    public LeoVerifyResult ValidMany(IDictionary<string, object> keyValueCollections)
    {
        // ReSharper disable once InconsistentlySynchronizedField
        return InternalValidator.ValidMany(keyValueCollections, _correctValueRules);
    }
}

internal class CorrectContext<T> : CorrectContext, ILeoValidationContext<T>
{
    private readonly ICoreVisitor<T> _visitor;

    public CorrectContext(ICoreVisitor<T> visitor, bool strictMode) : base(strictMode)
    {
        _visitor = visitor ?? throw new ArgumentNullException(nameof(visitor));
    }

    public new ILeoValidationContext<T> SetStrategy<TStrategy>(StrategyMode mode = StrategyMode.OverallOverwrite)
        where TStrategy : class, ILeoValidationStrategy<T>, new()
    {
        var rel = (ICorrectStrategy<T>) new TStrategy();
        AddOrUpdateValueRules(rel.GetValueRuleBuilders().Select(builder => builder.Build()), mode);
        return this;
    }

    public new ILeoValidationContext<T> SetStrategy<TStrategy>(TStrategy strategy, StrategyMode mode = StrategyMode.OverallOverwrite)
        where TStrategy : class, ILeoValidationStrategy<T>, new()
    {
        if (strategy is null) throw new ArgumentNullException(nameof(strategy));
        var rel = (ICorrectStrategy<T>) strategy;
        AddOrUpdateValueRules(rel.GetValueRuleBuilders().Select(builder => builder.Build()), mode);
        return this;
    }

    public ILeoValidationContext<T> ForMember(string name, Func<ILeoValueRuleBuilder<T>, ILeoValueRuleBuilder<T>> func)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentNullException(nameof(name));
        var handler = _visitor.ExposeLazyMemberHandler().Value;
        var builder = new CorrectValueRuleBuilder<T>(handler.GetMember(name), () => handler.GetInstanceObject().As<T>(), () => handler.GetValueObject(name));
        var rule = ((CorrectValueRuleBuilder<T>) func(builder)).Build();
        AddOrUpdateValueRule(rule);
        return this;
    }

    public ILeoValidationContext<T> ForMember(PropertyInfo propertyInfo, Func<ILeoValueRuleBuilder<T>, ILeoValueRuleBuilder<T>> func)
    {
        if (propertyInfo is null)
            throw new ArgumentNullException(nameof(propertyInfo));
        return ForMember(propertyInfo.Name, func);
    }

    public ILeoValidationContext<T> ForMember(FieldInfo fieldInfo, Func<ILeoValueRuleBuilder<T>, ILeoValueRuleBuilder<T>> func)
    {
        if (fieldInfo is null)
            throw new ArgumentNullException(nameof(fieldInfo));
        return ForMember(fieldInfo.Name, func);
    }

    public ILeoValidationContext<T> ForMember<TVal>(Expression<Func<T, TVal>> expression, Func<ILeoValueRuleBuilder<T, TVal>, ILeoValueRuleBuilder<T, TVal>> func)
    {
        if (expression is null)
            throw new ArgumentNullException(nameof(expression));
        var name = PropertySelector.GetPropertyName(expression);
        var handler = _visitor.ExposeLazyMemberHandler().Value;
        var builder = new CorrectValueRuleBuilder<T, TVal>(handler.GetMember(name), () => handler.GetInstanceObject().As<T>(), () => handler.GetValue<TVal>(name));
        var rule = ((CorrectValueRuleBuilder<T, TVal>) func(builder)).Build();
        AddOrUpdateValueRule(rule);
        return this;
    }

    ILeoValidationContext ILeoValidationContext.ForMember(string name, Func<ILeoValueRuleBuilder, ILeoValueRuleBuilder> func)
    {
        return ((ILeoValidationContext) this).ForMember(name, func);
    }

    ILeoValidationContext ILeoValidationContext.ForMember(PropertyInfo propertyInfo, Func<ILeoValueRuleBuilder, ILeoValueRuleBuilder> func)
    {
        return ((ILeoValidationContext) this).ForMember(propertyInfo, func);
    }

    ILeoValidationContext ILeoValidationContext.ForMember(FieldInfo fieldInfo, Func<ILeoValueRuleBuilder, ILeoValueRuleBuilder> func)
    {
        return ((ILeoValidationContext) this).ForMember(fieldInfo, func);
    }

    public new LeoVerifyResult ValidValue()
    {
        // ReSharper disable once InconsistentlySynchronizedField
        return InternalValidator.Valid(_visitor.ExposeLazyMemberHandler().Value, _correctValueRules);
    }

    public new LeoVerifyResult ValidMember(string name)
    {
        // ReSharper disable once InconsistentlySynchronizedField
        return InternalValidator.ValidOne(_visitor.ExposeLazyMemberHandler().Value, _correctValueRules, name);
    }

    public new LeoVerifyResult ValidMember(string name, object value)
    {
        // ReSharper disable once InconsistentlySynchronizedField
        return InternalValidator.ValidOne(name, value, _correctValueRules);
    }
}