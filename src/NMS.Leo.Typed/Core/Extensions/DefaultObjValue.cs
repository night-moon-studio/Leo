using System;
using NMS.Leo.Metadata;

namespace NMS.Leo.Typed.Core.Extensions
{
    internal static class DefaultObjValue
    {
        public static object Get(Type type, object value)
        {
            if (type.IsValueType)
            {
                return value switch
                {
                    byte _ => default,
                    short _ => default,
                    int _ => default,
                    long _ => default,
                    sbyte _ => default,
                    ushort _ => default,
                    uint _ => default,
                    ulong _ => default,
                    char _ => default,
                    float _ => default,
                    double _ => default,
                    decimal _ => default,
                    DateTime _ => default,
                    DateTimeOffset _ => default,
                    Enum _ => default,
                    _ => default
                };
            }

            return default;
        }
    }

    internal static class DefaultObjValueExtensions
    {
        public static object GetDefaultValue(this LeoMember member, object value)
        {
            return DefaultObjValue.Get(member.MemberType, value);
        }
    }
}