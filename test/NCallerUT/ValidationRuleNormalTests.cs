using System;
using System.Collections.Generic;
using System.Linq;
using NCallerUT.Model;
using NMS.Leo.Typed;
using NMS.Leo.Typed.Validation;
using Xunit;

namespace NCallerUT
{
    [Trait("Validation Rules", "Generic")]
    public class ValidationRuleNormalTests : Prepare
    {
        public ValidationRuleNormalTests()
        {
            Data = new NiceAct2(true);
            Type = typeof(NiceAct2);
        }

        public Type Type { get; set; }
        public NiceAct2 Data { get; set; }

        [Fact(DisplayName = "Required Token")]
        public void RequiredTest()
        {
            var v = LeoVisitorFactory.Create(Type, Data);
            v.ValidationEntry.ForMember("SomeObj", c => c.NotEmpty());

            var r = v.Verify();
            Assert.True(r.IsValid);

            v.ValidationEntry.ForMember("MustByNullObj", c => c.NotEmpty());

            r = v.Verify();
            Assert.False(r.IsValid);
        }

        [Fact(DisplayName = "Required Null Token")]
        public void RequiredNullTest()
        {
            var v = LeoVisitorFactory.Create(Type, Data);
            v.ValidationEntry.ForMember("MustByNullObj", c => c.Empty());

            var r = v.Verify();
            Assert.True(r.IsValid);

            v.ValidationEntry.ForMember("SomeObj", c => c.Empty());

            r = v.Verify();
            Assert.False(r.IsValid);
        }

        [Fact(DisplayName = "Max Length Token with Str")]
        public void StringMaxLengthTest()
        {
            var v = LeoVisitorFactory.Create(Type, Data);
            v.ValidationEntry.ForMember("Str", c => c.MaxLength(6));

            var r = v.Verify();
            Assert.True(r.IsValid);

            v.ValidationEntry.ForMember("Str", c => c.MaxLength(4).OverwriteRule());

            r = v.Verify();
            Assert.False(r.IsValid);
        }

        [Fact(DisplayName = "Min Length Token with Str")]
        public void StringMinLengthTest()
        {
            var v = LeoVisitorFactory.Create(Type, Data);
            v.ValidationEntry.ForMember("Str", c => c.MinLength(6));

            var r = v.Verify();
            Assert.True(r.IsValid);

            v.ValidationEntry.ForMember("Str", c => c.MinLength(8).OverwriteRule());

            r = v.Verify();
            Assert.False(r.IsValid);
        }

        [Fact(DisplayName = "Max and Min Length Token with Str")]
        public void StringMaxMinLengthTest()
        {
            var v = LeoVisitorFactory.Create(Type, Data);
            v.ValidationEntry.ForMember("Str", c => c.Length(6, 12));

            var r = v.Verify();
            Assert.True(r.IsValid);

            v.ValidationEntry.ForMember("Str", c => c.Length(1, 6).OverwriteRule());

            r = v.Verify();
            Assert.True(r.IsValid);

            v.ValidationEntry.ForMember("Str", c => c.Length(8, 12).OverwriteRule());

            r = v.Verify();
            Assert.False(r.IsValid);
        }

        [Fact(DisplayName = "Max Length Token with Bytes")]
        public void BytesMaxLengthTest()
        {
            var v = LeoVisitorFactory.Create(Type, Data);
            v.ValidationEntry.ForMember("Bytes", c => c.MaxLength(12));

            var r = v.Verify();
            Assert.True(r.IsValid);

            v.ValidationEntry.ForMember("Bytes", c => c.MaxLength(8).OverwriteRule());

            r = v.Verify();
            Assert.False(r.IsValid);
        }

        [Fact(DisplayName = "Min Length Token with Bytes")]
        public void BytesMinLengthTest()
        {
            var v = LeoVisitorFactory.Create(Type, Data);
            v.ValidationEntry.ForMember("Bytes", c => c.MinLength(8));

            var r = v.Verify();
            Assert.True(r.IsValid);

            v.ValidationEntry.ForMember("Bytes", c => c.MinLength(12).OverwriteRule());

            r = v.Verify();
            Assert.False(r.IsValid);
        }

        [Fact(DisplayName = "Max and Min Length Token with Bytes")]
        public void BytesMaxMinLengthTest()
        {
            var v = LeoVisitorFactory.Create(Type, Data);
            v.ValidationEntry.ForMember("Bytes", c => c.Length(8, 12));

            var r = v.Verify();
            Assert.True(r.IsValid);

            v.ValidationEntry.ForMember("Bytes", c => c.Length(12, 16).OverwriteRule());

            r = v.Verify();
            Assert.False(r.IsValid);
        }

        [Fact(DisplayName = "Max and Min Length Token with Obj Array")]
        public void ObjArrayMaxMinLengthTest()
        {
            var v = LeoVisitorFactory.Create(Type, Data);
            v.ValidationEntry.ForMember("SomeNiceActArray", c => c.Length(5, 12));

            var r = v.Verify();
            Assert.True(r.IsValid);

            v.ValidationEntry.ForMember("SomeNiceActArray", c => c.Length(12, 16).OverwriteRule());

            r = v.Verify();
            Assert.False(r.IsValid);
        }

        [Fact(DisplayName = "Max and Min Length Token with Obj List")]
        public void ObjListMaxMinLengthTest()
        {
            var v = LeoVisitorFactory.Create(Type, Data);
            v.ValidationEntry.ForMember("SomeNiceActList", c => c.Length(5, 12));

            var r = v.Verify();
            Assert.True(r.IsValid);

            v.ValidationEntry.ForMember("SomeNiceActList", c => c.Length(12, 16).OverwriteRule());

            r = v.Verify();
            Assert.False(r.IsValid);
        }

        [Fact(DisplayName = "Equal Token with Str")]
        public void StringEqualTest()
        {
            var v = LeoVisitorFactory.Create(Type, Data);
            v.ValidationEntry.ForMember("Str", c => c.Equal("StrStr"));

            var r = v.Verify();
            Assert.True(r.IsValid);

            v.ValidationEntry.ForMember("Str", c => c.Equal("Str").OverwriteRule());

            r = v.Verify();
            Assert.False(r.IsValid);
        }

        [Fact(DisplayName = "Equal Token with Int16")]
        public void Int16EqualTest()
        {
            var v = LeoVisitorFactory.Create(Type, Data);
            v.ValidationEntry.ForMember("Int16", c => c.Equal(16));

            var r = v.Verify();
            Assert.True(r.IsValid);

            v.ValidationEntry.ForMember("Int16", c => c.Equal(99).OverwriteRule());

            r = v.Verify();
            Assert.False(r.IsValid);
        }

        [Fact(DisplayName = "Equal Token with Int32")]
        public void Int32EqualTest()
        {
            var v = LeoVisitorFactory.Create(Type, Data);
            v.ValidationEntry.ForMember("Int32", c => c.Equal(32));

            var r = v.Verify();
            Assert.True(r.IsValid);

            v.ValidationEntry.ForMember("Int32", c => c.Equal(99).OverwriteRule());

            r = v.Verify();
            Assert.False(r.IsValid);
        }

        [Fact(DisplayName = "Equal Token with Int64")]
        public void Int64EqualTest()
        {
            var v = LeoVisitorFactory.Create(Type, Data);
            v.ValidationEntry.ForMember("Int64", c => c.Equal(64));

            var r = v.Verify();
            Assert.True(r.IsValid);

            v.ValidationEntry.ForMember("Int64", c => c.Equal(99).OverwriteRule());

            r = v.Verify();
            Assert.False(r.IsValid);
        }

        [Fact(DisplayName = "Equal Token with Char")]
        public void CharEqualTest()
        {
            var v = LeoVisitorFactory.Create(Type, Data);
            v.ValidationEntry.ForMember("Char", c => c.Equal('c'));

            var r = v.Verify();
            Assert.True(r.IsValid);

            v.ValidationEntry.ForMember("Char", c => c.Equal('d').OverwriteRule());

            r = v.Verify();
            Assert.False(r.IsValid);
        }

        [Fact(DisplayName = "Equal Token with Obj")]
        public void ObjEqualTest()
        {
            var v = LeoVisitorFactory.Create(Type, Data);
            v.ValidationEntry.ForMember("SomeObj", c => c.Equal(Data.SomeObj));

            var r = v.Verify();
            Assert.True(r.IsValid);

            v.ValidationEntry.ForMember("SomeObj", c => c.Equal(new NiceAct2()).OverwriteRule());

            r = v.Verify();
            Assert.False(r.IsValid);
        }

