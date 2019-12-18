using Natasha.Builder;
using Natasha.Operator;
using System;
using System.Collections.Generic;
using System.Text;

namespace NCaller.Core.Builder
{
    internal class ProxyBuilder : OopBuilder<ProxyBuilder>
    {

        public string Result;
        public Type TargetType;
        private readonly Type _oop_type;
        private readonly Dictionary<string, string> _oop_methods_mapping;

        public ProxyBuilder() { }
        public ProxyBuilder(Type oopType) : base()
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
                var template = FakeMethodOperator.Create();

                if (!_oop_type.IsInterface)
                {

                    _ = (reflectMethodInfo.IsAbstract || reflectMethodInfo.IsVirtual) ? template.OverrideMember : template.NewMember;

                }


                _oop_methods_mapping[key] = template.UseMethod(reflectMethodInfo).MethodContent(value).Script;

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
            return CtorOperator.Create().NewDelegate(TargetType);
        }

    }
}
