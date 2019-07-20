using Natasha;
using NCaller.Core;
using NCaller.ExtensionAPI;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace NCaller.Builder
{

    public class StaticCallerBuilder
    {
        public static readonly ConcurrentDictionary<Type, Func<CallerBase>> TypeCreatorMapping;
        static StaticCallerBuilder()
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
            CallerActionBuilder callerBuilder = new CallerActionBuilder(typeName);


            var fields = type.GetFields();
            var props = type.GetProperties();
            List<BuilderModel> buildCache = new List<BuilderModel>(fields.Length + props.Length);


            fields.For(item => buildCache.Add(item));
            props.For(item => buildCache.Add(item));


            body.Append(callerBuilder.GetScript_GetDynamicBase(buildCache));
            body.Append(callerBuilder.GetScript_GetByName(buildCache));
            body.Append(callerBuilder.GetScript_GetByIndex(buildCache));
            body.Append(callerBuilder.GetScript_SetByName(buildCache));
            body.Append(callerBuilder.GetScript_SetByIndex(buildCache));

            //Dictionary<Type, List<string>> memberCache = new Dictionary<Type, List<string>>();

            //var fields = type.GetFields();
            //for (int i = 0; i < fields.Length; i += 1)
            //{
            //    if (!memberCache.ContainsKey(fields[i].FieldType))
            //    {
            //        memberCache[fields[i].FieldType] = new List<string>();
            //    }
            //    memberCache[fields[i].FieldType].Add(fields[i].Name);
            //}

            //var props = type.GetProperties();
            //for (int i = 0; i < props.Length; i += 1)
            //{
            //    if (!memberCache.ContainsKey(props[i].PropertyType))
            //    {
            //        memberCache[props[i].PropertyType] = new List<string>();
            //    }
            //    memberCache[props[i].PropertyType].Add(props[i].Name);
            //}





            //body.AppendLine("public override T Get<T>(string name){");
            //body.Append("int typeCode = typeof(T).GetHashCode();");
            //body.Append("int nameCode = name.GetHashCode();");
            //int indexType = 0;
            //foreach (var item in memberCache)
            //{
            //    if (indexType != 0)
            //    {
            //        body.Append("else ");
            //    }
            //    body.Append($"if(typeCode=={item.Key.GetHashCode()}){{");
            //    int indexName = 0;
            //    foreach (var name in item.Value)
            //    {
            //        if (indexName != 0)
            //        {
            //            body.Append("else ");
            //        }
            //         body.Append($"if( nameCode == {name.GetHashCode()}){{");
            //        body.Append($"return (T)((object){typeName}.{name});");
            //        body.Append("}");
            //        indexName++;
            //    }
            //    body.Append("}");
            //    indexType++;
            //}
            //body.Append("return default;}");


            //body.AppendLine("public override T Get<T>(){");
            //body.Append("int typeCode = typeof(T).GetHashCode();");
            //body.Append("int nameCode = _current_name.GetHashCode();");
            //indexType = 0;
            //foreach (var item in memberCache)
            //{
            //    if (indexType != 0)
            //    {
            //        body.Append("else ");
            //    }
            //    body.Append($"if(typeCode=={item.Key.GetHashCode()}){{");
            //    int indexName = 0;
            //    foreach (var name in item.Value)
            //    {
            //        if (indexName != 0)
            //        {
            //            body.Append("else ");
            //        }
            //        body.Append($"if( nameCode == {name.GetHashCode()}){{");
            //        body.Append($"return (T)((object){typeName}.{name});");
            //        body.Append("}");
            //        indexName++;
            //    }
            //    body.Append("}");
            //    indexType++;
            //}
            //body.Append("return default;}");


            //body.AppendLine("public override void Set(string name,object value){");
            //body.Append("int nameCode = name.GetHashCode();");
            //foreach (var item in memberCache)
            //{
            //    int indexName = 0;
            //    foreach (var name in item.Value)
            //    {
            //        if (indexName != 0)
            //        {
            //            body.Append("else ");
            //        }
            //        body.Append($"if( nameCode == {name.GetHashCode()}){{");
            //        body.Append($"{typeName}.{name}=({item.Key.GetDevelopName()})value;");
            //        body.Append("}");
            //        indexName++;
            //    }
            //}
            //body.Append("}");

            //body.AppendLine("public override void Set(object value){");
            //body.Append("int nameCode = _current_name.GetHashCode();");
            //foreach (var item in memberCache)
            //{
            //    int indexName = 0;
            //    foreach (var name in item.Value)
            //    {
            //        if (indexName != 0)
            //        {
            //            body.Append("else ");
            //        }
            //        body.Append($"if( nameCode == {name.GetHashCode()}){{");
            //        body.Append($"{typeName}.{name}=({item.Key.GetDevelopName()})value;");
            //        body.Append("}");
            //        indexName++;
            //    }
            //}
            //body.Append("}");


            //body.AppendLine("public override CallerBase Get(string name){");
            //body.Append("int nameCode = name.GetHashCode();");
            //foreach (var item in memberCache)
            //{
            //    int indexName = 0;
            //    if (!item.Key.IsOnceType())
            //    {
            //        foreach (var name in item.Value)
            //        {
            //            if (indexName != 0)
            //            {
            //                body.Append("else ");
            //            }
            //             body.Append($"if( nameCode == {name.GetHashCode()}){{");
            //            body.Append($"   return {typeName}.{name}.Caller();");
            //            body.Append("}");
            //            indexName++;
            //        }
            //    }
            //}
            //body.Append("return null;}");


            Type tempClass = builder
                    .Using(type)
                    .Using("System")
                    .Using("NCaller")
                    .Using("System.Collections.Concurrent")
                    .ClassAccess(AccessTypes.Public)
                    .ClassName(className)
                    .Namespace("NCallerDynamic")
                    .Inheritance<CallerBase>()
                    .ClassBody(body)
                    .GetType();

            return TypeCreatorMapping[type] = (Func<CallerBase>)CtorBuilder.NewDelegate(tempClass);
        }
    }
}
