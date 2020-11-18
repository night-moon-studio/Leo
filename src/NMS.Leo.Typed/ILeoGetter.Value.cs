namespace NMS.Leo.Typed
{
    public interface ILeoValueGetter
    {
        object Value { get; }

        object HostedInstance { get; }
    }

    public interface ILeoValueGetter<T>
    {
        object Value { get; }

        T HostedInstance { get; }
    }

    public interface ILeoValueGetter<T, TVal>
    {
        TVal Value { get; }

        T HostedInstance { get; }
    }
}