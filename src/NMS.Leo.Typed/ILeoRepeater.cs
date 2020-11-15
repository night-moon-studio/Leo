using System.Collections.Generic;

namespace NMS.Leo.Typed
{
    public interface ILeoRepeater
    {
        object Play(object originalObj);
        
        object Play(Dictionary<string, object> keyValueCollections);

        object NewAndPlay();
    }

    public interface ILeoRepeater<T> : ILeoRepeater
    {
        T Play(T originalObj);
        
        new T Play(Dictionary<string, object> keyValueCollections);

        new T NewAndPlay();
    }
}