using System;
using System.Collections.Generic;
using NMS.Leo.Metadata;
using NMS.Leo.Typed.Core;
using NMS.Leo.Typed.Core.Loop;
using NMS.Leo.Typed.Core.Repeat;
using NMS.Leo.Typed.Core.Select;

namespace NMS.Leo.Typed
{
    public static class LeoVisitorExtensions
    {
        #region To Visitor

        public static ILeoVisitor<T> ToVisitor<T>(this T instanceObj, AlgorithmKind kind = AlgorithmKind.Precision, bool repeatable = true)
            where T : class
        {
            return LeoVisitorFactory.Create(instanceObj, kind, repeatable);
        }

        public static ILeoVisitor ToVisitor(this Type type, AlgorithmKind kind = AlgorithmKind.Precision, bool repeatable = true)
        {
            return LeoVisitorFactory.Create(type, kind, repeatable);
        }

        public static ILeoVisitor ToVisitor(this Type type, IDictionary<string, object> initialValues, AlgorithmKind kind = AlgorithmKind.Precision, bool repeatable = true)
        {
            return LeoVisitorFactory.Create(type, initialValues, kind, repeatable);
        }

        #endregion

        #region To Dictionary

        public static Dictionary<string, object> ToDictionary(this ILeoVisitor visitor)
        {
            if (visitor is null)
                throw new ArgumentNullException(nameof(visitor));
            var val = new Dictionary<string, object>();
            var rel = (ICoreVisitor) visitor;
            var lazyHandler = rel.ExposeLazyMemberHandler();
            foreach (var name in lazyHandler.Value.GetNames())
                val[name] = visitor.GetValue(name);
            return val;
        }

        public static Dictionary<string, object> ToDictionary<T>(this ILeoVisitor<T> visitor)
        {
            if (visitor is null)
                throw new ArgumentNullException(nameof(visitor));
            var val = new Dictionary<string, object>();
            var rel = (ICoreVisitor<T>) visitor;
            var lazyHandler = rel.ExposeLazyMemberHandler();
            foreach (var name in lazyHandler.Value.GetNames())
                val[name] = visitor.GetValue(name);
            return val;
        }

        #endregion

        #region Select

        public static ILeoSelector<TVal> Select<TVal>(this ILeoVisitor visitor, Func<string, object, LeoMember, TVal> loopFunc)
        {
            if (visitor is null)
                throw new ArgumentNullException(nameof(visitor));
            var rel = (ICoreVisitor) visitor;
            return new LeoSelector<TVal>(rel.Owner, rel.ExposeLazyMemberHandler(), loopFunc);
        }

        public static ILeoSelector<TVal> Select<TVal>(this ILeoVisitor visitor, Func<string, object, TVal> loopFunc)
        {
            if (visitor is null)
                throw new ArgumentNullException(nameof(visitor));
            var rel = (ICoreVisitor) visitor;
            return new LeoSelector<TVal>(rel.Owner, rel.ExposeLazyMemberHandler(), loopFunc);
        }

        public static ILeoSelector<TVal> Select<TVal>(this ILeoVisitor visitor, Func<LeoLoopContext, TVal> loopFunc)
        {
            if (visitor is null)
                throw new ArgumentNullException(nameof(visitor));
            var rel = (ICoreVisitor) visitor;
            return new LeoSelector<TVal>(rel.Owner, rel.ExposeLazyMemberHandler(), loopFunc);
        }

        public static ILeoSelector<T, TVal> Select<T, TVal>(this ILeoVisitor<T> visitor, Func<string, object, LeoMember, TVal> loopFunc)
        {
            if (visitor is null)
                throw new ArgumentNullException(nameof(visitor));
            var rel = (ICoreVisitor<T>) visitor;
            return new LeoSelector<T, TVal>(rel.Owner, rel.ExposeLazyMemberHandler(), loopFunc);
        }