        [Fact(DisplayName = "Not Equal Token with Str")]
        public void StringNotEqualTest()
        {
            var v = LeoVisitorFactory.Create(Type, Data);
            v.ValidationEntry.ForMember("Str", c => c.NotEqual("StrStr"));

            var r = v.Verify();
            Assert.False(r.IsValid);

            v.ValidationEntry.ForMember("Str", c => c.NotEqual("Str").OverwriteRule());

            r = v.Verify();
            Assert.True(r.IsValid);
        }

        [Fact(DisplayName = "Not Equal Token with Int16")]
        public void Int16NotEqualTest()
        {
            var v = LeoVisitorFactory.Create(Type, Data);
            v.ValidationEntry.ForMember("Int16", c => c.NotEqual(16));

            var r = v.Verify();
            Assert.False(r.IsValid);

            v.ValidationEntry.ForMember("Int16", c => c.NotEqual(99).OverwriteRule());

            r = v.Verify();
            Assert.True(r.IsValid);
        }

        [Fact(DisplayName = "Not Equal Token with Int32")]
        public void Int32NotEqualTest()
        {
            var v = LeoVisitorFactory.Create(Type, Data);
            v.ValidationEntry.ForMember("Int32", c => c.NotEqual(32));

            var r = v.Verify();
            Assert.False(r.IsValid);

            v.ValidationEntry.ForMember("Int32", c => c.NotEqual(99).OverwriteRule());

            r = v.Verify();
            Assert.True(r.IsValid);
        }

        [Fact(DisplayName = "Not Equal Token with Int64")]
        public void Int64NotEqualTest()
        {
            var v = LeoVisitorFactory.Create(Type, Data);
            v.ValidationEntry.ForMember("Int64", c => c.NotEqual(64));

            var r = v.Verify();
            Assert.False(r.IsValid);

            v.ValidationEntry.ForMember("Int64", c => c.NotEqual(99).OverwriteRule());

            r = v.Verify();
            Assert.True(r.IsValid);
        }

        [Fact(DisplayName = "Not Equal Token with Char")]
        public void CharNotEqualTest()
        {
            var v = LeoVisitorFactory.Create(Type, Data);
            v.ValidationEntry.ForMember("Char", c => c.NotEqual('c'));

            var r = v.Verify();
            Assert.False(r.IsValid);

            v.ValidationEntry.ForMember("Char", c => c.NotEqual('d').OverwriteRule());

            r = v.Verify();
            Assert.True(r.IsValid);
        }

        [Fact(DisplayName = "Not Equal Token with Obj")]
        public void ObjNotEqualTest()
        {
            var v = LeoVisitorFactory.Create(Type, Data);
            v.ValidationEntry.ForMember("SomeObj", c => c.NotEqual(Data.SomeObj));

            var r = v.Verify();
            Assert.False(r.IsValid);

            v.ValidationEntry.ForMember("SomeObj", c => c.NotEqual(new NiceAct2()).OverwriteRule());

            r = v.Verify();
            Assert.True(r.IsValid);
        }

        [Fact(DisplayName = "Range Token with int16")]
        public void Int16RangeTest()
        {
            var v = LeoVisitorFactory.Create(Type, Data);
            v.ValidationEntry.ForMember("Int16", c => c.Range(1, 100));

            var r = v.Verify();
            Assert.True(r.IsValid);

            v.ValidationEntry.ForMember("Int16", c => c.Range(88, 100));

            r = v.Verify();
            Assert.False(r.IsValid);

            v.ValidationEntry.ForMember("Int16", c => c.RangeWithCloseInterval(16, 17).OverwriteRule());

            r = v.Verify();
            Assert.True(r.IsValid);

            v.ValidationEntry.ForMember("Int16", c => c.RangeWithOpenInterval(16, 17).OverwriteRule());

            r = v.Verify();
            Assert.False(r.IsValid);
        }

        [Fact(DisplayName = "Range Token with int32")]
        public void Int32RangeTest()
        {
            var v = LeoVisitorFactory.Create(Type, Data);
            v.ValidationEntry.ForMember("Int32", c => c.Range(1, 100));

            var r = v.Verify();
            Assert.True(r.IsValid);

            v.ValidationEntry.ForMember("Int32", c => c.Range(88, 100));

            r = v.Verify();
            Assert.False(r.IsValid);

            v.ValidationEntry.ForMember("Int32", c => c.RangeWithCloseInterval(32, 33).OverwriteRule());

            r = v.Verify();
            Assert.True(r.IsValid);

            v.ValidationEntry.ForMember("Int32", c => c.RangeWithOpenInterval(32, 33).OverwriteRule());

            r = v.Verify();
            Assert.False(r.IsValid);
        }

        [Fact(DisplayName = "Range Token with int64")]
        public void Int64RangeTest()
        {
            var v = LeoVisitorFactory.Create(Type, Data);
            v.ValidationEntry.ForMember("Int64", c => c.Range(1, 100));

            var r = v.Verify();
            Assert.True(r.IsValid);

            v.ValidationEntry.ForMember("Int64", c => c.Range(88, 100));

            r = v.Verify();
            Assert.False(r.IsValid);

            v.ValidationEntry.ForMember("Int64", c => c.RangeWithCloseInterval(64, 65).OverwriteRule());

            r = v.Verify();
            Assert.True(r.IsValid);

            v.ValidationEntry.ForMember("Int64", c => c.RangeWithOpenInterval(64, 65).OverwriteRule());

            r = v.Verify();
            Assert.False(r.IsValid);
        }

        [Fact(DisplayName = "Range Token with Char")]
        public void CharRangeTest()
        {
            var v = LeoVisitorFactory.Create(Type, Data);
            v.ValidationEntry.ForMember("Char", c => c.Range('a', 'd'));

            var r = v.Verify();
            Assert.True(r.IsValid);

            v.ValidationEntry.ForMember("Char", c => c.Range('x', 'z'));

            r = v.Verify();
            Assert.False(r.IsValid);

            v.ValidationEntry.ForMember("Char", c => c.RangeWithCloseInterval('c', 'd').OverwriteRule());

            r = v.Verify();
            Assert.True(r.IsValid);

            v.ValidationEntry.ForMember("Char", c => c.RangeWithOpenInterval('c', 'd').OverwriteRule());

            r = v.Verify();
            Assert.False(r.IsValid);
        }

        [Fact(DisplayName = "Range Token with DateTime")]
        public void DateTimeRangeTest()
        {
            var v = LeoVisitorFactory.Create(Type, Data);
            v.ValidationEntry.ForMember("DateTime", c => c.Range(DateTime.Today.AddDays(-1), DateTime.Today.AddDays(1)));

            var r = v.Verify();
            Assert.True(r.IsValid);

            v.ValidationEntry.ForMember("DateTime", c => c.Range(DateTime.Today.AddDays(1), DateTime.Today.AddDays(2)));

            r = v.Verify();
            Assert.False(r.IsValid);

            v.ValidationEntry.ForMember("DateTime", c => c.RangeWithCloseInterval(DateTime.Today, DateTime.Today.AddDays(1)).OverwriteRule());

            r = v.Verify();
            Assert.True(r.IsValid);

            v.ValidationEntry.ForMember("DateTime", c => c.RangeWithOpenInterval(DateTime.Today, DateTime.Today.AddDays(1)).OverwriteRule());

            r = v.Verify();
            Assert.False(r.IsValid);
        }

        [Fact(DisplayName = "Range Token with DateTimeOffset")]
        public void DateTimeOffsetRangeTest()
        {
            var v = LeoVisitorFactory.Create(Type, Data);
            v.ValidationEntry.ForMember("DateTimeOffset", c => c.Range(DateTime.Today.AddDays(-1), DateTime.Today.AddDays(1)));

            var r = v.Verify();
            Assert.True(r.IsValid);

            v.ValidationEntry.ForMember("DateTimeOffset", c => c.Range(DateTime.Today.AddDays(1), DateTime.Today.AddDays(2)));

            r = v.Verify();
            Assert.False(r.IsValid);

            v.ValidationEntry.ForMember("DateTimeOffset", c => c.RangeWithCloseInterval(DateTime.Today, DateTime.Today.AddDays(1)).OverwriteRule());

            r = v.Verify();
            Assert.True(r.IsValid);

            v.ValidationEntry.ForMember("DateTimeOffset", c => c.RangeWithOpenInterval(DateTime.Today, DateTime.Today.AddDays(1)).OverwriteRule());

            r = v.Verify();
            Assert.False(r.IsValid);
        }

