using System;

namespace NMS.Leo.Typed.Core
{
    internal class InstanceLeoVisitor : ILeoVisitor
    {
        private readonly DictBase _handler;
        private readonly object _instance;
        private readonly LeoType _leoType;
        
        protected HistoricalContext NormalHistoricalContext { get; set; }

        public InstanceLeoVisitor(DictBase handler, Type sourceType, object instance, LeoType leoType, bool repeatable)
        {
            _handler = handler ?? throw new ArgumentNullException(nameof(handler));
            _instance = instance;
            _leoType = leoType;

            _handler.SetObjInstance(_instance);

            SourceType = sourceType ?? throw new ArgumentNullException(nameof(sourceType));

            NormalHistoricalContext = repeatable
                ? new HistoricalContext(sourceType, leoType)
                : null;
        }

        public Type SourceType { get; }

        public bool IsStatic => false;

        public LeoType AlgorithmType => _leoType;

        public void SetValue(string name, object value)
        {
            NormalHistoricalContext?.RegisterOperation(c => c[name] = value);
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
            get => GetValue(name);
            set => SetValue(name, value);
        }

        public bool TryRepeat(out object result)
        {
            result = default;
            if (IsStatic) return false;
            if (NormalHistoricalContext is null) return false;
            result = NormalHistoricalContext.Repeat();
            return true;
        }

        public bool TryRepeat(object instance, out object result)
        {
            result = default;
            if (IsStatic) return false;
            if (NormalHistoricalContext is null) return false;
            result = NormalHistoricalContext.Repeat(instance);
            return true;
        }
    }

    internal class InstanceLeoVisitor<T> : ILeoVisitor<T>
    {
        private readonly DictBase<T> _handler;
        private readonly T _instance;
        private readonly LeoType _leoType;

        protected HistoricalContext<T> GenericHistoricalContext { get; set; }

        public InstanceLeoVisitor(DictBase<T> handler, T instance, LeoType leoType, bool repeatable)
        {
            _handler = handler ?? throw new ArgumentNullException(nameof(handler));
            _instance = instance;
            _leoType = leoType;

            _handler.SetInstance(_instance);

            SourceType = typeof(T);

            GenericHistoricalContext = repeatable
                ? new HistoricalContext<T>(leoType)
                : null;
        }

        public Type SourceType { get; }

        public bool IsStatic => false;

        public LeoType AlgorithmType => _leoType;

        public void SetValue(string name, object value)
        {
            GenericHistoricalContext?.RegisterOperation(c => c[name] = value);
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
            get => GetValue(name);
            set => SetValue(name, value);
        }

        public bool TryRepeat(out object result)
        {
            result = default;
            if (IsStatic) return false;
            if (GenericHistoricalContext is null) return false;
            result = GenericHistoricalContext.Repeat();
            return true;
        }

        public bool TryRepeat(object instance, out object result)
        {
            result = default;
            if (IsStatic) return false;
            if (GenericHistoricalContext is null) return false;
            result = GenericHistoricalContext.Repeat(instance);
            return true;
        }

        public bool TryRepeat(out T result)
        {
            result = default;
            if (IsStatic) return false;
            if (GenericHistoricalContext is null) return false;
            result = GenericHistoricalContext.Repeat();
            return true;
        }

        public bool TryRepeat(T instance, out T result)
        {
            result = default;
            if (IsStatic) return false;
            if (GenericHistoricalContext is null) return false;
            result = GenericHistoricalContext.Repeat(instance);
            return true;
        }
    }
}