using System;
using NMS.Leo.Metadata;

namespace NMS.Leo.Typed.Core.Correct.Token
{
    internal class ValueLessThanToken : ValueToken
    {
        // ReSharper disable once InconsistentNaming
        public const string NAME = "ValueLessThanToken";

        private readonly object _valueToCompare;
        private readonly Type _typeOfValueToCompare;

        public ValueLessThanToken(LeoMember member, object valueToCompare) : base(member)
        {
            _valueToCompare = valueToCompare;
            _typeOfValueToCompare = _valueToCompare.GetType();
        }

        public override CorrectValueOps Ops => CorrectValueOps.LessThan;

        public override string TokenName => NAME;

        public override bool MutuallyExclusive => false;

        public override int[] MutuallyExclusiveFlags => NoMutuallyExclusiveFlags;

        public override CorrectVerifyVal ValidValue(object value)
        {
            var val = new CorrectVerifyVal {NameOfExecutedRule = NAME};

            var success = ValidCore(value, out var message);

            if (!success)
            {
                UpdateVal(val, value, message);
            }

            return val;
        }

        private bool ValidCore(object value, out string message)
        {
            message = null;

            if (Member.MemberType.IsValueType && _typeOfValueToCompare.IsValueType)
            {
                if (ValueTypeEqualCalculator.CompareTo(Member.MemberType, value, _typeOfValueToCompare, _valueToCompare) < 0)
                {
                    return true;
                }
            }

            else if (value is IComparable c1 && _valueToCompare is IComparable c2)
            {
                if (c1.CompareTo(c2) < 0)
                    return true;
            }

            message = "Neither the given value nor the value being compared can be converted to IComparable.";
            return false;
        }

        private void UpdateVal(CorrectVerifyVal val, object obj, string message = null)
        {
            val.IsSuccess = false;
            val.VerifiedValue = obj;
            val.ErrorMessage = message ?? $"The given value must be less than {_valueToCompare}.";
        }

        public override string ToString() => NAME;
    }
}