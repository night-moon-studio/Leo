namespace NMS.Leo.Typed.Core.Correct.Token
{
    internal interface IValueToken
    {
        CorrectValueOps Ops { get; }

        string TokenName { get; }

        TokenClass TokenClass { get; }

        bool MutuallyExclusive { get; }

        int[] MutuallyExclusiveFlags { get; }

        CorrectVerifyVal ValidValue(object value);
    }
}