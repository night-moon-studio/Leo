using System.Collections.Generic;
using System.Linq;
using NMS.Leo.Typed.Core.Correct.Token;
using NMS.Leo.Typed.Core.Members;

namespace NMS.Leo.Typed.Core.Correct
{
    internal class CorrectValueRule
    {
        public string Name { get; set; }

        public CorrectValueRuleMode Mode { get; set; }

        public List<IValueToken> Tokens { get; set; }

        public void Merge(CorrectValueRule rule)
        {
            if (rule is null)
                return;

            if (Name != rule.Name)
                return;

            if (rule.Mode != CorrectValueRuleMode.Append)
                return;

            foreach (var token in rule.Tokens)
            {
                if (token.MutuallyExclusive)
                    continue;

                if (!TokenMutexCalculator.Available(Tokens, token))
                    continue;

                Tokens.Add(token);
            }
        }

        public IEnumerable<CorrectVerifyVal> ValidValue(MemberHandler handler)
        {
            handler.GetValueObject(Name);

            return Tokens.Select(token => token.ValidValue(handler.GetValueObject(Name)));
        }

        public IEnumerable<CorrectVerifyVal> ValidValue(object value)
        {
            return Tokens.Select(token => token.ValidValue(value));
        }

        public object GetValue(MemberHandler handler)
        {
            return handler.GetValueObject(Name);
        }
    }

    internal class CorrectValueRule<TVal> : CorrectValueRule { }
}