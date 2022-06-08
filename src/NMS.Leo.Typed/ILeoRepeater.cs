namespace NMS.Leo.Typed;

public interface ILeoRepeater
{
    object Play(object originalObj);
        
    object Play(IDictionary<string, object> keyValueCollections);

    object NewAndPlay();
}

public interface ILeoRepeater<T> : ILeoRepeater
{
    T Play(T originalObj);
        
    new T Play(IDictionary<string, object> keyValueCollections);

    new T NewAndPlay();
}