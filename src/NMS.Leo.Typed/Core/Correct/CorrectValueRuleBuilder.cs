using System;
using System.Collections;
using System.Collections.Generic;
using NMS.Leo.Metadata;
using NMS.Leo.Typed.Core.Correct.Token;
using NMS.Leo.Typed.Validation;

namespace NMS.Leo.Typed.Core.Correct
{
    internal class CorrectValueRuleBuilder : ILeoValueRuleBuilder
    {
        internal readonly LeoMember _member;

        private readonly List<IValueToken> _valueTokens;

        private IValueToken _currentTokenPtr;

        internal IValueToken CurrentToken
        {
            get => _currentTokenPtr;
            set
            {
                if (value != null)
                {
                    _currentTokenPtr = value;
                    _valueTokens.Add(value);
                }
            }
        }

        internal void ClearCurrentToken()
        {
            _currentTokenPtr = null;
        }

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
            CurrentToken = new ValueEmptyToken(_member);
            return this;
        }

        public ILeoValueRuleBuilder NotEmpty()
        {
            CurrentToken = new ValueNotEmptyToken(_member);
            return this;
        }

        public ILeoValueRuleBuilder Required()
        {
            CurrentToken = new ValueNotEmptyToken(_member);
            return this;
        }

        public ILeoValueRuleBuilder Null()
        {
            CurrentToken=new ValueNullToken(_member);
            return this;
        }

        public ILeoValueRuleBuilder NotNull()
        {
            CurrentToken = new ValueNotNullToken(_member);
            return this;
        }

        public ILeoValueRuleBuilder Length(int min, int max)
        {
            CurrentToken = new ValueLengthLimitedToken(_member, min, max);
            return this;
        }

        public ILeoValueRuleBuilder Range(object from, object to, RangeOptions options = RangeOptions.OpenInterval)
        {
            CurrentToken = new ValueRangeToken(_member, from, to, options);
            return this;
        }

        public ILeoValueRuleBuilder RangeWithOpenInterval(object from, object to)
        {
            CurrentToken = new ValueRangeToken(_member, from, to, RangeOptions.OpenInterval);
            return this;
        }

        public ILeoValueRuleBuilder RangeWithCloseInterval(object from, object to)
        {
            CurrentToken = new ValueRangeToken(_member, from, to, RangeOptions.CloseInterval);
            return this;
        }

        public ILeoValueRuleBuilder MinLength(int min)
        {
            CurrentToken = new ValueMinLengthLimitedToken(_member, min);
            return this;
        }

        public ILeoValueRuleBuilder MaxLength(int max)
        {
            CurrentToken = new ValueMaxLengthLimitedToken(_member, max);
            return this;
        }

        public ILeoValueRuleBuilder AtLeast(int count)
        {
            CurrentToken = new ValueMinLengthLimitedToken(_member, count);
            return this;
        }

        public ILeoValueRuleBuilder Equal(object value)
        {
            CurrentToken = new ValueEqualToken(_member, value, null);
            return this;
        }

        public ILeoValueRuleBuilder Equal(object value, IEqualityComparer comparer)
        {
            CurrentToken = new ValueEqualToken(_member, value, comparer);
            return this;
        }

        public ILeoValueRuleBuilder NotEqual(object value)
        {
            CurrentToken = new ValueNotEqualToken(_member, value, null);
            return this;
        }

        public ILeoValueRuleBuilder NotEqual(object value, IEqualityComparer comparer)
        {
            CurrentToken = new ValueNotEqualToken(_member, value, comparer);
            return this;
        }

        public ILeoValueRuleBuilder LessThan(object value)
        {
            CurrentToken = new ValueLessThanToken(_member, value);
            return this;
        }

        public ILeoValueRuleBuilder LessThanOrEqual(object value)
        {
            CurrentToken = new ValueLessThanOrEqualToken(_member, value);
            return this;
        }

        public ILeoValueRuleBuilder GreaterThan(object value)
        {
            CurrentToken = new ValueGreaterThanToken(_member, value);
            return this;
        }

        public ILeoValueRuleBuilder GreaterThanOrEqual(object value)
        {
            CurrentToken = new ValueGreaterThanOrEqualToken(_member, value);
            return this;
        }

        public ILeoValueRuleBuilder Func(Func<object, CustomVerifyResult> func)
        {
            CurrentToken = new ValueFuncToken(_member, func);
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
            CurrentToken = new ValueFuncToken(_member, func);
            return this;
        }

