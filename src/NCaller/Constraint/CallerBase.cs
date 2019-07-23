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


        public int _nameCode;
        public CallerBase this[string name]
        {
            get
            {

                _nameCode = name.GetHashCode();
                return this;

            }
        }



        /// <summary>
        /// 如果不是静态类，可以新建一个实例
        /// </summary>
        public virtual void New() { }




        /// <summary>
        /// 将字段或者属性作为动态调用返回
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public abstract CallerBase Get(string name);




        /// <summary>
        /// 通过指定泛型以及成员名反射出强类型的字段或者属性值
        /// </summary>
        /// <typeparam name="T">字段/属性的类型</typeparam>
        /// <param name="name">字段/属性名</param>
        /// <returns></returns>
        public abstract T Get<T>(string name);




        /// <summary>
        /// 通过索引名以及指定类型反射出强类型的字段或者属性值
        /// </summary>
        /// <typeparam name="T">字段/属性的类型</typeparam>
        /// <returns></returns>
        public abstract T Get<T>();




        /// <summary>
        /// 通过指定泛型以及成员名设置字段或者属性值
        /// </summary>
        /// <param name="name">字段/属性名</param>
        /// <param name="value">字段/属性新值</param>
        public abstract void Set(string name, object value);




        /// <summary>
        /// 通过索引设置字段或者属性值
        /// </summary>
        /// <param name="value">字段/属性新值</param>
        public abstract void Set(object value);

    }
}
