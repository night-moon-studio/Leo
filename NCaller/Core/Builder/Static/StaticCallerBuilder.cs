using Natasha;
using NCaller.Core;
using NCaller.ExtensionAPI.Array;
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
            ClassBuilder builder = new ClassBuilder();
            StringBuilder body = new StringBuilder();
            

            var fields = type.GetFields();
            var props = type.GetProperties();
            List<BuilderModel> buildCache = new List<BuilderModel>(fields.Length + props.Length);
            

            fields.For(item => buildCache.Add(item));
            props.For(item => buildCache.Add(item));
            buildCache.Sort();


            CallerActionBuilder callerBuilder = new CallerActionBuilder(buildCache, type.GetDevelopName());
            body.Append(callerBuilder.GetScript_GetDynamicBase());
            body.Append(callerBuilder.GetScript_GetByName());
            body.Append(callerBuilder.GetScript_GetByIndex());
            body.Append(callerBuilder.GetScript_SetByName());
            body.Append(callerBuilder.GetScript_SetByIndex());


            Type tempClass = builder
                    .Using(type)
                    .Using("System")
                    .Using("NCaller")
                    .Using("System.Collections.Concurrent")
                    .ClassAccess(AccessTypes.Public)
                    .ClassName("NatashaDynamic" + type.GetAvailableName())
                    .Namespace("NCallerDynamic")
                    .Inheritance<CallerBase>()
                    .ClassBody(body)
                    .GetType();


            return TypeCreatorMapping[type] = (Func<CallerBase>)CtorBuilder.NewDelegate(tempClass);
        }
    }
}