        public static ILeoSelector<T, TVal> Select<T, TVal>(this ILeoVisitor<T> visitor, Func<string, object, TVal> loopFunc)
        {
            if (visitor is null)
                throw new ArgumentNullException(nameof(visitor));
            var rel = (ICoreVisitor<T>) visitor;
            return new LeoSelector<T, TVal>(rel.Owner, rel.ExposeLazyMemberHandler(), loopFunc);
        }

        public static ILeoSelector<T, TVal> Select<T, TVal>(this ILeoVisitor<T> visitor, Func<LeoLoopContext, TVal> loopFunc)
        {
            if (visitor is null)
                throw new ArgumentNullException(nameof(visitor));
            var rel = (ICoreVisitor<T>) visitor;
            return new LeoSelector<T, TVal>(rel.Owner, rel.ExposeLazyMemberHandler(), loopFunc);
        }

        #endregion

        #region For Each

        public static ILeoLooper ForEach(this ILeoVisitor visitor, Action<string, object, LeoMember> loopAct)
        {
            if (visitor is null)
                throw new ArgumentNullException(nameof(visitor));
            var rel = (ICoreVisitor) visitor;
            return new LeoLooper(rel.Owner, rel.ExposeLazyMemberHandler(), loopAct);
        }

        public static ILeoLooper ForEach(this ILeoVisitor visitor, Action<string, object> loopAct)
        {
            if (visitor is null)
                throw new ArgumentNullException(nameof(visitor));
            var rel = (ICoreVisitor) visitor;
            return new LeoLooper(rel.Owner, rel.ExposeLazyMemberHandler(), loopAct);
        }

        public static ILeoLooper ForEach(this ILeoVisitor visitor, Action<LeoLoopContext> loopAct)
        {
            if (visitor is null)
                throw new ArgumentNullException(nameof(visitor));
            var rel = (ICoreVisitor) visitor;
            return new LeoLooper(rel.Owner, rel.ExposeLazyMemberHandler(), loopAct);
        }

        public static ILeoLooper<T> ForEach<T>(this ILeoVisitor<T> visitor, Action<string, object, LeoMember> loopAct)
        {
            if (visitor is null)
                throw new ArgumentNullException(nameof(visitor));
            var rel = (ICoreVisitor<T>) visitor;
            return new LeoLooper<T>(rel.Owner, rel.ExposeLazyMemberHandler(), loopAct);
        }

        public static ILeoLooper<T> ForEach<T>(this ILeoVisitor<T> visitor, Action<string, object> loopAct)
        {
            if (visitor is null)
                throw new ArgumentNullException(nameof(visitor));
            var rel = (ICoreVisitor<T>) visitor;
            return new LeoLooper<T>(rel.Owner, rel.ExposeLazyMemberHandler(), loopAct);
        }

        public static ILeoLooper<T> ForEach<T>(this ILeoVisitor<T> visitor, Action<LeoLoopContext> loopAct)
        {
            if (visitor is null)
                throw new ArgumentNullException(nameof(visitor));
            var rel = (ICoreVisitor<T>) visitor;
            return new LeoLooper<T>(rel.Owner, rel.ExposeLazyMemberHandler(), loopAct);
        }

        #endregion

        #region For Repeat

        public static ILeoRepeater ForRepeat(this ILeoVisitor visitor)
        {
            if (visitor is null)
                throw new ArgumentNullException(nameof(visitor));
            if (visitor.IsStatic) return new EmptyRepeater(visitor.SourceType);
            var rel = (ICoreVisitor) visitor;
            if (rel.ExposeHistoricalContext() is null) return new EmptyRepeater(visitor.SourceType);
            return new LeoRepeater(rel.ExposeHistoricalContext());
        }

        public static ILeoRepeater<T> ForRepeat<T>(this ILeoVisitor<T> visitor)
        {
            if (visitor is null)
                throw new ArgumentNullException(nameof(visitor));
            if (visitor.IsStatic) return new EmptyRepeater<T>();
            var rel = (ICoreVisitor<T>) visitor;
            if (rel.ExposeHistoricalContext() is null) return new EmptyRepeater<T>();
            return new LeoRepeater<T>(rel.ExposeHistoricalContext());
        }

