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

        string CustomMessage { get; set; }

        bool WithMessageMode { get; set; }

        /// <summary>
        /// If WithMessage is true, this AppendOrOverwrite takes effect. <br />
        /// true - Append <br />
        /// false - Overwrite
        /// </summary>
        bool AppendOrOverwrite { get; set; }
    }

    internal interface IValueToken<in TVal> : IValueToken
    {
        CorrectVerifyVal ValidValue(TVal value);
    }
}