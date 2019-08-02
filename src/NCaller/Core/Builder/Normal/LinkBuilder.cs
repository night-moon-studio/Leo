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

    public class LinkBuilder<T>
    {
        public static readonly Func<LinkBase> Ctor;
        static LinkBuilder() => Ctor = LinkBuilder.InitType(typeof(T));
    }




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
            ClassBuilder builder = new ClassBuilder();


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
                    .ClassAccess(AccessTypes.Public)
                    .Inheritance(callType)
                    .ClassName("NatashaDynamicLink" + type.GetAvailableName())
                    .Namespace("NCallerDynamic")
                    .ClassBody(body)
                    .GetType();


            return (Func<LinkBase>)CtorBuilder.NewDelegate(tempClass);

        }

    }
}

