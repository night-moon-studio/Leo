using System.Collections.Generic;
using System.Linq;
using NMS.Leo.Typed.Core.Extensions;

namespace NMS.Leo.Typed.Core.Correct.Token
{
    internal static class TokenMutexCalculator
    {
        public static bool Available(List<IValueToken> tokens, IValueToken token)
        {
            if (!tokens.Any())
                return true;

            if (token.MutuallyExclusive)
                return false;

            return !tokens.Any(x => x.MutuallyExclusive && x.MutuallyExclusiveFlags.ContainsAny(token.MutuallyExclusiveFlags));
        }
    }
}