using System;
using System.Collections.Generic;
using NMS.Leo.Typed.Core.Correct;
using NMS.Leo.Typed.Core.Correct.Token;

namespace NMS.Leo.Typed.Validation
{
    public static class LeoValueRuleBuilderExtensions
    {
        public static ILeoValueRuleBuilder<T, TItem[]> Any<T, TItem>(this ILeoValueRuleBuilder<T, TItem[]> builder, Func<TItem, bool> func)
        {
            var current = (CorrectValueRuleBuilder<T, TItem[]>) builder;
            current._valueTokens.Add(new ValueAnyToken<TItem[], TItem>(current._member, func));
            return builder;
        }

        public static ILeoValueRuleBuilder<T, TVal> Any<T, TVal, TItem>(this ILeoValueRuleBuilder<T, TVal> builder, Func<TItem, bool> func)
            where TVal : ICollection<TItem>
        {
            var current = (CorrectValueRuleBuilder<T, TVal>) builder;
            current._valueTokens.Add(new ValueAnyToken<TVal, TItem>(current._member, func));
            return builder;
        }

        public static ILeoValueRuleBuilder<T, TItem[]> All<T, TItem>(this ILeoValueRuleBuilder<T, TItem[]> builder, Func<TItem, bool> func)
        {
            var current = (CorrectValueRuleBuilder<T, TItem[]>) builder;
            current._valueTokens.Add(new ValueAllToken<TItem[], TItem>(current._member, func));
            return builder;
        }

        public static ILeoValueRuleBuilder<T, TVal> All<T, TVal, TItem>(this ILeoValueRuleBuilder<T, TVal> builder, Func<TItem, bool> func)
            where TVal : ICollection<TItem>
        {
            var current = (CorrectValueRuleBuilder<T, TVal>) builder;
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
    }
}