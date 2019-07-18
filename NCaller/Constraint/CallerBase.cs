namespace NCaller
{
    public abstract class CallerBase<T> : CallerBase
    {
        public T Instance;
        public void SetInstance(T value) {
            Instance = value;
        }
    }
    public abstract class CallerBase
    {
        public string _current_name;
        public CallerBase this[string name]
        {
            get {
                _current_name = name;
                return this;
            }
        }
        public virtual void New() { }

        public abstract CallerBase Get(string name);

        public abstract T Get<T>(string name);

        public abstract T Get<T>();

        public abstract void Set(string name, object value);

        public abstract void Set(object value);

    }
}
