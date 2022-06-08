using NMS.Leo.Metadata;
using NMS.Leo.Typed.Core.Extensions;

namespace NMS.Leo.Typed.Core.Correct.Token;

internal class ValueRequiredTypeToken : ValueToken
{
    // ReSharper disable once InconsistentNaming
    public const string NAME = "ValueRequiredTypeToken";
    private readonly Type _type;

    public ValueRequiredTypeToken(LeoMember member, Type type) : base(member)
    {
        _type = type ?? throw new ArgumentNullException(nameof(type));
    }

    public override CorrectValueOps Ops => CorrectValueOps.Type;

    public override string TokenName => NAME;

    public override bool MutuallyExclusive => true;

    public override int[] MutuallyExclusiveFlags => NoMutuallyExclusiveFlags;

    public override CorrectVerifyVal ValidValue(object value)
    {
        var val = new CorrectVerifyVal {NameOfExecutedRule = NAME};
        var flag = false;

        if (Member.MemberType == _type)
        {
            flag = true;
        }

        else if (value != null && value.GetType() == _type)
        {
            flag = true;
        }

        else if (Member.MemberType.IsDeriveClassFrom(_type))
        {
            flag = true;
        }

        if (!flag)
        {
            UpdateVal(val, value);
        }

        return val;
    }

    private void UpdateVal(CorrectVerifyVal val, object obj)
    {
        val.IsSuccess = false;
        val.VerifiedValue = obj;
        val.ErrorMessage = MergeMessage($"The given type is not a derived class of {_type.GetDevelopName()} or its implementation. The current type is {Member.MemberType.GetDevelopName()}.");
    }

    public override string ToString() => NAME;
}