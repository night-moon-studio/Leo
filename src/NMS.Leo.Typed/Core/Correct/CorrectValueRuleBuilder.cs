﻿using System;
using System.Collections;
using System.Collections.Generic;
using NMS.Leo.Metadata;
using NMS.Leo.Typed.Core.Correct.Token;
using NMS.Leo.Typed.Validation;

namespace NMS.Leo.Typed.Core.Correct
{
    internal class CorrectValueRuleBuilder : ILeoValueRuleBuilder
    {
        private readonly LeoMember _member;
        private readonly List<IValueToken> _valueTokens;

        public CorrectValueRuleBuilder(LeoMember member)
        {
            _member = member;
            _valueTokens = new List<IValueToken>();
        }

        public string Name => _member.MemberName;

        public CorrectValueRuleMode Mode { get; set; } = CorrectValueRuleMode.Append;

        public ILeoValueRuleBuilder AppendRule()
        {
            Mode = CorrectValueRuleMode.Append;
            return this;
        }

        public ILeoValueRuleBuilder OverwriteRule()
        {
            Mode = CorrectValueRuleMode.Overwrite;
            return this;
        }

        public ILeoValueRuleBuilder Empty()
        {
            _valueTokens.Add(new ValueEmptyToken(_member));
            return this;
        }

        public ILeoValueRuleBuilder NotEmpty()
        {
            _valueTokens.Add(new ValueNotEmptyToken(_member));
            return this;
        }

        public ILeoValueRuleBuilder Required()
        {
            _valueTokens.Add(new ValueNotEmptyToken(_member));
            return this;
        }

        public ILeoValueRuleBuilder Null()
        {
            _valueTokens.Add(new ValueNullToken(_member));
            return this;
        }

        public ILeoValueRuleBuilder NotNull()
        {
            _valueTokens.Add(new ValueNotNullToken(_member));
            return this;
        }

        public ILeoValueRuleBuilder Length(int min, int max)
        {
            _valueTokens.Add(new ValueLengthLimitedToken(_member, min, max));
            return this;
        }

        public ILeoValueRuleBuilder Range(object from, object to, RangeOptions options = RangeOptions.OpenInterval)
        {
            _valueTokens.Add(new ValueRangeToken(_member, from, to, options));
            return this;
        }

        public ILeoValueRuleBuilder RangeWithOpenInterval(object from, object to)
        {
            _valueTokens.Add(new ValueRangeToken(_member, from, to, RangeOptions.OpenInterval));
            return this;
        }

        public ILeoValueRuleBuilder RangeWithCloseInterval(object from, object to)
        {
            _valueTokens.Add(new ValueRangeToken(_member, from, to, RangeOptions.CloseInterval));
            return this;
        }

        public ILeoValueRuleBuilder MinLength(int min)
        {
            _valueTokens.Add(new ValueMinLengthLimitedToken(_member, min));
            return this;
        }

        public ILeoValueRuleBuilder MaxLength(int max)
        {
            _valueTokens.Add(new ValueMaxLengthLimitedToken(_member, max));
            return this;
        }

        public ILeoValueRuleBuilder AtLeast(int count)
        {
            _valueTokens.Add(new ValueMinLengthLimitedToken(_member, count));
            return this;
        }

        public ILeoValueRuleBuilder Equal(object value)
        {
            _valueTokens.Add(new ValueEqualToken(_member, value, null));
            return this;
        }

        public ILeoValueRuleBuilder Equal(object value, IEqualityComparer comparer)
        {
            _valueTokens.Add(new ValueEqualToken(_member, value, comparer));
            return this;
        }

        public ILeoValueRuleBuilder NotEqual(object value)
        {
            _valueTokens.Add(new ValueNotEqualToken(_member, value, null));
            return this;
        }

        public ILeoValueRuleBuilder NotEqual(object value, IEqualityComparer comparer)
        {
            _valueTokens.Add(new ValueNotEqualToken(_member, value, comparer));
            return this;
        }

        public ILeoValueRuleBuilder LessThan(object value)
        {
            _valueTokens.Add(new ValueLessThanToken(_member, value));
            return this;
        }