        [Fact(DisplayName = "Any Token with Bytes")]
        public void BytesAnyTest()
        {
            var v = LeoVisitorFactory.Create(Type, Data);

            v.ValidationEntry.ForMember("Bytes", c => c.Any(b => (byte) b == 0));

            var r = v.Verify();
            Assert.True(r.IsValid);
        }

        [Fact(DisplayName = "Any Token with Array")]
        public void ObjArrayAnyTest()
        {
            var v = LeoVisitorFactory.Create(Type, Data);
            var o = v.GetValue("SomeObj");

            v.ValidationEntry.ForMember("SomeNiceActArray", c => c.Any(s => s == o));

            var r = v.Verify();
            Assert.True(r.IsValid);
        }

        [Fact(DisplayName = "Any Token with List")]
        public void ObjListAnyTest()
        {
            var v = LeoVisitorFactory.Create(Type, Data);
            var o = v.GetValue("SomeObj");

            v.ValidationEntry.ForMember("SomeNiceActList", c => c.Any(s => s == o));

            var r = v.Verify();
            Assert.True(r.IsValid);
        }

        [Fact(DisplayName = "All Token with Bytes")]
        public void BytesAllTest()
        {
            var v = LeoVisitorFactory.Create(Type, Data);

            v.ValidationEntry.ForMember("Bytes", c => c.All(b => (byte) b == 0));

            var r = v.Verify();
            Assert.False(r.IsValid);
        }

        [Fact(DisplayName = "All Token with Array")]
        public void ObjArrayAllTest()
        {
            var n = new NiceAct();
            var d = new NiceAct2(true);
            var o = (NiceAct) d.SomeObj;
            d.SomeNiceActArray = new[] {o, n, o, n, o, n};

            var v = LeoVisitorFactory.Create(Type, d);

            v.ValidationEntry.ForMember("SomeNiceActArray", c => c.All(s => s == o));

            var r = v.Verify();
            Assert.False(r.IsValid);

            var v2 = LeoVisitorFactory.Create(Type, Data);
            var o2 = v2.GetValue("SomeObj");
            v.ValidationEntry.ForMember("SomeNiceActArray", c => c.All(s => s == o2));

            var r2 = v2.Verify();
            Assert.True(r2.IsValid);
        }

        [Fact(DisplayName = "All Token with List")]
        public void ObjListAllTest()
        {
            var n = new NiceAct();
            var d = new NiceAct2(true);
            var o = (NiceAct) d.SomeObj;
            d.SomeNiceActList = new List<NiceAct> {o, n, o, n, o, n};

            var v = LeoVisitorFactory.Create(Type, d);

            v.ValidationEntry.ForMember("SomeNiceActList", c => c.All(s => s == o));

            var r = v.Verify();
            Assert.False(r.IsValid);

            var v2 = LeoVisitorFactory.Create(Type, Data);
            var o2 = v2.GetValue("SomeObj");
            v.ValidationEntry.ForMember("SomeNiceActList", c => c.All(s => s == o2));

            var r2 = v2.Verify();
            Assert.True(r2.IsValid);
        }

        [Fact(DisplayName = "In/NotIn Token with Str")]
        public void StrInTest()
        {
            var v = LeoVisitorFactory.Create(Type, Data);

            v.ValidationEntry.ForMember("Str", c => c.In(new List<object> {"Str", "StrStr"}));

            var r = v.Verify();
            Assert.True(r.IsValid);

            v.ValidationEntry.ForMember("Str", c => c.NotIn(new List<object> {"Str", "StrStr"}).OverwriteRule());

            r = v.Verify();
            Assert.False(r.IsValid);
        }

        [Fact(DisplayName = "In/NotIn Token with Int16")]
        public void Int16InTest()
        {
            var v = LeoVisitorFactory.Create(Type, Data);

            v.ValidationEntry.ForMember("Int16", c => c.In(new List<object> {(short) 16, (short) 17}));

            var r = v.Verify();
            Assert.True(r.IsValid);

            v.ValidationEntry.ForMember("Int16", c => c.NotIn(new List<object> {(short) 16, (short) 17}).OverwriteRule());

            r = v.Verify();
            Assert.False(r.IsValid);
        }

        [Fact(DisplayName = "In/NotIn Token with Int32")]
        public void Int32InTest()
        {
            var v = LeoVisitorFactory.Create(Type, Data);

            v.ValidationEntry.ForMember("Int32", c => c.In(new List<object> {32, 33}));

            var r = v.Verify();
            Assert.True(r.IsValid);

            v.ValidationEntry.ForMember("Int32", c => c.NotIn(new List<object> {32, 33}).OverwriteRule());

            r = v.Verify();
            Assert.False(r.IsValid);
        }

        [Fact(DisplayName = "In/NotIn Token with Int64")]
        public void Int64InTest()
        {
            var v = LeoVisitorFactory.Create(Type, Data);

            v.ValidationEntry.ForMember("Int64", c => c.In(new List<object> {64L, 65L}));

            var r = v.Verify();
            Assert.True(r.IsValid);

            v.ValidationEntry.ForMember("Int64", c => c.NotIn(new List<object> {64L, 65L}).OverwriteRule());

            r = v.Verify();
            Assert.False(r.IsValid);
        }

        [Fact(DisplayName = "In/NotIn Token with Char")]
        public void CharInTest()
        {
            var v = LeoVisitorFactory.Create(Type, Data);

            v.ValidationEntry.ForMember("Char", c => c.In(new List<object> {'c', 'd'}));

            var r = v.Verify();
            Assert.True(r.IsValid);

            v.ValidationEntry.ForMember("Char", c => c.NotIn(new List<object> {'c', 'd'}).OverwriteRule());

            r = v.Verify();
            Assert.False(r.IsValid);
        }

        [Fact(DisplayName = "LessThan Token with Char")]
        public void CharLessThanTest()
        {
            var v = LeoVisitorFactory.Create(Type, Data);

            v.ValidationEntry.ForMember("Char", c => c.LessThan('d'));

            var r = v.Verify();
            Assert.True(r.IsValid);

            v.ValidationEntry.ForMember("Char", c => c.LessThan('c').OverwriteRule());

            r = v.Verify();
            Assert.False(r.IsValid);
        }

        [Fact(DisplayName = "LessThan Token with Int16")]
        public void Int16LessThanTest()
        {
            var v = LeoVisitorFactory.Create(Type, Data);

            v.ValidationEntry.ForMember("Int16", c => c.LessThan(17));

            var r = v.Verify();
            Assert.True(r.IsValid);

            v.ValidationEntry.ForMember("Int16", c => c.LessThan(16).OverwriteRule());

            r = v.Verify();
            Assert.False(r.IsValid);
        }

        [Fact(DisplayName = "LessThan Token with Int32")]
        public void Int32LessThanTest()
        {
            var v = LeoVisitorFactory.Create(Type, Data);

            v.ValidationEntry.ForMember("Int32", c => c.LessThan(33));

            var r = v.Verify();
            Assert.True(r.IsValid);

            v.ValidationEntry.ForMember("Int32", c => c.LessThan(32).OverwriteRule());

            r = v.Verify();
            Assert.False(r.IsValid);
        }

        [Fact(DisplayName = "LessThan Token with Int64")]
        public void Int64LessThanTest()
        {
            var v = LeoVisitorFactory.Create(Type, Data);

            v.ValidationEntry.ForMember("Int64", c => c.LessThan(65));

            var r = v.Verify();
            Assert.True(r.IsValid);

            v.ValidationEntry.ForMember("Int64", c => c.LessThan(64).OverwriteRule());

            r = v.Verify();
            Assert.False(r.IsValid);
        }

        [Fact(DisplayName = "LessThan Token with DateTime")]
        public void DateTimeLessThanTest()
        {
            var v = LeoVisitorFactory.Create(Type, Data);

            v.ValidationEntry.ForMember("DateTime", c => c.LessThan(DateTime.Today.AddDays(1)));

            var r = v.Verify();
            Assert.True(r.IsValid);

            v.ValidationEntry.ForMember("DateTime", c => c.LessThan(DateTime.Today).OverwriteRule());

            r = v.Verify();
            Assert.False(r.IsValid);
        }

        [Fact(DisplayName = "LessThanOrEqual Token with Char")]
        public void CharLessThanOrEqualTest()
        {
            var v = LeoVisitorFactory.Create(Type, Data);

            v.ValidationEntry.ForMember("Char", c => c.LessThanOrEqual('c'));

            var r = v.Verify();
            Assert.True(r.IsValid);

            v.ValidationEntry.ForMember("Char", c => c.LessThanOrEqual('b').OverwriteRule());

            r = v.Verify();
            Assert.False(r.IsValid);
        }

