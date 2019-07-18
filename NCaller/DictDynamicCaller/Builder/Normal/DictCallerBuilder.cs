using System;
using System.Collections.Concurrent;
using System.Text;
using Natasha;

namespace NCaller.Builder
{
    public class DictCallerBuilder<T>
    {
        public static readonly Func<CallerBase> Ctor;
        static DictCallerBuilder() => Ctor = DictCallerBuilder.InitType(typeof(T));
    }


    public class DictCallerBuilder
    {


        public static readonly ConcurrentDictionary<Type, Func<CallerBase>> TypeCreatorMapping;
        static DictCallerBuilder()
        {
            TypeCreatorMapping = new ConcurrentDictionary<Type, Func<CallerBase>>();
        }

        public static CallerBase Ctor(Type type)
        {
            if (!TypeCreatorMapping.ContainsKey(type))
            {
                InitType(type);
            }
            return TypeCreatorMapping[type]();
        }


        public static Func<CallerBase> InitType(Type type)
        {
            string innerBody = "InnerDynamic" + type.GetAvailableName();
            string entityClassName = type.GetDevelopName();
            string className = "NatashaDynamic" + type.GetAvailableName();


            ClassBuilder builder = new ClassBuilder();
            StringBuilder body = new StringBuilder();


            body.Append($@" 
                    public readonly static ConcurrentDictionary<string,Func<{entityClassName},CallerBase>> LinkMapping;
                    static {className}(){{

                        LinkMapping = new ConcurrentDictionary<string,Func<{entityClassName},CallerBase>>();
                       

                        var fields = typeof({entityClassName}).GetFields();
                        for (int i = 0; i < fields.Length; i+=1)
                        {{
                            if(!fields[i].FieldType.IsOnceType())
                            {{
                                LinkMapping[fields[i].Name] = FastMethodOperator
                                                                .New
                                                                .Using(fields[i].FieldType)
                                                                .Using(""Natasha.Caller"")
                                                                .Param<{entityClassName}>(""obj"")
                                                                .MethodBody($@""return obj.{{fields[i].Name}}.Caller<{{fields[i].FieldType.GetDevelopName()}}>();"")
                                                                .Return<CallerBase>()
                                                                .Complie<Func<{entityClassName}, CallerBase>>();
                            }}
                        }}


                        var props = typeof({entityClassName}).GetProperties(); 
                        for (int i = 0; i < props.Length; i+=1)
                        {{
                            if(!props[i].PropertyType.IsOnceType())
                            {{
                                LinkMapping[props[i].Name] = FastMethodOperator
                                                                .New
                                                                .Using(props[i].PropertyType)
                                                                .Using(""Natasha.Caller"")
                                                                .Param<{entityClassName}>(""obj"")
                                                                .MethodBody($@""return obj.{{props[i].Name}}.Caller<{{props[i].PropertyType.GetDevelopName()}}>();"")
                                                                .Return<CallerBase>()
                                                                .Complie<Func<{entityClassName}, CallerBase>>();
                            }}
                        }}
                    }}");

            body.Append($@" 
                    public override void New(){{
                         Instance = new {type.GetDevelopName()}();
                    }}");

            body.Append($@" 
                    public override CallerBase Get(string name){{
                         return LinkMapping[name](Instance);
                    }}");

            body.Append($@" 
                    public override T Get<T>(string name){{
                        return {innerBody}<T>.GetterMapping[name](Instance);
                    }}");

            body.Append($@" 
                    public override T Get<T>(){{
                        return {innerBody}<T>.GetterMapping[_current_name](Instance);
                    }}");

            body.Append($@" 
                    public override void Set<T>(string name,T value){{
                        {innerBody}<T>.SetterMapping[name](Instance,value);
                    }}");
            body.Append($@" 
                    public override void Set<T>(T value){{
                        {innerBody}<T>.SetterMapping[_current_name](Instance,value);
                    }}");

            Type tempClass = builder
                    .Using(type)
                    .Using("System")
                    .Using("System.Collections.Concurrent")
                    .ClassAccess(AccessTypes.Public)
                    .ClassName(className)
                    .Namespace("NCallerDynamic")
                    .Inheritance(GenericBuilder.GetType(typeof(CallerBase<>),type))
                    .ClassBody(body + InnerTemplate.GetNormalInnerString(innerBody, entityClassName))
                    .GetType();

            return TypeCreatorMapping[type] = (Func<CallerBase>)CtorBuilder.NewDelegate(tempClass);
        }
    }
}
