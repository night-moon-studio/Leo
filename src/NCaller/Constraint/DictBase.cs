namespace NCaller
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
            get
            {

                return GetObject(name);

            }
            set
            {

                Set(name, value);

            }
        }


        public unsafe abstract object GetObject(string name);

    }

}
