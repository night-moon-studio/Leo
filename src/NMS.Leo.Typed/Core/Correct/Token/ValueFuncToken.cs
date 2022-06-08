using NMS.Leo.Metadata;
using NMS.Leo.Typed.Validation;

namespace NMS.Leo.Typed.Core.Correct.Token;

internal class ValueFuncToken : ValueToken
{
    // ReSharper disable once InconsistentNaming
    public const string NAME = "Value Func condition rule";
    private readonly Func<object, CustomVerifyResult> _func;

    public ValueFuncToken(LeoMember member, Func<object, CustomVerifyResult> func) : base(member)
    {
        _func = func;
    }

    public override CorrectValueOps Ops => CorrectValueOps.Func;

    public override string TokenName => NAME;

    public override bool MutuallyExclusive => false;

    public override int[] MutuallyExclusiveFlags => NoMutuallyExclusiveFlags;

    public override CorrectVerifyVal ValidValue(object value)
    {
        var val = new CorrectVerifyVal {NameOfExecutedRule = NAME};

        var result = _func.Invoke(value);

        if (result != null && !result.VerifyResult)
        {
            UpdateVal(val, value, result.ErrorMessage);
        }

        return val;
    }

    private void UpdateVal(CorrectVerifyVal val, object obj, string message)
    {
        if (string.IsNullOrWhiteSpace(message))
            message = "The value does not satisfy the given Func condition.";

        val.IsSuccess = false;
        val.VerifiedValue = obj;
        val.ErrorMessage = MergeMessage(message);
    }

    public override string ToString() => NAME;
}