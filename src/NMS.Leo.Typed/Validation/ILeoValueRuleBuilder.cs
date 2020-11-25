﻿using System;
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

        ILeoValueRuleBuilder Required();

        ILeoValueRuleBuilder Null();

        ILeoValueRuleBuilder NotNull();

        ILeoValueRuleBuilder Range(object from, object to, RangeOptions options = RangeOptions.OpenInterval);

        ILeoValueRuleBuilder RangeWithOpenInterval(object from, object to);

        ILeoValueRuleBuilder RangeWithCloseInterval(object from, object to);

        ILeoValueRuleBuilder Length(int min, int max);

        ILeoValueRuleBuilder MinLength(int min);

        ILeoValueRuleBuilder MaxLength(int max);

        ILeoValueRuleBuilder AtLeast(int count);

        ILeoValueRuleBuilder Equal(object value);

        ILeoValueRuleBuilder Equal(object value, IEqualityComparer comparer);

        ILeoValueRuleBuilder NotEqual(object value);

        ILeoValueRuleBuilder NotEqual(object value, IEqualityComparer comparer);

        ILeoValueRuleBuilder LessThan(object value);

        ILeoValueRuleBuilder LessThanOrEqual(object value);

        ILeoValueRuleBuilder GreaterThan(object value);

        ILeoValueRuleBuilder GreaterThanOrEqual(object value);

        ILeoValueRuleBuilder Func(Func<object, CustomVerifyResult> func);

        ILeoWaitForMessageValueRuleBuilder Func(Func<object, bool> func);

        ILeoWaitForMessageValueRuleBuilder Predicate(Predicate<object> predicate);

        ILeoValueRuleBuilder Must(Func<object, CustomVerifyResult> func);

        ILeoWaitForMessageValueRuleBuilder Must(Func<object, bool> func);

        ILeoValueRuleBuilder Any(Func<object, bool> func);

        ILeoValueRuleBuilder All(Func<object, bool> func);

        ILeoValueRuleBuilder NotAny(Func<object, bool> func);

        ILeoValueRuleBuilder NotAll(Func<object, bool> func);

        ILeoValueRuleBuilder In(ICollection<object> collection);

        ILeoValueRuleBuilder In(params object[] objects);

        ILeoValueRuleBuilder NotIn(ICollection<object> collection);

        ILeoValueRuleBuilder NotIn(params object[] objects);
    }

    public interface ILeoValueRuleBuilder<T>
    {
        ILeoValueRuleBuilder<T> AppendRule();

        ILeoValueRuleBuilder<T> OverwriteRule();

        ILeoValueRuleBuilder<T> Empty();

        ILeoValueRuleBuilder<T> NotEmpty();

        ILeoValueRuleBuilder<T> Required();

        ILeoValueRuleBuilder<T> Null();

        ILeoValueRuleBuilder<T> NotNull();

        ILeoValueRuleBuilder<T> Range(object from, object to, RangeOptions options = RangeOptions.OpenInterval);

        ILeoValueRuleBuilder<T> RangeWithOpenInterval(object from, object to);

        ILeoValueRuleBuilder<T> RangeWithCloseInterval(object from, object to);

        ILeoValueRuleBuilder<T> Length(int min, int max);

        ILeoValueRuleBuilder<T> MinLength(int min);

        ILeoValueRuleBuilder<T> MaxLength(int max);

        ILeoValueRuleBuilder<T> AtLeast(int count);

        ILeoValueRuleBuilder<T> Equal(object value);

        ILeoValueRuleBuilder<T> Equal(object value, IEqualityComparer comparer);

        ILeoValueRuleBuilder<T> NotEqual(object value);

        ILeoValueRuleBuilder<T> NotEqual(object value, IEqualityComparer comparer);

        ILeoValueRuleBuilder<T> LessThan(object value);

        ILeoValueRuleBuilder<T> LessThanOrEqual(object value);

        ILeoValueRuleBuilder<T> GreaterThan(object value);

        ILeoValueRuleBuilder<T> GreaterThanOrEqual(object value);

        ILeoValueRuleBuilder<T> Func(Func<object, CustomVerifyResult> func);

        ILeoWaitForMessageValueRuleBuilder<T> Func(Func<object, bool> func);

        ILeoWaitForMessageValueRuleBuilder<T> Predicate(Predicate<object> predicate);

        ILeoValueRuleBuilder<T> Must(Func<object, CustomVerifyResult> func);

        ILeoWaitForMessageValueRuleBuilder<T> Must(Func<object, bool> func);

        // ILeoValueRuleBuilder<T> Any(Func<object, bool> func);
        //
        // ILeoValueRuleBuilder<T> All(Func<object, bool> func);
        //
        // ILeoValueRuleBuilder<T> NotAny(Func<object, bool> func);
        //
        // ILeoValueRuleBuilder<T> NotAll(Func<object, bool> func);

        ILeoValueRuleBuilder<T> In(ICollection<object> collection);

        ILeoValueRuleBuilder<T> In(params object[] objects);

        ILeoValueRuleBuilder<T> NotIn(ICollection<object> collection);

        ILeoValueRuleBuilder<T> NotIn(params object[] objects);
    }

    public interface ILeoValueRuleBuilder<T, TVal> : ILeoValueRuleBuilder<T>
    {
        new ILeoValueRuleBuilder<T, TVal> AppendRule();

        new ILeoValueRuleBuilder<T, TVal> OverwriteRule();

        new ILeoValueRuleBuilder<T, TVal> Empty();

        new ILeoValueRuleBuilder<T, TVal> NotEmpty();

        new ILeoValueRuleBuilder<T, TVal> Required();

        new ILeoValueRuleBuilder<T, TVal> Null();

        new ILeoValueRuleBuilder<T, TVal> NotNull();

        ILeoValueRuleBuilder<T, TVal> Range(TVal from, TVal to, RangeOptions options = RangeOptions.OpenInterval);

        ILeoValueRuleBuilder<T, TVal> RangeWithOpenInterval(TVal from, TVal to);

        ILeoValueRuleBuilder<T, TVal> RangeWithCloseInterval(TVal from, TVal to);

        new ILeoValueRuleBuilder<T, TVal> Length(int min, int max);

        new ILeoValueRuleBuilder<T, TVal> MinLength(int min);

        new ILeoValueRuleBuilder<T, TVal> MaxLength(int max);

        new ILeoValueRuleBuilder<T, TVal> AtLeast(int count);

        ILeoValueRuleBuilder<T, TVal> Equal(TVal value);

        ILeoValueRuleBuilder<T, TVal> Equal(TVal value, IEqualityComparer<TVal> comparer);

        ILeoValueRuleBuilder<T, TVal> NotEqual(TVal value);

        ILeoValueRuleBuilder<T, TVal> NotEqual(TVal value, IEqualityComparer<TVal> comparer);

        ILeoValueRuleBuilder<T, TVal> LessThan(TVal value);

        ILeoValueRuleBuilder<T, TVal> LessThanOrEqual(TVal value);

        ILeoValueRuleBuilder<T, TVal> GreaterThan(TVal value);

        ILeoValueRuleBuilder<T, TVal> GreaterThanOrEqual(TVal value);

        ILeoValueRuleBuilder<T, TVal> Func(Func<TVal, CustomVerifyResult> func);

        ILeoWaitForMessageValueRuleBuilder<T, TVal> Func(Func<TVal, bool> func);

        ILeoWaitForMessageValueRuleBuilder<T, TVal> Predicate(Predicate<TVal> predicate);

        ILeoValueRuleBuilder<T, TVal> Must(Func<TVal, CustomVerifyResult> func);

        ILeoWaitForMessageValueRuleBuilder<T, TVal> Must(Func<TVal, bool> func);

        ILeoValueRuleBuilder<T, TVal> In(ICollection<TVal> collection);

        ILeoValueRuleBuilder<T, TVal> In(params TVal[] objects);

        ILeoValueRuleBuilder<T, TVal> NotIn(ICollection<TVal> collection);

        ILeoValueRuleBuilder<T, TVal> NotIn(params TVal[] objects);
    }
}