        public ILeoWaitForMessageValueRuleBuilder Must(Func<object, bool> func)
        {
            return new CorrectWaitForMessageValueRuleBuilder(this, func);
        }

        public ILeoValueRuleBuilder Any(Func<object, bool> func)
        {
            CurrentToken = new ValueAnyToken(_member, func);
            return this;
        }

        public ILeoValueRuleBuilder All(Func<object, bool> func)
        {
            CurrentToken = new ValueAllToken(_member, func);
            return this;
        }

        public ILeoValueRuleBuilder NotAny(Func<object, bool> func)
        {
            CurrentToken = new ValueAllToken(_member, func);
            return this;
        }

        public ILeoValueRuleBuilder NotAll(Func<object, bool> func)
        {
            CurrentToken = new ValueAnyToken(_member, func);
            return this;
        }

        public ILeoValueRuleBuilder In(ICollection<object> collection)
        {
            CurrentToken = new ValueInToken(_member, collection);
            return this;
        }

        public ILeoValueRuleBuilder In(params object[] objects)
        {
            CurrentToken = new ValueInToken(_member, objects);
            return this;
        }

        public ILeoValueRuleBuilder NotIn(ICollection<object> collection)
        {
            CurrentToken = new ValueNotInToken(_member, collection);
            return this;
        }

        public ILeoValueRuleBuilder NotIn(params object[] objects)
        {
            CurrentToken = new ValueNotInToken(_member, objects);
            return this;
        }

        public ILeoValueRuleBuilder InEnum(Type enumType)
        {
            CurrentToken = new ValueEnumToken(_member, enumType);
            return this;
        }

        public ILeoValueRuleBuilder InEnum<TEnum>()
        {
            CurrentToken = new ValueEnumToken<TEnum>(_member);
            return this;
        }

        public ILeoValueRuleBuilder IsEnumName(Type enumType, bool caseSensitive)
        {
            CurrentToken = new ValueStringEnumToken(_member, enumType, caseSensitive);
            return this;
        }

        public ILeoValueRuleBuilder IsEnumName<TEnum>(bool caseSensitive)
        {
            CurrentToken = new ValueStringEnumToken<TEnum>(_member, caseSensitive);
            return this;
        }

        public ILeoValueRuleBuilder ScalePrecision(int scale, int precision, bool ignoreTrailingZeros = false)
        {
            CurrentToken = new ValueScalePrecisionToken(_member, scale, precision, ignoreTrailingZeros);
            return this;
        }

        public ILeoValueRuleBuilder RequiredType(Type type)
        {
            CurrentToken = new ValueRequiredTypeToken(_member, type);
            return this;
        }

        public ILeoValueRuleBuilder RequiredTypes(params Type[] types)
        {
            CurrentToken = new ValueRequiredTypesToken(_member, types);
            return this;
        }

        public ILeoValueRuleBuilder RequiredTypes<T1>()
        {
            CurrentToken = new ValueRequiredTypeToken<T1>(_member);
            return this;
        }

        public ILeoValueRuleBuilder RequiredTypes<T1, T2>()
        {
            CurrentToken = new ValueRequiredTypesToken<T1, T2>(_member);
            return this;
        }

        public ILeoValueRuleBuilder RequiredTypes<T1, T2, T3>()
        {
            CurrentToken = new ValueRequiredTypesToken<T1, T2, T3>(_member);
            return this;
        }

        public ILeoValueRuleBuilder RequiredTypes<T1, T2, T3, T4>()
        {
            CurrentToken = new ValueRequiredTypesToken<T1, T2, T3, T4>(_member);
            return this;
        }

        public ILeoValueRuleBuilder RequiredTypes<T1, T2, T3, T4, T5>()
        {
            CurrentToken = new ValueRequiredTypesToken<T1, T2, T3, T4, T5>(_member);
            return this;
        }

        public ILeoValueRuleBuilder RequiredTypes<T1, T2, T3, T4, T5, T6>()
        {
            CurrentToken = new ValueRequiredTypesToken<T1, T2, T3, T4, T5, T6>(_member);
            return this;
        }

        public ILeoValueRuleBuilder RequiredTypes<T1, T2, T3, T4, T5, T6, T7>()
        {
            CurrentToken = new ValueRequiredTypesToken<T1, T2, T3, T4, T5, T6, T7>(_member);
            return this;
        }

