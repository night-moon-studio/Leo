using BTFindTree;
using Natasha;
using Natasha.Operator;
using NCaller.Core.Model;
using System;
using System.Collections.Generic;
using System.Text;


namespace NCaller.Builder
{

    public class LinkBuilder
    {

        public static Func<LinkBase> InitType(Type type, FindTreeType kind = FindTreeType.Hash)
        {

            bool isStatic = (type.IsSealed && type.IsAbstract);
            Type callType = typeof(LinkBase);


            StringBuilder body = new StringBuilder();
            OopOperator builder = new OopOperator();


            var cache = NBuildInfo.GetInfos(type);

            var setByObjectCache = new Dictionary<string, string>();
            var getIndexBodyCache = new Dictionary<string, string>();
            var getByStrongTypeCache = new Dictionary<string, string>();
            var getLinkBaseBodyCache = new Dictionary<string, string>();

            foreach (var item in cache)
            {
                var info = item.Value;
                string caller = "Instance";
                if (info != null)
                {

                    if (info.IsStatic)
                    {
                        caller = type.GetDevelopName();
                    }


                    if (info.CanRead)
                    {
                        getIndexBodyCache[info.MemberName] = $"return (T)(object)({caller}.{info.MemberName});";
                        getByStrongTypeCache[info.MemberName] = $"return (T)(object)({caller}.{info.MemberName});";
                        getLinkBaseBodyCache[info.MemberName] = $"return {caller}.{info.MemberName}.LinkCaller();";
                    }


                    if (info.CanWrite)
                    {
                        setByObjectCache[info.MemberName] = $"{caller}.{info.MemberName} = ({info.MemberTypeName})value;";
                    }
                    
                }
            }

            string setObjectBody = default;
            string getStrongTypeBody = default;
            string setIndexBody = default;
            string getIndexBody = default;
            string getLinkBaseBody = default;

            switch (kind)
            {
                case FindTreeType.Fuzzy:
                    setObjectBody = BTFTemplate.GetFuzzyPointBTFScript(setByObjectCache, "name");
                    setIndexBody = BTFTemplate.GetFuzzyPointBTFScript(setByObjectCache, "_name");
                    getIndexBody = BTFTemplate.GetFuzzyPointBTFScript(getIndexBodyCache, "_name");
                    getStrongTypeBody = BTFTemplate.GetFuzzyPointBTFScript(getByStrongTypeCache, "name");
                    getLinkBaseBody = BTFTemplate.GetFuzzyPointBTFScript(getLinkBaseBodyCache, "name");
                    break;
                case FindTreeType.Hash:
                    setObjectBody = BTFTemplate.GetHashBTFScript(setByObjectCache, "name");
                    setIndexBody = BTFTemplate.GetHashBTFScript(setByObjectCache, "_name");
                    getIndexBody = BTFTemplate.GetHashBTFScript(getIndexBodyCache, "_name");
                    getStrongTypeBody = BTFTemplate.GetHashBTFScript(getByStrongTypeCache, "name");
                    getLinkBaseBody = BTFTemplate.GetHashBTFScript(getLinkBaseBodyCache, "name");
                    break;
                case FindTreeType.Precision:
                    setObjectBody = BTFTemplate.GetPrecisionPointBTFScript(setByObjectCache, "name");
                    setIndexBody = BTFTemplate.GetPrecisionPointBTFScript(setByObjectCache, "_name");
                    getIndexBody = BTFTemplate.GetPrecisionPointBTFScript(getIndexBodyCache, "_name");
                    getStrongTypeBody = BTFTemplate.GetPrecisionPointBTFScript(getByStrongTypeCache, "name");
                    getLinkBaseBody = BTFTemplate.GetPrecisionPointBTFScript(getLinkBaseBodyCache, "name");
                    break;
                default:
                    break;
            }


            body.AppendLine("public unsafe override LinkBase Get(string name){");
            body.AppendLine(getLinkBaseBody);
            body.Append("return default;}");



            body.AppendLine("public unsafe override void Set(string name,object value){");
            body.AppendLine(setObjectBody);
            body.Append('}');


            body.AppendLine("public unsafe override T Get<T>(string name){");
            body.AppendLine(getStrongTypeBody);
            body.Append("return default;}");


            body.AppendLine("public unsafe override T Get<T>(){");
            body.AppendLine(getIndexBody);
            body.Append("return default;}");


            body.AppendLine("public unsafe override void Set(object value){");
            body.AppendLine(setIndexBody);
            body.Append("}");


            if (!isStatic)
            {
                callType = typeof(LinkBase<>).With(type);
                body.Append($@"public override void New(){{ Instance = new {type.GetDevelopName()}();}}");
            }



            Type tempClass = builder.Public
                    .Using(type)
                    .Using("System")
                    .Using("NCaller")
                    .Inheritance(callType)
                    .OopName("NatashaDynamicLink" + type.GetAvailableName())
                    .Namespace("NCallerDynamic")
                    .OopBody(body)
                    .GetType();


            return (Func<LinkBase>)CtorOperator.NewDelegate(tempClass);

        }

    }
}

