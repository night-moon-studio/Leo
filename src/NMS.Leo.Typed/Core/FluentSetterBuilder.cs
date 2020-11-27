using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using NMS.Leo.Typed.Core.Members;

namespace NMS.Leo.Typed.Core
{
    internal class FluentSetterBuilder : IFluentSetter, IFluentValueSetter
    {
        private readonly Type _type;
        private readonly AlgorithmKind _kind;

        public FluentSetterBuilder(Type type, AlgorithmKind kind)
        {
            _type = type;
            _kind = kind;
        }

        #region Fluent building methods for Instance Setter

        ILeoSetter IFluentSetter.NewInstance(bool strictMode)
        {
            if (_type.IsAbstract && _type.IsSealed)
                return LeoVisitorFactoryCore.CreateForStaticType(_type, _kind, LvMode.LITE, strictMode);
            return LeoVisitorFactoryCore.CreateForFutureInstance(_type, _kind, RpMode.NON_REPEATABLE, LvMode.LITE, strictMode);
        }

        ILeoSetter IFluentSetter.Instance(object instance, bool strictMode)
        {
            if (_type.IsAbstract && _type.IsSealed)
                return LeoVisitorFactoryCore.CreateForStaticType(_type, _kind, LvMode.LITE, strictMode);
            return LeoVisitorFactoryCore.CreateForInstance(_type, instance, _kind, RpMode.NON_REPEATABLE, LvMode.LITE, strictMode);
        }

        ILeoSetter IFluentSetter.InitialValues(IDictionary<string, object> initialValues, bool strictMode)
        {
            if (_type.IsAbstract && _type.IsSealed)
            {
                var visitor = LeoVisitorFactoryCore.CreateForStaticType(_type, _kind, LvMode.LITE, strictMode);
                visitor.SetValue(initialValues);
                return visitor;
            }

            return LeoVisitorFactoryCore.CreateForFutureInstance(_type, _kind, RpMode.NON_REPEATABLE, LvMode.LITE, strictMode, initialValues);
        }

        #endregion

        #region Fluent building methods for Value Setter

        private Func<object, bool, ValueSetter> _func1;

        IFluentValueSetter IFluentSetter.Value(PropertyInfo propertyInfo)
        {
            if (propertyInfo is null)
                throw new ArgumentNullException(nameof(propertyInfo));

            _func1 = (t, strictMode) =>
            {
                var visitor = LeoVisitorFactoryCore.CreateForInstance(_type, t, _kind, RpMode.NON_REPEATABLE, LvMode.LITE, strictMode);
                return new ValueSetter(visitor, propertyInfo.Name);
            };

            return this;
        }

        IFluentValueSetter IFluentSetter.Value(FieldInfo fieldInfo)
        {
            if (fieldInfo is null)
                throw new ArgumentNullException(nameof(fieldInfo));

            _func1 = (t, strictMode) =>
            {
                var visitor = LeoVisitorFactoryCore.CreateForInstance(_type, t, _kind, RpMode.NON_REPEATABLE, LvMode.LITE, strictMode);
                return new ValueSetter(visitor, fieldInfo.Name);
            };

            return this;
        }

        IFluentValueSetter IFluentSetter.Value(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));

            _func1 = (t, strictMode) =>
            {
                var visitor = LeoVisitorFactoryCore.CreateForInstance(_type, t, _kind, RpMode.NON_REPEATABLE, LvMode.LITE, strictMode);
                return new ValueSetter(visitor, name);
            };