        public ILeoValueRuleBuilder LessThanOrEqual(object value)
        {
            _valueTokens.Add(new ValueLessThanOrEqualToken(_member, value));
            return this;
        }

        public ILeoValueRuleBuilder GreaterThan(object value)
        {
            _valueTokens.Add(new ValueGreaterThanToken(_member, value));
            return this;
        }

        public ILeoValueRuleBuilder GreaterThanOrEqual(object value)
        {
            _valueTokens.Add(new ValueGreaterThanOrEqualToken(_member, value));
            return this;
        }

        public ILeoValueRuleBuilder Func(Func<object, CustomVerifyResult> func)
        {
            _valueTokens.Add(new ValueFuncToken(_member, func));
            return this;
        }

        public ILeoWaitForMessageValueRuleBuilder Func(Func<object, bool> func)
        {
            return new CorrectWaitForMessageValueRuleBuilder(this, func);
        }

        public ILeoWaitForMessageValueRuleBuilder Predicate(Predicate<object> predicate)
        {
            return new CorrectWaitForMessageValueRuleBuilder(this, predicate);
        }

        public ILeoValueRuleBuilder Must(Func<object, CustomVerifyResult> func)
        {
            _valueTokens.Add(new ValueFuncToken(_member, func));
            return this;
        }

        public ILeoWaitForMessageValueRuleBuilder Must(Func<object, bool> func)
        {
            return new CorrectWaitForMessageValueRuleBuilder(this, func);
        }

        public ILeoValueRuleBuilder Any(Func<object, bool> func)
        {
            _valueTokens.Add(new ValueAnyToken(_member, func));
            return this;
        }

        public ILeoValueRuleBuilder All(Func<object, bool> func)
        {
            _valueTokens.Add(new ValueAllToken(_member, func));
            return this;
        }

        public ILeoValueRuleBuilder NotAny(Func<object, bool> func)
        {
            _valueTokens.Add(new ValueAllToken(_member, func));
            return this;
        }

        public ILeoValueRuleBuilder NotAll(Func<object, bool> func)
        {
            _valueTokens.Add(new ValueAnyToken(_member, func));
            return this;
        }

        public ILeoValueRuleBuilder In(ICollection<object> collection)
        {
            _valueTokens.Add(new ValueInToken(_member, collection));
            return this;
        }

        public ILeoValueRuleBuilder In(params object[] objects)
        {
            _valueTokens.Add(new ValueInToken(_member, objects));
            return this;
        }

        public ILeoValueRuleBuilder NotIn(ICollection<object> collection)
        {
            _valueTokens.Add(new ValueNotInToken(_member, collection));
            return this;
        }

        public ILeoValueRuleBuilder NotIn(params object[] objects)
        {
            _valueTokens.Add(new ValueNotInToken(_member, objects));
            return this;
        }

        public CorrectValueRule Build()
        {
            return new CorrectValueRule
            {
                Name = Name,
                Mode = Mode,
                Tokens = _valueTokens,
            };
        }
    }

    internal class CorrectValueRuleBuilder<T> : ILeoValueRuleBuilder<T>
    {
        internal readonly LeoMember _member;
        internal readonly List<IValueToken> _valueTokens;

        public CorrectValueRuleBuilder(LeoMember member)
        {
            _member = member;
            _valueTokens = new List<IValueToken>();
        }

        public string Name => _member.MemberName;

        public CorrectValueRuleMode Mode { get; set; } = CorrectValueRuleMode.Append;

        public ILeoValueRuleBuilder<T> AppendRule()
        {
            Mode = CorrectValueRuleMode.Append;
            return this;
        }

        public ILeoValueRuleBuilder<T> OverwriteRule()
        {
            Mode = CorrectValueRuleMode.Overwrite;
            return this;
        }

        public ILeoValueRuleBuilder<T> Empty()
        {
            _valueTokens.Add(new ValueEmptyToken(_member));
            return this;
        }

        public ILeoValueRuleBuilder<T> NotEmpty()
        {
            _valueTokens.Add(new ValueNotEmptyToken(_member));
            return this;
        }

        public ILeoValueRuleBuilder<T> Required()
        {
            _valueTokens.Add(new ValueNotEmptyToken(_member));
            return this;
        }

