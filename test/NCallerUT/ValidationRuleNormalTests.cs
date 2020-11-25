using System;
using System.Collections.Generic;
using NCallerUT.Model;
using NMS.Leo.Typed;
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
    }
}