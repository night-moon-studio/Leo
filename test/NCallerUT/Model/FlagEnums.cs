using System;

/*
 * Reference to:
 *     https://github.com/FluentValidation/FluentValidation/blob/master/src/FluentValidation.Tests/EnumValidatorTests.cs
 */

namespace NCallerUT.Model
{
    public class FlagsEnumModel
    {
        public SByteEnum SByteValue { get; set; }
        public ByteEnum ByteValue { get; set; }
        public Int16Enum Int16Value { get; set; }
        public UInt16Enum UInt16Value { get; set; }
        public Int32Enum Int32Value { get; set; }
        public UInt32Enum UInt32Value { get; set; }
        public Int64Enum Int64Value { get; set; }
        public UInt64Enum UInt64Value { get; set; }
        public EnumWithNegatives EnumWithNegativesValue { get; set; }
        public EnumWithOverlappingFlags EnumWithOverlappingFlagsValue { get; set; }

        public void PopulateWithValidValues()
        {
            SByteValue = SByteEnum.B | SByteEnum.C;
            ByteValue = ByteEnum.B | ByteEnum.C;
            Int16Value = Int16Enum.B | Int16Enum.C;
            UInt16Value = UInt16Enum.B | UInt16Enum.C;
            Int32Value = Int32Enum.B | Int32Enum.C;
            UInt32Value = UInt32Enum.B | UInt32Enum.C;
            Int64Value = Int64Enum.B | Int64Enum.C;
            UInt64Value = UInt64Enum.B | UInt64Enum.C;
            EnumWithNegativesValue = EnumWithNegatives.Bar;
            EnumWithOverlappingFlagsValue = EnumWithOverlappingFlags.A;
        }

        public void PopulateWithInvalidPositiveValues()
        {
            SByteValue = (SByteEnum) 123;
            ByteValue = (ByteEnum) 123;
            Int16Value = (Int16Enum) 123;
            UInt16Value = (UInt16Enum) 123;
            Int32Value = (Int32Enum) 123;
            UInt32Value = (UInt32Enum) 123;
            Int64Value = (Int64Enum) 123;
            UInt64Value = (UInt64Enum) 123;
            EnumWithNegativesValue = (EnumWithNegatives) 123;
            EnumWithOverlappingFlagsValue = (EnumWithOverlappingFlags) 123;
        }

        public void PopulateWithInvalidNegativeValues()
        {
            SByteValue = (SByteEnum) (-123);
            Int16Value = (Int16Enum) (-123);
            Int32Value = (Int32Enum) (-123);
            Int64Value = (Int64Enum) (-123);
            EnumWithNegativesValue = (EnumWithNegatives) (-123);
            EnumWithOverlappingFlagsValue = (EnumWithOverlappingFlags) (-123);
        }
    }

    [Flags]
    public enum SByteEnum : sbyte
    {
        A = 0,
        B = 1,
        C = 2
    }

    [Flags]
    public enum ByteEnum : byte
    {
        A = 0,
        B = 1,
        C = 2
    }

    [Flags]
    public enum Int16Enum : short
    {
        A = 0,
        B = 1,
        C = 2
    }

    [Flags]
    public enum UInt16Enum : ushort
    {
        A = 0,
        B = 1,
        C = 2
    }

    [Flags]
    public enum Int32Enum : int
    {
        A = 0,
        B = 1,
        C = 2
    }

    [Flags]
    public enum UInt32Enum : uint
    {
        A = 0,
        B = 1,
        C = 2
    }

    [Flags]
    public enum Int64Enum : long
    {
        A = 0,
        B = 1,
        C = 2
    }

    [Flags]
    public enum UInt64Enum : ulong
    {
        A = 0,
        B = 1,
        C = 2
    }

    [Flags]
    public enum EnumWithNegatives
    {
        All = ~0,
        Bar = 1,
        Foo = 2
    }

    // NB this enum actually confuses the built-in Enum.ToString() functionality - it shows 7 for A|B.
    [Flags]
    public enum EnumWithOverlappingFlags
    {
        A = 3,
        B = 4,
        C = 5
    }
}