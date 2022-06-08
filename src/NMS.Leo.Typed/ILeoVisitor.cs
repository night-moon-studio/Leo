using NMS.Leo.Metadata;
using NMS.Leo.Typed.Validation;

namespace NMS.Leo.Typed;

public interface ILeoVisitor
{
    Type SourceType { get; }

    bool IsStatic { get; }

    AlgorithmKind AlgorithmKind { get; }

    object Instance { get; }

    ILeoValidationContext ValidationEntry { get; }

    LeoVerifyResult Verify();

    void VerifyAndThrow();

    void SetValue(string name, object value);

    void SetValue<TObj>(Expression<Func<TObj, object>> expression, object value);

    void SetValue<TObj, TValue>(Expression<Func<TObj, TValue>> expression, TValue value);

    void SetValue(IDictionary<string, object> keyValueCollections);

    object GetValue(string name);

    TValue GetValue<TValue>(string name);

    object GetValue<TObj>(Expression<Func<TObj, object>> expression);

    TValue GetValue<TObj, TValue>(Expression<Func<TObj, TValue>> expression);

    object this[string name] { get; set; }

    IEnumerable<string> GetMemberNames();

    LeoMember GetMember(string name);

    bool Contains(string name);
}

public interface ILeoVisitor<T> : ILeoVisitor
{
    new T Instance { get; }

    new ILeoValidationContext<T> ValidationEntry { get; }

    void SetValue(Expression<Func<T, object>> expression, object value);

    void SetValue<TValue>(Expression<Func<T, TValue>> expression, TValue value);

    object GetValue(Expression<Func<T, object>> expression);

    TValue GetValue<TValue>(Expression<Func<T, TValue>> expression);

    LeoMember GetMember<TValue>(Expression<Func<T, TValue>> expression);
}