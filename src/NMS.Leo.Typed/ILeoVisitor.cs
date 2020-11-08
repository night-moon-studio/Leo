using System;

namespace NMS.Leo.Typed
{
    public interface ILeoVisitor
    {
        Type SourceType { get; }

        bool IsStatic { get; }

        LeoType AlgorithmType { get; }

        void SetValue(string name, object value);

        object GetValue(string name);

        TValue GetValue<TValue>(string name);

        object this[string name] { get; set; }

        bool TryRepeat(out object result);

        bool TryRepeat(object instance, out object result);
    }

    public interface ILeoVisitor<T> : ILeoVisitor
    {
        bool TryRepeat(out T result);

        bool TryRepeat(T instance, out T result);
    }
}