using System;
using System.Linq;
using System.Reflection;

/*
 * The code of this document comes from: COSMOSLOOPS/Standard
 * https://github.com/cosmos-loops/cosmos-standard
 * Author: Alex LEWIS
 */

namespace NMS.Leo.Typed.Core.Extensions
{
    internal static class TypeHelper
    {
        #region Enum Type

        /// <summary>
        /// Is enum type
        /// </summary>
        /// <param name="mayNullable"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static bool IsEnumType<T>(bool mayNullable = false) => mayNullable
            ? (Nullable.GetUnderlyingType(typeof(T)) ?? typeof(T)).IsEnum
            : typeof(T).IsEnum;

        /// <summary>
        /// Is enum type
        /// </summary>
        /// <param name="type"></param>
        /// <param name="mayNullable"></param>
        /// <returns></returns>
        public static bool IsEnumType(Type type, bool mayNullable = false) =>
            mayNullable
                ? (Nullable.GetUnderlyingType(type) ?? type).IsEnum
                : type.IsEnum;

        /// <summary>
        /// Is enum type
        /// </summary>
        /// <param name="typeInfo"></param>
        /// <param name="mayNullable"></param>
        /// <returns></returns>
        public static bool IsEnumType(TypeInfo typeInfo, bool mayNullable = false) =>
            mayNullable
                ? (Nullable.GetUnderlyingType(typeInfo) ?? typeInfo).IsEnum
                : typeInfo.IsEnum;

        #endregion

        #region Numeric Type

        /// <summary>
        /// Is numeric type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static bool IsNumericType<T>() => IsNumericType(typeof(T));

        /// <summary>
        /// Is numeric type
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsNumericType(Type type)
        {
            return type == typeof(byte)
                || type == typeof(short)
                || type == typeof(int)
                || type == typeof(long)
                || type == typeof(sbyte)
                || type == typeof(ushort)
                || type == typeof(uint)
                || type == typeof(ulong)
                || type == typeof(decimal)
                || type == typeof(double)
                || type == typeof(float);
        }

        /// <summary>
        /// Is numeric type
        /// </summary>
        /// <param name="typeInfo"></param>
        /// <returns></returns>
        public static bool IsNumericType(TypeInfo typeInfo) => IsNumericType(typeInfo.AsType());

        #endregion

        #region Nullable Type

        /// <summary>
        /// Is nullable type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static bool IsNullableType<T>() => IsNullableType(typeof(T));

        /// <summary>
        /// Is nullable type
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsNullableType(Type type) =>
            type != null
         && type.IsGenericType
         && type.GetGenericTypeDefinition() == typeof(Nullable<>);

        /// <summary>
        /// Is nullable type
        /// </summary>
        /// <param name="typeInfo"></param>
        /// <returns></returns>
        public static bool IsNullableType(TypeInfo typeInfo) => IsNullableType(typeInfo.AsType());

        #endregion
    }

    internal static class Types
    {
        #region GenericImplementation and raw type

        /// <summary>
        /// Determine whether the given type can be assigned to the specified generic type.<br />
        /// 判断给定的类型是否可赋值给指定的泛型类型。
        /// </summary>
        /// <param name="type">The given type</param>
        /// <param name="genericType">The generic type</param>
        /// <returns></returns>
        public static bool IsGenericImplementation(Type type, Type genericType)
        {
            return IsGenericImplementation(type, genericType, out _);
        }

        /// <summary>
        /// Determine whether the given type can be assigned to the specified generic type.<br />
        /// 判断给定的类型是否可赋值给指定的泛型类型。
        /// </summary>
        /// <typeparam name="TGot">The given type TGot</typeparam>
        /// <typeparam name="TGeneric">The generic type TGeneric</typeparam>
        /// <returns></returns>
        public static bool IsGenericImplementation<TGot, TGeneric>() => IsGenericImplementation(typeof(TGot), typeof(TGeneric));

        /// <summary>
        /// Determine whether the given type can be assigned to the specified generic type.<br />
        /// 判断给定的类型是否可赋值给指定的泛型类型。
        /// </summary>
        /// <param name="type">The given type</param>
        /// <param name="genericType">The generic type</param>
        /// <param name="genericArguments"></param>
        /// <returns></returns>
        public static bool IsGenericImplementation(Type type, Type genericType, out Type[] genericArguments)
        {
            if (type is null)
                throw new ArgumentNullException(nameof(type));

            if (genericType is null)
                throw new ArgumentNullException(nameof(genericType));

            if (!genericType.IsGenericType)
            {
                genericArguments = null;
                return false;
            }

            // ReSharper disable once JoinDeclarationAndInitializer
            bool testFlag;

            //Testing interface
            testFlag = type.GetInterfaces().Any(_checkRawGenericType);
            if (testFlag)
            {
                genericArguments = type.GetGenericArguments();
                return true;
            }

            //Testing class
            while (type != null && type != TypeClass.ObjectClazz)
            {
                testFlag = _checkRawGenericType(type);
                if (testFlag)
                {
                    genericArguments = type.GetGenericArguments();
                    return true;
                }

                type = type.BaseType;
            }

            //no matched for any classed or interfaces
            genericArguments = null;
            return false;

            // To check such type equals to specific type of class or interface
            // 检查给定的这个 test 类型是否等于指定类 Class 或接口 Interface 的类型 Type
            // ReSharper disable once InconsistentNaming
            bool _checkRawGenericType(Type test)
                => genericType == (test.IsGenericType ? test.GetGenericTypeDefinition() : test);
        }

