namespace NMS.Leo
{

    public abstract class DictBase<T> : DictBase
    {

        public T Instance;
        public void SetInstance(T value) => Instance = value;

    }
    public abstract class DictBase : CallerBase
    {

        public object this[string name]
        {
            get => GetObject(name);
            set => Set(name, value);
        }


        public abstract unsafe object GetObject(string name);

    }

}
