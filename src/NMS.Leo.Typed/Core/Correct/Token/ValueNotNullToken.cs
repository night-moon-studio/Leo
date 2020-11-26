using NMS.Leo.Metadata;

namespace NMS.Leo.Typed.Core.Correct.Token
{
    internal class ValueNotNullToken : ValueToken
    {
        // ReSharper disable once InconsistentNaming
        public const string NAME = "NullValueToken";

        public static int[] _mutuallyExclusiveFlags = {90111, 90112, 90113, 90114};

        public ValueNotNullToken(LeoMember member) : base(member) { }

        public override CorrectValueOps Ops => CorrectValueOps.Null;

        public override string TokenName => NAME;

        public override bool MutuallyExclusive => true;

        public override int[] MutuallyExclusiveFlags => _mutuallyExclusiveFlags;

        public override CorrectVerifyVal ValidValue(object value)
        {
            var val = new CorrectVerifyVal {NameOfExecutedRule = NAME};

            if (value is null)
            {
                UpdateVal(val, null);
            }

            return val;
        }

        private void UpdateVal(CorrectVerifyVal val, object obj)
        {
            val.IsSuccess = false;
            val.VerifiedValue = obj;
            val.ErrorMessage = MergeMessage("The value is must be not null.");
        }

        public override string ToString() => NAME;
    }
}