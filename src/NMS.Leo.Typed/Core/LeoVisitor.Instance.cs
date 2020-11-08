using System;

namespace NMS.Leo.Typed.Core
{
    internal class InstanceLeoVisitor : ILeoVisitor
    {
        private readonly DictBase _handler;
        private readonly object _instance;
        private readonly LeoType _leoType;

        public InstanceLeoVisitor(DictBase handler, Type targetType, object instance, LeoType leoType)
        {
            _handler = handler ?? throw new ArgumentNullException(nameof(handler));
            _instance = instance;
            _leoType = leoType;

            _handler.SetObjInstance(_instance);

            TargetType = targetType ?? throw new ArgumentNullException(nameof(targetType));
        }

        public Type TargetType { get; }

        public bool IsStatic => false;

        public LeoType AlgorithmType => _leoType;

        public void SetValue(string name, object value)
        {
            _handler[name] = value;
        }

        public object GetValue(string name)
        {
            return _handler[name];
        }

        public TValue GetValue<TValue>(string name)
        {
            return _handler.Get<TValue>(name);
        }

        public object this[string name]
        {
            get => _handler[name];
            set => _handler[name] = value;
        }
    }

    internal class InstanceLeoVisitor<T> : ILeoVisitor<T>
    {
        private readonly DictBase<T> _handler;
        private readonly T _instance;
        private readonly LeoType _leoType;

        public InstanceLeoVisitor(DictBase<T> handler, T instance, LeoType leoType)
        {
            _handler = handler ?? throw new ArgumentNullException(nameof(handler));
            _instance = instance;
            _leoType = leoType;

            _handler.SetInstance(_instance);

            TargetType = typeof(T);
        }

        public Type TargetType { get; }

        public bool IsStatic => false;

        public LeoType AlgorithmType => _leoType;

        public void SetValue(string name, object value)
        {
            _handler[name] = value;
        }

        public object GetValue(string name)
        {
            return _handler[name];
        }

        public TValue GetValue<TValue>(string name)
        {
            return _handler.Get<TValue>(name);
        }

        public object this[string name]
        {
            get => _handler[name];
            set => _handler[name] = value;
        }
    }
}