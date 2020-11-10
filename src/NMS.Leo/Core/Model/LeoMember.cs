using System;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace NMS.Leo
{
    public class LeoMember
    {

        public readonly bool CanWrite;
        public readonly bool CanRead;
        public readonly bool IsReadOnly;
        public readonly bool IsAsync;
        public readonly bool IsConst;
        public readonly bool IsArray;
        public readonly bool IsStatic;
        public readonly bool IsAbstract;
        public readonly bool IsVirtual;
        public readonly bool IsInterface;
        public readonly bool IsOverride;
        public readonly bool IsNew;
        public readonly bool IsOverrided;
        public readonly string MemberName;
        public readonly Type MemberType;
        public readonly Type ElementType;
        public readonly int ArrayLayer;
        public readonly int ArrayDimensions;
        public LeoMember(
            bool canWrite,
            bool canRead,
            bool isConst,
            string name,
            Type type,
            bool isStatic,
            bool isAsync,
            bool isAbstract,
            bool isVirtural,
            bool isNew,
            bool isOverride,
            bool isReadonly
            )

        {

            CanWrite = canWrite;
            CanRead = canRead;
            IsConst = isConst;
            IsStatic = isStatic;
            IsAsync = isAsync;
            IsAbstract = isAbstract;
            IsVirtual = isVirtural;
            IsOverride = isOverride;
            IsNew = isNew;
            MemberName = name;
            MemberType = type;
            IsArray = ElementType.IsArray;
            IsInterface = type.IsInterface;
            if (IsArray)
            {

                while (ElementType.HasElementType)
                {

                    ArrayLayer += 1;
                    ElementType = ElementType.GetElementType();

                }
                ArrayDimensions = type.GetConstructors()[0].GetParameters().Length;

            }
            IsReadOnly = isReadonly;

        }


        public static implicit operator LeoMember(FieldInfo info)
        {

            var isNew = false;
            var baseType = info.DeclaringType.BaseType;
            if (baseType != null && baseType != typeof(object))
            {
                isNew = baseType.GetField(info.Name) != null;
            }

            return new LeoMember(
               !info.IsInitOnly,
               !info.IsPrivate,
               info.IsLiteral,
               info.Name,
               info.FieldType,
               info.IsStatic, false, false, false,
               isNew, false, info.IsInitOnly);

        }


        public static implicit operator LeoMember(PropertyInfo info)
        {
            var (isAsync, isStatic, isAbstract, isVirtual, isNew, isOverride) = info.CanRead ? GetMethodInfo(info.GetGetMethod()) : GetMethodInfo(info.GetSetMethod());
            return new LeoMember(
                info.CanWrite,
                info.CanRead,
                info.CanRead && !info.CanWrite,
                info.Name,
                info.PropertyType,
                isStatic,
                isAsync,
                isAbstract,
                isVirtual,
                isNew,
                isOverride,
                false);

        }


        public static implicit operator LeoMember(MethodInfo info)
        {
            var (isAsync, isStatic, isAbstract, isVirtual, isNew, isOverride) = GetMethodInfo(info);
            return new LeoMember(
                false,
                true,
                false,
                info.Name,
                info.ReturnType,
                isStatic,
                isAsync,
                isAbstract,
                isVirtual,
                isNew,
                isOverride,
                false);

        }


        public static (bool isAsync,
            bool isStatic,
            bool isAbstract,
            bool isVirtual,
            bool isNew,
            bool isOverride
            ) GetMethodInfo(MethodInfo info)
        {
            var isAsync = info.GetCustomAttribute(typeof(AsyncStateMachineAttribute)) != null;
            bool isStatic = info.IsStatic;
            bool isAbstract = false;
            bool isVirtual = false;
            bool isNew = false;
            bool isOverride = false;
            if (!info.DeclaringType.IsInterface && !isStatic)
            {

                //如果没有被重写
                if (info.Equals(info.GetBaseDefinition()))
                {

                    if (info.IsAbstract)
                    {
                        isAbstract = true;
                    }
                    else if (!info.IsFinal && info.IsVirtual)
                    {
                        isVirtual = true;
                    }
                    else
                    {

                        var baseType = info.DeclaringType.BaseType;
                        if (baseType != null && baseType != typeof(object))
                        {
                            var baseInfo = info
                            .DeclaringType
                            .BaseType
                            .GetMethod(info.Name,
                            BindingFlags.Public
                            | BindingFlags.Instance
                            | BindingFlags.NonPublic);
                            if (info != baseInfo)
                            {
                                isNew = true;
                            }
                        }

                    }

                }
                else
                {
                    isOverride = true;
                }

            }

            return (isAsync, isStatic, isAbstract, isVirtual, isNew, isOverride);
        }

    }
}