        public ILeoValueRuleBuilder RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8>()
        {
            CurrentToken = new ValueRequiredTypesToken<T1, T2, T3, T4, T5, T6, T7, T8>(_member);
            return this;
        }

        public ILeoValueRuleBuilder RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9>()
        {
            CurrentToken = new ValueRequiredTypesToken<T1, T2, T3, T4, T5, T6, T7, T8, T9>(_member);
            return this;
        }

        public ILeoValueRuleBuilder RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>()
        {
            CurrentToken = new ValueRequiredTypesToken<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(_member);
            return this;
        }

        public ILeoValueRuleBuilder RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>()
        {
            CurrentToken = new ValueRequiredTypesToken<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(_member);
            return this;
        }

        public ILeoValueRuleBuilder RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>()
        {
            CurrentToken = new ValueRequiredTypesToken<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(_member);
            return this;
        }

        public ILeoValueRuleBuilder RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>()
        {
            CurrentToken = new ValueRequiredTypesToken<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(_member);
            return this;
        }

        public ILeoValueRuleBuilder RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>()
        {
            CurrentToken = new ValueRequiredTypesToken<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(_member);
            return this;
        }

        public ILeoValueRuleBuilder RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>()
        {
            CurrentToken = new ValueRequiredTypesToken<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(_member);
            return this;
        }

        public ILeoValueRuleBuilder RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>()
        {
            CurrentToken = new ValueRequiredTypesToken<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(_member);
            return this;
        }

        public CorrectValueRule Build()
        {
            ClearCurrentToken();
            
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
        protected readonly List<IValueToken> _valueTokens;

        private IValueToken _currentTokenPtr;

        internal IValueToken CurrentToken
        {
            get => _currentTokenPtr;
            set
            {
                if (value != null)
                {
                    _currentTokenPtr = value;
                    _valueTokens.Add(value);
                }
            }
        }

        internal void ClearCurrentToken()
        {
            _currentTokenPtr = null;
        }

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
            CurrentToken = new ValueEmptyToken(_member);
            return this;
        }

        public ILeoValueRuleBuilder<T> NotEmpty()
        {
            CurrentToken = new ValueNotEmptyToken(_member);
            return this;
        }

        public ILeoValueRuleBuilder<T> Required()
        {
            CurrentToken = new ValueNotEmptyToken(_member);
            return this;
        }

        public ILeoValueRuleBuilder<T> Null()
        {
            CurrentToken = new ValueNullToken(_member);
            return this;
        }

        public ILeoValueRuleBuilder<T> NotNull()
        {
            CurrentToken = new ValueNotNullToken(_member);
            return this;
        }

        public ILeoValueRuleBuilder<T> Range(object from, object to, RangeOptions options = RangeOptions.OpenInterval)
        {
            CurrentToken = new ValueRangeToken(_member, from, to, options);
            return this;
        }

        public ILeoValueRuleBuilder<T> RangeWithOpenInterval(object from, object to)
        {
            CurrentToken = new ValueRangeToken(_member, from, to, RangeOptions.OpenInterval);
            return this;
        }

        public ILeoValueRuleBuilder<T> RangeWithCloseInterval(object from, object to)
        {
            CurrentToken = new ValueRangeToken(_member, from, to, RangeOptions.CloseInterval);
            return this;
        }

        public ILeoValueRuleBuilder<T> Length(int min, int max)
        {
            CurrentToken = new ValueLengthLimitedToken(_member, min, max);
            return this;
        }

        public ILeoValueRuleBuilder<T> MinLength(int min)
        {
            CurrentToken = new ValueMinLengthLimitedToken(_member, min);
            return this;
        }

        public ILeoValueRuleBuilder<T> MaxLength(int max)
        {
            CurrentToken = new ValueMaxLengthLimitedToken(_member, max);
            return this;
        }

        public ILeoValueRuleBuilder<T> AtLeast(int count)
        {
            CurrentToken = new ValueMinLengthLimitedToken(_member, count);
            return this;
        }

        public ILeoValueRuleBuilder<T> Equal(object value)
        {
            CurrentToken = new ValueEqualToken(_member, value, null);
            return this;
        }

        public ILeoValueRuleBuilder<T> Equal(object value, IEqualityComparer comparer)
        {
            CurrentToken = new ValueEqualToken(_member, value, comparer);
            return this;
        }

