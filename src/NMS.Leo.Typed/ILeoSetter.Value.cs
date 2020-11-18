namespace NMS.Leo.Typed
{
    public interface ILeoValueSetter
    {
        void Value(object value);

        object HostedInstance { get; }
    }

    public interface ILeoValueSetter<T>
    {
        void Value(object value);

        T HostedInstance { get; }
    }

    public interface ILeoValueSetter<T, TVal>
    {
        void Value(TVal value);

        T HostedInstance { get; }
    }
}