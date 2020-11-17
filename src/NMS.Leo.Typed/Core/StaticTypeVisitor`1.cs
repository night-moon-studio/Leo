using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using NMS.Leo.Typed.Core.Loop;
using NMS.Leo.Typed.Core.Members;
using NMS.Leo.Typed.Core.Repeat;
using NMS.Leo.Typed.Core.Select;

namespace NMS.Leo.Typed.Core
{
    internal class StaticTypeLeoVisitor<T> : ILeoVisitor<T>, ILeoGetter<T>, ILeoSetter<T>
    {
        private readonly DictBase<T> _handler;
        private readonly AlgorithmKind _algorithmKind;

        private Lazy<MemberHandler> _lazyMemberHandler;

        public StaticTypeLeoVisitor(DictBase<T> handler, AlgorithmKind kind)
        {
            _handler = handler ?? throw new ArgumentNullException(nameof(handler));
            _algorithmKind = kind;

            SourceType = typeof(T);

            _lazyMemberHandler = MemberHandler.Lazy(() => new MemberHandler(_handler, SourceType));
        }

        public Type SourceType { get; }

        public bool IsStatic => true;

        public AlgorithmKind AlgorithmKind => _algorithmKind;

        object ILeoVisitor.Instance => default;

        public T Instance => default;

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

        void ILeoSetter<T>.SetValue<TObj>(Expression<Func<TObj, object>> expression, object value)
            => ((ILeoVisitor) this).SetValue(expression, value);

        void ILeoSetter<T>.SetValue<TObj, TValue>(Expression<Func<TObj, TValue>> expression, TValue value)
            => ((ILeoVisitor) this).SetValue(expression, value);

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

        public bool TryRepeat(IDictionary<string, object> keyValueCollections, out object result)
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

        public bool TryRepeat(IDictionary<string, object> keyValueCollections, out T result)
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