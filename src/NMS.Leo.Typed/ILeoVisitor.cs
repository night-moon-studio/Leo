using System;

namespace NMS.Leo.Typed
{
    public interface ILeoVisitor
    {
        Type TargetType { get; }

        bool IsStatic { get; }

        LeoType AlgorithmType { get; }

        void SetValue(string name, object value);

        object GetValue(string name);

        TValue GetValue<TValue>(string name);

        object this[string name] { get; set; }
    }

    public interface ILeoVisitor<T> : ILeoVisitor { }
}