        [Fact(DisplayName = "LessThanOrEqual Token with Int16")]
        public void Int16LessThanOrEqualTest()
        {
            var v = LeoVisitorFactory.Create(Type, Data);

            v.ValidationEntry.ForMember("Int16", c => c.LessThanOrEqual(16));

            var r = v.Verify();
            Assert.True(r.IsValid);

            v.ValidationEntry.ForMember("Int16", c => c.LessThanOrEqual(15).OverwriteRule());

            r = v.Verify();
            Assert.False(r.IsValid);
        }

        [Fact(DisplayName = "LessThanOrEqual Token with Int32")]
        public void Int32LessThanOrEqualTest()
        {
            var v = LeoVisitorFactory.Create(Type, Data);

            v.ValidationEntry.ForMember("Int32", c => c.LessThanOrEqual(32));

            var r = v.Verify();
            Assert.True(r.IsValid);

            v.ValidationEntry.ForMember("Int32", c => c.LessThanOrEqual(31).OverwriteRule());

            r = v.Verify();
            Assert.False(r.IsValid);
        }

        [Fact(DisplayName = "LessThanOrEqual Token with Int64")]
        public void Int64LessThanOrEqualTest()
        {
            var v = LeoVisitorFactory.Create(Type, Data);

            v.ValidationEntry.ForMember("Int64", c => c.LessThanOrEqual(64));

            var r = v.Verify();
            Assert.True(r.IsValid);

            v.ValidationEntry.ForMember("Int64", c => c.LessThanOrEqual(63).OverwriteRule());

            r = v.Verify();
            Assert.False(r.IsValid);
        }

        [Fact(DisplayName = "LessThanOrEqual Token with DateTime")]
        public void DateTimeLessThanOrEqualTest()
        {
            var v = LeoVisitorFactory.Create(Type, Data);

            v.ValidationEntry.ForMember("DateTime", c => c.LessThanOrEqual(DateTime.Today));

            var r = v.Verify();
            Assert.True(r.IsValid);

            v.ValidationEntry.ForMember("DateTime", c => c.LessThanOrEqual(DateTime.Today.AddDays(-1)).OverwriteRule());

            r = v.Verify();
            Assert.False(r.IsValid);
        }

        [Fact(DisplayName = "GreaterThan Token with Char")]
        public void CharGreaterThanTest()
        {
            var v = LeoVisitorFactory.Create(Type, Data);

            v.ValidationEntry.ForMember("Char", c => c.GreaterThan('b'));

            var r = v.Verify();
            Assert.True(r.IsValid);

            v.ValidationEntry.ForMember("Char", c => c.GreaterThan('c').OverwriteRule());

            r = v.Verify();
            Assert.False(r.IsValid);
        }

        [Fact(DisplayName = "GreaterThan Token with Int16")]
        public void Int16GreaterThanTest()
        {
            var v = LeoVisitorFactory.Create(Type, Data);

            v.ValidationEntry.ForMember("Int16", c => c.GreaterThan(15));

            var r = v.Verify();
            Assert.True(r.IsValid);

            v.ValidationEntry.ForMember("Int16", c => c.GreaterThan(16).OverwriteRule());

            r = v.Verify();
            Assert.False(r.IsValid);
        }

        [Fact(DisplayName = "GreaterThan Token with Int32")]
        public void Int32GreaterThanTest()
        {
            var v = LeoVisitorFactory.Create(Type, Data);

            v.ValidationEntry.ForMember("Int32", c => c.GreaterThan(31));

            var r = v.Verify();
            Assert.True(r.IsValid);

            v.ValidationEntry.ForMember("Int32", c => c.GreaterThan(32).OverwriteRule());

            r = v.Verify();
            Assert.False(r.IsValid);
        }

        [Fact(DisplayName = "GreaterThan Token with Int64")]
        public void Int64GreaterThanTest()
        {
            var v = LeoVisitorFactory.Create(Type, Data);

            v.ValidationEntry.ForMember("Int64", c => c.GreaterThan(63));

            var r = v.Verify();
            Assert.True(r.IsValid);

            v.ValidationEntry.ForMember("Int64", c => c.GreaterThan(64).OverwriteRule());

            r = v.Verify();
            Assert.False(r.IsValid);
        }

        [Fact(DisplayName = "GreaterThan Token with DateTime")]
        public void DateTimeGreaterThanTest()
        {
            var v = LeoVisitorFactory.Create(Type, Data);

            v.ValidationEntry.ForMember("DateTime", c => c.GreaterThan(DateTime.Today.AddDays(-1)));

            var r = v.Verify();
            Assert.True(r.IsValid);

            v.ValidationEntry.ForMember("DateTime", c => c.GreaterThan(DateTime.Today).OverwriteRule());

            r = v.Verify();
            Assert.False(r.IsValid);
        }

        [Fact(DisplayName = "GreaterThanOrEqual Token with Char")]
        public void CharGreaterThanOrEqualTest()
        {
            var v = LeoVisitorFactory.Create(Type, Data);

            v.ValidationEntry.ForMember("Char", c => c.GreaterThanOrEqual('c'));

            var r = v.Verify();
            Assert.True(r.IsValid);

            v.ValidationEntry.ForMember("Char", c => c.GreaterThanOrEqual('d').OverwriteRule());

            r = v.Verify();
            Assert.False(r.IsValid);
        }

        [Fact(DisplayName = "GreaterThanOrEqual Token with Int16")]
        public void Int16GreaterThanOrEqualTest()
        {
            var v = LeoVisitorFactory.Create(Type, Data);

            v.ValidationEntry.ForMember("Int16", c => c.GreaterThanOrEqual(16));

            var r = v.Verify();
            Assert.True(r.IsValid);

            v.ValidationEntry.ForMember("Int16", c => c.GreaterThanOrEqual(17).OverwriteRule());

            r = v.Verify();
            Assert.False(r.IsValid);
        }

        [Fact(DisplayName = "GreaterThanOrEqual Token with Int32")]
        public void Int32GreaterThanOrEqualTest()
        {
            var v = LeoVisitorFactory.Create(Type, Data);

            v.ValidationEntry.ForMember("Int32", c => c.GreaterThanOrEqual(32));

            var r = v.Verify();
            Assert.True(r.IsValid);

            v.ValidationEntry.ForMember("Int32", c => c.GreaterThanOrEqual(33).OverwriteRule());

            r = v.Verify();
            Assert.False(r.IsValid);
        }

        [Fact(DisplayName = "GreaterThanOrEqual Token with Int64")]
        public void Int64GreaterThanOrEqualTest()
        {
            var v = LeoVisitorFactory.Create(Type, Data);

            v.ValidationEntry.ForMember("Int64", c => c.GreaterThanOrEqual(64));

            var r = v.Verify();
            Assert.True(r.IsValid);

            v.ValidationEntry.ForMember("Int64", c => c.GreaterThanOrEqual(65).OverwriteRule());

            r = v.Verify();
            Assert.False(r.IsValid);
        }

        [Fact(DisplayName = "GreaterThanOrEqual Token with DateTime")]
        public void DateTimeGreaterThanOrEqualTest()
        {
            var v = LeoVisitorFactory.Create(Type, Data);

            v.ValidationEntry.ForMember("DateTime", c => c.GreaterThanOrEqual(DateTime.Today));

            var r = v.Verify();
            Assert.True(r.IsValid);

            v.ValidationEntry.ForMember("DateTime", c => c.GreaterThanOrEqual(DateTime.Today.AddDays(1)).OverwriteRule());

            r = v.Verify();
            Assert.False(r.IsValid);
        }

        [Fact(DisplayName = "RequireType(s) Token with String")]
        public void StringTypeRequireTest()
        {
            var v = LeoVisitorFactory.Create(Type, Data);

            v.ValidationEntry.ForMember("Str", c => c.RequiredType(typeof(string)));

            var r = v.Verify();
            Assert.True(r.IsValid);

            v.ValidationEntry.ForMember("Str", c => c.RequiredTypes<string>().OverwriteRule());

            r = v.Verify();
            Assert.True(r.IsValid);

            v.ValidationEntry.ForMember("Str", c => c.RequiredTypes<char, string>().OverwriteRule());

            r = v.Verify();
            Assert.True(r.IsValid);

            v.ValidationEntry.ForMember("Str", c => c.RequiredType(typeof(char)).OverwriteRule());

            r = v.Verify();
            Assert.False(r.IsValid);

            v.ValidationEntry.ForMember("Str", c => c.RequiredTypes(typeof(string), typeof(char)).OverwriteRule());

            r = v.Verify();
            Assert.True(r.IsValid);

            v.ValidationEntry.ForMember("Str", c => c.RequiredTypes<DateTime, int>().OverwriteRule());

            r = v.Verify();
            Assert.False(r.IsValid);
        }