        public ILeoValueRuleBuilder<T> Null()
        {
            _valueTokens.Add(new ValueNullToken(_member));
            return this;
        }

        public ILeoValueRuleBuilder<T> NotNull()
        {
            _valueTokens.Add(new ValueNotNullToken(_member));
            return this;
        }

        public ILeoValueRuleBuilder<T> Range(object from, object to, RangeOptions options = RangeOptions.OpenInterval)
        {
            _valueTokens.Add(new ValueRangeToken(_member, from, to, options));
            return this;
        }

        public ILeoValueRuleBuilder<T> RangeWithOpenInterval(object from, object to)
        {
            _valueTokens.Add(new ValueRangeToken(_member, from, to, RangeOptions.OpenInterval));
            return this;
        }

        public ILeoValueRuleBuilder<T> RangeWithCloseInterval(object from, object to)
        {
            _valueTokens.Add(new ValueRangeToken(_member, from, to, RangeOptions.CloseInterval));
            return this;
        }

        public ILeoValueRuleBuilder<T> Length(int min, int max)
        {
            _valueTokens.Add(new ValueLengthLimitedToken(_member, min, max));
            return this;
        }

        public ILeoValueRuleBuilder<T> MinLength(int min)
        {
            _valueTokens.Add(new ValueMinLengthLimitedToken(_member, min));
            return this;
        }

        public ILeoValueRuleBuilder<T> MaxLength(int max)
        {
            _valueTokens.Add(new ValueMaxLengthLimitedToken(_member, max));
            return this;
        }

        public ILeoValueRuleBuilder<T> AtLeast(int count)
        {
            _valueTokens.Add(new ValueMinLengthLimitedToken(_member, count));
            return this;
        }

        public ILeoValueRuleBuilder<T> Equal(object value)
        {
            _valueTokens.Add(new ValueEqualToken(_member, value, null));
            return this;
        }

        public ILeoValueRuleBuilder<T> Equal(object value, IEqualityComparer comparer)
        {
            _valueTokens.Add(new ValueEqualToken(_member, value, comparer));
            return this;
        }

        public ILeoValueRuleBuilder<T> NotEqual(object value)
        {
            _valueTokens.Add(new ValueNotEqualToken(_member, value, null));
            return this;
        }

        public ILeoValueRuleBuilder<T> NotEqual(object value, IEqualityComparer comparer)
        {
            _valueTokens.Add(new ValueNotEqualToken(_member, value, comparer));
            return this;
        }

        public ILeoValueRuleBuilder<T> LessThan(object value)
        {
            _valueTokens.Add(new ValueLessThanToken(_member, value));
            return this;
        }

        public ILeoValueRuleBuilder<T> LessThanOrEqual(object value)
        {
            _valueTokens.Add(new ValueLessThanOrEqualToken(_member, value));
            return this;
        }

        public ILeoValueRuleBuilder<T> GreaterThan(object value)
        {
            _valueTokens.Add(new ValueGreaterThanToken(_member, value));
            return this;
        }

        public ILeoValueRuleBuilder<T> GreaterThanOrEqual(object value)
        {
            _valueTokens.Add(new ValueGreaterThanOrEqualToken(_member, value));
            return this;
        }

        public ILeoValueRuleBuilder<T> Func(Func<object, CustomVerifyResult> func)
        {
            _valueTokens.Add(new ValueFuncToken(_member, func));
            return this;
        }

        public ILeoWaitForMessageValueRuleBuilder<T> Func(Func<object, bool> func)
        {
            return new CorrectWaitForMessageValueRuleBuilder<T>(this, func);
        }

        public ILeoWaitForMessageValueRuleBuilder<T> Predicate(Predicate<object> predicate)
        {
            return new CorrectWaitForMessageValueRuleBuilder<T>(this, predicate);
        }

        public ILeoValueRuleBuilder<T> Must(Func<object, CustomVerifyResult> func)
        {
            _valueTokens.Add(new ValueFuncToken(_member, func));
            return this;
        }

        public ILeoWaitForMessageValueRuleBuilder<T> Must(Func<object, bool> func)
        {
            return new CorrectWaitForMessageValueRuleBuilder<T>(this, func);
        }

