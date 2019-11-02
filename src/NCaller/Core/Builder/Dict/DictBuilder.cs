using BTFindTree;
using Natasha;
using Natasha.Operator;
using NCaller.Core.Model;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;


namespace NCaller.Builder
{

    public class DictBuilder
    {

        public static Type InitType(Type type, FindTreeType kind = FindTreeType.Hash)
        {

            bool isStatic = (type.IsSealed && type.IsAbstract);
            Type callType = typeof(DictBase);


            StringBuilder body = new StringBuilder();
            OopOperator builder = new OopOperator();
            var cache  = NBuildInfo.GetInfos(type);

            var setByObjectCache = new Dictionary<string, string>();
            var getByObjectCache = new Dictionary<string, string>();
            var getByStrongTypeCache = new Dictionary<string, string>();
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


                    if (info.CanWrite)
                    {
                        setByObjectCache[info.MemberName] = $"{caller}.{info.MemberName} = ({info.MemberTypeName})value;";
                    }

                    if (info.CanRead)
                    {
                        getByObjectCache[info.MemberName] = $"return {caller}.{info.MemberName};";
                        getByStrongTypeCache[info.MemberName] = $"return (T)(object)({caller}.{info.MemberName});";
                    }
                }

            }

            string setObjectBody = default;
            string getObjectBody = default;
            string getStrongTypeBody = default;

            switch (kind)
            {
                case FindTreeType.Fuzzy:
                    setObjectBody = BTFTemplate.GetFuzzyPointBTFScript(setByObjectCache,"name");
                    getObjectBody = BTFTemplate.GetFuzzyPointBTFScript(getByObjectCache, "name");
                    getStrongTypeBody = BTFTemplate.GetFuzzyPointBTFScript(getByStrongTypeCache, "name");
                    break;
                case FindTreeType.Hash:
                    setObjectBody = BTFTemplate.GetHashBTFScript(setByObjectCache, "name");
                    getObjectBody = BTFTemplate.GetHashBTFScript(getByObjectCache, "name");
                    getStrongTypeBody = BTFTemplate.GetHashBTFScript(getByStrongTypeCache, "name");
                    break;
                case FindTreeType.Precision:
                    setObjectBody = BTFTemplate.GetPrecisionPointBTFScript(setByObjectCache, "name");
                    getObjectBody = BTFTemplate.GetPrecisionPointBTFScript(getByObjectCache, "name");
                    getStrongTypeBody = BTFTemplate.GetPrecisionPointBTFScript(getByStrongTypeCache, "name");
                    break;
                default:
                    break;
            }


            body.AppendLine("public unsafe override void Set(string name,object value){");
            body.AppendLine(setObjectBody);
            body.Append('}');


            body.AppendLine("public unsafe override T Get<T>(string name){");
            body.AppendLine(getStrongTypeBody);
            body.Append("return default;}");


            body.AppendLine("public unsafe override object GetObject(string name){");
            body.AppendLine(getObjectBody);
            body.Append("return default;}");


            if (!isStatic)
            {
                callType = typeof(DictBase<>).With(type);
                body.Append($@"public override void New(){{ Instance = new {type.GetDevelopName()}();}}");
            }


            Type tempClass = builder.Public
                    .Using(type)
                    .Using("System")
                    .Using("NCaller")
                    .Namespace("NCallerDynamic")
                    .Inheritance(callType)
                    .OopBody(body)
                    .GetType();


            return tempClass;

        }

    }
}