        [Fact(DisplayName = "RequireType(s) Token with Char")]
        public void CharTypeRequireTest()
        {
            var v = LeoVisitorFactory.Create(Type, Data);

            v.ValidationEntry.ForMember("Char", c => c.RequiredType(typeof(char)));

            var r = v.Verify();
            Assert.True(r.IsValid);

            v.ValidationEntry.ForMember("Char", c => c.RequiredTypes<char>().OverwriteRule());

            r = v.Verify();
            Assert.True(r.IsValid);

            v.ValidationEntry.ForMember("Char", c => c.RequiredTypes<char, string>().OverwriteRule());

            r = v.Verify();
            Assert.True(r.IsValid);

            v.ValidationEntry.ForMember("Char", c => c.RequiredType(typeof(string)).OverwriteRule());

            r = v.Verify();
            Assert.False(r.IsValid);

            v.ValidationEntry.ForMember("Char", c => c.RequiredTypes(typeof(string), typeof(char)).OverwriteRule());

            r = v.Verify();
            Assert.True(r.IsValid);

            v.ValidationEntry.ForMember("Char", c => c.RequiredTypes<DateTime, string>().OverwriteRule());

            r = v.Verify();
            Assert.False(r.IsValid);
        }

        [Fact(DisplayName = "RequireType(s) Token with Int32")]
        public void Int32TypeRequireTest()
        {
            var v = LeoVisitorFactory.Create(Type, Data);

            v.ValidationEntry.ForMember("Int32", c => c.RequiredType(typeof(Int32)));

            var r = v.Verify();
            Assert.True(r.IsValid);

            v.ValidationEntry.ForMember("Int32", c => c.RequiredTypes<Int32>().OverwriteRule());

            r = v.Verify();
            Assert.True(r.IsValid);

            v.ValidationEntry.ForMember("Int32", c => c.RequiredTypes<Int32, string>().OverwriteRule());

            r = v.Verify();
            Assert.True(r.IsValid);

            v.ValidationEntry.ForMember("Int32", c => c.RequiredType(typeof(string)).OverwriteRule());

            r = v.Verify();
            Assert.False(r.IsValid);

            v.ValidationEntry.ForMember("Int32", c => c.RequiredTypes(typeof(string), typeof(Int32)).OverwriteRule());

            r = v.Verify();
            Assert.True(r.IsValid);

            v.ValidationEntry.ForMember("Int32", c => c.RequiredTypes<DateTime, string>().OverwriteRule());

            r = v.Verify();
            Assert.False(r.IsValid);
        }

        [Fact(DisplayName = "RequireType(s) Token with DateTime")]
        public void DateTimeTypeRequireTest()
        {
            var v = LeoVisitorFactory.Create(Type, Data);

            v.ValidationEntry.ForMember("DateTime", c => c.RequiredType(typeof(DateTime)));

            var r = v.Verify();
            Assert.True(r.IsValid);

            v.ValidationEntry.ForMember("DateTime", c => c.RequiredTypes<DateTime>().OverwriteRule());

            r = v.Verify();
            Assert.True(r.IsValid);

            v.ValidationEntry.ForMember("DateTime", c => c.RequiredTypes<DateTime, string>().OverwriteRule());

            r = v.Verify();
            Assert.True(r.IsValid);

            v.ValidationEntry.ForMember("DateTime", c => c.RequiredType(typeof(string)).OverwriteRule());

            r = v.Verify();
            Assert.False(r.IsValid);

            v.ValidationEntry.ForMember("DateTime", c => c.RequiredTypes(typeof(string), typeof(DateTime)).OverwriteRule());

            r = v.Verify();
            Assert.True(r.IsValid);

            v.ValidationEntry.ForMember("DateTime", c => c.RequiredTypes<DateTimeOffset, string>().OverwriteRule());

            r = v.Verify();
            Assert.False(r.IsValid);
        }

        [Fact(DisplayName = "RequireType(s) Token with Obj")]
        public void ObjTypeRequireTest()
        {
            var v = LeoVisitorFactory.Create(Type, Data);

            v.ValidationEntry.ForMember("SomeObj", c => c.RequiredType(typeof(NiceAct)));

            var r = v.Verify();
            Assert.True(r.IsValid);

            v.ValidationEntry.ForMember("SomeObj", c => c.RequiredTypes<NiceAct>().OverwriteRule());

            r = v.Verify();
            Assert.True(r.IsValid);

            v.ValidationEntry.ForMember("SomeObj", c => c.RequiredTypes<NiceAct, string>().OverwriteRule());

            r = v.Verify();
            Assert.True(r.IsValid);

            v.ValidationEntry.ForMember("SomeObj", c => c.RequiredType(typeof(string)).OverwriteRule());

            r = v.Verify();
            Assert.False(r.IsValid);

            v.ValidationEntry.ForMember("SomeObj", c => c.RequiredTypes(typeof(string), typeof(NiceAct)).OverwriteRule());

            r = v.Verify();
            Assert.True(r.IsValid);

            v.ValidationEntry.ForMember("SomeObj", c => c.RequiredTypes<long, int, string>().OverwriteRule());

            r = v.Verify();
            Assert.False(r.IsValid);
        }

        [Fact(DisplayName = "RequireType(s) Token with NullObj")]
        public void NullObjTypeRequireTest()
        {
            var v = LeoVisitorFactory.Create(Type, Data);

            v.ValidationEntry.ForMember("MustByNullObj", c => c.RequiredType(typeof(object)));

            var r = v.Verify();
            Assert.True(r.IsValid);

            v.ValidationEntry.ForMember("MustByNullObj", c => c.RequiredTypes<object>().OverwriteRule());

            r = v.Verify();
            Assert.True(r.IsValid);

            v.ValidationEntry.ForMember("MustByNullObj", c => c.RequiredTypes<object, string>().OverwriteRule());

            r = v.Verify();
            Assert.True(r.IsValid);

            v.ValidationEntry.ForMember("MustByNullObj", c => c.RequiredType(typeof(string)).OverwriteRule());

            r = v.Verify();
            Assert.False(r.IsValid);

            v.ValidationEntry.ForMember("MustByNullObj", c => c.RequiredTypes(typeof(object), typeof(NiceAct)).OverwriteRule());

            r = v.Verify();
            Assert.True(r.IsValid);

            v.ValidationEntry.ForMember("MustByNullObj", c => c.RequiredTypes<long, int, string>().OverwriteRule());

            r = v.Verify();
            Assert.False(r.IsValid);
        }

        [Fact(DisplayName = "RequireType(s) Token with ObjArray")]
        public void ObjArrayTypeRequireTest()
        {
            var v = LeoVisitorFactory.Create(Type, Data);

            v.ValidationEntry.ForMember("SomeNiceActArray", c => c.RequiredType(typeof(NiceAct[])));

            var r = v.Verify();
            Assert.True(r.IsValid);

            v.ValidationEntry.ForMember("SomeNiceActArray", c => c.RequiredTypes<NiceAct[]>().OverwriteRule());

            r = v.Verify();
            Assert.True(r.IsValid);

            v.ValidationEntry.ForMember("SomeNiceActArray", c => c.RequiredTypes<NiceAct[], string>().OverwriteRule());

            r = v.Verify();
            Assert.True(r.IsValid);

            v.ValidationEntry.ForMember("SomeNiceActArray", c => c.RequiredType(typeof(string)).OverwriteRule());

            r = v.Verify();
            Assert.False(r.IsValid);

            v.ValidationEntry.ForMember("SomeNiceActArray", c => c.RequiredTypes(typeof(string), typeof(NiceAct[])).OverwriteRule());

            r = v.Verify();
            Assert.True(r.IsValid);

            v.ValidationEntry.ForMember("SomeNiceActArray", c => c.RequiredTypes<long, NiceAct, string>().OverwriteRule());

            r = v.Verify();
            Assert.False(r.IsValid);
        }