        public ILeoValueRuleBuilder<T> NotEqual(object value)
        {
            CurrentToken = new ValueNotEqualToken(_member, value, null);
            return this;
        }

        public ILeoValueRuleBuilder<T> NotEqual(object value, IEqualityComparer comparer)
        {
            CurrentToken = new ValueNotEqualToken(_member, value, comparer);
            return this;
        }

        public ILeoValueRuleBuilder<T> LessThan(object value)
        {
            CurrentToken = new ValueLessThanToken(_member, value);
            return this;
        }

        public ILeoValueRuleBuilder<T> LessThanOrEqual(object value)
        {
            CurrentToken = new ValueLessThanOrEqualToken(_member, value);
            return this;
        }

        public ILeoValueRuleBuilder<T> GreaterThan(object value)
        {
            CurrentToken = new ValueGreaterThanToken(_member, value);
            return this;
        }

        public ILeoValueRuleBuilder<T> GreaterThanOrEqual(object value)
        {
            CurrentToken = new ValueGreaterThanOrEqualToken(_member, value);
            return this;
        }

        public ILeoValueRuleBuilder<T> Func(Func<object, CustomVerifyResult> func)
        {
            CurrentToken = new ValueFuncToken(_member, func);
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
            CurrentToken = new ValueFuncToken(_member, func);
            return this;
        }

        public ILeoWaitForMessageValueRuleBuilder<T> Must(Func<object, bool> func)
        {
            return new CorrectWaitForMessageValueRuleBuilder<T>(this, func);
        }

        public ILeoValueRuleBuilder<T> Any(Func<object, bool> func)
        {
            CurrentToken = new ValueAnyToken(_member, func);
            return this;
        }

        public ILeoValueRuleBuilder<T> All(Func<object, bool> func)
        {
            CurrentToken = new ValueAllToken(_member, func);
            return this;
        }

        public ILeoValueRuleBuilder<T> NotAny(Func<object, bool> func)
        {
            CurrentToken = new ValueAllToken(_member, func);
            return this;
        }

        public ILeoValueRuleBuilder<T> NotAll(Func<object, bool> func)
        {
            CurrentToken = new ValueAnyToken(_member, func);
            return this;
        }

        public ILeoValueRuleBuilder<T> In(ICollection<object> collection)
        {
            CurrentToken = new ValueInToken(_member, collection);
            return this;
        }

        public ILeoValueRuleBuilder<T> In(params object[] objects)
        {
            CurrentToken = new ValueInToken(_member, objects);
            return this;
        }

        public ILeoValueRuleBuilder<T> NotIn(ICollection<object> collection)
        {
            CurrentToken = new ValueNotInToken(_member, collection);
            return this;
        }

        public ILeoValueRuleBuilder<T> NotIn(params object[] objects)
        {
            CurrentToken = new ValueNotInToken(_member, objects);
            return this;
        }

        public ILeoValueRuleBuilder<T> InEnum(Type enumType)
        {
            CurrentToken = new ValueEnumToken(_member, enumType);
            return this;
        }

        public ILeoValueRuleBuilder<T> InEnum<TEnum>()
        {
            CurrentToken = new ValueEnumToken<TEnum>(_member);
            return this;
        }

        public ILeoValueRuleBuilder<T> IsEnumName(Type enumType, bool caseSensitive)
        {
            CurrentToken = new ValueStringEnumToken(_member, enumType, caseSensitive);
            return this;
        }

        public ILeoValueRuleBuilder<T> IsEnumName<TEnum>(bool caseSensitive)
        {
            CurrentToken = new ValueStringEnumToken<TEnum>(_member, caseSensitive);
            return this;
        }

        public ILeoValueRuleBuilder<T> ScalePrecision(int scale, int precision, bool ignoreTrailingZeros = false)
        {
            CurrentToken = new ValueScalePrecisionToken(_member, scale, precision, ignoreTrailingZeros);
            return this;
        }

        public ILeoValueRuleBuilder<T> RequiredType(Type type)
        {
            CurrentToken = new ValueRequiredTypeToken(_member, type);
            return this;
        }

        public ILeoValueRuleBuilder<T> RequiredTypes(params Type[] types)
        {
            CurrentToken = new ValueRequiredTypesToken(_member, types);
            return this;
        }

        public ILeoValueRuleBuilder<T> RequiredTypes<T1>()
        {
            CurrentToken = new ValueRequiredTypeToken<T1>(_member);
            return this;
        }

