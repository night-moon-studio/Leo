namespace NCaller
{

    public abstract class LinkBase<T> : LinkBase
    {

        public T Instance;
        public void SetInstance(T value) => Instance = value;

    }
    public abstract class LinkBase:CallerBase
    {

        public string _name;
        public LinkBase this[string name]
        {
            get
            {
                _name = name;
                return this;

            }
        }




        /// <summary>
        /// 将字段或者属性作为动态调用返回
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public unsafe abstract LinkBase Get(string name);




        /// <summary>
        /// 通过索引设置字段或者属性值
        /// </summary>
        /// <param name="value">字段/属性新值</param>
        public unsafe abstract void Set(object value);




        /// <summary>
        /// 通过索引名以及指定类型反射出强类型的字段或者属性值
        /// </summary>
        /// <typeparam name="T">字段/属性的类型</typeparam>
        /// <returns></returns>
        public unsafe abstract T Get<T>();

    }

}
