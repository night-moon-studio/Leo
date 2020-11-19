using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using NMS.Leo.Typed.Core.Members;

namespace NMS.Leo.Typed.Core
{
    internal class FluentGetterBuilder : IFluentGetter, IFluentValueGetter
    {
        private readonly Type _type;
        private readonly AlgorithmKind _kind;

        public FluentGetterBuilder(Type type, AlgorithmKind kind)
        {
            _type = type;
            _kind = kind;
        }

        #region Fluent building methods for Instance Getter

        ILeoGetter IFluentGetter.Instance(object instance)
        {
            if (_type.IsAbstract && _type.IsSealed)
                return LeoVisitorFactoryCore.CreateForStaticType(_type, _kind, VisitorMode.LITE);
            return LeoVisitorFactoryCore.CreateForInstance(_type, instance, _kind, VisitorMode.NON_REPEATABLE, VisitorMode.LITE);
        }

        ILeoGetter IFluentGetter.InitialValues(IDictionary<string, object> initialValues)
        {
            if (_type.IsAbstract && _type.IsSealed)
            {
                var visitor = LeoVisitorFactoryCore.CreateForStaticType(_type, _kind, VisitorMode.LITE);
                visitor.SetValue(initialValues);
                return visitor;
            }

            return LeoVisitorFactoryCore.CreateForFutureInstance(_type, _kind, VisitorMode.NON_REPEATABLE, VisitorMode.LITE, initialValues);
        }

        #endregion

        #region Fluent building methods for Value Getter

        private Func<object, ValueGetter> _func1;

        IFluentValueGetter IFluentGetter.Value(PropertyInfo propertyInfo)
        {
            if (propertyInfo is null)
                throw new ArgumentNullException(nameof(propertyInfo));

            _func1 = t =>
            {
                var visitor = LeoVisitorFactoryCore.CreateForInstance(_type, t, _kind, VisitorMode.NON_REPEATABLE, VisitorMode.LITE);
                return new ValueGetter(visitor, propertyInfo.Name);
            };

            return this;
        }

        IFluentValueGetter IFluentGetter.Value(FieldInfo fieldInfo)
        {
            if (fieldInfo is null)
                throw new ArgumentNullException(nameof(fieldInfo));

            _func1 = t =>
            {
                var visitor = LeoVisitorFactoryCore.CreateForInstance(_type, t, _kind, VisitorMode.NON_REPEATABLE, VisitorMode.LITE);
                return new ValueGetter(visitor, fieldInfo.Name);
            };

            return this;
        }

        IFluentValueGetter IFluentGetter.Value(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));

            _func1 = t =>
            {
                var visitor = LeoVisitorFactoryCore.CreateForInstance(_type, t, _kind, VisitorMode.NON_REPEATABLE, VisitorMode.LITE);
                return new ValueGetter(visitor, name);
            };

            return this;
        }

        ILeoValueGetter IFluentValueGetter.Instance(object instance)
        {
            return _func1.Invoke(instance);
        }

        #endregion
    }

    internal class FluentGetterBuilder<T> : IFluentGetter<T>, IFluentValueGetter<T>
    {
        private readonly Type _type;
        private readonly AlgorithmKind _kind;

        public FluentGetterBuilder(AlgorithmKind kind)
        {
            _type = typeof(T);
            _kind = kind;
        }

        #region Fluent building methods for Instance Getter

        ILeoGetter<T> IFluentGetter<T>.Instance(T instance)
        {
            if (_type.IsAbstract && _type.IsSealed)
                return LeoVisitorFactoryCore.CreateForStaticType<T>(_kind, VisitorMode.LITE);
            return LeoVisitorFactoryCore.CreateForInstance<T>(instance, _kind, VisitorMode.NON_REPEATABLE, VisitorMode.LITE);
        }

        ILeoGetter<T> IFluentGetter<T>.InitialValues(IDictionary<string, object> initialValues)
        {
            if (_type.IsAbstract && _type.IsSealed)
            {
                var visitor = LeoVisitorFactoryCore.CreateForStaticType<T>(_kind, VisitorMode.LITE);
                visitor.SetValue(initialValues);
                return visitor;
            }

            return LeoVisitorFactoryCore.CreateForFutureInstance<T>(_kind, VisitorMode.NON_REPEATABLE, VisitorMode.LITE, initialValues);
        }

        #endregion

        #region Fluent building methods for Value Getter

        private Func<T, ValueGetter<T>> _func1;

        IFluentValueGetter<T> IFluentGetter<T>.Value(PropertyInfo propertyInfo)
        {
            if (propertyInfo is null)
                throw new ArgumentNullException(nameof(propertyInfo));

            _func1 = t =>
            {
                var visitor = LeoVisitorFactoryCore.CreateForInstance<T>(t, _kind, VisitorMode.NON_REPEATABLE, VisitorMode.LITE);
                return new ValueGetter<T>(visitor, propertyInfo.Name);
            };

            return this;
        }

        IFluentValueGetter<T> IFluentGetter<T>.Value(FieldInfo fieldInfo)
        {
            if (fieldInfo is null)
                throw new ArgumentNullException(nameof(fieldInfo));

            _func1 = t =>
            {
                var visitor = LeoVisitorFactoryCore.CreateForInstance<T>(t, _kind, VisitorMode.NON_REPEATABLE, VisitorMode.LITE);
                return new ValueGetter<T>(visitor, fieldInfo.Name);
            };

            return this;
        }

        IFluentValueGetter<T> IFluentGetter<T>.Value(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));

            _func1 = t =>
            {
                var visitor = LeoVisitorFactoryCore.CreateForInstance<T>(t, _kind, VisitorMode.NON_REPEATABLE, VisitorMode.LITE);
                return new ValueGetter<T>(visitor, name);
            };

            return this;
        }

        IFluentValueGetter<T> IFluentGetter<T>.Value(Expression<Func<T, object>> expression)
        {
            if (expression is null)
                throw new ArgumentNullException(nameof(expression));

            _func1 = t =>
            {
                var visitor = LeoVisitorFactoryCore.CreateForInstance<T>(t, _kind, VisitorMode.NON_REPEATABLE, VisitorMode.LITE);
                return new ValueGetter<T>(visitor, expression);
            };

            return this;
        }

        IFluentValueGetter<T, TVal> IFluentGetter<T>.Value<TVal>(Expression<Func<T, TVal>> expression)
        {
            if (expression is null)
                throw new ArgumentNullException(nameof(expression));

            Func<T, ValueGetter<T, TVal>> func = t =>
            {
                var visitor = LeoVisitorFactoryCore.CreateForInstance<T>(t, _kind, VisitorMode.NON_REPEATABLE, VisitorMode.LITE);
                return new ValueGetter<T, TVal>(visitor, expression);
            };

            return new FluentGetterBuilder<T, TVal>(func);
        }

        ILeoValueGetter<T> IFluentValueGetter<T>.Instance(T instance)
        {
            return _func1.Invoke(instance);
        }

        #endregion
    }

    internal class FluentGetterBuilder<T, TVal> : IFluentValueGetter<T, TVal>
    {
        public FluentGetterBuilder(Func<T, ValueGetter<T, TVal>> func)
        {
            _func1 = func;
        }

        #region Fluent building methods for Value Getter

        private Func<T, ValueGetter<T, TVal>> _func1;

        ILeoValueGetter<T, TVal> IFluentValueGetter<T, TVal>.Instance(T instance)
        {
            return _func1.Invoke(instance);
        }

        #endregion
    }
}