        public ILeoValueRuleBuilder<T> RequiredTypes<T1, T2>()
        {
            CurrentToken = new ValueRequiredTypesToken<T1, T2>(_member);
            return this;
        }

        public ILeoValueRuleBuilder<T> RequiredTypes<T1, T2, T3>()
        {
            CurrentToken = new ValueRequiredTypesToken<T1, T2, T3>(_member);
            return this;
        }

        public ILeoValueRuleBuilder<T> RequiredTypes<T1, T2, T3, T4>()
        {
            CurrentToken = new ValueRequiredTypesToken<T1, T2, T3, T4>(_member);
            return this;
        }

        public ILeoValueRuleBuilder<T> RequiredTypes<T1, T2, T3, T4, T5>()
        {
            CurrentToken = new ValueRequiredTypesToken<T1, T2, T3, T4, T5>(_member);
            return this;
        }

        public ILeoValueRuleBuilder<T> RequiredTypes<T1, T2, T3, T4, T5, T6>()
        {
            CurrentToken = new ValueRequiredTypesToken<T1, T2, T3, T4, T5, T6>(_member);
            return this;
        }

        public ILeoValueRuleBuilder<T> RequiredTypes<T1, T2, T3, T4, T5, T6, T7>()
        {
            CurrentToken = new ValueRequiredTypesToken<T1, T2, T3, T4, T5, T6, T7>(_member);
            return this;
        }

        public ILeoValueRuleBuilder<T> RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8>()
        {
            CurrentToken = new ValueRequiredTypesToken<T1, T2, T3, T4, T5, T6, T7, T8>(_member);
            return this;
        }

        public ILeoValueRuleBuilder<T> RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9>()
        {
            CurrentToken = new ValueRequiredTypesToken<T1, T2, T3, T4, T5, T6, T7, T8, T9>(_member);
            return this;
        }

        public ILeoValueRuleBuilder<T> RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>()
        {
            CurrentToken = new ValueRequiredTypesToken<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(_member);
            return this;
        }

        public ILeoValueRuleBuilder<T> RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>()
        {
            CurrentToken = new ValueRequiredTypesToken<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(_member);
            return this;
        }

        public ILeoValueRuleBuilder<T> RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>()
        {
            CurrentToken = new ValueRequiredTypesToken<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(_member);
            return this;
        }

        public ILeoValueRuleBuilder<T> RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>()
        {
            CurrentToken = new ValueRequiredTypesToken<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(_member);
            return this;
        }

        public ILeoValueRuleBuilder<T> RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>()
        {
            CurrentToken = new ValueRequiredTypesToken<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(_member);
            return this;
        }

        public ILeoValueRuleBuilder<T> RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>()
        {
            CurrentToken = new ValueRequiredTypesToken<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(_member);
            return this;
        }

        public ILeoValueRuleBuilder<T> RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>()
        {
            CurrentToken = new ValueRequiredTypesToken<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(_member);
            return this;
        }

        public CorrectValueRule Build()
        {
            ClearCurrentToken();

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
            CurrentToken = new ValueEmptyToken(_member);
            return this;
        }

        public new ILeoValueRuleBuilder<T, TVal> NotEmpty()
        {
            CurrentToken = new ValueNotEmptyToken(_member);
            return this;
        }

        public new ILeoValueRuleBuilder<T, TVal> Required()
        {
            CurrentToken = new ValueNotEmptyToken(_member);
            return this;
        }

        public new ILeoValueRuleBuilder<T, TVal> Null()
        {
            CurrentToken = new ValueNullToken(_member);
            return this;
        }

        public new ILeoValueRuleBuilder<T, TVal> NotNull()
        {
            CurrentToken = new ValueNotNullToken(_member);
            return this;
        }

        public ILeoValueRuleBuilder<T, TVal> Range(TVal from, TVal to, RangeOptions options = RangeOptions.OpenInterval)
        {
            CurrentToken = new ValueRangeToken<TVal>(_member, from, to, options);
            return this;
        }

        public ILeoValueRuleBuilder<T, TVal> RangeWithOpenInterval(TVal from, TVal to)
        {
            CurrentToken = new ValueRangeToken<TVal>(_member, from, to, RangeOptions.OpenInterval);
            return this;
        }

        public ILeoValueRuleBuilder<T, TVal> RangeWithCloseInterval(TVal from, TVal to)
        {
            CurrentToken = new ValueRangeToken<TVal>(_member, from, to, RangeOptions.CloseInterval);
            return this;
        }

