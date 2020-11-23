using NMS.Leo.Metadata;
using NMS.Leo.Typed.Core.Members;

namespace NMS.Leo.Typed.Core.Correct.Token
{
    internal class ValueRequiredNullToken : ValueToken
    {
        // ReSharper disable once InconsistentNaming
        public const string NAME = "Value must be null";
        public static int[] _mutuallyExclusiveFlags = {90115, 90116, 90117, 90118};

        public ValueRequiredNullToken(LeoMember member) : base(member) { }

        public override CorrectValueOps Ops => CorrectValueOps.RequiredNull;

        public override string TokenName => NAME;

        public override bool MutuallyExclusive => true;

        public override int[] MutuallyExclusiveFlags => _mutuallyExclusiveFlags;

        public override CorrectVerifyVal ValidValue(object value)
        {
            var val = new CorrectVerifyVal {NameOfExecutedRule = NAME};

            switch (value)
            {
                case null:
                    break;

                case string s:
                    if (!string.IsNullOrEmpty(s)) UpdateVal(val, value);
                    break;

                default:
                    if (!Member.MemberType.IsValueType) UpdateVal(val, value);
                    break;
            }

            return val;
        }

        private void UpdateVal(CorrectVerifyVal val, object obj)
        {
            val.IsSuccess = false;
            val.VerifiedValue = obj;
            val.ErrorMessage = $"The value is must be null. The current value type is: {Member.MemberType}.";
        }

        public override string ToString() => NAME;
    }
}