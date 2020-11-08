using System;

namespace NMS.Leo.Typed
{
    public static class LeoVisitorExtensions
    {
        public static ILeoVisitor<T> ToLeoVisitor<T>(this T instanceObj, LeoType leoType = LeoType.Precision)
            where T : class
        {
            return LeoVisitorFactory.Create(instanceObj, leoType);
        }

        public static ILeoVisitor ToLeoVisitor(this Type type, LeoType leoType = LeoType.Precision)
        {
            return LeoVisitorFactory.Create(type, leoType);
        }
    }
}