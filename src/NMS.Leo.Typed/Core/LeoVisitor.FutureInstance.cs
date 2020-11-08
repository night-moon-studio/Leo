using System;

namespace NMS.Leo.Typed.Core
{
    internal class FutureInstanceLeoVisitor : ILeoVisitor
    {
        private readonly DictBase _handler;
        private readonly Type _futureType;
        private readonly LeoType _leoType;

        public FutureInstanceLeoVisitor(DictBase handler, Type type, LeoType leoType)
        {
            _handler = handler ?? throw new ArgumentNullException(nameof(handler));
            _futureType = type ?? throw new ArgumentNullException(nameof(type));
            _leoType = leoType;

            _handler.New();
        }

        public Type TargetType => _futureType;

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

    internal class FutureInstanceLeoVisitor<T> : ILeoVisitor<T>
    {
        private readonly DictBase<T> _handler;
        private readonly Type _futureType;
        private readonly LeoType _leoType;

        public FutureInstanceLeoVisitor(DictBase<T> handler, LeoType leoType)
        {
            _handler = handler ?? throw new ArgumentNullException(nameof(handler));
            _futureType = typeof(T);
            _leoType = leoType;

            _handler.New();
        }

        public Type TargetType => _futureType;

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