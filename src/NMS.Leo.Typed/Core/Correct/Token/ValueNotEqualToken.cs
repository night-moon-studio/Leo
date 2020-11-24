﻿using System;
using System.Collections;
using NMS.Leo.Metadata;

namespace NMS.Leo.Typed.Core.Correct.Token
{
    internal class ValueNotEqualToken : ValueToken
    {
        // ReSharper disable once InconsistentNaming
        public const string NAME = "ValueNotEqualToken";

        private readonly object _valueToCompare;
        private readonly Type _typeOfValueToCompare;
        private readonly IEqualityComparer _comparer;

        public ValueNotEqualToken(LeoMember member, object valueToCompare,IEqualityComparer comparer) : base(member)
        {
            _valueToCompare = valueToCompare;
            _typeOfValueToCompare = _valueToCompare.GetType();
            _comparer = comparer;
        }

        public override CorrectValueOps Ops => CorrectValueOps.NotEqual;

        public override string TokenName => NAME;

        public override bool MutuallyExclusive => false;

        public override int[] MutuallyExclusiveFlags => NoMutuallyExclusiveFlags;

        public override CorrectVerifyVal ValidValue(object value)
        {
            var val = new CorrectVerifyVal {NameOfExecutedRule = NAME};

            var success = !ValidCore(value);
            
            if (!success)
            {
                UpdateVal(val, value);
            }

            // if (value is null && _valueToCompare is null)
            // {
            //     UpdateVal(val, null);
            // }
            //
            // else if (value is char charLeft && _valueToCompare is char charRight)
            // {
            //     if (charLeft == charRight)
            //     {
            //         UpdateVal(val, charLeft);
            //     }
            // }
            //
            // else if (Member.MemberType.IsPrimitive && Member.MemberType.IsValueType)
            // {
            //     var left = Convert.ToDecimal(value);
            //     var right = Convert.ToDecimal(_valueToCompare);
            //
            //     if (left == right)
            //     {
            //         UpdateVal(val, left);
            //     }
            // }
            //
            // else if (value != null && _valueToCompare != null && value.Equals(_valueToCompare))
            // {
            //     UpdateVal(val, null);
            // }

            return val;
        }

        private bool ValidCore(object value)
        {
            if (_comparer != null)
            {
                return _comparer.Equals(_valueToCompare, value);
            }

            if (Member.MemberType.IsValueType && _typeOfValueToCompare.IsValueType)
            {
                if (ValueTypeEqualCalculator.Valid(Member.MemberType, value, _typeOfValueToCompare, _valueToCompare))
                    return true;
            }

            return Equals(_valueToCompare, value);
        }

        private void UpdateVal(CorrectVerifyVal val, object obj)
        {
            val.IsSuccess = false;
            val.VerifiedValue = obj;
            val.ErrorMessage = $"The values must not be equal. The current value type is: {Member.MemberType}.";
        }

        public override string ToString() => NAME;
    }
}