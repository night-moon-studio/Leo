using NMS.Leo.Metadata;

namespace NMS.Leo.Typed.Core.Correct.Token
{
    internal class ValueRequiredToken : ValueToken
    {
        // ReSharper disable once InconsistentNaming
        public const string NAME = "Value required rule";
        public static int[] _mutuallyExclusiveFlags = {90118};

        public ValueRequiredToken(LeoMember member) : base(member) { }

        public override CorrectValueOps Ops => CorrectValueOps.Required;

        public override string TokenName => NAME;

        public override bool MutuallyExclusive => true;

        public override int[] MutuallyExclusiveFlags => _mutuallyExclusiveFlags;

        public override CorrectVerifyVal ValidValue(object value)
        {
            var val = new CorrectVerifyVal {NameOfExecutedRule = NAME};

            switch (value)
            {
                case null:
                    UpdateVal(val, null);
                    break;

                case byte[] b:
                    if (b.Length == 0) UpdateVal(val, value);
                    break;

                case string s:
                    if (string.IsNullOrEmpty(s)) UpdateVal(val, value);
                    break;
            }

            return val;
        }

        private void UpdateVal(CorrectVerifyVal val, object obj)
        {
            val.IsSuccess = false;
            val.VerifiedValue = obj;
            val.ErrorMessage = $"The value is required. The current value type is: {Member.MemberType}.";
        }

        public override string ToString() => NAME;
    }
}