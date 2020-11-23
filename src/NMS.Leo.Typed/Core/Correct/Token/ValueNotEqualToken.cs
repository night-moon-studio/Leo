using NMS.Leo.Metadata;

namespace NMS.Leo.Typed.Core.Correct.Token
{
    internal class ValueNotEqualToken : ValueToken
    {
        // ReSharper disable once InconsistentNaming
        public const string NAME = "Value not equal rule";

        private readonly object _value;

        public ValueNotEqualToken(LeoMember member, object value) : base(member)
        {
            _value = value;
        }

        public override CorrectValueOps Ops => CorrectValueOps.NotEqual;

        public override string TokenName => NAME;

        public override bool MutuallyExclusive => false;

        public override int[] MutuallyExclusiveFlags => NoMutuallyExclusiveFlags;

        public override CorrectVerifyVal ValidValue(object value)
        {
            var val = new CorrectVerifyVal {NameOfExecutedRule = NAME};

            if (value is null && _value is null)
            {
                UpdateVal(val, null);
            }

            else if (value != null && _value != null && value.Equals(_value))
            {
                UpdateVal(val, null);
            }

            return val;
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