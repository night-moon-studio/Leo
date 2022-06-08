using NMS.Leo.Metadata;

namespace NMS.Leo.Typed.Core.Correct.Token;

internal class ValueGreaterThanOrEqualToken<TVal> : ValueToken<TVal>
{
    // ReSharper disable once InconsistentNaming
    public const string NAME = "GenericValueGreaterThanOrEqualToken";

    private readonly TVal _valueToCompare;
    private readonly Type _typeOfValueToCompare;

    public ValueGreaterThanOrEqualToken(LeoMember member, TVal valueToCompare) : base(member)
    {
        _valueToCompare = valueToCompare;
        _typeOfValueToCompare = typeof(TVal);
    }

    public override CorrectValueOps Ops => CorrectValueOps.GreaterThanOrEqual_T1;

    public override string TokenName => NAME;

    public override bool MutuallyExclusive => false;

    public override int[] MutuallyExclusiveFlags => NoMutuallyExclusiveFlags;

    public override CorrectVerifyVal ValidValue(TVal value)
    {
        var val = new CorrectVerifyVal {NameOfExecutedRule = NAME};

        var success = ValidCore(value, out var message);

        if (!success)
        {
            UpdateVal(val, value, message);
        }

        return val;
    }

    private bool ValidCore(TVal value, out string message)
    {
        message = null;

        if (Member.MemberType.IsValueType && _typeOfValueToCompare.IsValueType)
        {
            if (ValueTypeEqualCalculator.CompareTo(Member.MemberType, value, _typeOfValueToCompare, _valueToCompare) >= 0)
            {
                return true;
            }
        }

        else if (value is IComparable c1 && _valueToCompare is IComparable c2)
        {
            if (c1.CompareTo(c2) >= 0)
            {
                return true;
            }
        }

        message = $"Neither the given value nor the value being compared can be converted to IComparable<{_typeOfValueToCompare.GetDevelopName()}>.";
        return false;
    }

    private void UpdateVal(CorrectVerifyVal val, TVal obj, string message = null)
    {
        val.IsSuccess = false;
        val.VerifiedValue = obj;
        val.ErrorMessage = MergeMessage(message ?? $"The given value must be greater than or equal to {_valueToCompare}.");
    }

    public override string ToString() => NAME;
}