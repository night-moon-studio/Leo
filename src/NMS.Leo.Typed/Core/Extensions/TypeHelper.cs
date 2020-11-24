using System;
using System.Reflection;

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
}