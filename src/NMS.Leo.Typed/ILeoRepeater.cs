namespace NMS.Leo.Typed
{
    public interface ILeoRepeater
    {
        object Play(object originalObj);

        object NewAndPlay();
    }

    public interface ILeoRepeater<T> : ILeoRepeater
    {
        T Play(T originalObj);

        new T NewAndPlay();
    }
}