        [Fact(DisplayName = "RequireType(s) Token with ObjList")]
        public void ObjListTypeRequireTest()
        {
            var v = LeoVisitorFactory.Create(Type, Data);

            v.ValidationEntry.ForMember("SomeNiceActList", c => c.RequiredType(typeof(List<NiceAct>)));

            var r = v.Verify();
            Assert.True(r.IsValid);

            v.ValidationEntry.ForMember("SomeNiceActList", c => c.RequiredTypes<List<NiceAct>>().OverwriteRule());

            r = v.Verify();
            Assert.True(r.IsValid);

            v.ValidationEntry.ForMember("SomeNiceActList", c => c.RequiredTypes<List<NiceAct>, string>().OverwriteRule());

            r = v.Verify();
            Assert.True(r.IsValid);

            v.ValidationEntry.ForMember("SomeNiceActList", c => c.RequiredType(typeof(string)).OverwriteRule());

            r = v.Verify();
            Assert.False(r.IsValid);

            v.ValidationEntry.ForMember("SomeNiceActList", c => c.RequiredTypes(typeof(string), typeof(List<NiceAct>)).OverwriteRule());

            r = v.Verify();
            Assert.True(r.IsValid);

            v.ValidationEntry.ForMember("SomeNiceActList", c => c.RequiredTypes<long, NiceAct[], string>().OverwriteRule());

            r = v.Verify();
            Assert.False(r.IsValid);
        }

        [Fact(DisplayName = "ScalePrecision Token with Decimal and should be valid")]
        public void DecimalScalePrecisionShouldBeValidTest()
        {
            var v = LeoVisitorFactory.Create(typeof(NiceAct2));
            v.ValidationEntry.ForMember("Discount", c => c.ScalePrecision(2, 4));

            v.SetValue("Discount", 12.34M);
            var r = v.Verify();
            Assert.True(r.IsValid);

            v.SetValue("Discount", 2.34M);
            r = v.Verify();
            Assert.True(r.IsValid);

            v.SetValue("Discount", -2.34M);
            r = v.Verify();
            Assert.True(r.IsValid);

            v.SetValue("Discount", 0.34M);
            r = v.Verify();
            Assert.True(r.IsValid);
        }

        [Fact(DisplayName = "ScalePrecision Token with Decimal and should not be valid")]
        public void DecimalScalePrecisionShouldNotBeValidTest()
        {
            var v = LeoVisitorFactory.Create(typeof(NiceAct2));
            v.ValidationEntry.ForMember("Discount", c => c.ScalePrecision(2, 4));

            v.SetValue("Discount", 123.456778M);
            var r = v.Verify();
            Assert.False(r.IsValid);

            v.SetValue("Discount", 12.341M);
            r = v.Verify();
            Assert.False(r.IsValid);

            v.SetValue("Discount", 1.341M);
            r = v.Verify();
            Assert.False(r.IsValid);

            v.SetValue("Discount", 134.1M);
            r = v.Verify();
            Assert.False(r.IsValid);

            v.SetValue("Discount", 13.401M);
            r = v.Verify();
            Assert.False(r.IsValid);
        }

        [Fact(DisplayName = "ScalePrecision Token with Decimal and should be valid when equal")]
        public void DecimalScalePrecisionShouldBeValidWhenEqualTest()
        {
            var v = LeoVisitorFactory.Create(typeof(NiceAct2));
            v.ValidationEntry.ForMember("Discount", c => c.ScalePrecision(2, 2));

            v.SetValue("Discount", 0.34M);
            var r = v.Verify();
            Assert.True(r.IsValid);

            v.SetValue("Discount", 0.3M);
            r = v.Verify();
            Assert.True(r.IsValid);

            v.SetValue("Discount", 0M);
            r = v.Verify();
            Assert.True(r.IsValid);

            v.SetValue("Discount", -0.34M);
            r = v.Verify();
            Assert.True(r.IsValid);
        }

        [Fact(DisplayName = "ScalePrecision Token with Decimal and should not be valid when equal")]
        public void DecimalScalePrecisionShouldNotBeValidWhenEqualTest()
        {
            var v = LeoVisitorFactory.Create(typeof(NiceAct2));
            v.ValidationEntry.ForMember("Discount", c => c.ScalePrecision(2, 2));

            v.SetValue("Discount", 123.456778M);
            var r = v.Verify();
            Assert.False(r.IsValid);

            v.SetValue("Discount", 0.331M);
            r = v.Verify();
            Assert.False(r.IsValid);

            v.SetValue("Discount", 1.34M);
            r = v.Verify();
            Assert.False(r.IsValid);

            v.SetValue("Discount", 1M);
            r = v.Verify();
            Assert.False(r.IsValid);
        }

        [Fact(DisplayName = "ScalePrecision Token with Decimal and should be valid when ignore trailing zeroes")]
        public void DecimalScalePrecisionShouldBeValidWhenIgnoreTrailingZeroesTest()
        {
            var v = LeoVisitorFactory.Create(typeof(NiceAct2));
            v.ValidationEntry.ForMember("Discount", c => c.ScalePrecision(2, 4, true));

            v.SetValue("Discount", 15.0000000000000000000000000M);
            var r = v.Verify();
            Assert.True(r.IsValid);

            v.SetValue("Discount", 0000000000000000000015.0000000000000000000000000M);
            r = v.Verify();
            Assert.True(r.IsValid);

            v.SetValue("Discount", 65.430M);
            r = v.Verify();
            Assert.True(r.IsValid);
        }

        [Fact(DisplayName = "ScalePrecision Token with Decimal and should not be valid when ignore trailing zeroes")]
        public void DecimalScalePrecisionShouldNotBeValidWhenIgnoreTrailingZeroesTest()
        {
            var v = LeoVisitorFactory.Create(typeof(NiceAct2));
            v.ValidationEntry.ForMember("Discount", c => c.ScalePrecision(2, 4, true));

            v.SetValue("Discount", 1565.0M);
            var r = v.Verify();
            Assert.False(r.IsValid);

            v.SetValue("Discount", 15.0000000000000000000000001M);
            r = v.Verify();
            Assert.False(r.IsValid);
        }

        [Fact(DisplayName = "Enum Token basic test")]
        public void EnumValidTest()
        {
            var n1 = new NiceAct2() {Country = Country.China};
            var n2 = new NiceAct2() {Country = (Country) 1};

            var v1 = LeoVisitorFactory.Create(Type, n1);
            var v2 = LeoVisitorFactory.Create(Type, n2);

            v1.ValidationEntry.ForMember("Country", x => x.InEnum(typeof(Country)));
            v2.ValidationEntry.ForMember("Country", x => x.InEnum(typeof(Country)));

            var r1 = v1.Verify();
            var r2 = v2.Verify();
            Assert.True(r1.IsValid);
            Assert.True(r2.IsValid);

            v1.ValidationEntry.ForMember("Country", c => c.InEnum<Country>().OverwriteRule());
            v2.ValidationEntry.ForMember("Country", c => c.InEnum<Country>().OverwriteRule());

            r1 = v1.Verify();
            r2 = v2.Verify();
            Assert.True(r1.IsValid);
            Assert.True(r2.IsValid);
        }

        [Fact(DisplayName = "Enum Token with non-init enum value should not be valid")]
        public void EnumWithValidValueAndWithoutInitThenShouldBeFailTest()
        {
            var v = LeoVisitorFactory.Create(typeof(NiceAct2));

            v.ValidationEntry.ForMember("Country", x => x.InEnum(typeof(Country)));

            var r = v.Verify();
            Assert.False(r.IsValid);

            v.ValidationEntry.ForMember("Country", c => c.InEnum<Country>().OverwriteRule());

            r = v.Verify();
            Assert.False(r.IsValid);
        }

        [Fact(DisplayName = "Enum Token with invalid value should not be valid")]
        public void EnumWithInvalidValueThenShouldBeFailTest()
        {
            var v = LeoVisitorFactory.Create(typeof(NiceAct2));
            v["Country"] = (Country) 100;

            v.ValidationEntry.ForMember("Country", x => x.InEnum(typeof(Country)));

            var r = v.Verify();
            Assert.False(r.IsValid);

            v.ValidationEntry.ForMember("Country", c => c.InEnum<Country>().OverwriteRule());

            r = v.Verify();
            Assert.False(r.IsValid);
        }

        [Fact(DisplayName = "Enum Token with nullable value without init should not valid")]
        public void EnumWithNullableTypeAndWithoutInitTest()
        {
            var v = LeoVisitorFactory.Create(typeof(NiceAct3), new NiceAct3());
            v.ValidationEntry.ForMember("Country", c => c.InEnum(typeof(Country)));

            var r = v.Verify();
            Assert.True(r.IsValid);

            v.ValidationEntry.ForMember("Country", c => c.InEnum<Country>().OverwriteRule());

            r = v.Verify();
            Assert.True(r.IsValid);
        }

