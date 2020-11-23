using System;
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

        public ILeoValueRuleBuilder Required()
        {
            _valueTokens.Add(new ValueRequiredToken(_member));
            return this;
        }

        public ILeoValueRuleBuilder RequiredNull()
        {
            _valueTokens.Add(new ValueRequiredNullToken(_member));
            return this;
        }

        public ILeoValueRuleBuilder Length(int min, int max)
        {
            _valueTokens.Add(new ValueMaxAndMinLengthLimitedToken(_member, min, max));
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

        public ILeoValueRuleBuilder NotEqual(object value)
        {
            _valueTokens.Add(new ValueNotEqualToken(_member, value));
            return this;
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
        protected readonly LeoMember _member;
        protected readonly List<IValueToken> _valueTokens;

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

        public ILeoValueRuleBuilder<T> Required()
        {
            _valueTokens.Add(new ValueRequiredToken(_member));
            return this;
        }

        public ILeoValueRuleBuilder<T> RequiredNull()
        {
            _valueTokens.Add(new ValueRequiredNullToken(_member));
            return this;
        }

        public ILeoValueRuleBuilder<T> Length(int min, int max)
        {
            _valueTokens.Add(new ValueMaxAndMinLengthLimitedToken(_member, min, max));
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

        public ILeoValueRuleBuilder<T> NotEqual(object value)
        {
            _valueTokens.Add(new ValueNotEqualToken(_member, value));
            return this;
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

        public new ILeoValueRuleBuilder<T, TVal> Required()
        {
            _valueTokens.Add(new ValueRequiredToken(_member));
            return this;
        }

        public new ILeoValueRuleBuilder<T, TVal> RequiredNull()
        {
            _valueTokens.Add(new ValueRequiredNullToken(_member));
            return this;
        }

        public new ILeoValueRuleBuilder<T, TVal> Length(int min, int max)
        {
            _valueTokens.Add(new ValueMaxAndMinLengthLimitedToken(_member, min, max));
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

        public new ILeoValueRuleBuilder<T, TVal> NotEqual(object value)
        {
            _valueTokens.Add(new ValueNotEqualToken(_member, value));
            return this;
        }

        public new ILeoValueRuleBuilder<T, TVal> Must(Func<object, CustomVerifyResult> func)
        {
            _valueTokens.Add(new ValueFuncToken(_member, func));
            return this;
        }

        public new ILeoWaitForMessageValueRuleBuilder<T, TVal> Must(Func<object, bool> func)
        {
            return new CorrectWaitForMessageValueRuleBuilder<T, TVal>(this, func);
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