        public ILeoValueRuleBuilder<T> Any(Func<object, bool> func)
        {
            _valueTokens.Add(new ValueAnyToken(_member, func));
            return this;
        }

        public ILeoValueRuleBuilder<T> All(Func<object, bool> func)
        {
            _valueTokens.Add(new ValueAllToken(_member, func));
            return this;
        }

        public ILeoValueRuleBuilder<T> NotAny(Func<object, bool> func)
        {
            _valueTokens.Add(new ValueAllToken(_member, func));
            return this;
        }

        public ILeoValueRuleBuilder<T> NotAll(Func<object, bool> func)
        {
            _valueTokens.Add(new ValueAnyToken(_member, func));
            return this;
        }

        public ILeoValueRuleBuilder<T> In(ICollection<object> collection)
        {
            _valueTokens.Add(new ValueInToken(_member, collection));
            return this;
        }

        public ILeoValueRuleBuilder<T> In(params object[] objects)
        {
            _valueTokens.Add(new ValueInToken(_member, objects));
            return this;
        }

        public ILeoValueRuleBuilder<T> NotIn(ICollection<object> collection)
        {
            _valueTokens.Add(new ValueNotInToken(_member, collection));
            return this;
        }

        public ILeoValueRuleBuilder<T> NotIn(params object[] objects)
        {
            _valueTokens.Add(new ValueNotInToken(_member, objects));
            return this;
        }

        public CorrectValueRule Build()
        {
            return new CorrectValueRule
            {
                Name = Name,
                Mode = Mode,
                Tokens = _valueTokens,
            };
        }
    }

    internal class CorrectValueRuleBuilder<T, TVal> : CorrectValueRuleBuilder<T>, ILeoValueRuleBuilder<T, TVal>
    {
        public CorrectValueRuleBuilder(LeoMember member) : base(member) { }

        public new ILeoValueRuleBuilder<T, TVal> AppendRule()
        {
            Mode = CorrectValueRuleMode.Append;
            return this;
        }

        public new ILeoValueRuleBuilder<T, TVal> OverwriteRule()
        {
            Mode = CorrectValueRuleMode.Overwrite;
            return this;
        }

        public new ILeoValueRuleBuilder<T, TVal> Empty()
        {
            _valueTokens.Add(new ValueEmptyToken(_member));
            return this;
        }

        public new ILeoValueRuleBuilder<T, TVal> NotEmpty()
        {
            _valueTokens.Add(new ValueNotEmptyToken(_member));
            return this;
        }

        public new ILeoValueRuleBuilder<T, TVal> Required()
        {
            _valueTokens.Add(new ValueNotEmptyToken(_member));
            return this;
        }

        public new ILeoValueRuleBuilder<T, TVal> Null()
        {
            _valueTokens.Add(new ValueNullToken(_member));
            return this;
        }

        public new ILeoValueRuleBuilder<T, TVal> NotNull()
        {
            _valueTokens.Add(new ValueNotNullToken(_member));
            return this;
        }

        public ILeoValueRuleBuilder<T, TVal> Range(TVal from, TVal to, RangeOptions options = RangeOptions.OpenInterval)
        {
            _valueTokens.Add(new ValueRangeToken<TVal>(_member, from, to, options));
            return this;
        }

        public ILeoValueRuleBuilder<T, TVal> RangeWithOpenInterval(TVal from, TVal to)
        {
            _valueTokens.Add(new ValueRangeToken<TVal>(_member, from, to, RangeOptions.OpenInterval));
            return this;
        }

        public ILeoValueRuleBuilder<T, TVal> RangeWithCloseInterval(TVal from, TVal to)
        {
            _valueTokens.Add(new ValueRangeToken<TVal>(_member, from, to, RangeOptions.CloseInterval));
            return this;
        }

        public new ILeoValueRuleBuilder<T, TVal> Length(int min, int max)
        {
            _valueTokens.Add(new ValueLengthLimitedToken(_member, min, max));
            return this;
        }

        public new ILeoValueRuleBuilder<T, TVal> MinLength(int min)
        {
            _valueTokens.Add(new ValueMinLengthLimitedToken(_member, min));
            return this;
        }

