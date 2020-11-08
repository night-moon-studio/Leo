using System;

namespace NMS.Leo.Typed
{
    public static class LeoVisitorExtensions
    {
        public static ILeoVisitor<T> ToLeoVisitor<T>(this T instanceObj, LeoType leoType = LeoType.Precision, bool repeatable = true)
            where T : class
        {
            return LeoVisitorFactory.Create(instanceObj, leoType, repeatable);
        }

        public static ILeoVisitor ToLeoVisitor(this Type type, LeoType leoType = LeoType.Precision, bool repeatable = true)
        {
            return LeoVisitorFactory.Create(type, leoType, repeatable);
        }
    }
}