using NCaller.Core;
using System.Reflection;

namespace NCaller.ExtensionAPI.Reflect
{
    public static class MemberInfoExtension
    {
        public static BuilderModel GetBuilderInfo(this MemberInfo info)
        {
            BuilderModel model = new BuilderModel();
            model.SetName(info.Name);
            switch (info.MemberType)
            {
                case MemberTypes.Field:
                    model.SetType(((FieldInfo)info).FieldType);
                    break;
                case MemberTypes.Property:
                    model.SetType(((PropertyInfo)info).PropertyType);
                    break;
                default:
                    break;
            }
            return model;
        }

    }
}
