namespace NMS.Leo.Typed;

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
    ILeoSetter NewInstance(bool strictMode = StMode.NORMALE);

    ILeoSetter Instance(object instance, bool strictMode = StMode.NORMALE);

    ILeoSetter InitialValues(IDictionary<string, object> initialValues, bool strictMode = StMode.NORMALE);

    IFluentValueSetter Value(PropertyInfo propertyInfo);

    IFluentValueSetter Value(FieldInfo fieldInfo);

    IFluentValueSetter Value(string name);
}

public interface IFluentSetter<T>
{
    ILeoSetter<T> NewInstance(bool strictMode = StMode.NORMALE);

    ILeoSetter<T> Instance(T instance, bool strictMode = StMode.NORMALE);

    ILeoSetter<T> InitialValues(IDictionary<string, object> initialValues, bool strictMode = StMode.NORMALE);

    IFluentValueSetter<T> Value(PropertyInfo propertyInfo);

    IFluentValueSetter<T> Value(FieldInfo fieldInfo);

    IFluentValueSetter<T> Value(string name);

    IFluentValueSetter<T> Value(Expression<Func<T, object>> expression);

    IFluentValueSetter<T, TVal> Value<TVal>(Expression<Func<T, TVal>> expression);
}

public interface IFluentValueSetter
{
    ILeoValueSetter Instance(object instance, bool strictMode = StMode.NORMALE);
}

public interface IFluentValueSetter<T>
{
    ILeoValueSetter<T> Instance(T instance, bool strictMode = StMode.NORMALE);
}

public interface IFluentValueSetter<T, TVal>
{
    ILeoValueSetter<T, TVal> Instance(T instance, bool strictMode = StMode.NORMALE);
}