        /// <summary>
        /// Determine whether the given type can be assigned to the specified generic type.<br />
        /// 判断给定的类型是否可赋值给指定的泛型类型。
        /// </summary>
        /// <typeparam name="TGot">The given type TGot</typeparam>
        /// <typeparam name="TGeneric">The generic type TGeneric</typeparam>
        /// <param name="genericArguments"></param>
        /// <returns></returns>
        public static bool IsGenericImplementation<TGot, TGeneric>(out Type[] genericArguments) => IsGenericImplementation(typeof(TGot), typeof(TGeneric), out genericArguments);

        /// <summary>
        /// Get the original type. <br />
        /// When type inherits from genericType, gets the first type parameter in the genericType corresponding to the type.
        /// </summary>
        /// <param name="type">The given type</param>
        /// <param name="genericType">The generic type</param>
        /// <returns></returns>
        public static Type GetRawTypeFromGenericClass(Type type, Type genericType) => TypeReflections.GetRawTypeFromGenericClass(type, genericType);

        /// <summary>
        /// Get the original type. <br />
        /// When type inherits from genericType, gets the first type parameter in the genericType corresponding to the type.
        /// </summary>
        /// <typeparam name="TGot">The given type TGot</typeparam>
        /// <typeparam name="TGeneric">The generic type TGeneric</typeparam>
        /// <returns></returns>
        public static Type GetRawTypeFromGenericClass<TGot, TGeneric>() => TypeReflections.GetRawTypeFromGenericClass<TGot, TGeneric>();

        #endregion
    }

    internal static class TypeReflections
    {
        #region GetUnderlyingType

        /// <summary>
        /// Get underlying type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static Type GetUnderlyingType<T>()
        {
            var type = typeof(T);
            return Nullable.GetUnderlyingType(type) ?? type;
        }

        /// <summary>
        /// Get underlying type.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static Type GetUnderlyingType(Type type)
        {
            if (type is null)
                return null;
            return Nullable.GetUnderlyingType(type) ?? type;
        }

        /// <summary>
        /// Get underlying type.
        /// </summary>
        /// <param name="typeInfo"></param>
        /// <returns></returns>
        public static Type GetUnderlyingType(TypeInfo typeInfo)
        {
            if (typeInfo is null)
                return null;
            var type = typeInfo.AsType();
            return Nullable.GetUnderlyingType(type) ?? type;
        }

        #endregion
        
        #region GenericImplementation and raw type

        /// <summary>
        /// Get the original type. <br />
        /// When type inherits from genericType, gets the first type parameter in the genericType corresponding to the type.
        /// </summary>
        /// <param name="type">The given type</param>
        /// <param name="genericType">The generic type</param>
        /// <returns></returns>
        public static Type GetRawTypeFromGenericClass(Type type, Type genericType)
        {
            if (type is null)
                throw new ArgumentNullException(nameof(type));

            if (genericType is null)
                throw new ArgumentNullException(nameof(genericType));

            if (!genericType.IsGenericType)
                return null;

            while (type != null && type != TypeClass.ObjectClazz)
            {
                var testFlag = _checkRawGenericType(type);
                if (testFlag)
                {
                    return type.GenericTypeArguments.Length > 0
                        ? type.GenericTypeArguments[0]
                        : null;
                }

                type = type.BaseType;
            }

            return null;

            // To check such type equals to specific type of class or interface
            // ReSharper disable once InconsistentNaming
            bool _checkRawGenericType(Type test)
                => genericType == (test.IsGenericType ? test.GetGenericTypeDefinition() : test);
        }

        /// <summary>
        /// Get the original type. <br />
        /// When type inherits from genericType, gets the first type parameter in the genericType corresponding to the type.
        /// </summary>
        /// <typeparam name="TGot">The given type TGot</typeparam>
        /// <typeparam name="TGeneric">The generic type TGeneric</typeparam>
        /// <returns></returns>
        public static Type GetRawTypeFromGenericClass<TGot, TGeneric>() => GetRawTypeFromGenericClass(typeof(TGot), typeof(TGeneric));

        #endregion
    }

    internal static class TypeExtensions
    {
        #region IsDeriveClassFrom

