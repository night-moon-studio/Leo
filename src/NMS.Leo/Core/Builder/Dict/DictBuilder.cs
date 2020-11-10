using BTFindTree;
using Natasha.CSharp;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;


namespace NMS.Leo.Builder
{
    public class DictBuilder
    {
        public static Type InitType(Type type, AlgorithmKind kind = AlgorithmKind.Hash)
        {
            var isStatic = type.IsSealed && type.IsAbstract;
            var callType = typeof(DictBase);

            var body = new StringBuilder();
            var setByObjectCache = new Dictionary<string, string>();
            var getByObjectCache = new Dictionary<string, string>();
            var getByStrongTypeCache = new Dictionary<string, string>();
            var getByLeoMembersCache = new Dictionary<string, LeoMember>();

            #region Field

            var fields = type.GetFields(BindingFlags.Static | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            foreach (var field in fields)
            {
                if (field.IsSpecialName || field.Name.Contains("<"))
                {
                    continue;
                }

                var caller = "Instance";
                
                if (field.IsStatic)
                {
                    caller = type.GetDevelopName();
                }

                var fieldName = field.Name;
                var fieldType = field.FieldType.GetDevelopName();

                //set
                if (!field.IsLiteral)
                {
                    var fieldScript = $"{caller}.{fieldName}";
                    
                    if (field.IsInitOnly)
                    {
                        fieldScript = fieldScript.ReadonlyScript();
                    }

                    setByObjectCache[fieldName] = $"{fieldScript} = ({fieldType})value;";
                }


                //get
                getByObjectCache[fieldName] = $"return {caller}.{fieldName};";
                getByStrongTypeCache[fieldName] = $"return (T)(object)({caller}.{fieldName});";

                //member metadata
                getByLeoMembersCache[fieldName] = field;
            }

            #endregion

            #region Property

            var props = type.GetProperties(BindingFlags.Static | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            foreach (var property in props)
            {
                var method = property.CanRead ? property.GetGetMethod(true) : property.GetSetMethod(true);

                var caller = "Instance";
                
                if (method.IsStatic)
                {
                    caller = type.GetDevelopName();
                }

                var propertyName = property.Name;
                var propertyType = property.PropertyType.GetDevelopName();
                var propertyScript = $"{caller}.{propertyName}";

                //set
                if (property.CanWrite)
                {
                    setByObjectCache[propertyName] = $"{propertyScript} = ({propertyType})value;";
                }


                //get
                if (property.CanRead)
                {
                    getByObjectCache[propertyName] = $"return {caller}.{propertyName};";
                    getByStrongTypeCache[propertyName] = $"return (T)(object)({caller}.{propertyName});";
                }

                //member metadata
                getByLeoMembersCache[propertyName] = property;
            }

            #endregion

            string setObjectBody = default;
            string getObjectBody = default;
            string getStrongTypeBody = default;

            switch (kind)
            {
                case AlgorithmKind.Fuzzy:
                    setObjectBody = BTFTemplate.GetFuzzyPointBTFScript(setByObjectCache, "name");
                    getObjectBody = BTFTemplate.GetFuzzyPointBTFScript(getByObjectCache, "name");
                    getStrongTypeBody = BTFTemplate.GetFuzzyPointBTFScript(getByStrongTypeCache, "name");
                    break;
                case AlgorithmKind.Hash:
                    setObjectBody = BTFTemplate.GetHashBTFScript(setByObjectCache, "name");
                    getObjectBody = BTFTemplate.GetHashBTFScript(getByObjectCache, "name");
                    getStrongTypeBody = BTFTemplate.GetHashBTFScript(getByStrongTypeCache, "name");
                    break;
                case AlgorithmKind.Precision:
                    setObjectBody = BTFTemplate.GetGroupPrecisionPointBTFScript(setByObjectCache, "name");
                    getObjectBody = BTFTemplate.GetGroupPrecisionPointBTFScript(getByObjectCache, "name");
                    getStrongTypeBody = BTFTemplate.GetGroupPrecisionPointBTFScript(getByStrongTypeCache, "name");
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
            else
            {
                body.Append($@"public override void SetObjInstance(object obj){{ }}");
            }

            var leoMemberMetadataScriptBuilder = getByLeoMembersCache.ToLeoMemberMetadataScript();

            if (leoMemberMetadataScriptBuilder.Length > 0)
            {
                body.Append($@"protected override Dictionary<string, LeoMember> InternalMembersMetadata {{ get; }} = new Dictionary<string, LeoMember>() {{ {leoMemberMetadataScriptBuilder} }};");
            }
            else
            {
                body.Append($@"protected override Dictionary<string, LeoMember> InternalMembersMetadata {{ get; }} = new Dictionary<string, LeoMember>();");
            }

            var tempClass = NClass.UseDomain(type.GetDomain())
                                  .Public()
                                  .Using(type)
                                  .AllowPrivate(type.Assembly)
                                  .Using("System")
                                  .Using("NMS.Leo")
                                  .UseRandomName()
                                  .Namespace("NMS.Leo.NCallerDynamic")
                                  .Inheritance(callType)
                                  .Body(body.ToString())
                                  .GetType();

            return tempClass;
        }
    }
}