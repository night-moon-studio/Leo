using System;
using System.Linq.Expressions;

namespace NMS.Leo.Typed
{
    public interface ILeoVisitor
    {
        Type SourceType { get; }

        bool IsStatic { get; }

        AlgorithmType AlgorithmType { get; }

        void SetValue(string name, object value);

        void SetValue<TObj>(Expression<Func<TObj, object>> expression, object value);

        void SetValue<TObj, TValue>(Expression<Func<TObj, TValue>> expression, TValue value);

        object GetValue(string name);

        TValue GetValue<TValue>(string name);

        object GetValue<TObj>(Expression<Func<TObj, object>> expression);

        TValue GetValue<TObj, TValue>(Expression<Func<TObj, TValue>> expression);

        object this[string name] { get; set; }

        bool TryRepeat(out object result);

        bool TryRepeat(object instance, out object result);
        
        ILeoRepeater ToRepeater();
    }

    public interface ILeoVisitor<T> : ILeoVisitor
    {
        void SetValue(Expression<Func<T, object>> expression, object value);

        void SetValue<TValue>(Expression<Func<T, TValue>> expression, TValue value);

        object GetValue(Expression<Func<T, object>> expression);

        TValue GetValue<TValue>(Expression<Func<T, TValue>> expression);

        bool TryRepeat(out T result);

        bool TryRepeat(T instance, out T result);

        new ILeoRepeater<T> ToRepeater();
    }
}