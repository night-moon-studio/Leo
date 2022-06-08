namespace NMS.Leo.Typed.Core.Members;

internal class ValueGetter : ILeoValueGetter
{
    private readonly Func<object> _getter;
    private readonly ILeoVisitor _visitor;

    public ValueGetter(ILeoVisitor visitor, string name)
    {
        _visitor = visitor;
        _getter = () => visitor.GetValue(name);
    }

    public object Value => _getter.Invoke();

    public object HostedInstance => _visitor.Instance;
}

internal class ValueGetter<T> : ILeoValueGetter<T>
{
    private readonly Func<object> _getter;
    private readonly ILeoVisitor<T> _visitor;

    public ValueGetter(ILeoVisitor<T> visitor, string name)
    {
        _visitor = visitor;
        _getter = () => visitor.GetValue(name);
    }

    public ValueGetter(ILeoVisitor<T> visitor, Expression<Func<T, object>> expression)
    {
        _visitor = visitor;
        _getter = () => visitor.GetValue(expression);
    }

    public object Value => _getter.Invoke();

    public T HostedInstance => _visitor.Instance;
}

internal class ValueGetter<T, TVal> : ILeoValueGetter<T, TVal>
{
    private readonly Func<TVal> _getter;
    private readonly ILeoVisitor<T> _visitor;

    public ValueGetter(ILeoVisitor<T> visitor, Expression<Func<T, TVal>> expression)
    {
        _visitor = visitor;
        _getter = () => visitor.GetValue(expression);
    }

    public TVal Value => _getter.Invoke();

    public T HostedInstance => _visitor.Instance;
}