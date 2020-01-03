using Natasha.Builder;
using Natasha.Operator;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace NCaller
{
    /// <summary>
    /// 运行时类型动态构建器
    /// </summary>
    /// <typeparam name="T">运行时类型</typeparam>
    public class ProxyOperator<T> : ProxyOperator
    {

        private static PrecisionCache<Func<T>> _fast_cache;
        private static object _cache_lock;
        private readonly static ConcurrentDictionary<string, Func<T>> _ctor_mapping;
        static ProxyOperator()
        {
            _cache_lock = new object();
            _ctor_mapping = new ConcurrentDictionary<string, Func<T>>();
            //_ctor_mapping[Guid.NewGuid().ToString()] = ()=> default;
            _fast_cache = _ctor_mapping.PrecisioTree();
        }




        public ProxyOperator() : base(typeof(T)) { }




        /// <summary>
        /// 生成实例
        /// </summary>
        /// <param name="class">类名</param>
        /// <returns></returns>
        public T CreateProxy(string @class)
        {

            var result = _fast_cache.GetValue(@class);
            if (result == default)
            {

                lock (_cache_lock)
                {
                    var @delegate = (Func<T>)(base.Compile());
                    _ctor_mapping[OopNameScript] = @delegate;
                    _fast_cache = _ctor_mapping.PrecisioTree();
                    return @delegate();
                }

            }
            return result();
           

        }

    }




    /// <summary>
    /// 类构建器
    /// </summary>
    public class ProxyOperator : OopBuilder<ProxyOperator>
    {

        public Type TargetType;
        private readonly Type _oop_type;
        private readonly Dictionary<string, string> _oop_methods_mapping;

        public ProxyOperator() { }
        public ProxyOperator(Type oopType) : base()
        {

            Link = this;
            _oop_type = oopType;
            _oop_methods_mapping = new Dictionary<string, string>();

            Using(_oop_type)
               .Namespace("NatashaProxy")
               .Public
               .Inheritance(_oop_type);

        }





        /// <summary>
        /// 操作当前函数
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string this[string key]
        {
            get
            {

                return _oop_methods_mapping[key];

            }
            set
            {

                //获取反射信息
                var reflectMethodInfo = _oop_type.GetMethod(key);
                if (reflectMethodInfo == null)
                {

                    throw new Exception($"无法在{_oop_type.Name}中找到{key}函数！");

                }


                //填装引用
                Using(reflectMethodInfo);


                //使用伪造函数模板
                var template = FakeMethodOperator.Default();

                if (!_oop_type.IsInterface)
                {

                    _ = (reflectMethodInfo.IsAbstract || reflectMethodInfo.IsVirtual) ? template.OverrideMember : template.NewMember;

                }


                _oop_methods_mapping[key] = template.UseMethod(reflectMethodInfo).MethodContent(value).Builder().MethodScript;

            }

        }




        /// <summary>
        /// 组装编译
        /// </summary>
        /// <returns></returns>
        public Delegate Compile()
        {

            StringBuilder sb = new StringBuilder();
            foreach (var item in _oop_methods_mapping)
            {

                sb.Append(item.Value);

            }


            //生成整类脚本
            OopBody(sb.ToString());


            //获取类型
            TargetType = GetType();


            //返回委托
            return CtorOperator.Default().NewDelegate(TargetType);
        }

    }

}
