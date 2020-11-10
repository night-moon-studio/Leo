using BTFindTree;
using Natasha.CSharp;
using NMS.Leo.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;


namespace NMS.Leo.Builder
{
    public class DictBuilder
    {
        public static Type InitType(Type type, FindTreeType kind = FindTreeType.Hash)
        {
            bool isStatic = (type.IsSealed && type.IsAbstract);
            Type callType = typeof(DictBase);

            StringBuilder body = new StringBuilder();
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

                string caller = "Instance";
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

                string caller = "Instance";
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
                case FindTreeType.Fuzzy:
                    setObjectBody = BTFTemplate.GetFuzzyPointBTFScript(setByObjectCache, "name");
                    getObjectBody = BTFTemplate.GetFuzzyPointBTFScript(getByObjectCache, "name");
                    getStrongTypeBody = BTFTemplate.GetFuzzyPointBTFScript(getByStrongTypeCache, "name");
                    break;
                case FindTreeType.Hash:
                    setObjectBody = BTFTemplate.GetHashBTFScript(setByObjectCache, "name");
                    getObjectBody = BTFTemplate.GetHashBTFScript(getByObjectCache, "name");
                    getStrongTypeBody = BTFTemplate.GetHashBTFScript(getByStrongTypeCache, "name");
                    break;
                case FindTreeType.Precision:
                    setObjectBody = BTFTemplate.GetGroupPrecisionPointBTFScript(setByObjectCache, "name");
                    getObjectBody = BTFTemplate.GetGroupPrecisionPointBTFScript(getByObjectCache, "name");
                    getStrongTypeBody = BTFTemplate.GetGroupPrecisionPointBTFScript(getByStrongTypeCache, "name");
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

            Type tempClass = NClass.UseDomain(type.GetDomain())
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

    internal static class LeoMemberExtensions
    {
        public static StringBuilder ToLeoMemberMetadataScript(this Dictionary<string, LeoMember> leoMembersCache)
        {
            var builder = new StringBuilder();

            /*
             * 样本：
             *
        protected virtual Dictionary<string, LeoMember> InternalMembersMetadata { get; } = new Dictionary<string, LeoMember>()
        {
            {"123", new LeoMember(true, true, true, "", typeof(string), true, true, true, true, true, true, true)},
            {"123", new LeoMember(true, true, true, "", typeof(string), true, true, true, true, true, true, true)},
            {"123", new LeoMember(true, true, true, "", typeof(string), true, true, true, true, true, true, true)},
            {"123", new LeoMember(true, true, true, "", typeof(string), true, true, true, true, true, true, true)},
        };
             *
             * */

            if (leoMembersCache.Any())
            {
                var leoMemberDevelopName = typeof(LeoMember).GetDevelopName();
                foreach (var member in leoMembersCache)
                {
                    var v = member.Value;
                    builder.Append(
                        $@"{{ ""{member.Key}"", new {leoMemberDevelopName}({v.CanWrite.L()},{v.CanRead.L()},{v.IsConst.L()},""{v.MemberName}"",typeof({v.MemberType.GetDevelopName()}),{v.IsStatic.L()},{v.IsAsync.L()},{v.IsAbstract.L()},{v.IsVirtual.L()},{v.IsNew.L()},{v.IsOverride.L()},{v.IsReadOnly.L()}) }},");
                }
            }

            return builder;
        }

        private static string L(this bool boolean)
        {
            return boolean ? "true" : "false";
        }
    }
}