            return this;
        }


        ILeoValueSetter IFluentValueSetter.Instance(object instance, bool strictMode)
        {
            return _func1.Invoke(instance, strictMode);
        }

        #endregion
    }

    internal class FluentSetterBuilder<T> : IFluentSetter<T>, IFluentValueSetter<T>
    {
        private readonly Type _type;
        private readonly AlgorithmKind _kind;

        public FluentSetterBuilder(AlgorithmKind kind)
        {
            _type = typeof(T);
            _kind = kind;
        }

        #region Fluent building methods for Instance Setter

        ILeoSetter<T> IFluentSetter<T>.NewInstance(bool strictMode)
        {
            if (_type.IsAbstract && _type.IsSealed)
                return LeoVisitorFactoryCore.CreateForStaticType<T>(_kind, LvMode.LITE, strictMode);
            return LeoVisitorFactoryCore.CreateForFutureInstance<T>(_kind, RpMode.NON_REPEATABLE, LvMode.LITE, strictMode);
        }

        ILeoSetter<T> IFluentSetter<T>.Instance(T instance, bool strictMode)
        {
            if (_type.IsAbstract && _type.IsSealed)
                return LeoVisitorFactoryCore.CreateForStaticType<T>(_kind, LvMode.LITE, strictMode);
            return LeoVisitorFactoryCore.CreateForInstance<T>(instance, _kind, RpMode.NON_REPEATABLE, LvMode.LITE, strictMode);
        }

        ILeoSetter<T> IFluentSetter<T>.InitialValues(IDictionary<string, object> initialValues, bool strictMode)
        {
            if (_type.IsAbstract && _type.IsSealed)
            {
                var visitor = LeoVisitorFactoryCore.CreateForStaticType<T>(_kind, LvMode.LITE, strictMode);
                visitor.SetValue(initialValues);
                return visitor;
            }

            return LeoVisitorFactoryCore.CreateForFutureInstance<T>(_kind, RpMode.NON_REPEATABLE, LvMode.LITE, strictMode, initialValues);
        }

        #endregion

        #region Fluent building methods for Value Setter

        private Func<T, bool, ValueSetter<T>> _func1;

        IFluentValueSetter<T> IFluentSetter<T>.Value(PropertyInfo propertyInfo)
        {
            if (propertyInfo is null)
                throw new ArgumentNullException(nameof(propertyInfo));

            _func1 = (t, strictMode) =>
            {
                var visitor = LeoVisitorFactoryCore.CreateForInstance<T>(t, _kind, RpMode.NON_REPEATABLE, LvMode.LITE, strictMode);
                return new ValueSetter<T>(visitor, propertyInfo.Name);
            };

            return this;
        }

        IFluentValueSetter<T> IFluentSetter<T>.Value(FieldInfo fieldInfo)
        {
            if (fieldInfo is null)
                throw new ArgumentNullException(nameof(fieldInfo));

            _func1 = (t, strictMode) =>
            {
                var visitor = LeoVisitorFactoryCore.CreateForInstance<T>(t, _kind, RpMode.NON_REPEATABLE, LvMode.LITE, strictMode);
                return new ValueSetter<T>(visitor, fieldInfo.Name);
            };

            return this;
        }

        IFluentValueSetter<T> IFluentSetter<T>.Value(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));

            _func1 = (t, strictMode) =>
            {
                var visitor = LeoVisitorFactoryCore.CreateForInstance<T>(t, _kind, RpMode.NON_REPEATABLE, LvMode.LITE, strictMode);
                return new ValueSetter<T>(visitor, name);
            };

            return this;
        }

        IFluentValueSetter<T> IFluentSetter<T>.Value(Expression<Func<T, object>> expression)
        {
            if (expression is null)
                throw new ArgumentNullException(nameof(expression));

            _func1 = (t, strictMode) =>
            {
                var visitor = LeoVisitorFactoryCore.CreateForInstance<T>(t, _kind, RpMode.NON_REPEATABLE, LvMode.LITE, strictMode);
                return new ValueSetter<T>(visitor, expression);
            };

            return this;
        }

        IFluentValueSetter<T, TVal> IFluentSetter<T>.Value<TVal>(Expression<Func<T, TVal>> expression)
        {
            if (expression is null)
                throw new ArgumentNullException(nameof(expression));

            Func<T, bool, ValueSetter<T, TVal>> func = (t, strictMode) =>
            {
                var visitor = LeoVisitorFactoryCore.CreateForInstance<T>(t, _kind, RpMode.NON_REPEATABLE, LvMode.LITE, strictMode);
                return new ValueSetter<T, TVal>(visitor, expression);
            };

            return new FluentSetterBuilder<T, TVal>(func);
        }

        ILeoValueSetter<T> IFluentValueSetter<T>.Instance(T instance, bool strictMode)
        {
            return _func1.Invoke(instance, strictMode);
        }

        #endregion
    }

    internal class FluentSetterBuilder<T, TVal> : IFluentValueSetter<T, TVal>
    {
        public FluentSetterBuilder(Func<T, bool, ValueSetter<T, TVal>> func)
        {
            _func1 = func;
        }

        #region Fluent building methods for Value Setter

        private Func<T, bool, ValueSetter<T, TVal>> _func1;

        ILeoValueSetter<T, TVal> IFluentValueSetter<T, TVal>.Instance(T instance, bool strictMode)
        {
            return _func1.Invoke(instance, strictMode);
        }

        #endregion
    }
}