using Natasha;
using NCaller.Core;
using NCaller.ExtensionAPI.Array;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Reflection;
using System.Text;


namespace NCaller.Builder
{

    public class LinkBuilder
    {

        public static readonly ConcurrentDictionary<Type, Func<LinkBase>> TypeCreatorMapping;
        static LinkBuilder() => TypeCreatorMapping = new ConcurrentDictionary<Type, Func<LinkBase>>();


        public static LinkBase Ctor(Type type)
        {

            if (!TypeCreatorMapping.ContainsKey(type))
            {

                TypeCreatorMapping[type] = InitType(type);

            }


            return TypeCreatorMapping[type]();

        }




        public static Func<LinkBase> InitType(Type type)
        {

            bool isStatic = (type.IsSealed && type.IsAbstract);
            Type callType = typeof(LinkBase);


            StringBuilder body = new StringBuilder();
            OopBuilder builder = new OopBuilder();


            var fields = type.GetFields();
            var props = type.GetProperties();
            List<BuilderModel> buildCache = new List<BuilderModel>(fields.Length + props.Length);


            fields.For(item => buildCache.Add(item));
            props.For(item => buildCache.Add(item));
            buildCache.Sort();


            CallerActionBuilder callerBuilder = new CallerActionBuilder(buildCache);
            body.Append(callerBuilder.GetScript_GetDynamicBase());
            body.Append(callerBuilder.GetScript_GetByName());
            body.Append(callerBuilder.GetScript_GetByIndex());
            body.Append(callerBuilder.GetScript_SetByName());
            body.Append(callerBuilder.GetScript_SetByIndex());

            if (!isStatic)
            {
                callType = typeof(LinkBase<>).With(type);
                body.Append($@"public override void New(){{{callerBuilder.Caller} = new {type.GetDevelopName()}();}}");
            }
            


            Type tempClass = builder
                    .Using(type)
                    .Using("System")
                    .Using("NCaller")
                    .OopAccess(AccessTypes.Public)
                    .Inheritance(callType)
                    .OopName("NatashaDynamicLink" + type.GetAvailableName())
                    .Namespace("NCallerDynamic")
                    .OopBody(body)
                    .GetType();


            return (Func<LinkBase>)CtorOperator.NewDelegate(tempClass);

        }

    }
}