        [Fact(DisplayName = "Enum Token with nullable value with init should not valid")]
        public void EnumWithNullableTypeAndWithInitTest()
        {
            var v1 = LeoVisitorFactory.Create(typeof(NiceAct3), new NiceAct3() {Country = Country.China});
            var v2 = LeoVisitorFactory.Create(typeof(NiceAct3), new NiceAct3() {Country = (Country) 1});

            v1.ValidationEntry.ForMember("Country", c => c.InEnum(typeof(Country)));
            v2.ValidationEntry.ForMember("Country", c => c.InEnum(typeof(Country)));

            var r1 = v1.Verify();
            var r2 = v2.Verify();
            Assert.True(r1.IsValid);
            Assert.True(r2.IsValid);

            v1.ValidationEntry.ForMember("Country", c => c.InEnum<Country>().OverwriteRule());
            v2.ValidationEntry.ForMember("Country", c => c.InEnum<Country>().OverwriteRule());

            r1 = v1.Verify();
            r2 = v2.Verify();
            Assert.True(r1.IsValid);
            Assert.True(r2.IsValid);
        }

        [Fact(DisplayName = "Enum Token with flag value when using bitwise value should valid")]
        public void FlagEnumWhenUsingBitwiseValueTest()
        {
            var m = new FlagsEnumModel();
            m.PopulateWithValidValues();

            var v = LeoVisitorFactory.Create(typeof(FlagsEnumModel), m);

            v.ValidationEntry
             .ForMember("SByteValue", c => c.InEnum(typeof(SByteEnum)))
             .ForMember("ByteValue", c => c.InEnum(typeof(ByteEnum)))
             .ForMember("Int16Value", c => c.InEnum(typeof(Int16Enum)))
             .ForMember("Int32Value", c => c.InEnum(typeof(Int32Enum)))
             .ForMember("Int64Value", c => c.InEnum(typeof(Int64Enum)))
             .ForMember("UInt16Value", c => c.InEnum(typeof(UInt16Enum)))
             .ForMember("UInt32Value", c => c.InEnum(typeof(UInt32Enum)))
             .ForMember("UInt64Value", c => c.InEnum(typeof(UInt64Enum)))
             .ForMember("EnumWithNegativesValue", c => c.InEnum(typeof(EnumWithNegatives)))
             .ForMember("EnumWithOverlappingFlagsValue", c => c.InEnum(typeof(EnumWithOverlappingFlags)));

            var r = v.Verify();
            Assert.True(r.IsValid);

            v.SetValue("EnumWithNegativesValue", EnumWithNegatives.All);

            r = v.Verify();
            Assert.True(r.IsValid);
        }

        [Fact(DisplayName = "Enum Token with flag value and OverlappingFlags when using bitwise value should valid")]
        public void FlagEnumWhenUsingBitwiseValueWithOverlappingFlagsTest()
        {
            var m = new FlagsEnumModel();
            var v = LeoVisitorFactory.Create(typeof(FlagsEnumModel), m);
            v.ValidationEntry.ForMember("EnumWithOverlappingFlagsValue", c => c.InEnum(typeof(EnumWithOverlappingFlags)));

            LeoVerifyResult r = null;

            v["EnumWithOverlappingFlagsValue"] = EnumWithOverlappingFlags.A | EnumWithOverlappingFlags.B;
            r = v.Verify();
            Assert.True(r.IsValid);

            v["EnumWithOverlappingFlagsValue"] = EnumWithOverlappingFlags.B | EnumWithOverlappingFlags.C;
            r = v.Verify();
            Assert.True(r.IsValid);

            v["EnumWithOverlappingFlagsValue"] = EnumWithOverlappingFlags.A | EnumWithOverlappingFlags.C;
            r = v.Verify();
            Assert.True(r.IsValid);

            v["EnumWithOverlappingFlagsValue"] = EnumWithOverlappingFlags.A | EnumWithOverlappingFlags.B | EnumWithOverlappingFlags.C;
            r = v.Verify();
            Assert.True(r.IsValid);
        }

        [Fact(DisplayName = "Enum Token with flag value when using zero value")]
        public void FlagEnumWhenUsingZeroValueTest()
        {
            var m = new FlagsEnumModel();
            var v = LeoVisitorFactory.Create(typeof(FlagsEnumModel), m);

            v.ValidationEntry
             .ForMember("SByteValue", c => c.InEnum(typeof(SByteEnum)))
             .ForMember("ByteValue", c => c.InEnum(typeof(ByteEnum)))
             .ForMember("Int16Value", c => c.InEnum(typeof(Int16Enum)))
             .ForMember("Int32Value", c => c.InEnum(typeof(Int32Enum)))
             .ForMember("Int64Value", c => c.InEnum(typeof(Int64Enum)))
             .ForMember("UInt16Value", c => c.InEnum(typeof(UInt16Enum)))
             .ForMember("UInt32Value", c => c.InEnum(typeof(UInt32Enum)))
             .ForMember("UInt64Value", c => c.InEnum(typeof(UInt64Enum)))
             .ForMember("EnumWithNegativesValue", c => c.InEnum(typeof(EnumWithNegatives)))
             .ForMember("EnumWithOverlappingFlagsValue", c => c.InEnum(typeof(EnumWithOverlappingFlags)));

            var r = v.Verify();
            Assert.False(r.IsValid);
            Assert.NotNull(r.Errors.SingleOrDefault(x => x.PropertyName == "EnumWithNegativesValue"));
            Assert.NotNull(r.Errors.SingleOrDefault(x => x.PropertyName == "EnumWithOverlappingFlagsValue"));
            Assert.Equal(2, r.Errors.Count);
        }

        [Fact(DisplayName = "Enum Token with flag value when using positive value")]
        public void FlagEnumWhenUsingOutOfRangePositiveValueTest()
        {
            var m = new FlagsEnumModel();
            m.PopulateWithInvalidPositiveValues();
            var v = LeoVisitorFactory.Create(typeof(FlagsEnumModel), m);

            v.ValidationEntry
             .ForMember("SByteValue", c => c.InEnum(typeof(SByteEnum)))
             .ForMember("ByteValue", c => c.InEnum(typeof(ByteEnum)))
             .ForMember("Int16Value", c => c.InEnum(typeof(Int16Enum)))
             .ForMember("Int32Value", c => c.InEnum(typeof(Int32Enum)))
             .ForMember("Int64Value", c => c.InEnum(typeof(Int64Enum)))
             .ForMember("UInt16Value", c => c.InEnum(typeof(UInt16Enum)))
             .ForMember("UInt32Value", c => c.InEnum(typeof(UInt32Enum)))
             .ForMember("UInt64Value", c => c.InEnum(typeof(UInt64Enum)))
             .ForMember("EnumWithNegativesValue", c => c.InEnum(typeof(EnumWithNegatives)))
             .ForMember("EnumWithOverlappingFlagsValue", c => c.InEnum(typeof(EnumWithOverlappingFlags)));

            var r = v.Verify();
            Assert.False(r.IsValid);
            Assert.NotNull(r.Errors.SingleOrDefault(x => x.PropertyName == "SByteValue"));
            Assert.NotNull(r.Errors.SingleOrDefault(x => x.PropertyName == "ByteValue"));
            Assert.NotNull(r.Errors.SingleOrDefault(x => x.PropertyName == "Int16Value"));
            Assert.NotNull(r.Errors.SingleOrDefault(x => x.PropertyName == "Int32Value"));
            Assert.NotNull(r.Errors.SingleOrDefault(x => x.PropertyName == "Int64Value"));
            Assert.NotNull(r.Errors.SingleOrDefault(x => x.PropertyName == "UInt16Value"));
            Assert.NotNull(r.Errors.SingleOrDefault(x => x.PropertyName == "UInt32Value"));
            Assert.NotNull(r.Errors.SingleOrDefault(x => x.PropertyName == "UInt64Value"));
            Assert.NotNull(r.Errors.SingleOrDefault(x => x.PropertyName == "EnumWithNegativesValue"));
        }

        [Fact(DisplayName = "Enum Token with flag value when using negative value")]
        public void FlagEnumWhenUsingOutOfRangeNegativeValueTest()
        {
            var m = new FlagsEnumModel();
            m.PopulateWithInvalidNegativeValues();
            var v = LeoVisitorFactory.Create(typeof(FlagsEnumModel), m);

            v.ValidationEntry
             .ForMember("SByteValue", c => c.InEnum(typeof(SByteEnum)))
             .ForMember("ByteValue", c => c.InEnum(typeof(ByteEnum)))
             .ForMember("Int16Value", c => c.InEnum(typeof(Int16Enum)))
             .ForMember("Int32Value", c => c.InEnum(typeof(Int32Enum)))
             .ForMember("Int64Value", c => c.InEnum(typeof(Int64Enum)))
             .ForMember("UInt16Value", c => c.InEnum(typeof(UInt16Enum)))
             .ForMember("UInt32Value", c => c.InEnum(typeof(UInt32Enum)))
             .ForMember("UInt64Value", c => c.InEnum(typeof(UInt64Enum)))
             .ForMember("EnumWithNegativesValue", c => c.InEnum(typeof(EnumWithNegatives)))
             .ForMember("EnumWithOverlappingFlagsValue", c => c.InEnum(typeof(EnumWithOverlappingFlags)));

            var r = v.Verify();
            Assert.False(r.IsValid);
            Assert.NotNull(r.Errors.SingleOrDefault(x => x.PropertyName == "SByteValue"));
            Assert.NotNull(r.Errors.SingleOrDefault(x => x.PropertyName == "Int16Value"));
            Assert.NotNull(r.Errors.SingleOrDefault(x => x.PropertyName == "Int32Value"));
            Assert.NotNull(r.Errors.SingleOrDefault(x => x.PropertyName == "Int64Value"));
        }

