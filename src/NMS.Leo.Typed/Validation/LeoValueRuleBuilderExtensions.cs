using System;
using System.Collections.Generic;
using NMS.Leo.Typed.Core.Correct;
using NMS.Leo.Typed.Core.Correct.Token;

namespace NMS.Leo.Typed.Validation
{
    public static class LeoValueRuleBuilderExtensions
    {
        private static CorrectValueRuleBuilder<T, TVal> _impl<T, TVal>(this ILeoValueRuleBuilder<T, TVal> builder)
        {
            return (CorrectValueRuleBuilder<T, TVal>) builder;
        }

        private static CorrectValueRuleBuilder<T> _impl<T>(this ILeoValueRuleBuilder<T> builder)
        {
            return (CorrectValueRuleBuilder<T>) builder;
        }

        private static CorrectValueRuleBuilder _impl(this ILeoValueRuleBuilder builder)
        {
            return (CorrectValueRuleBuilder) builder;
        }

        #region Any/All/NotAny/NotAll

        public static ILeoValueRuleBuilder<T, TItem[]> Any<T, TItem>(this ILeoValueRuleBuilder<T, TItem[]> builder, Func<TItem, bool> func)
        {
            var current = builder._impl();
            current._valueTokens.Add(new ValueAnyToken<TItem[], TItem>(current._member, func));
            return builder;
        }

        public static ILeoValueRuleBuilder<T, TVal> Any<T, TVal, TItem>(this ILeoValueRuleBuilder<T, TVal> builder, Func<TItem, bool> func)
            where TVal : ICollection<TItem>
        {
            var current = builder._impl();
            current._valueTokens.Add(new ValueAnyToken<TVal, TItem>(current._member, func));
            return builder;
        }

        public static ILeoValueRuleBuilder<T, TItem[]> All<T, TItem>(this ILeoValueRuleBuilder<T, TItem[]> builder, Func<TItem, bool> func)
        {
            var current = builder._impl();
            current._valueTokens.Add(new ValueAllToken<TItem[], TItem>(current._member, func));
            return builder;
        }

        public static ILeoValueRuleBuilder<T, TVal> All<T, TVal, TItem>(this ILeoValueRuleBuilder<T, TVal> builder, Func<TItem, bool> func)
            where TVal : ICollection<TItem>
        {
            var current = builder._impl();
            current._valueTokens.Add(new ValueAllToken<TVal, TItem>(current._member, func));
            return builder;
        }

        public static ILeoValueRuleBuilder<T, TItem[]> NotAny<T, TItem>(this ILeoValueRuleBuilder<T, TItem[]> builder, Func<TItem, bool> func)
            => builder.All(func);

        public static ILeoValueRuleBuilder<T, TVal> NotAny<T, TVal, TItem>(this ILeoValueRuleBuilder<T, TVal> builder, Func<TItem, bool> func)
            where TVal : ICollection<TItem>
            => builder.All(func);

        public static ILeoValueRuleBuilder<T, TItem[]> NotAll<T, TItem>(this ILeoValueRuleBuilder<T, TItem[]> builder, Func<TItem, bool> func)
            => builder.Any(func);

        public static ILeoValueRuleBuilder<T, TVal> NotAll<T, TVal, TItem>(this ILeoValueRuleBuilder<T, TVal> builder, Func<TItem, bool> func)
            where TVal : ICollection<TItem>
            => builder.Any(func);

        #endregion
    }
}