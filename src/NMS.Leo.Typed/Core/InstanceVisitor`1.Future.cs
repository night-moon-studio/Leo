using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using NMS.Leo.Typed.Core.Members;
using NMS.Leo.Typed.Core.Repeat;

namespace NMS.Leo.Typed.Core
{
    internal class FutureInstanceVisitor<T> : ILeoVisitor<T>, ICoreVisitor<T>, ILeoGetter<T>, ILeoSetter<T>
    {
        private readonly DictBase<T> _handler;
        private readonly Type _sourceType;
        private readonly AlgorithmKind _algorithmKind;

        private Lazy<MemberHandler> _lazyMemberHandler;

        protected HistoricalContext<T> GenericHistoricalContext { get; set; }

        public FutureInstanceVisitor(DictBase<T> handler, AlgorithmKind kind, bool repeatable,
            IDictionary<string, object> initialValues = null)
        {
            _handler = handler ?? throw new ArgumentNullException(nameof(handler));
            _sourceType = typeof(T);
            _algorithmKind = kind;

            _handler.New();

            GenericHistoricalContext = repeatable
                ? new HistoricalContext<T>(kind)
                : null;

            _lazyMemberHandler = MemberHandler.Lazy(() => new MemberHandler(_handler, _sourceType));

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

        public HistoricalContext<T> ExposeHistoricalContext() => GenericHistoricalContext;

        public Lazy<MemberHandler> ExposeLazyMemberHandler() => _lazyMemberHandler;

        public ILeoVisitor<T> Owner => this;

        public IEnumerable<string> GetMemberNames() => _lazyMemberHandler.Value.GetNames();

        public LeoMember GetMember(string name) => _lazyMemberHandler.Value.GetMember(name);

        public LeoMember GetMember<TValue>(Expression<Func<T, TValue>> expression)
        {
            if (expression is null)
                throw new ArgumentNullException(nameof(expression));

            var name = PropertySelector.GetPropertyName(expression);

            return _lazyMemberHandler.Value.GetMember(name);
        }

        public bool Contains(string name) => _lazyMemberHandler.Value.Contains(name);
    }
}