using Natasha;
using NCaller.Core;
using NCaller.ExtensionAPI;
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
            CallerActionBuilder callerBuilder = new CallerActionBuilder(type.GetDevelopName());


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
