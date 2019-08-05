using Natasha;
using NCaller.Core;
using NCaller.ExtensionAPI.Array;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;


namespace NCaller.Builder
{

    public class DictBuilder
    {

        public static readonly ConcurrentDictionary<Type, Func<DictBase>> TypeCreatorMapping;
        static DictBuilder() => TypeCreatorMapping = new ConcurrentDictionary<Type, Func<DictBase>>();


        public static DictBase Ctor(Type type)
        {

            if (!TypeCreatorMapping.ContainsKey(type))
            {

                InitType(type);

            }


            return TypeCreatorMapping[type]();

        }




        public static Func<DictBase> InitType(Type type)
        {

            bool isStatic = (type.IsSealed && type.IsAbstract);
            Type callType = typeof(DictBase);


            StringBuilder body = new StringBuilder();
            OopBuilder builder = new OopBuilder();
           

            var fields = type.GetFields();
            var props = type.GetProperties();
            List<BuilderModel> buildCache = new List<BuilderModel>(fields.Length + props.Length);
            

            fields.For(item => buildCache.Add(item));
            props.For(item => buildCache.Add(item));
            buildCache.Sort();


            CallerActionBuilder callerBuilder = new CallerActionBuilder(buildCache);
            body.Append(callerBuilder.GetScript_SetByName());
            body.Append(callerBuilder.GetScript_GetByName());
            body.Append(callerBuilder.GetScript_GetObjectByName());


            if (!isStatic)
            {
                callType = typeof(DictBase<>).With(type);
                body.Append($@"public override void New(){{{callerBuilder.Caller} = new {type.GetDevelopName()}();}}");
            }


            Type tempClass = builder
                    .Using(type)
                    .Using("System")
                    .Using("NCaller")
                    .OopAccess(AccessTypes.Public)
                    .OopName("NatashaDynamicDict" + type.GetAvailableName())
                    .Namespace("NCallerDynamic")
                    .Inheritance(callType)
                    .OopBody(body)
                    .GetType();


            return TypeCreatorMapping[type] = (Func<DictBase>)CtorOperator.NewDelegate(tempClass);

        }

    }
}

