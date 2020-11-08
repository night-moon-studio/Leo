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

        public static bool TryRepeatAs<TObj>(this ILeoVisitor visitor, out TObj result)
        {
            result = default;
            var ret = visitor.TryRepeat(out var val);
            if (!ret) return false;

            try
            {
                result = (TObj)val;
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool TryRepeatAs<TObj>(this ILeoVisitor visitor, object instance, out TObj result)
        {
            result = default;
            var ret = visitor.TryRepeat(instance, out var val);
            if (!ret) return false;

            try
            {
                result = (TObj)val;
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}