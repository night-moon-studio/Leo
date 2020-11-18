using System;
using System.Linq.Expressions;

namespace NMS.Leo.Typed.Core.Members
{
    internal class ValueSetter : ILeoValueSetter
    {
        private readonly Action<object> _setter;
        private readonly ILeoVisitor _visitor;

        public ValueSetter(ILeoVisitor visitor, string name)
        {
            _visitor = visitor;
            _setter = val => visitor.SetValue(name, val);
        }

        public void Value(object value)
        {
            _setter.Invoke(value);
        }

        public object HostedInstance => _visitor.Instance;
    }
    
    internal class ValueSetter<T> : ILeoValueSetter<T>
    {
        private readonly Action<object> _setter;
        private readonly ILeoVisitor<T> _visitor;

        public ValueSetter(ILeoVisitor<T> visitor, string name)
        {
            _visitor = visitor;
            _setter = val => visitor.SetValue(name, val);
        }

        public ValueSetter(ILeoVisitor<T> visitor, Expression<Func<T, object>> expression)
        {
            _visitor = visitor;
            _setter = val => visitor.SetValue(expression, val);
        }

        public void Value(object value)
        {
            _setter.Invoke(value);
        }

        public T HostedInstance => _visitor.Instance;
    }

    internal class ValueSetter<T, TVal> : ILeoValueSetter<T, TVal>
    {
        private readonly Action<TVal> _setter;
        private readonly ILeoVisitor<T> _visitor;

        public ValueSetter(ILeoVisitor<T> visitor, Expression<Func<T, TVal>> expression)
        {
            _visitor = visitor;
            _setter = val => visitor.SetValue(expression, val);
        }

        public void Value(TVal value)
        {
            _setter.Invoke(value);
        }

        public T HostedInstance => _visitor.Instance;
    }
}