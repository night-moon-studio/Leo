namespace NMS.Leo.Typed;

public interface ILeoSetter
{
    object Instance { get; }
        
    void SetValue(string name, object value);

    void SetValue<TObj>(Expression<Func<TObj, object>> expression, object value);

    void SetValue<TObj, TValue>(Expression<Func<TObj, TValue>> expression, TValue value);

    void SetValue(IDictionary<string, object> keyValueCollections);
        
    bool Contains(string name);
}

public interface ILeoSetter<T>
{
    T Instance { get; }
        
    void SetValue(string name, object value);
        
    void SetValue(Expression<Func<T, object>> expression, object value);

    void SetValue<TValue>(Expression<Func<T, TValue>> expression, TValue value);

    void SetValue<TObj>(Expression<Func<TObj, object>> expression, object value);

    void SetValue<TObj, TValue>(Expression<Func<TObj, TValue>> expression, TValue value);

    void SetValue(IDictionary<string, object> keyValueCollections);
        
    bool Contains(string name);
}