        public new ILeoValueRuleBuilder<T, TVal> Length(int min, int max)
        {
            CurrentToken = new ValueLengthLimitedToken(_member, min, max);
            return this;
        }

        public new ILeoValueRuleBuilder<T, TVal> MinLength(int min)
        {
            CurrentToken = new ValueMinLengthLimitedToken(_member, min);
            return this;
        }

        public new ILeoValueRuleBuilder<T, TVal> MaxLength(int max)
        {
            CurrentToken = new ValueMaxLengthLimitedToken(_member, max);
            return this;
        }

        public new ILeoValueRuleBuilder<T, TVal> AtLeast(int count)
        {
            CurrentToken = new ValueMinLengthLimitedToken(_member, count);
            return this;
        }

        public ILeoValueRuleBuilder<T, TVal> Equal(TVal value)
        {
            CurrentToken = new ValueEqualToken<TVal>(_member, value, null);
            return this;
        }

        public ILeoValueRuleBuilder<T, TVal> Equal(TVal value, IEqualityComparer<TVal> comparer)
        {
            CurrentToken = new ValueEqualToken<TVal>(_member, value, comparer);
            return this;
        }

        public ILeoValueRuleBuilder<T, TVal> NotEqual(TVal value)
        {
            CurrentToken = new ValueNotEqualToken<TVal>(_member, value, null);
            return this;
        }

        public ILeoValueRuleBuilder<T, TVal> NotEqual(TVal value, IEqualityComparer<TVal> comparer)
        {
            CurrentToken = new ValueNotEqualToken<TVal>(_member, value, comparer);
            return this;
        }

        public ILeoValueRuleBuilder<T, TVal> LessThan(TVal value)
        {
            CurrentToken = new ValueLessThanToken<TVal>(_member, value);
            return this;
        }

        public ILeoValueRuleBuilder<T, TVal> LessThanOrEqual(TVal value)
        {
            CurrentToken = new ValueLessThanOrEqualToken<TVal>(_member, value);
            return this;
        }

        public ILeoValueRuleBuilder<T, TVal> GreaterThan(TVal value)
        {
            CurrentToken = new ValueGreaterThanToken<TVal>(_member, value);
            return this;
        }

        public ILeoValueRuleBuilder<T, TVal> GreaterThanOrEqual(TVal value)
        {
            CurrentToken = new ValueGreaterThanOrEqualToken<TVal>(_member, value);
            return this;
        }

        public ILeoValueRuleBuilder<T, TVal> Func(Func<TVal, CustomVerifyResult> func)
        {
            CurrentToken = new ValueFuncToken<TVal>(_member, func);
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
            CurrentToken = new ValueFuncToken<TVal>(_member, func);
            return this;
        }

        public ILeoWaitForMessageValueRuleBuilder<T, TVal> Must(Func<TVal, bool> func)
        {
            return new CorrectWaitForMessageValueRuleBuilder<T, TVal>(this, func);
        }

        public ILeoValueRuleBuilder<T, TVal> In(ICollection<TVal> collection)
        {
            CurrentToken = new ValueInToken<TVal>(_member, collection);
            return this;
        }

        public ILeoValueRuleBuilder<T, TVal> In(params TVal[] objects)
        {
            CurrentToken = new ValueInToken<TVal>(_member, objects);
            return this;
        }

        public ILeoValueRuleBuilder<T, TVal> NotIn(ICollection<TVal> collection)
        {
            CurrentToken = new ValueNotInToken<TVal>(_member, collection);
            return this;
        }

        public ILeoValueRuleBuilder<T, TVal> NotIn(params TVal[] objects)
        {
            CurrentToken = new ValueInToken<TVal>(_member, objects);
            return this;
        }

        public new ILeoValueRuleBuilder<T, TVal> InEnum(Type enumType)
        {
            CurrentToken = new ValueEnumToken(_member, enumType);
            return this;
        }

        public new ILeoValueRuleBuilder<T, TVal> InEnum<TEnum>()
        {
            CurrentToken = new ValueEnumToken<TEnum>(_member);
            return this;
        }

        public new ILeoValueRuleBuilder<T, TVal> IsEnumName(Type enumType, bool caseSensitive)
        {
            CurrentToken = new ValueStringEnumToken(_member, enumType, caseSensitive);
            return this;
        }

