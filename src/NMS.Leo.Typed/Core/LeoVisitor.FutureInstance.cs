using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace NMS.Leo.Typed.Core
{
    internal class FutureInstanceLeoVisitor : ILeoVisitor, ILeoGetter, ILeoSetter
    {
        private readonly DictBase _handler;
        private readonly Type _sourceType;
        private readonly AlgorithmKind _algorithmKind;

        private Lazy<LeoMemberHandler> _lazyMemberHandler;

        protected HistoricalContext NormalHistoricalContext { get; set; }

        public FutureInstanceLeoVisitor(DictBase handler, Type sourceType, AlgorithmKind kind, bool repeatable,
            IDictionary<string, object> initialValues = null)
        {
            _handler = handler ?? throw new ArgumentNullException(nameof(handler));
            _sourceType = sourceType ?? throw new ArgumentNullException(nameof(sourceType));
            _algorithmKind = kind;

            _handler.New();

            NormalHistoricalContext = repeatable
                ? new HistoricalContext(sourceType, kind)
                : null;

            _lazyMemberHandler = new Lazy<LeoMemberHandler>(() => new LeoMemberHandler(_handler, _sourceType));

            if (initialValues != null)
                SetValue(initialValues);
        }

        public Type SourceType => _sourceType;

        public bool IsStatic => false;

        public AlgorithmKind AlgorithmKind => _algorithmKind;

        public object Instance => _handler.GetInstance();

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

        public void SetValue(IDictionary<string, object> keyValueCollections)
        {
            if (keyValueCollections is null)
                throw new ArgumentNullException(nameof(keyValueCollections));
            foreach (var keyValue in keyValueCollections)
                SetValue(keyValue.Key, keyValue.Value);
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

        public bool TryRepeat(IDictionary<string, object> keyValueCollections, out object result)
        {
            result = default;
            if (IsStatic) return false;
            if (NormalHistoricalContext is null) return false;
            result = NormalHistoricalContext.Repeat(keyValueCollections);
            return true;
        }

        public ILeoRepeater ForRepeat()
        {
            if (IsStatic) return new EmptyRepeater(_sourceType);
            if (NormalHistoricalContext is null) return new EmptyRepeater(_sourceType);
            return new LeoRepeater(NormalHistoricalContext);
        }

        public IEnumerable<string> GetMemberNames() => _lazyMemberHandler.Value.GetNames();

        public LeoMember GetMember(string name) => _lazyMemberHandler.Value.GetMember(name);

        public ILeoLooper ForEach(Action<string, object, LeoMember> loopAct)
        {
            return new LeoLooper(this, _lazyMemberHandler, loopAct);
        }

        public ILeoLooper ForEach(Action<string, object> loopAct)
        {
            return new LeoLooper(this, _lazyMemberHandler, loopAct);
        }

        public ILeoLooper ForEach(Action<LeoLoopContext> loopAct)
        {
            return new LeoLooper(this, _lazyMemberHandler, loopAct);
        }

        public ILeoSelector<TVal> Select<TVal>(Func<string, object, LeoMember, TVal> loopFunc)
        {
            return new LeoSelector<TVal>(this, _lazyMemberHandler, loopFunc);
        }

        public ILeoSelector<TVal> Select<TVal>(Func<string, object, TVal> loopFunc)
        {
            return new LeoSelector<TVal>(this, _lazyMemberHandler, loopFunc);
        }

        public ILeoSelector<TVal> Select<TVal>(Func<LeoLoopContext, TVal> loopFunc)
        {
            return new LeoSelector<TVal>(this, _lazyMemberHandler, loopFunc);
        }

        public Dictionary<string, object> ToDictionary()
        {
            var val = new Dictionary<string, object>();
            foreach (var name in _lazyMemberHandler.Value.GetNames())
                val[name] = _handler[name];
            return val;
        }

        public bool Contains(string name) => _handler.Contains(name);
    }

    internal class FutureInstanceLeoVisitor<T> : ILeoVisitor<T>, ILeoGetter<T>, ILeoSetter<T>
    {
        private readonly DictBase<T> _handler;
        private readonly Type _sourceType;
        private readonly AlgorithmKind _algorithmKind;

        private Lazy<LeoMemberHandler> _lazyMemberHandler;

        protected HistoricalContext<T> GenericHistoricalContext { get; set; }

        public FutureInstanceLeoVisitor(DictBase<T> handler, AlgorithmKind kind, bool repeatable,
            IDictionary<string, object> initialValues = null)
        {
            _handler = handler ?? throw new ArgumentNullException(nameof(handler));
            _sourceType = typeof(T);
            _algorithmKind = kind;

            _handler.New();

            GenericHistoricalContext = repeatable
                ? new HistoricalContext<T>(kind)
                : null;

            _lazyMemberHandler = new Lazy<LeoMemberHandler>(() => new LeoMemberHandler(_handler, _sourceType));

            if (initialValues != null)
                SetValue(initialValues);
        }

        public Type SourceType => _sourceType;

        public bool IsStatic => false;

        public AlgorithmKind AlgorithmKind => _algorithmKind;

        object ILeoVisitor.Instance => _handler.GetInstance();

        public T Instance => _handler.GetInstance();

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

        void ILeoSetter<T>.SetValue<TObj>(Expression<Func<TObj, object>> expression, object value)
            => ((ILeoVisitor) this).SetValue(expression, value);

        void ILeoSetter<T>.SetValue<TObj, TValue>(Expression<Func<TObj, TValue>> expression, TValue value)
            => ((ILeoVisitor) this).SetValue(expression, value);

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

        public void SetValue(IDictionary<string, object> keyValueCollections)
        {
            if (keyValueCollections is null)
                throw new ArgumentNullException(nameof(keyValueCollections));
            foreach (var keyValue in keyValueCollections)
                SetValue(keyValue.Key, keyValue.Value);
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

        object ILeoGetter<T>.GetValue<TObj>(Expression<Func<TObj, object>> expression)
            => ((ILeoVisitor) this).GetValue(expression);
                
        TValue ILeoGetter<T>.GetValue<TObj, TValue>(Expression<Func<TObj, TValue>> expression)
            => ((ILeoVisitor) this).GetValue(expression);

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

        public bool TryRepeat(IDictionary<string, object> keyValueCollections, out object result)
        {
            result = default;
            if (IsStatic) return false;
            if (GenericHistoricalContext is null) return false;
            result = GenericHistoricalContext.Repeat(keyValueCollections);
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

        public bool TryRepeat(IDictionary<string, object> keyValueCollections, out T result)
        {
            result = default;
            if (IsStatic) return false;
            if (GenericHistoricalContext is null) return false;
            result = GenericHistoricalContext.Repeat(keyValueCollections);
            return true;
        }

        public ILeoRepeater<T> ForRepeat()
        {
            if (IsStatic) return new EmptyRepeater<T>();
            if (GenericHistoricalContext is null) return new EmptyRepeater<T>();
            return new LeoRepeater<T>(GenericHistoricalContext);
        }

        ILeoRepeater ILeoVisitor.ForRepeat()
        {
            return ForRepeat();
        }

        public IEnumerable<string> GetMemberNames() => _lazyMemberHandler.Value.GetNames();

        public LeoMember GetMember(string name) => _lazyMemberHandler.Value.GetMember(name);

        public LeoMember GetMember<TValue>(Expression<Func<T, TValue>> expression)
        {
            if (expression is null)
                throw new ArgumentNullException(nameof(expression));

            var name = PropertySelector.GetPropertyName(expression);

            return _lazyMemberHandler.Value.GetMember(name);
        }

        public ILeoLooper<T> ForEach(Action<string, object, LeoMember> loopAct)
        {
            return new LeoLooper<T>(this, _lazyMemberHandler, loopAct);
        }

        public ILeoLooper<T> ForEach(Action<string, object> loopAct)
        {
            return new LeoLooper<T>(this, _lazyMemberHandler, loopAct);
        }

        public ILeoLooper<T> ForEach(Action<LeoLoopContext> loopAct)
        {
            return new LeoLooper<T>(this, _lazyMemberHandler, loopAct);
        }

        ILeoLooper ILeoVisitor.ForEach(Action<string, object, LeoMember> loopAct)
        {
            return new LeoLooper(this, _lazyMemberHandler, loopAct);
        }

        ILeoLooper ILeoVisitor.ForEach(Action<string, object> loopAct)
        {
            return new LeoLooper(this, _lazyMemberHandler, loopAct);
        }

        ILeoLooper ILeoVisitor.ForEach(Action<LeoLoopContext> loopAct)
        {
            return new LeoLooper(this, _lazyMemberHandler, loopAct);
        }

        public ILeoSelector<T, TVal> Select<TVal>(Func<string, object, LeoMember, TVal> loopFunc)
        {
            return new LeoSelector<T, TVal>(this, _lazyMemberHandler, loopFunc);
        }

        public ILeoSelector<T, TVal> Select<TVal>(Func<string, object, TVal> loopFunc)
        {
            return new LeoSelector<T, TVal>(this, _lazyMemberHandler, loopFunc);
        }

        public ILeoSelector<T, TVal> Select<TVal>(Func<LeoLoopContext, TVal> loopFunc)
        {
            return new LeoSelector<T, TVal>(this, _lazyMemberHandler, loopFunc);
        }

        ILeoSelector<TVal> ILeoVisitor.Select<TVal>(Func<string, object, LeoMember, TVal> loopFunc)
        {
            return new LeoSelector<TVal>(this, _lazyMemberHandler, loopFunc);
        }

        ILeoSelector<TVal> ILeoVisitor.Select<TVal>(Func<string, object, TVal> loopFunc)
        {
            return new LeoSelector<TVal>(this, _lazyMemberHandler, loopFunc);
        }

        ILeoSelector<TVal> ILeoVisitor.Select<TVal>(Func<LeoLoopContext, TVal> loopFunc)
        {
            return new LeoSelector<TVal>(this, _lazyMemberHandler, loopFunc);
        }

        public Dictionary<string, object> ToDictionary()
        {
            var val = new Dictionary<string, object>();
            foreach (var name in _lazyMemberHandler.Value.GetNames())
                val[name] = _handler[name];
            return val;
        }

        public bool Contains(string name) => _handler.Contains(name);
    }
}