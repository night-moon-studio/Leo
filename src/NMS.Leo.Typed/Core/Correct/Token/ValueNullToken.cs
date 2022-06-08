using NMS.Leo.Metadata;

namespace NMS.Leo.Typed.Core.Correct.Token;

internal class ValueNullToken : ValueToken
{
    // ReSharper disable once InconsistentNaming
    public const string NAME = "NullValueToken";

    public static int[] _mutuallyExclusiveFlags = {90114};

    public ValueNullToken(LeoMember member) : base(member) { }

    public override CorrectValueOps Ops => CorrectValueOps.NotNull;

    public override string TokenName => NAME;

    public override bool MutuallyExclusive => true;

    public override int[] MutuallyExclusiveFlags => _mutuallyExclusiveFlags;

    public override CorrectVerifyVal ValidValue(object value)
    {
        var val = new CorrectVerifyVal {NameOfExecutedRule = NAME};

        if (value != null)
        {
            UpdateVal(val, value);
        }

        return val;
    }

    private void UpdateVal(CorrectVerifyVal val, object obj)
    {
        val.IsSuccess = false;
        val.VerifiedValue = obj;
        val.ErrorMessage = MergeMessage("The value is must be null.");
    }

    public override string ToString() => NAME;
}