        /// <summary>
        /// Determine whether the current Type (HereGivenType) is derived from the given class ThereBaseType,
        /// or is an implementation of the interface ThereType.<br />
        /// 判断当前 Type 是否派生自给定的类 ThereType，或为接口 ThereType 的实现。
        /// </summary>
        /// <param name="hereGivenType"></param>
        /// <param name="thereBaseType"></param>
        /// <param name="canAbstract"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static bool IsDeriveClassFrom(this Type hereGivenType, Type thereBaseType, bool canAbstract = false)
        {
            if (thereBaseType is null) throw new ArgumentNullException(nameof(thereBaseType));
            if (hereGivenType is null) throw new ArgumentNullException(nameof(hereGivenType));

            return hereGivenType.IsClass
                && (canAbstract || !hereGivenType.IsAbstract)
                && hereGivenType.IsBaseOn(thereBaseType);
        }

        #endregion

        #region IsBaseOn

        /// <summary>
        /// Determine whether the specified type is a derived class of the given base class.<br />
        /// 判断制定类型是否是给定基类的派生类。
        /// </summary>
        /// <param name="type"></param>
        /// <param name="baseType"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static bool IsBaseOn(this Type type, Type baseType)
        {
            if (type is null) throw new ArgumentNullException(nameof(type));
            if (baseType is null) throw new ArgumentNullException(nameof(baseType));

            if (baseType.IsGenericTypeDefinition)
            {
                return baseType.IsGenericAssignableFrom(type);
            }

            return baseType.IsAssignableFrom(type);
        }

        /// <summary>
        /// Determine whether the specified type is a derived class of the given base class.<br />
        /// 判断制定类型是否是给定基类的派生类。
        /// </summary>
        /// <param name="type"></param>
        /// <typeparam name="TBaseType"></typeparam>
        /// <returns></returns>
        public static bool IsBaseOn<TBaseType>(this Type type)
        {
            return type.IsBaseOn(typeof(TBaseType));
        }

        #endregion

        #region IsGenericAssignableFrom

        /// <summary>
        /// Determine whether the current HereGivenType is derived from the generic class thereGenericType
        /// or is an implementation of the generic interface thereGenericType. <br />
        /// 判断当前类型 HereGivenType 是否派生自泛型类 thereGenericType，或为泛型接口 thereGenericType 的实现。
        /// </summary>
        /// <param name="genericBaseType"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsGenericAssignableFrom(this Type genericBaseType, Type type)
            => type.IsGenericImplementationFor(genericBaseType);

        #endregion

        #region IsGenericImplementationFor

        /// <summary>
        /// Determine whether the current HereGivenType is derived from the generic class thereGenericType
        /// or is an implementation of the generic interface thereGenericType. <br />
        /// 判断当前类型 HereGivenType 是否派生自泛型类 thereGenericType，或为泛型接口 thereGenericType 的实现。
        /// </summary>
        /// <param name="hereGivenType">给定类型</param>
        /// <param name="thereGenericType">泛型类型</param>
        /// <returns></returns>
        public static bool IsGenericImplementationFor(this Type hereGivenType, Type thereGenericType)
            => Types.IsGenericImplementation(hereGivenType, thereGenericType);

        /// <summary>
        /// Determine whether the current HereGivenType is derived from the generic class thereGenericType
        /// or is an implementation of the generic interface thereGenericType. <br />
        /// 判断当前类型 HereGivenType 是否派生自泛型类 thereGenericType，或为泛型接口 thereGenericType 的实现。
        /// </summary>
        /// <param name="hereGivenType">给定类型</param>
        /// <param name="thereGenericType">泛型类型</param>
        /// <returns></returns>
        public static bool IsGenericImplementationFor(this TypeInfo hereGivenType, TypeInfo thereGenericType)
            => Types.IsGenericImplementation(hereGivenType, thereGenericType);

        /// <summary>
        /// Determine whether the current HereGivenType is derived from the generic class thereGenericType
        /// or is an implementation of the generic interface thereGenericType. <br />
        /// 判断当前类型 HereGivenType 是否派生自泛型类 thereGenericType，或为泛型接口 thereGenericType 的实现。
        /// </summary>
        /// <typeparam name="TGeneric">泛型类型</typeparam>
        /// <param name="hereGivenType">给定类型</param>
        /// <returns></returns>
        public static bool IsGenericImplementationFor<TGeneric>(this Type hereGivenType)
            => Types.IsGenericImplementation(hereGivenType, typeof(TGeneric));

        /// <summary>
        /// Determine whether the current HereGivenType is derived from the generic class thereGenericType
        /// or is an implementation of the generic interface thereGenericType. <br />
        /// 判断当前类型 HereGivenType 是否派生自泛型类 thereGenericType，或为泛型接口 thereGenericType 的实现。
        /// </summary>
        /// <typeparam name="TThereGenericClazz">泛型类型</typeparam>
        /// <param name="hereGivenType">给定类型</param>
        /// <returns></returns>
        public static bool IsGenericImplementationFor<TThereGenericClazz>(this TypeInfo hereGivenType)
            => Types.IsGenericImplementation(hereGivenType, typeof(TThereGenericClazz));

        #endregion
    }

    internal static class TypeClass
    {
        /// <summary>
        /// Gets clazz for object.
        /// </summary>
        public static Type ObjectClazz { get; } = typeof(object);
    }
}