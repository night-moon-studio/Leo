using System.Text.RegularExpressions;
using NMS.Leo.Typed.Core.Correct;
using NMS.Leo.Typed.Core.Correct.Token;
using NMS.Leo.Typed.Core.Members;

namespace NMS.Leo.Typed.Validation;

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
        current.CurrentToken = new ValueAnyToken<TItem[], TItem>(current._member, func);
        return builder;
    }

    public static ILeoValueRuleBuilder<T, TVal> Any<T, TVal, TItem>(this ILeoValueRuleBuilder<T, TVal> builder, Func<TItem, bool> func)
        where TVal : ICollection<TItem>
    {
        var current = builder._impl();
        current.CurrentToken = new ValueAnyToken<TVal, TItem>(current._member, func);
        return builder;
    }

    public static ILeoValueRuleBuilder<T, TItem[]> All<T, TItem>(this ILeoValueRuleBuilder<T, TItem[]> builder, Func<TItem, bool> func)
    {
        var current = builder._impl();
        current.CurrentToken = new ValueAllToken<TItem[], TItem>(current._member, func);
        return builder;
    }

    public static ILeoValueRuleBuilder<T, TVal> All<T, TVal, TItem>(this ILeoValueRuleBuilder<T, TVal> builder, Func<TItem, bool> func)
        where TVal : ICollection<TItem>
    {
        var current = builder._impl();
        current.CurrentToken = new ValueAllToken<TVal, TItem>(current._member, func);
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

    #region Matches

    public static ILeoValueRuleBuilder<T, TVal> Matches<T, TVal>(this ILeoValueRuleBuilder<T, TVal> builder, Expression<Func<T, string>> expression)
    {
        var current = builder._impl();
        var regexExpression = PropertyValueGetter.Get(expression, current.CorrespondingInstance);
        return builder.Matches(regexExpression);
    }

    public static ILeoValueRuleBuilder<T, TVal> Matches<T, TVal>(this ILeoValueRuleBuilder<T, TVal> builder, Expression<Func<T, Regex>> expression)
    {
        var current = builder._impl();
        var regex = PropertyValueGetter.Get(expression, current.CorrespondingInstance);
        return builder.Matches(regex);
    }

    public static ILeoValueRuleBuilder<T, TVal> Matches<T, TVal>(this ILeoValueRuleBuilder<T, TVal> builder, Expression<Func<T, string>> expression, RegexOptions options)
    {
        var current = builder._impl();
        var regexExpression = PropertyValueGetter.Get(expression, current.CorrespondingInstance);
        return builder.Matches(regexExpression, options);
    }

    #endregion

    #region WithMessage

    public static ILeoValueRuleBuilder WithMessage(this ILeoValueRuleBuilder builder, string message)
    {
        return builder.WithMessage(message, true);
    }

    public static ILeoValueRuleBuilder WithMessage(this ILeoValueRuleBuilder builder, string message, bool appendOrOverwrite)
    {
        var current = builder._impl().CurrentToken;

        if (current != null)
        {
            if (current.WithMessageMode)
            {
                if (appendOrOverwrite)
                    current.CustomMessage += message;
                else
                    current.CustomMessage = message;
            }
            else
            {
                current.CustomMessage = message;
                current.AppendOrOverwrite = appendOrOverwrite;
                current.WithMessageMode = true;
            }
        }

        return builder;
    }

    public static ILeoValueRuleBuilder<T> WithMessage<T>(this ILeoValueRuleBuilder<T> builder, string message)
    {
        return builder.WithMessage(message, true);
    }

    public static ILeoValueRuleBuilder<T> WithMessage<T>(this ILeoValueRuleBuilder<T> builder, string message, bool appendOrOverwrite)
    {
        var current = builder._impl().CurrentToken;

        if (current != null)
        {
            if (current.WithMessageMode)
            {
                if (appendOrOverwrite)
                    current.CustomMessage += message;
                else
                    current.CustomMessage = message;
            }
            else
            {
                current.CustomMessage = message;
                current.AppendOrOverwrite = appendOrOverwrite;
                current.WithMessageMode = true;
            }
        }

        return builder;
    }

    public static ILeoValueRuleBuilder<T, TVal> WithMessage<T, TVal>(this ILeoValueRuleBuilder<T, TVal> builder, string message)
    {
        return builder.WithMessage(message, true);
    }

    public static ILeoValueRuleBuilder<T, TVal> WithMessage<T, TVal>(this ILeoValueRuleBuilder<T, TVal> builder, string message, bool appendOrOverwrite)
    {
        var current = builder._impl().CurrentToken;

        if (current != null)
        {
            if (current.WithMessageMode)
            {
                if (appendOrOverwrite)
                    current.CustomMessage += message;
                else
                    current.CustomMessage = message;
            }
            else
            {
                current.CustomMessage = message;
                current.AppendOrOverwrite = appendOrOverwrite;
                current.WithMessageMode = true;
            }
        }

        return builder;
    }

    #endregion
}