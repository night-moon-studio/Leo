namespace NMS.Leo
{

    public abstract class DictBase<T> : DictBase
    {

        protected T Instance;
        public void SetInstance(T value) => Instance = value;

        public override void SetObjInstance(object obj)
        {
            Instance = (T)obj;
        }

    }
    public abstract class DictBase : CallerBase
    {

        public object this[string name]
        {
            get => GetObject(name);
            set => Set(name, value);
        }
        public abstract void SetObjInstance(object obj);
        public abstract unsafe object GetObject(string name);

    }

}
