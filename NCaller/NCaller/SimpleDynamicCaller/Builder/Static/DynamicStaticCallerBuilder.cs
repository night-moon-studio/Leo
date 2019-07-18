using Natasha;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace NCaller.Builder
{

    public class SimpleStaticCallerBuilder
    {
        public static readonly ConcurrentDictionary<Type, Func<CallerBase>> TypeCreatorMapping;
        static SimpleStaticCallerBuilder()
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
            string className = "NatashaDynamic" + type.GetAvailableName();
            string typeName = type.GetDevelopName();


            ClassBuilder builder = new ClassBuilder();
            StringBuilder body = new StringBuilder();

            Dictionary<Type, List<string>> memberCache = new Dictionary<Type, List<string>>();

            var fields = type.GetFields();
            for (int i = 0; i < fields.Length; i += 1)
            {
                if (!memberCache.ContainsKey(fields[i].FieldType))
                {
                    memberCache[fields[i].FieldType] = new List<string>();
                }
                memberCache[fields[i].FieldType].Add(fields[i].Name);
            }

            var props = type.GetProperties();
            for (int i = 0; i < props.Length; i += 1)
            {
                if (!memberCache.ContainsKey(props[i].PropertyType))
                {
                    memberCache[props[i].PropertyType] = new List<string>();
                }
                memberCache[props[i].PropertyType].Add(props[i].Name);
            }





            body.AppendLine("public override T Get<T>(string name){");
            body.Append("var targetType = typeof(T);");
            int indexType = 0;
            foreach (var item in memberCache)
            {
                if (indexType != 0)
                {
                    body.Append("else ");
                }
                body.Append($"if(targetType==typeof({item.Key.GetDevelopName()})){{");
                int indexName = 0;
                foreach (var name in item.Value)
                {
                    if (indexName != 0)
                    {
                        body.Append("else ");
                    }
                    body.Append($"if( name == \"{name}\"){{");
                    body.Append($"return (T)((object){typeName}.{name});");
                    body.Append("}");
                    indexName++;
                }
                body.Append("}");
                indexType++;
            }
            body.Append("return default;}");


            body.AppendLine("public override T Get<T>(){");
            body.Append("var targetType = typeof(T);");
            indexType = 0;
            foreach (var item in memberCache)
            {
                if (indexType != 0)
                {
                    body.Append("else ");
                }
                body.Append($"if(targetType==typeof({item.Key.GetDevelopName()})){{");
                int indexName = 0;
                foreach (var name in item.Value)
                {
                    if (indexName != 0)
                    {
                        body.Append("else ");
                    }
                    body.Append($"if( _current_name == \"{name}\"){{");
                    body.Append($"return (T)((object){typeName}.{name});");
                    body.Append("}");
                    indexName++;
                }
                body.Append("}");
                indexType++;
            }
            body.Append("return default;}");


            body.AppendLine("public override void Set(string name,object value){");

            foreach (var item in memberCache)
            {
                int indexName = 0;
                foreach (var name in item.Value)
                {
                    if (indexName != 0)
                    {
                        body.Append("else ");
                    }
                    body.Append($"if( name == \"{name}\"){{");
                    body.Append($"{typeName}.{name}=({item.Key.GetDevelopName()})value;");
                    body.Append("}");
                    indexName++;
                }
            }
            body.Append("}");

            body.AppendLine("public override void Set(object value){");
            foreach (var item in memberCache)
            {
                int indexName = 0;
                foreach (var name in item.Value)
                {
                    if (indexName != 0)
                    {
                        body.Append("else ");
                    }
                    body.Append($"if( _current_name == \"{name}\"){{");
                    body.Append($"{typeName}.{name}=({item.Key.GetDevelopName()})value;");
                    body.Append("}");
                    indexName++;
                }
            }
            body.Append("}");


            body.AppendLine("public override CallerBase Get(string name){");

            foreach (var item in memberCache)
            {
                int indexName = 0;
                if (!item.Key.IsOnceType())
                {
                    foreach (var name in item.Value)
                    {
                        if (indexName != 0)
                        {
                            body.Append("else ");
                        }
                        body.Append($"if( name == \"{name}\"){{");
                        body.Append($"   return {typeName}.{name}.Caller();");
                        body.Append("}");
                        indexName++;
                    }
                }
            }
            body.Append("return null;}");


            Type tempClass = builder
                    .Using(type)
                    .Using("System")
                    .Using("NCaller")
                    .Using("System.Collections.Concurrent")
                    .ClassAccess(AccessTypes.Public)
                    .ClassName(className)
                    .Namespace("NatashaDynamic")
                    .Inheritance(typeof(CallerBase<>).With(type))
                    .ClassBody(body)
                    .GetType();

            return TypeCreatorMapping[type] = (Func<CallerBase>)CtorBuilder.NewDelegate(tempClass);
        }
    }
}
