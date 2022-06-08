namespace NMS.Leo.Typed;

public interface ILeoGetter
{
    object Instance { get; }
        
    object GetValue(string name);

    TValue GetValue<TValue>(string name);

    object GetValue<TObj>(Expression<Func<TObj, object>> expression);

    TValue GetValue<TObj, TValue>(Expression<Func<TObj, TValue>> expression);
        
    bool Contains(string name);
}

public interface ILeoGetter<T>
{
    T Instance { get; }
        
    object GetValue(string name);

    TValue GetValue<TValue>(string name);
        
    object GetValue(Expression<Func<T, object>> expression);

    TValue GetValue<TValue>(Expression<Func<T, TValue>> expression);

    object GetValue<TObj>(Expression<Func<TObj, object>> expression);

    TValue GetValue<TObj, TValue>(Expression<Func<TObj, TValue>> expression);
        
    bool Contains(string name);
}