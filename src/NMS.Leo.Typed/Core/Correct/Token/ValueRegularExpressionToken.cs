using System;
using System.Text.RegularExpressions;
using NMS.Leo.Metadata;

namespace NMS.Leo.Typed.Core.Correct.Token
{
    internal class ValueRegularExpressionToken : ValueToken
    {
        // ReSharper disable once InconsistentNaming
        public const string NAME = "ValueRegularExpressionToken";
        readonly Func<object, Regex> _regexFunc;

        public ValueRegularExpressionToken(LeoMember member, string expression) : base(member)
        {
            _regexFunc = x => CreateRegex(expression);
        }

        public ValueRegularExpressionToken(LeoMember member, Regex regex) : base(member)
        {
            _regexFunc = x => regex;
        }

        public ValueRegularExpressionToken(LeoMember member, string expression, RegexOptions options) : base(member)
        {
            _regexFunc = x => CreateRegex(expression, options);
        }

        public ValueRegularExpressionToken(LeoMember member, Func<object, string> expressionFunc) : base(member)
        {
            _regexFunc = x => CreateRegex(expressionFunc(x));
        }

        public ValueRegularExpressionToken(LeoMember member, Func<object, Regex> regexFunc) : base(member)
        {
            _regexFunc = regexFunc;
        }

        public ValueRegularExpressionToken(LeoMember member, Func<object, string> expressionFunc, RegexOptions options) : base(member)
        {
            _regexFunc = x => CreateRegex(expressionFunc(x), options);
        }

        public override CorrectValueOps Ops => CorrectValueOps.RegularExpression;

        public override string TokenName => NAME;

        public override bool MutuallyExclusive => false;

        public override int[] MutuallyExclusiveFlags => NoMutuallyExclusiveFlags;

        public override CorrectVerifyVal ValidValue(object value)
        {
            var val = new CorrectVerifyVal {NameOfExecutedRule = NAME};

            var regex = _regexFunc(value);

            if (regex == null || value == null || !regex.IsMatch((string) value))
            {
                UpdateVal(val, value, regex?.ToString());
            }

            return val;
        }

        private void UpdateVal(CorrectVerifyVal val, object obj, string expression = null)
        {
            var message = "The regular expression match failed.";
            if (!string.IsNullOrWhiteSpace(expression))
                message += $" The current expression is: {expression}.";

            val.IsSuccess = false;
            val.VerifiedValue = obj;
            val.ErrorMessage = MergeMessage(message);
        }

        private static Regex CreateRegex(string expression, RegexOptions options = RegexOptions.None)
        {
            return new Regex(expression, options, TimeSpan.FromSeconds(2.0));
        }

        public override string ToString() => NAME;
    }
}