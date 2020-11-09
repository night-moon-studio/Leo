using System;
using System.Linq.Expressions;
using Leo.Typed.Core;

namespace NMS.Leo.Typed.Core
{
    internal class FutureInstanceLeoVisitor : ILeoVisitor
    {
        private readonly DictBase _handler;
        private readonly Type _sourceType;
        private readonly AlgorithmType _algorithmType;

        protected HistoricalContext NormalHistoricalContext { get; set; }

        public FutureInstanceLeoVisitor(DictBase handler, Type sourceType, AlgorithmType algorithmType, bool repeatable)
        {
            _handler = handler ?? throw new ArgumentNullException(nameof(handler));
            _sourceType = sourceType ?? throw new ArgumentNullException(nameof(sourceType));
            _algorithmType = algorithmType;

            _handler.New();

            NormalHistoricalContext = repeatable
                ? new HistoricalContext(sourceType, algorithmType)
                : null;
        }

        public Type SourceType => _sourceType;

        public bool IsStatic => false;

        public AlgorithmType AlgorithmType => _algorithmType;

        public void SetValue(string name, object value)
        {
            NormalHistoricalContext?.RegisterOperation(c => c[name] = value);
            _handler[name] = value;
        }

        public void SetValue<TObj>(Expression<Func<TObj, object>> expression, object value)
        {
            if (expression is null)
                return;

            var name = PropertySelector.GetPropertyName(expression);

            NormalHistoricalContext?.RegisterOperation(c => c[name] = value);
            _handler[name] = value;
        }

        public void SetValue<TObj, TValue>(Expression<Func<TObj, TValue>> expression, TValue value)
        {
            if (expression is null)
                return;

            var name = PropertySelector.GetPropertyName(expression);

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

        public object GetValue<TObj>(Expression<Func<TObj, object>> expression)
        {
            if (expression is null)
                throw new ArgumentNullException(nameof(expression));

            var name = PropertySelector.GetPropertyName(expression);

            return _handler[name];
        }

        public TValue GetValue<TObj, TValue>(Expression<Func<TObj, TValue>> expression)
        {
            if (expression is null)
                throw new ArgumentNullException(nameof(expression));

            var name = PropertySelector.GetPropertyName(expression);

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

        public ILeoRepeater ToRepeater()
        {
            if (IsStatic) return new EmptyRepeater(_sourceType);
            if (NormalHistoricalContext is null) return new EmptyRepeater(_sourceType);
            return new LeoRepeater(NormalHistoricalContext);
        }
    }

    internal class FutureInstanceLeoVisitor<T> : ILeoVisitor<T>
    {
        private readonly DictBase<T> _handler;
        private readonly Type _sourceType;
        private readonly AlgorithmType _algorithmType;

        protected HistoricalContext<T> GenericHistoricalContext { get; set; }

        public FutureInstanceLeoVisitor(DictBase<T> handler, AlgorithmType algorithmType, bool repeatable)
        {
            _handler = handler ?? throw new ArgumentNullException(nameof(handler));
            _sourceType = typeof(T);
            _algorithmType = algorithmType;

            _handler.New();

            GenericHistoricalContext = repeatable
                ? new HistoricalContext<T>(algorithmType)
                : null;
        }

        public Type SourceType => _sourceType;

        public bool IsStatic => false;

        public AlgorithmType AlgorithmType => _algorithmType;

        public void SetValue(string name, object value)
        {
            GenericHistoricalContext?.RegisterOperation(c => c[name] = value);
            _handler[name] = value;
        }

        void ILeoVisitor.SetValue<TObj>(Expression<Func<TObj, object>> expression, object value)
        {
            if (expression is null)
                return;

            var name = PropertySelector.GetPropertyName(expression);

            GenericHistoricalContext?.RegisterOperation(c => c[name] = value);
            _handler[name] = value;
        }

        void ILeoVisitor.SetValue<TObj, TValue>(Expression<Func<TObj, TValue>> expression, TValue value)
        {
            if (expression is null)
                return;

            var name = PropertySelector.GetPropertyName(expression);

            GenericHistoricalContext?.RegisterOperation(c => c[name] = value);
            _handler[name] = value;
        }

        public void SetValue(Expression<Func<T, object>> expression, object value)
        {
            if (expression is null)
                return;

            var name = PropertySelector.GetPropertyName(expression);

            GenericHistoricalContext?.RegisterOperation(c => c[name] = value);
            _handler[name] = value;
        }

        public void SetValue<TValue>(Expression<Func<T, TValue>> expression, TValue value)
        {
            if (expression is null)
                return;

            var name = PropertySelector.GetPropertyName(expression);

            GenericHistoricalContext?.RegisterOperation(c => c[name] = value);
            _handler[name] = value;
        }

        public object GetValue(string name)
        {
            return _handler[name];
        }

        public object GetValue(Expression<Func<T, object>> expression)
        {
            if (expression is null)
                throw new ArgumentNullException(nameof(expression));

            var name = PropertySelector.GetPropertyName(expression);

            return _handler[name];
        }

        public TValue GetValue<TValue>(string name)
        {
            return _handler.Get<TValue>(name);
        }

        object ILeoVisitor.GetValue<TObj>(Expression<Func<TObj, object>> expression)
        {
            if (expression is null)
                throw new ArgumentNullException(nameof(expression));

            var name = PropertySelector.GetPropertyName(expression);

            return _handler[name];
        }

        TValue ILeoVisitor.GetValue<TObj, TValue>(Expression<Func<TObj, TValue>> expression)
        {
            if (expression is null)
                throw new ArgumentNullException(nameof(expression));

            var name = PropertySelector.GetPropertyName(expression);

            return _handler.Get<TValue>(name);
        }

        public TValue GetValue<TValue>(Expression<Func<T, TValue>> expression)
        {
            if (expression is null)
                throw new ArgumentNullException(nameof(expression));

            var name = PropertySelector.GetPropertyName(expression);

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

        public ILeoRepeater<T> ToRepeater()
        {
            if (IsStatic) return new EmptyRepeater<T>();
            if (GenericHistoricalContext is null) return new EmptyRepeater<T>();
            return new LeoRepeater<T>(GenericHistoricalContext);
        }
        
        ILeoRepeater ILeoVisitor.ToRepeater()
        {
            return ToRepeater();
        }
    }
}