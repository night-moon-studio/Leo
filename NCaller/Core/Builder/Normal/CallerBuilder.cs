using Natasha;
using NCaller.Core;
using NCaller.ExtensionAPI;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace NCaller.Builder
{
    public class CallerBuilder<T>
    {
        public static readonly Func<CallerBase> Ctor;
        static CallerBuilder() => Ctor = CallerBuilder.InitType(typeof(T));
    }


    public class CallerBuilder
    {

        public static readonly ConcurrentDictionary<Type, Func<CallerBase>> TypeCreatorMapping;
        static CallerBuilder()
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
            StringBuilder body = new StringBuilder();
            ClassBuilder builder = new ClassBuilder();
            CallerActionBuilder callerBuilder = new CallerActionBuilder();
           

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
            body.Append($@"public override void New(){{{callerBuilder.Caller} = new {type.GetDevelopName()}();}}");


            Type tempClass = builder
                    .Using(type)
                    .Using("System")
                    .Using("NCaller")
                    .ClassAccess(AccessTypes.Public)
                    .ClassName("NatashaDynamic" + type.GetAvailableName())
                    .Namespace("NCallerDynamic")
                    .Inheritance(typeof(CallerBase<>).With(type))
                    .ClassBody(body)
                    .GetType();


            return TypeCreatorMapping[type] = (Func<CallerBase>)CtorBuilder.NewDelegate(tempClass);
        }



    }
}

