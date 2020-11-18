using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace NMS.Leo.Typed
{
    public interface IFluentGetter
    {
        ILeoGetter Instance(object instance);

        ILeoGetter InitialValues(IDictionary<string, object> initialValues);

        IFluentValueGetter Value(PropertyInfo propertyInfo);

        IFluentValueGetter Value(FieldInfo fieldInfo);

        IFluentValueGetter Value(string name);
    }

    public interface IFluentGetter<T>
    {
        ILeoGetter<T> Instance(T instance);

        ILeoGetter<T> InitialValues(IDictionary<string, object> initialValues);

        IFluentValueGetter<T> Value(PropertyInfo propertyInfo);

        IFluentValueGetter<T> Value(FieldInfo fieldInfo);

        IFluentValueGetter<T> Value(string name);

        IFluentValueGetter<T> Value(Expression<Func<T, object>> expression);

        IFluentValueGetter<T, TVal> Value<TVal>(Expression<Func<T, TVal>> expression);
    }

    public interface IFluentValueGetter
    {
        ILeoValueGetter Instance(object instance);
    }

    public interface IFluentValueGetter<T>
    {
        ILeoValueGetter<T> Instance(T instance);
    }

    public interface IFluentValueGetter<T, TVal>
    {
        ILeoValueGetter<T, TVal> Instance(T instance);
    }

    public interface IFluentSetter
    {
        ILeoSetter NewInstance();

        ILeoSetter Instance(object instance);

        ILeoSetter InitialValues(IDictionary<string, object> initialValues);

        IFluentValueSetter Value(PropertyInfo propertyInfo);

        IFluentValueSetter Value(FieldInfo fieldInfo);

        IFluentValueSetter Value(string name);
    }

    public interface IFluentSetter<T>
    {
        ILeoSetter<T> NewInstance();

        ILeoSetter<T> Instance(T instance);

        ILeoSetter<T> InitialValues(IDictionary<string, object> initialValues);

        IFluentValueSetter<T> Value(PropertyInfo propertyInfo);

        IFluentValueSetter<T> Value(FieldInfo fieldInfo);

        IFluentValueSetter<T> Value(string name);

        IFluentValueSetter<T> Value(Expression<Func<T, object>> expression);

        IFluentValueSetter<T, TVal> Value<TVal>(Expression<Func<T, TVal>> expression);
    }

    public interface IFluentValueSetter
    {
        ILeoValueSetter Instance(object instance);
    }

    public interface IFluentValueSetter<T>
    {
        ILeoValueSetter<T> Instance(T instance);
    }

    public interface IFluentValueSetter<T, TVal>
    {
        ILeoValueSetter<T, TVal> Instance(T instance);
    }
}