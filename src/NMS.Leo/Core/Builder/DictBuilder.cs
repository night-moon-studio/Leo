using BTFindTree;
using System.Reflection;
using System.Text;
using NMS.Leo.Metadata;
using Natasha.CSharp.Compiler;

// ReSharper disable once CheckNamespace
namespace NMS.Leo.Builder;

public class DictBuilder
{
    public static Type InitType(Type type, AlgorithmKind kind = AlgorithmKind.Hash)
    {
        if (!LeoMemberAccessibilityMan._accessCache.TryGetValue(type, out var accessLevel))
        {
            accessLevel = LeoMemberAccessibilityLevel.Default;
        }

        var isStatic = type.IsSealed && type.IsAbstract;
        var callType = typeof(DictBase);

        var body = new StringBuilder();
        var setByObjectCache = new Dictionary<string, string>();
        var getByObjectCache = new Dictionary<string, string>();
        var getByStrongTypeCache = new Dictionary<string, string>();
        var getByLeoMembersCache = new Dictionary<string, LeoMember>();
        var getByLeoMembersScriptCache = new Dictionary<string, string>();

        var getByReadOnlyStaticScriptBuilder = new StringBuilder();
        var getByReadOnlySettingScriptBuilder = new StringBuilder();
        var getByInternalNamesScriptBuilder = new StringBuilder();

        #region Field

        var flag = BindingFlags.Static | BindingFlags.Instance | BindingFlags.Public;
        if (accessLevel == LeoMemberAccessibilityLevel.AllowNoPublic)
            flag |= BindingFlags.NonPublic;

        var fields = type.GetFields(flag);
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
                    fieldScript = fieldScript.ToReadonlyScript();
                }

                setByObjectCache[fieldName] = $"{fieldScript} = ({fieldType})value;";
            }


            //get
            getByObjectCache[fieldName] = $"return {caller}.{fieldName};";
            getByStrongTypeCache[fieldName] = $"return (T)(object)({caller}.{fieldName});";

            //member metadata
            getByLeoMembersCache[fieldName] = field;
            getByLeoMembersScriptCache[fieldName] = $"return __metadata_LeoMember_{fieldName};";
            getByReadOnlyStaticScriptBuilder.AppendLine($@"private static readonly LeoMember __metadata_LeoMember_{fieldName};");
            getByInternalNamesScriptBuilder.Append($@"""{fieldName}"",");
            getByReadOnlySettingScriptBuilder.Append($"__metadata_LeoMember_{fieldName}".ToReadonlyScript());
            getByReadOnlySettingScriptBuilder.Append($@" = leoMembersCache[""{fieldName}""];");
        }

        #endregion

        #region Property

        var props = type.GetProperties(flag);
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
            getByLeoMembersScriptCache[propertyName] = $"return __metadata_LeoMember_{propertyName};";
            getByReadOnlyStaticScriptBuilder.AppendLine($@"private static readonly LeoMember __metadata_LeoMember_{propertyName};");
            getByInternalNamesScriptBuilder.Append($@"""{propertyName}"",");
            getByReadOnlySettingScriptBuilder.Append($"__metadata_LeoMember_{propertyName}".ToReadonlyScript());
            getByReadOnlySettingScriptBuilder.Append($@" = leoMembersCache[""{propertyName}""];");
        }

        #endregion

        string setObjectBody = default;
        string getObjectBody = default;
        string getStrongTypeBody = default;
        string getLeoMemberBody = default;

        switch (kind)
        {
            case AlgorithmKind.Fuzzy:
                setObjectBody = BTFTemplate.GetGroupFuzzyPointBTFScript(setByObjectCache, "name");
                getObjectBody = BTFTemplate.GetGroupFuzzyPointBTFScript(getByObjectCache, "name");
                getStrongTypeBody = BTFTemplate.GetGroupFuzzyPointBTFScript(getByStrongTypeCache, "name");
                getLeoMemberBody = BTFTemplate.GetGroupFuzzyPointBTFScript(getByLeoMembersScriptCache, "name");
                break;
            case AlgorithmKind.Hash:
                setObjectBody = BTFTemplate.GetHashBTFScript(setByObjectCache, "name");
                getObjectBody = BTFTemplate.GetHashBTFScript(getByObjectCache, "name");
                getStrongTypeBody = BTFTemplate.GetHashBTFScript(getByStrongTypeCache, "name");
                getLeoMemberBody = BTFTemplate.GetHashBTFScript(getByLeoMembersScriptCache, "name");
                break;
            case AlgorithmKind.Precision:
                setObjectBody = BTFTemplate.GetGroupPrecisionPointBTFScript(setByObjectCache, "name");
                getObjectBody = BTFTemplate.GetGroupPrecisionPointBTFScript(getByObjectCache, "name");
                getStrongTypeBody = BTFTemplate.GetGroupPrecisionPointBTFScript(getByStrongTypeCache, "name");
                getLeoMemberBody = BTFTemplate.GetGroupPrecisionPointBTFScript(getByLeoMembersScriptCache, "name");
                break;
        }


        //To add readonly metadata (LeoMember) properties.
        body.AppendLine(getByReadOnlyStaticScriptBuilder.ToString());


        body.AppendLine("public unsafe override void Set(string name,object value){");
        body.AppendLine(setObjectBody);
        body.Append('}');

