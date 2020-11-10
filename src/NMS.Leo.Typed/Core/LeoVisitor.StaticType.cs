using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Leo.Typed.Core;

namespace NMS.Leo.Typed.Core
{
    internal class StaticTypeLeoVisitor : ILeoVisitor
    {
        private readonly DictBase _handler;
        private readonly AlgorithmType _algorithmType;

        private Lazy<LeoMemberHandler> _lazyMemberHandler;

        public StaticTypeLeoVisitor(DictBase handler, Type targetType, AlgorithmType algorithmType)
        {
            _handler = handler ?? throw new ArgumentNullException(nameof(handler));
            _algorithmType = algorithmType;

            SourceType = targetType ?? throw new ArgumentNullException(nameof(targetType));

            _lazyMemberHandler = new Lazy<LeoMemberHandler>(() => new LeoMemberHandler(_handler, SourceType));
        }

        public Type SourceType { get; }

        public bool IsStatic => true;

        public AlgorithmType AlgorithmType => _algorithmType;

        public void SetValue(string name, object value)
        {
            _handler[name] = value;
        }

        public void SetValue<TObj>(Expression<Func<TObj, object>> expression, object value)
        {
            if (expression is null)
                return;

            var name = PropertySelector.GetPropertyName(expression);

            _handler[name] = value;
        }

        public void SetValue<TObj, TValue>(Expression<Func<TObj, TValue>> expression, TValue value)
        {
            if (expression is null)
                return;

            var name = PropertySelector.GetPropertyName(expression);

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
            get => _handler[name];
            set => _handler[name] = value;
        }

        public bool TryRepeat(out object result)
        {
            result = default;
            return false;
        }

        public bool TryRepeat(object instance, out object result)
        {
            result = default;
            return false;
        }

        public ILeoRepeater ForRepeat()
        {
            return StaticEmptyRepeater.Instance;
        }

        public IEnumerable<string> GetMemberNames() => _lazyMemberHandler.Value.GetNames();

        public LeoMember GetMember(string name) => _lazyMemberHandler.Value.GetMember(name);
    }

    internal class StaticTypeLeoVisitor<T> : ILeoVisitor<T>
    {
        private readonly DictBase<T> _handler;
        private readonly AlgorithmType _algorithmType;

        private Lazy<LeoMemberHandler> _lazyMemberHandler;

        public StaticTypeLeoVisitor(DictBase<T> handler, AlgorithmType algorithmType)
        {
            _handler = handler ?? throw new ArgumentNullException(nameof(handler));
            _algorithmType = algorithmType;

            SourceType = typeof(T);

            _lazyMemberHandler = new Lazy<LeoMemberHandler>(() => new LeoMemberHandler(_handler, SourceType));
        }

        public Type SourceType { get; }

        public bool IsStatic => true;

        public AlgorithmType AlgorithmType => _algorithmType;

        public void SetValue(string name, object value)
        {
            _handler[name] = value;
        }

        void ILeoVisitor.SetValue<TObj>(Expression<Func<TObj, object>> expression, object value)
        {
            if (expression is null)
                return;

            var name = PropertySelector.GetPropertyName(expression);

            _handler[name] = value;
        }

        void ILeoVisitor.SetValue<TObj, TValue>(Expression<Func<TObj, TValue>> expression, TValue value)
        {
            if (expression is null)
                return;

            var name = PropertySelector.GetPropertyName(expression);

            _handler[name] = value;
        }

        public void SetValue(Expression<Func<T, object>> expression, object value)
        {
            if (expression is null)
                return;

            var name = PropertySelector.GetPropertyName(expression);

            _handler[name] = value;
        }

        public void SetValue<TValue>(Expression<Func<T, TValue>> expression, TValue value)
        {
            if (expression is null)
                return;

            var name = PropertySelector.GetPropertyName(expression);

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
            get => _handler[name];
            set => _handler[name] = value;
        }

        public bool TryRepeat(out object result)
        {
            result = default;
            return false;
        }

        public bool TryRepeat(object instance, out object result)
        {
            result = default;
            return false;
        }

        public bool TryRepeat(out T result)
        {
            result = default;
            return false;
        }

        public bool TryRepeat(T instance, out T result)
        {
            result = default;
            return false;
        }

        public ILeoRepeater<T> ForRepeat()
        {
            return StaticEmptyRepeater<T>.Instance;
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
    }
}