        public new ILeoValueRuleBuilder<T, TVal> IsEnumName<TEnum>(bool caseSensitive)
        {
            CurrentToken = new ValueStringEnumToken<TEnum>(_member, caseSensitive);
            return this;
        }

        public new ILeoValueRuleBuilder<T, TVal> ScalePrecision(int scale, int precision, bool ignoreTrailingZeros = false)
        {
            CurrentToken = new ValueScalePrecisionToken(_member, scale, precision, ignoreTrailingZeros);
            return this;
        }

        public new ILeoValueRuleBuilder<T, TVal> RequiredType(Type type)
        {
            CurrentToken = new ValueRequiredTypeToken(_member, type);
            return this;
        }

        public new ILeoValueRuleBuilder<T, TVal> RequiredTypes(params Type[] types)
        {
            CurrentToken = new ValueRequiredTypesToken(_member, types);
            return this;
        }

        public new ILeoValueRuleBuilder<T, TVal> RequiredTypes<T1>()
        {
            CurrentToken = new ValueRequiredTypeToken<T1>(_member);
            return this;
        }

        public new ILeoValueRuleBuilder<T, TVal> RequiredTypes<T1, T2>()
        {
            CurrentToken = new ValueRequiredTypesToken<T1, T2>(_member);
            return this;
        }

        public new ILeoValueRuleBuilder<T, TVal> RequiredTypes<T1, T2, T3>()
        {
            CurrentToken = new ValueRequiredTypesToken<T1, T2, T3>(_member);
            return this;
        }

        public new ILeoValueRuleBuilder<T, TVal> RequiredTypes<T1, T2, T3, T4>()
        {
            CurrentToken = new ValueRequiredTypesToken<T1, T2, T3, T4>(_member);
            return this;
        }

        public new ILeoValueRuleBuilder<T, TVal> RequiredTypes<T1, T2, T3, T4, T5>()
        {
            CurrentToken = new ValueRequiredTypesToken<T1, T2, T3, T4, T5>(_member);
            return this;
        }

        public new ILeoValueRuleBuilder<T, TVal> RequiredTypes<T1, T2, T3, T4, T5, T6>()
        {
            CurrentToken = new ValueRequiredTypesToken<T1, T2, T3, T4, T5, T6>(_member);
            return this;
        }

        public new ILeoValueRuleBuilder<T, TVal> RequiredTypes<T1, T2, T3, T4, T5, T6, T7>()
        {
            CurrentToken = new ValueRequiredTypesToken<T1, T2, T3, T4, T5, T6, T7>(_member);
            return this;
        }

        public new ILeoValueRuleBuilder<T, TVal> RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8>()
        {
            CurrentToken = new ValueRequiredTypesToken<T1, T2, T3, T4, T5, T6, T7, T8>(_member);
            return this;
        }

        public new ILeoValueRuleBuilder<T, TVal> RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9>()
        {
            CurrentToken = new ValueRequiredTypesToken<T1, T2, T3, T4, T5, T6, T7, T8, T9>(_member);
            return this;
        }

        public new ILeoValueRuleBuilder<T, TVal> RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>()
        {
            CurrentToken = new ValueRequiredTypesToken<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(_member);
            return this;
        }

        public new ILeoValueRuleBuilder<T, TVal> RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>()
        {
            CurrentToken = new ValueRequiredTypesToken<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(_member);
            return this;
        }

        public new ILeoValueRuleBuilder<T, TVal> RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>()
        {
            CurrentToken = new ValueRequiredTypesToken<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(_member);
            return this;
        }

        public new ILeoValueRuleBuilder<T, TVal> RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>()
        {
            CurrentToken = new ValueRequiredTypesToken<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(_member);
            return this;
        }

        public new ILeoValueRuleBuilder<T, TVal> RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>()
        {
            CurrentToken = new ValueRequiredTypesToken<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(_member);
            return this;
        }

        public new ILeoValueRuleBuilder<T, TVal> RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>()
        {
            CurrentToken = new ValueRequiredTypesToken<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(_member);
            return this;
        }

        public new ILeoValueRuleBuilder<T, TVal> RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>()
        {
            CurrentToken = new ValueRequiredTypesToken<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(_member);
            return this;
        }

        public new CorrectValueRule<TVal> Build()
        {
            ClearCurrentToken();

            return new CorrectValueRule<TVal>
            {
                Name = Name,
                Mode = Mode,
                Tokens = _valueTokens,
            };
        }
    }
}