        [Fact(DisplayName = "StringEnum Token with CaseInsensitive and CaseCorrect")]
        public void StringEnumCaseInsensitiveAndCaseCorrectTest()
        {
            var v1 = LeoVisitorFactory.Create(typeof(NiceAct3), new NiceAct3() {CountryString = "China"});
            var v2 = LeoVisitorFactory.Create(typeof(NiceAct3), new NiceAct3() {CountryString = "USA"});

            v1.ValidationEntry.ForMember("CountryString", c => c.IsEnumName(typeof(Country), false));
            v2.ValidationEntry.ForMember("CountryString", c => c.IsEnumName(typeof(Country), false));

            var r1 = v1.Verify();
            var r2 = v2.Verify();

            Assert.True(r1.IsValid);
            Assert.True(r2.IsValid);

            v1.ValidationEntry.ForMember("CountryString", c => c.IsEnumName<Country>(false).OverwriteRule());
            v2.ValidationEntry.ForMember("CountryString", c => c.IsEnumName<Country>(false).OverwriteRule());

            r1 = v1.Verify();
            r2 = v2.Verify();

            Assert.True(r1.IsValid);
            Assert.True(r2.IsValid);
        }

        [Fact(DisplayName = "StringEnum Token with CaseInsensitive and CaseIncorrect")]
        public void StringEnumCaseInsensitiveAndCaseIncorrectTest()
        {
            var v1 = LeoVisitorFactory.Create(typeof(NiceAct3), new NiceAct3() {CountryString = "chinA"});
            var v2 = LeoVisitorFactory.Create(typeof(NiceAct3), new NiceAct3() {CountryString = "usa"});

            v1.ValidationEntry.ForMember("CountryString", c => c.IsEnumName(typeof(Country), false));
            v2.ValidationEntry.ForMember("CountryString", c => c.IsEnumName(typeof(Country), false));

            var r1 = v1.Verify();
            var r2 = v2.Verify();

            Assert.True(r1.IsValid);
            Assert.True(r2.IsValid);

            v1.ValidationEntry.ForMember("CountryString", c => c.IsEnumName<Country>(false).OverwriteRule());
            v2.ValidationEntry.ForMember("CountryString", c => c.IsEnumName<Country>(false).OverwriteRule());

            r1 = v1.Verify();
            r2 = v2.Verify();

            Assert.True(r1.IsValid);
            Assert.True(r2.IsValid);
        }

        [Fact(DisplayName = "StringEnum Token with CaseSensitive and CaseCorrect")]
        public void StringEnumCaseSensitiveAndCaseCorrectTest()
        {
            var v1 = LeoVisitorFactory.Create(typeof(NiceAct3), new NiceAct3() {CountryString = "China"});
            var v2 = LeoVisitorFactory.Create(typeof(NiceAct3), new NiceAct3() {CountryString = "USA"});

            v1.ValidationEntry.ForMember("CountryString", c => c.IsEnumName(typeof(Country), true));
            v2.ValidationEntry.ForMember("CountryString", c => c.IsEnumName(typeof(Country), true));

            var r1 = v1.Verify();
            var r2 = v2.Verify();

            Assert.True(r1.IsValid);
            Assert.True(r2.IsValid);

            v1.ValidationEntry.ForMember("CountryString", c => c.IsEnumName<Country>(true).OverwriteRule());
            v2.ValidationEntry.ForMember("CountryString", c => c.IsEnumName<Country>(true).OverwriteRule());

            r1 = v1.Verify();
            r2 = v2.Verify();

            Assert.True(r1.IsValid);
            Assert.True(r2.IsValid);
        }

        [Fact(DisplayName = "StringEnum Token with CaseSensitive and CaseIncorrect")]
        public void StringEnumCaseSensitiveAndCaseIncorrectTest()
        {
            var v1 = LeoVisitorFactory.Create(typeof(NiceAct3), new NiceAct3() {CountryString = "chinA"});
            var v2 = LeoVisitorFactory.Create(typeof(NiceAct3), new NiceAct3() {CountryString = "uSA"});

            v1.ValidationEntry.ForMember("CountryString", c => c.IsEnumName(typeof(Country), true));
            v2.ValidationEntry.ForMember("CountryString", c => c.IsEnumName(typeof(Country), true));

            var r1 = v1.Verify();
            var r2 = v2.Verify();

            Assert.False(r1.IsValid);
            Assert.False(r2.IsValid);

            v1.ValidationEntry.ForMember("CountryString", c => c.IsEnumName<Country>(true).OverwriteRule());
            v2.ValidationEntry.ForMember("CountryString", c => c.IsEnumName<Country>(true).OverwriteRule());

            r1 = v1.Verify();
            r2 = v2.Verify();

            Assert.False(r1.IsValid);
            Assert.False(r2.IsValid);
        }

        [Fact(DisplayName = "StringEnum Token with wrong value")]
        public void StringEnumWithWrongValueTest()
        {
            var v = LeoVisitorFactory.Create(typeof(NiceAct3), new NiceAct3() {CountryString = "VVVV"});
            v.ValidationEntry.ForMember("CountryString", c => c.IsEnumName(typeof(Country), true));
            var r = v.Verify();
            Assert.False(r.IsValid);
        }

        [Fact(DisplayName = "StringEnum Token with empty value")]
        public void StringEnumWithEmptyValueTest()
        {
            var v = LeoVisitorFactory.Create(typeof(NiceAct3), new NiceAct3() {CountryString = string.Empty});
            v.ValidationEntry.ForMember("CountryString", c => c.IsEnumName(typeof(Country), true));
            var r = v.Verify();
            Assert.False(r.IsValid);
            Assert.Throws<LeoValidationException>(() => r.Raise());
        }

        [Fact(DisplayName = "StringEnum Token with null value")]
        public void StringEnumWithNullValueTest()
        {
            var v = LeoVisitorFactory.Create(typeof(NiceAct3), new NiceAct3() {CountryString = null});
            v.ValidationEntry.ForMember("CountryString", c => c.IsEnumName(typeof(Country), true));
            var r = v.Verify();
            Assert.True(r.IsValid);
        }

        [Fact(DisplayName = "StringEnum Token with null strategy")]
        public void StringEnumWithNullStrategyTest()
        {
            var v = LeoVisitorFactory.Create(typeof(NiceAct3), new NiceAct3() {CountryString = null});
            Assert.Throws<ArgumentNullException>(() =>
            {
                v.ValidationEntry.ForMember("CountryString", c => c.IsEnumName(null, true));
                v.VerifyAndThrow();
            });
        }

        [Fact(DisplayName = "RegexExpression Token Test")]
        public void RegexExpressionTest()
        {
            var m = new NiceAct3() {StringVal = "53"};
            var v = LeoVisitorFactory.Create(typeof(NiceAct3), m);

            v.ValidationEntry.ForMember("StringVal", c => c.Matches(@"^\w\d$"));

            var r = v.Verify();
            Assert.True(r.IsValid);

            v["StringVal"] = " 5";
            r = v.Verify();
            Assert.False(r.IsValid);

            v["StringVal"] = "S33";
            r = v.Verify();
            Assert.False(r.IsValid);

            v["StringVal"] = "";
            r = v.Verify();
            Assert.False(r.IsValid);

            v["StringVal"] = null;
            r = v.Verify();
            Assert.False(r.IsValid);

            v.ValidationEntry.ForMember("StringVal", c => c.Matches(@"^\w\d$").WithMessage("OH", false).OverwriteRule());
            r = v.Verify();
            Assert.False(r.IsValid);
            Assert.NotNull(r.Errors.SingleOrDefault(x => x.PropertyName == "StringVal"));
            Assert.Equal("OH", r.Errors.Single(x => x.PropertyName == "StringVal").Details[0].ErrorMessage);
        }
    }
}