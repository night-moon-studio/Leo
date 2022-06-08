using NMS.Leo.Metadata;

namespace NMS.Leo.Typed.Core.Correct.Token;

internal class ValueRequiredTypeToken<T> : ValueRequiredTypeToken
{
    public ValueRequiredTypeToken(LeoMember member) : base(member, typeof(T)) { }
}