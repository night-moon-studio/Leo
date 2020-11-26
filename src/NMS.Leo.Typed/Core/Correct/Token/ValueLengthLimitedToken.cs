using System;
using System.Collections;
using NMS.Leo.Metadata;

namespace NMS.Leo.Typed.Core.Correct.Token
{
    internal class ValueLengthLimitedToken : ValueToken
    {
        // ReSharper disable once InconsistentNaming
        public const string NAME = "ArrayLengthToken";
        public static int[] _mutuallyExclusiveFlags = {90111, 90115, 90119, 90120};

        private readonly int _minLength;
        private readonly int _maxLength;

        public ValueLengthLimitedToken(LeoMember member, int min, int max) : base(member)
        {
            if (min < 0)
                throw new ArgumentOutOfRangeException(nameof(min));
            if (max != -1 && max < min)
                throw new ArgumentOutOfRangeException(nameof(max), "Max should be larger than min.");

            _minLength = min;
            _maxLength = max;
        }

        public override CorrectValueOps Ops => CorrectValueOps.Length;

        public override string TokenName => NAME;

        public override bool MutuallyExclusive => true;

        public override int[] MutuallyExclusiveFlags => _mutuallyExclusiveFlags;

        public override CorrectVerifyVal ValidValue(object value)
        {
            var val = new CorrectVerifyVal {NameOfExecutedRule = NAME};

            if (value is string stringVal)
            {
                var len = stringVal.Length;

                if (len < _minLength ||
                    len > _maxLength && _maxLength != -1)
                {
                    UpdateVal(val, value, len);
                }
            }

            else if (Member.MemberType == typeof(string) && _minLength > 0)
            {
                // for ""
                UpdateVal(val, value, 0);
            }

            else if (value is ICollection collection)
            {
                var len = collection.Count;
                if (len < _minLength ||
                    len > _maxLength && _maxLength != -1)
                {
                    UpdateVal(val, value, len);
                }
            }

            return val;
        }

        private void UpdateVal(CorrectVerifyVal val, object obj, int currentLength)
        {
            val.IsSuccess = false;
            val.VerifiedValue = obj;
            val.ErrorMessage = MergeMessage($"The array length should be greater than {_minLength} and less than {_maxLength}, and the current length is {currentLength}.");
        }

        public override string ToString()
        {
            return $"{NAME}: The minimum length is {_minLength}, and the maximum length is {_maxLength}.";
        }
    }
}