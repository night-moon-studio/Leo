using NMS.Leo.Metadata;
using NMS.Leo.Typed.Core.Extensions;

namespace NMS.Leo.Typed.Core.Correct.Token;

internal class ValueEnumToken : ValueToken
{
    // ReSharper disable once InconsistentNaming
    public const string NAME = "ValueEnumToken";
    private readonly Type _enumType;

    public ValueEnumToken(LeoMember member, Type enumType) : base(member)
    {
        _enumType = enumType ?? throw new ArgumentNullException(nameof(enumType));
    }

    public override CorrectValueOps Ops => CorrectValueOps.Enum;

    public override string TokenName => NAME;

    public override bool MutuallyExclusive => false;

    public override int[] MutuallyExclusiveFlags => NoMutuallyExclusiveFlags;

    public override CorrectVerifyVal ValidValue(object value)
    {
        var val = new CorrectVerifyVal {NameOfExecutedRule = NAME};

        if (!ValidCore(value, out var message))
            UpdateVal(val, value, message);

        return val;
    }

    private bool ValidCore(object value, out string message)
    {
        message = null;
        if (value is null) return true;

        var enumType = TypeReflections.GetUnderlyingType(_enumType);

        if (!enumType.IsEnum) return false;

        if (enumType.GetCustomAttribute<FlagsAttribute>() != null)
        {
            return IsFlagsEnumDefined(enumType, value, out message);
        }

        return Enum.IsDefined(enumType, value);
    }

    private static bool IsFlagsEnumDefined(Type enumType, object value, out string message)
    {
        message = null;
        var nameOfType = Enum.GetUnderlyingType(enumType).Name;

        switch (nameOfType)
        {
            case "Byte":
                return EvaluateFlagEnumValues(value.As<byte>(), enumType);

            case "Int16":
                return EvaluateFlagEnumValues(value.As<short>(), enumType);

            case "Int32":
                return EvaluateFlagEnumValues(value.As<int>(), enumType);

            case "Int64":
                return EvaluateFlagEnumValues(value.As<long>(), enumType);

            case "SByte":
                return EvaluateFlagEnumValues(Convert.ToInt64(value.As<sbyte>()), enumType);

            case "UInt16":
                return EvaluateFlagEnumValues(value.As<ushort>(), enumType);

            case "UInt32":
                return EvaluateFlagEnumValues(value.As<uint>(), enumType);

            case "UInt64":
                return EvaluateFlagEnumValues((long) value.As<ulong>(), enumType);

            default:
                message = $"Unexpected typeName of '{nameOfType}' during flags enum evaluation.";
                return false;
        }

        bool EvaluateFlagEnumValues(long localValue, Type localEnumType)
        {
            long mask = 0;
            foreach (var enumValue in Enum.GetValues(localEnumType))
            {
                var enumValueAsInt64 = Convert.ToInt64(enumValue);
                if ((enumValueAsInt64 & localValue) == enumValueAsInt64)
                {
                    mask |= enumValueAsInt64;
                    if (mask == localValue)
                        return true;
                }
            }

            return false;
        }
    }

    private void UpdateVal(CorrectVerifyVal val, object obj, string message = null)
    {
        val.IsSuccess = false;
        val.VerifiedValue = obj;
        val.ErrorMessage = MergeMessage(message ?? $"'{Member.MemberName}' has a range of values which does not include '{obj}'.");
    }

    public override string ToString() => NAME;
}