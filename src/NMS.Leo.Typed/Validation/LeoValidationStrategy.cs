using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using NMS.Leo.Typed.Core.Correct;
using NMS.Leo.Typed.Core.Members;

namespace NMS.Leo.Typed.Validation
{
    public abstract class LeoValidationStrategy : ILeoValidationStrategy, ICorrectStrategy
    {
        private readonly List<CorrectValueRuleBuilder> _memberValueRuleBuilders;
        private readonly object _builderLockObj = new object();
        private readonly MemberHandler _handler;

        protected LeoValidationStrategy(Type type)
        {
            SourceType = type ?? throw new ArgumentNullException(nameof(type));

            _memberValueRuleBuilders = new List<CorrectValueRuleBuilder>();
            _handler = new MemberHandler(PrecisionDictOperator.CreateFromType(SourceType), SourceType);
        }

        public Type SourceType { get; }

        IEnumerable<CorrectValueRuleBuilder> ICorrectStrategy.GetValueRuleBuilders()
        {
            return _memberValueRuleBuilders;
        }

        protected ILeoValueRuleBuilder RuleFor(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));

            lock (_builderLockObj)
            {
                var builder = _memberValueRuleBuilders.FirstOrDefault(b => b.Name == name);
                if (builder is null)
                {
                    builder = new CorrectValueRuleBuilder(_handler.GetMember(name));
                    _memberValueRuleBuilders.Add(builder);
                }

                return builder;
            }
        }

        protected ILeoValueRuleBuilder RuleFor(PropertyInfo propertyInfo)
        {
            if (propertyInfo is null)
                throw new ArgumentNullException(nameof(propertyInfo));
            return RuleFor(propertyInfo.Name);
        }

        protected ILeoValueRuleBuilder RuleFor(FieldInfo fieldInfo)
        {
            if (fieldInfo is null)
                throw new ArgumentNullException(nameof(fieldInfo));
            return RuleFor(fieldInfo.Name);
        }
    }

    public abstract class LeoValidationStrategy<T> : ILeoValidationStrategy<T>, ICorrectStrategy<T>
    {
        private readonly List<CorrectValueRuleBuilder<T>> _memberValueRuleBuilders;
        private readonly object _builderLockObj = new object();
        private readonly MemberHandler _handler;

        protected LeoValidationStrategy()
        {
            SourceType = typeof(T);

            _memberValueRuleBuilders = new List<CorrectValueRuleBuilder<T>>();
            _handler = new MemberHandler(PrecisionDictOperator.CreateFromType(SourceType), SourceType);
        }

        public Type SourceType { get; }

        IEnumerable<CorrectValueRuleBuilder<T>> ICorrectStrategy<T>.GetValueRuleBuilders()
        {
            return _memberValueRuleBuilders;
        }

        protected ILeoValueRuleBuilder<T> RuleFor(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));

            lock (_builderLockObj)
            {
                var builder = _memberValueRuleBuilders.FirstOrDefault(b => b.Name == name);
                if (builder is null)
                {
                    builder = new CorrectValueRuleBuilder<T>(_handler.GetMember(name));
                    _memberValueRuleBuilders.Add(builder);
                }

                return builder;
            }
        }

        protected ILeoValueRuleBuilder<T> RuleFor(PropertyInfo propertyInfo)
        {
            if (propertyInfo is null)
                throw new ArgumentNullException(nameof(propertyInfo));
            return RuleFor(propertyInfo.Name);
        }

        protected ILeoValueRuleBuilder<T> RuleFor(FieldInfo fieldInfo)
        {
            if (fieldInfo is null)
                throw new ArgumentNullException(nameof(fieldInfo));
            return RuleFor(fieldInfo.Name);
        }

        protected ILeoValueRuleBuilder<T> RuleFor<TVal>(Expression<Func<T, TVal>> expression)
        {
            if (expression is null)
                throw new ArgumentNullException(nameof(expression));

            lock (_builderLockObj)
            {
                var name = PropertySelector.GetPropertyName(expression);
                var builder = _memberValueRuleBuilders.FirstOrDefault(b => b.Name == name);
                if (builder is null)
                {
                    builder = new CorrectValueRuleBuilder<T, TVal>(_handler.GetMember(name));
                    _memberValueRuleBuilders.Add(builder);
                }

                return builder;
            }
        }
    }
}