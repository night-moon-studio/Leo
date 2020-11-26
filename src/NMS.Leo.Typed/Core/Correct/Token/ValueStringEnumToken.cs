using System;
using System.Linq;
using NMS.Leo.Metadata;

namespace NMS.Leo.Typed.Core.Correct.Token
{
    internal class ValueStringEnumToken : ValueToken
    {
        // ReSharper disable once InconsistentNaming
        public const string NAME = "ValueStringEnumToken";

        private readonly Type _enumType;
        private readonly bool _caseSensitive;

        public ValueStringEnumToken(LeoMember member, Type enumType, bool caseSensitive) : base(member)
        {
            _enumType = enumType ?? throw new ArgumentNullException(nameof(enumType));
            _caseSensitive = caseSensitive;

            if (!enumType.IsEnum)
            {
                throw new ArgumentOutOfRangeException(nameof(enumType), $"The type '{enumType.Name}' is not an enum and can't be used with IsEnumName.");
            }
        }

        public override CorrectValueOps Ops => CorrectValueOps.StringEnum;

        public override string TokenName => NAME;

        public override bool MutuallyExclusive => false;

        public override int[] MutuallyExclusiveFlags => NoMutuallyExclusiveFlags;

        public override CorrectVerifyVal ValidValue(object value)
        {
            var val = new CorrectVerifyVal {NameOfExecutedRule = NAME};

            if (!ValidCore(value))
            {
                UpdateVal(val, value);
            }

            return val;
        }

        private bool ValidCore(object value)
        {
            if (value is null) return true;

            var stringValue = value.ToString();
            var comparison = _caseSensitive ? StringComparison.Ordinal : StringComparison.OrdinalIgnoreCase;

            return Enum.GetNames(_enumType).Any(x => x.Equals(stringValue, comparison));
        }

        private void UpdateVal(CorrectVerifyVal val, object obj)
        {
            val.IsSuccess = false;
            val.VerifiedValue = obj;
            val.ErrorMessage = "The given value is not a member of the specified enumeration type.";
        }

        public override string ToString() => NAME;
    }
}