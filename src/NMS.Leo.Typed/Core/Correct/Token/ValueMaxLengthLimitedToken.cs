using System;
using System.Collections;
using NMS.Leo.Metadata;

namespace NMS.Leo.Typed.Core.Correct.Token
{
    internal class ValueMaxLengthLimitedToken : ValueToken
    {
        // ReSharper disable once InconsistentNaming
        public const string NAME = "ArrayMaxLengthToken";
        public static int[] _mutuallyExclusiveFlags = {90112, 90116, 90120};

        private readonly int _maxLength;

        public ValueMaxLengthLimitedToken(LeoMember member, int max) : base(member)
        {
            if (max < 0)
                throw new ArgumentOutOfRangeException(nameof(max));
            _maxLength = max;
        }

        public override CorrectValueOps Ops => CorrectValueOps.MaxLen;

        public override string TokenName => NAME;

        public override bool MutuallyExclusive => true;

        public override int[] MutuallyExclusiveFlags => _mutuallyExclusiveFlags;

        public override CorrectVerifyVal ValidValue(object value)
        {
            var val = new CorrectVerifyVal {NameOfExecutedRule = NAME};

            if (value is string stringVal)
            {
                if (stringVal.Length > _maxLength)
                {
                    UpdateVal(val, value, stringVal.Length);
                }
            }

            else if (value is ICollection collection)
            {
                var len = collection.Count;
                if (len > _maxLength)
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
            val.ErrorMessage = MergeMessage($"The array length should be less than {_maxLength}, and the current length is {currentLength}.");
        }

        public override string ToString()
        {
            return $"{NAME}: The maximum length is {_maxLength}.";
        }
    }
}