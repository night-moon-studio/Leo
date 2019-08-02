using Natasha;
using System;
using System.Reflection;


namespace NCaller.Core
{

    public class BuilderModel:IComparable<BuilderModel>
    {

        public bool IsStatic;
        public string DeclareTypeName;
        public static implicit operator BuilderModel(MemberInfo info)
        {

            BuilderModel model = new BuilderModel();
            model.SetName(info.Name);
            
            switch (info.MemberType)
            {

                case MemberTypes.Field:

                    var fldInfo = (FieldInfo)info;
                    model.DeclareTypeName = fldInfo.DeclaringType.GetDevelopName();
                    model.SetType(fldInfo.FieldType);
                    model.IsStatic = fldInfo.Attributes.HasFlag(FieldAttributes.Static);
                    break;


                case MemberTypes.Property:

                    var propInfo = (PropertyInfo)info;
                    model.DeclareTypeName = propInfo.DeclaringType.GetDevelopName();
                    model.SetType(propInfo.PropertyType);
                    model.IsStatic = propInfo.GetGetMethod(true).IsStatic;
                    break;


                default:
                    break;

            }


            return model;

        }



       
        public Type MemberType;
        public int TypeHashCode;
        public string TypeName;


        public void SetType(Type type)
        {

            MemberType = type;
            TypeName = type.GetDevelopName();
            TypeHashCode = type.GetHashCode();

        }




        public string MemberName;
        public int NameHashCode;


        public void SetName(string name)
        {

            MemberName = name;
            NameHashCode = name.GetHashCode();

        }




        public override int GetHashCode()
        {
            return NameHashCode;
        }




        public int CompareTo(BuilderModel other)
        {
            return NameHashCode.CompareTo(other.NameHashCode);
        }

    }
    
}
