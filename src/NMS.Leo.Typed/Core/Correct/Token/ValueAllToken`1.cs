using System.Collections;
using NMS.Leo.Metadata;

namespace NMS.Leo.Typed.Core.Correct.Token;

internal class ValueAllToken<TVal, TItem> : ValueToken<TVal>
    where TVal : IEnumerable<TItem>
{
    // ReSharper disable once InconsistentNaming
    public const string NAME = "GenericValueAllToken";

    private readonly Func<TItem, bool> _func;

    public ValueAllToken(LeoMember member, Func<TItem, bool> func) : base(member)
    {
        _func = func;
    }

    public override CorrectValueOps Ops => CorrectValueOps.All_T1;

    public override string TokenName => NAME;

    public override bool MutuallyExclusive => false;

    public override int[] MutuallyExclusiveFlags => NoMutuallyExclusiveFlags;

    public override CorrectVerifyVal ValidValue(TVal value)
    {
        var val = new CorrectVerifyVal {NameOfExecutedRule = NAME};
        var flag = true;

        if (value is ICollection collection)
        {
            if (collection.Cast<TItem>().Any(one => !_func.Invoke(one)))
            {
                flag = false;
            }

            if (!flag)
            {
                UpdateVal(val, value);
            }
        }
        else
        {
            UpdateVal(val, value, "The type is not a collection or an array, and an exception occurs when using AllToken.");
        }

        return val;
    }

    private void UpdateVal(CorrectVerifyVal val, TVal obj, string message = null)
    {
        val.IsSuccess = false;
        val.VerifiedValue = obj;
        val.ErrorMessage = MergeMessage(message ?? "There is at least one unsatisfied member in the array or collection.");
    }

    public override string ToString() => NAME;
}