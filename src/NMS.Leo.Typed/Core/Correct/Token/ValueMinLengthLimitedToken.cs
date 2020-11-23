using System;
using NMS.Leo.Metadata;

namespace NMS.Leo.Typed.Core.Correct.Token
{
    internal class ValueMinLengthLimitedToken : ValueToken
    {
        // ReSharper disable once InconsistentNaming
        public const string NAME = "The minimum array length restriction rule";
        public static int[] _mutuallyExclusiveFlags = {90117, 90119};

        private readonly int _minLength;

        public ValueMinLengthLimitedToken(LeoMember member, int min) : base(member)
        {
            if (min < 0)
                throw new ArgumentOutOfRangeException(nameof(min));
            _minLength = min;
        }

        public override CorrectValueOps Ops => CorrectValueOps.StrMinLen;

        public override string TokenName => NAME;

        public override bool MutuallyExclusive => true;

        public override int[] MutuallyExclusiveFlags => _mutuallyExclusiveFlags;

        public override CorrectVerifyVal ValidValue(object value)
        {
            var val = new CorrectVerifyVal {NameOfExecutedRule = NAME};

            if (value is string stringVal)
            {
                if (stringVal.Length < _minLength)
                {
                    UpdateVal(val, value, stringVal.Length);
                }
            }

            else if (Member.MemberType == typeof(string) && _minLength > 0)
            {
                UpdateVal(val, value, 0);
            }

            else if (value is Array array)
            {
                if (array.Length < _minLength)
                {
                    UpdateVal(val, value, array.Length);
                }
            }

            return val;
        }

        private void UpdateVal(CorrectVerifyVal val, object obj, int currentLength)
        {
            val.IsSuccess = false;
            val.VerifiedValue = obj;
            val.ErrorMessage = $"The array length should be greater than {_minLength}, and the current length is {currentLength}.";
        }

        public override string ToString()
        {
            return $"{NAME}: The minimum length is {_minLength}.";
        }
    }
}