#if NET5_0_OR_GREATER
        body.AppendLine("[SkipLocalsInit]");
#endif
        body.AppendLine("public unsafe override T Get<T>(string name){");
        body.AppendLine(getStrongTypeBody);
        body.Append("return default;}");

#if NET5_0_OR_GREATER
        body.AppendLine("[SkipLocalsInit]");
#endif
        body.AppendLine("public unsafe override object GetObject(string name){");
        body.AppendLine(getObjectBody);
        body.Append("return default;}");


        body.AppendLine("public unsafe override LeoMember GetMember(string name){");
        body.AppendLine(getLeoMemberBody);
        body.Append("return default;}");


        body.AppendLine("protected override HashSet<string> InternalMemberNames { get; } = new HashSet<string>(){");
        body.AppendLine(getByInternalNamesScriptBuilder.ToString());
        body.Append("};");


        body.AppendLine("public static void InitMetadataMapping(Dictionary<string, LeoMember> leoMembersCache){");
        body.AppendLine(getByReadOnlySettingScriptBuilder.ToString());
        body.Append('}');


        if (!isStatic)
        {
            callType = typeof(DictBase<>).With(type);
            body.Append($@"public override void New(){{ Instance = new {type.GetDevelopName()}();}}");
        }
        else
        {
            body.Append($@"public override void SetObjInstance(object obj){{ }}");
        }


        var classBuilder = NClass.UseDomain(type.GetDomain())
                                 .Public()
                                 .Using(type)
                                 .Namespace("NMS.Leo.NCallerDynamic")
                                 .InheritanceAppend(callType)
                                 .Body(body.ToString());

        if (accessLevel == LeoMemberAccessibilityLevel.AllowNoPublic)
        {
            classBuilder.AllowPrivate(type.Assembly);
            classBuilder.AssemblyBuilder.ConfigCompilerOption(item => item.SetCompilerFlag(CompilerBinderFlags.IgnoreCorLibraryDuplicatedTypes | CompilerBinderFlags.IgnoreAccessibility));
        }
        else
        {
            classBuilder.AssemblyBuilder.ConfigCompilerOption(item => item.RemoveIgnoreAccessibility());
        }

        var tempClass = classBuilder.GetType();
        InitMetadataMappingCaller(tempClass)(getByLeoMembersCache);

        return tempClass;
    }

    private static Action<Dictionary<string, LeoMember>> InitMetadataMappingCaller(Type runtimeProxyType)
    {
        return NDelegate
               .UseDomain(runtimeProxyType.GetDomain())
               .Action<Dictionary<string, LeoMember>>($"{runtimeProxyType.GetDevelopName()}.InitMetadataMapping(obj);");
    }
}