        public new ILeoValueRuleBuilder<T, TVal> MaxLength(int max)
        {
            _valueTokens.Add(new ValueMaxLengthLimitedToken(_member, max));
            return this;
        }

        public new ILeoValueRuleBuilder<T, TVal> AtLeast(int count)
        {
            _valueTokens.Add(new ValueMinLengthLimitedToken(_member, count));
            return this;
        }

        public ILeoValueRuleBuilder<T, TVal> Equal(TVal value)
        {
            _valueTokens.Add(new ValueEqualToken<TVal>(_member, value, null));
            return this;
        }

        public ILeoValueRuleBuilder<T, TVal> Equal(TVal value, IEqualityComparer<TVal> comparer)
        {
            _valueTokens.Add(new ValueEqualToken<TVal>(_member, value, comparer));
            return this;
        }

        public ILeoValueRuleBuilder<T, TVal> NotEqual(TVal value)
        {
            _valueTokens.Add(new ValueNotEqualToken<TVal>(_member, value, null));
            return this;
        }

        public ILeoValueRuleBuilder<T, TVal> NotEqual(TVal value, IEqualityComparer<TVal> comparer)
        {
            _valueTokens.Add(new ValueNotEqualToken<TVal>(_member, value, comparer));
            return this;
        }

        public ILeoValueRuleBuilder<T, TVal> LessThan(TVal value)
        {
            _valueTokens.Add(new ValueLessThanToken<TVal>(_member, value));
            return this;
        }

        public ILeoValueRuleBuilder<T, TVal> LessThanOrEqual(TVal value)
        {
            _valueTokens.Add(new ValueLessThanOrEqualToken<TVal>(_member, value));
            return this;
        }

        public ILeoValueRuleBuilder<T, TVal> GreaterThan(TVal value)
        {
            _valueTokens.Add(new ValueGreaterThanToken<TVal>(_member, value));
            return this;
        }

        public ILeoValueRuleBuilder<T, TVal> GreaterThanOrEqual(TVal value)
        {
            _valueTokens.Add(new ValueGreaterThanOrEqualToken<TVal>(_member, value));
            return this;
        }

        public ILeoValueRuleBuilder<T, TVal> Func(Func<TVal, CustomVerifyResult> func)
        {
            _valueTokens.Add(new ValueFuncToken<TVal>(_member, func));
            return this;
        }

        public ILeoWaitForMessageValueRuleBuilder<T, TVal> Func(Func<TVal, bool> func)
        {
            return new CorrectWaitForMessageValueRuleBuilder<T, TVal>(this, func);
        }

        public ILeoWaitForMessageValueRuleBuilder<T, TVal> Predicate(Predicate<TVal> predicate)
        {
            return new CorrectWaitForMessageValueRuleBuilder<T, TVal>(this, predicate);
        }

        public ILeoValueRuleBuilder<T, TVal> Must(Func<TVal, CustomVerifyResult> func)
        {
            _valueTokens.Add(new ValueFuncToken<TVal>(_member, func));
            return this;
        }

        public ILeoWaitForMessageValueRuleBuilder<T, TVal> Must(Func<TVal, bool> func)
        {
            return new CorrectWaitForMessageValueRuleBuilder<T, TVal>(this, func);
        }

        public ILeoValueRuleBuilder<T, TVal> In(ICollection<TVal> collection)
        {
            _valueTokens.Add(new ValueInToken<TVal>(_member, collection));
            return this;
        }

        public ILeoValueRuleBuilder<T, TVal> In(params TVal[] objects)
        {
            _valueTokens.Add(new ValueInToken<TVal>(_member, objects));
            return this;
        }

        public ILeoValueRuleBuilder<T, TVal> NotIn(ICollection<TVal> collection)
        {
            _valueTokens.Add(new ValueNotInToken<TVal>(_member, collection));
            return this;
        }

        public ILeoValueRuleBuilder<T, TVal> NotIn(params TVal[] objects)
        {
            _valueTokens.Add(new ValueInToken<TVal>(_member, objects));
            return this;
        }

        public new CorrectValueRule<TVal> Build()
        {
            return new CorrectValueRule<TVal>
            {
                Name = Name,
                Mode = Mode,
                Tokens = _valueTokens,
            };
        }
    }
}