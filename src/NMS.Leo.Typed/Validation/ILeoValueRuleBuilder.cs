using System;
using System.Collections;
using System.Collections.Generic;

namespace NMS.Leo.Typed.Validation
{
    public interface ILeoValueRuleBuilder
    {
        ILeoValueRuleBuilder AppendRule();

        ILeoValueRuleBuilder OverwriteRule();

        ILeoValueRuleBuilder Empty();

        ILeoValueRuleBuilder NotEmpty();

        ILeoValueRuleBuilder Null();

        ILeoValueRuleBuilder NotNull();

        ILeoValueRuleBuilder Range(object from, object to);

        ILeoValueRuleBuilder Length(int min, int max);

        ILeoValueRuleBuilder MinLength(int min);

        ILeoValueRuleBuilder MaxLength(int max);

        ILeoValueRuleBuilder Equal(object value);

        ILeoValueRuleBuilder Equal(object value, IEqualityComparer comparer);

        ILeoValueRuleBuilder NotEqual(object value);

        ILeoValueRuleBuilder NotEqual(object value, IEqualityComparer comparer);

        ILeoValueRuleBuilder Must(Func<object, CustomVerifyResult> func);

        ILeoWaitForMessageValueRuleBuilder Must(Func<object, bool> func);
    }

    public interface ILeoValueRuleBuilder<T>
    {
        ILeoValueRuleBuilder<T> AppendRule();

        ILeoValueRuleBuilder<T> OverwriteRule();

        ILeoValueRuleBuilder<T> Empty();

        ILeoValueRuleBuilder<T> NotEmpty();

        ILeoValueRuleBuilder<T> Null();

        ILeoValueRuleBuilder<T> NotNull();

        ILeoValueRuleBuilder<T> Range(object from, object to);

        ILeoValueRuleBuilder<T> Length(int min, int max);

        ILeoValueRuleBuilder<T> MinLength(int min);

        ILeoValueRuleBuilder<T> MaxLength(int max);

        ILeoValueRuleBuilder<T> Equal(object value);

        ILeoValueRuleBuilder<T> Equal(object value, IEqualityComparer comparer);

        ILeoValueRuleBuilder<T> NotEqual(object value);

        ILeoValueRuleBuilder<T> NotEqual(object value, IEqualityComparer comparer);

        ILeoValueRuleBuilder<T> Must(Func<object, CustomVerifyResult> func);

        ILeoWaitForMessageValueRuleBuilder<T> Must(Func<object, bool> func);
    }

    public interface ILeoValueRuleBuilder<T, TVal> : ILeoValueRuleBuilder<T>
    {
        new ILeoValueRuleBuilder<T, TVal> AppendRule();

        new ILeoValueRuleBuilder<T, TVal> OverwriteRule();

        new ILeoValueRuleBuilder<T, TVal> Empty();

        new ILeoValueRuleBuilder<T, TVal> NotEmpty();

        new ILeoValueRuleBuilder<T, TVal> Null();

        new ILeoValueRuleBuilder<T, TVal> NotNull();

        ILeoValueRuleBuilder<T, TVal> Range(TVal from, TVal to);

        new ILeoValueRuleBuilder<T, TVal> Length(int min, int max);

        new ILeoValueRuleBuilder<T, TVal> MinLength(int min);

        new ILeoValueRuleBuilder<T, TVal> MaxLength(int max);

        new ILeoValueRuleBuilder<T, TVal> Equal(TVal value);

        ILeoValueRuleBuilder<T, TVal> Equal(TVal value, IEqualityComparer<TVal> comparer);

        new ILeoValueRuleBuilder<T, TVal> NotEqual(TVal value);

        ILeoValueRuleBuilder<T, TVal> NotEqual(TVal value, IEqualityComparer<TVal> comparer);

        ILeoValueRuleBuilder<T, TVal> Must(Func<TVal, CustomVerifyResult> func);

        ILeoWaitForMessageValueRuleBuilder<T, TVal> Must(Func<TVal, bool> func);
    }
}