        #endregion

        #region Try Repeat

        public static bool TryRepeat(this ILeoVisitor visitor, out object result)
        {
            if (visitor is null)
                throw new ArgumentNullException(nameof(visitor));
            result = default;
            if (visitor.IsStatic) return false;
            var rel = (ICoreVisitor) visitor;
            if (rel.ExposeHistoricalContext() is null) return false;
            result = rel.ExposeHistoricalContext().Repeat();
            return true;
        }

        public static bool TryRepeat(this ILeoVisitor visitor, object instance, out object result)
        {
            if (visitor is null)
                throw new ArgumentNullException(nameof(visitor));
            result = default;
            if (visitor.IsStatic) return false;
            var rel = (ICoreVisitor) visitor;
            if (rel.ExposeHistoricalContext() is null) return false;
            result = rel.ExposeHistoricalContext().Repeat(instance);
            return true;
        }

        public static bool TryRepeat(this ILeoVisitor visitor, IDictionary<string, object> keyValueCollections, out object result)
        {
            if (visitor is null)
                throw new ArgumentNullException(nameof(visitor));
            result = default;
            if (visitor.IsStatic) return false;
            var rel = (ICoreVisitor) visitor;
            if (rel.ExposeHistoricalContext() is null) return false;
            result = rel.ExposeHistoricalContext().Repeat(keyValueCollections);
            return true;
        }

        public static bool TryRepeat<T>(this ILeoVisitor<T> visitor, out T result)
        {
            if (visitor is null)
                throw new ArgumentNullException(nameof(visitor));
            result = default;
            if (visitor.IsStatic) return false;
            var rel = (ICoreVisitor<T>) visitor;
            if (rel.ExposeHistoricalContext() is null) return false;
            result = rel.ExposeHistoricalContext().Repeat();
            return true;
        }

        public static bool TryRepeat<T>(this ILeoVisitor<T> visitor, T instance, out T result)
        {
            if (visitor is null)
                throw new ArgumentNullException(nameof(visitor));
            result = default;
            if (visitor.IsStatic) return false;
            var rel = (ICoreVisitor<T>) visitor;
            if (rel.ExposeHistoricalContext() is null) return false;
            result = rel.ExposeHistoricalContext().Repeat(instance);
            return true;
        }

        public static bool TryRepeat<T>(this ILeoVisitor<T> visitor, IDictionary<string, object> keyValueCollections, out T result)
        {
            if (visitor is null)
                throw new ArgumentNullException(nameof(visitor));
            result = default;
            if (visitor.IsStatic) return false;
            var rel = (ICoreVisitor<T>) visitor;
            if (rel.ExposeHistoricalContext() is null) return false;
            result = rel.ExposeHistoricalContext().Repeat(keyValueCollections);
            return true;
        }

        #endregion

        #region Try Repeat As

        public static bool TryRepeatAs<TObj>(this ILeoVisitor visitor, out TObj result)
        {
            result = default;
            var ret = visitor.TryRepeat(out var val);
            if (!ret) return false;

            try
            {
                result = (TObj) val;
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
                result = (TObj) val;
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool TryRepeatAs<TObj>(this ILeoVisitor visitor, IDictionary<string, object> keyValueCollections, out TObj result)
        {
            result = default;
            var ret = visitor.TryRepeat(keyValueCollections, out var val);
            if (!ret) return false;

            try
            {
                result = (TObj) val;
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool TryRepeatAs<T>(this ILeoVisitor<T> visitor, out T result)
        {
            result = default;
            return visitor.TryRepeat(out result);
        }

        public static bool TryRepeatAs<T>(this ILeoVisitor<T> visitor, T instance, out T result)
        {
            result = default;
            return visitor.TryRepeat(instance, out result);
        }

        public static bool TryRepeatAs<T>(this ILeoVisitor<T> visitor, IDictionary<string, object> keyValueCollections, out T result)
        {
            result = default;
            return visitor.TryRepeat(keyValueCollections, out result);
        }

        #endregion
    }
}