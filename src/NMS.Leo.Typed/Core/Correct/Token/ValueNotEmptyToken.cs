using System.Collections;
using NMS.Leo.Metadata;
using NMS.Leo.Typed.Core.Extensions;

namespace NMS.Leo.Typed.Core.Correct.Token;

internal class ValueNotEmptyToken : ValueToken
{
    // ReSharper disable once InconsistentNaming
    public const string NAME = "ValueNotEmptyToken";
    public static int[] _mutuallyExclusiveFlags = {90118};

    public ValueNotEmptyToken(LeoMember member) : base(member) { }

    public override CorrectValueOps Ops => CorrectValueOps.NotEmpty;

    public override string TokenName => NAME;

    public override bool MutuallyExclusive => true;

    public override int[] MutuallyExclusiveFlags => _mutuallyExclusiveFlags;

    public override CorrectVerifyVal ValidValue(object value)
    {
        var val = new CorrectVerifyVal {NameOfExecutedRule = NAME};

        if (ValidCore(value))
            return val;

        UpdateVal(val, value);

        return val;
    }

    private bool ValidCore(object value)
    {
        switch (value)
        {
            case null:
            case string s when string.IsNullOrWhiteSpace(s):
            case ICollection {Count: 0}:
            case Array {Length: 0}:
            case IEnumerable e when !e.Cast<object>().Any():
                return false;
        }

        if (Equals(value, Member.GetDefaultValue(value)))
            return false;

        return true;
    }

    private void UpdateVal(CorrectVerifyVal val, object obj)
    {
        val.IsSuccess = false;
        val.VerifiedValue = obj;
        val.ErrorMessage = MergeMessage("The value must be not empty.");
    }

    public override string ToString() => NAME;
}