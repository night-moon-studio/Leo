using System;
using System.Collections.Generic;
using NCallerUT.Model;
using NMS.Leo.Typed;
using NMS.Leo.Typed.Validation;
using Xunit;

namespace NCallerUT
{
    [Trait("Validation Rules", "Normal")]
    public class ValidationRuleGenericTests : Prepare
    {
        public ValidationRuleGenericTests()
        {
            Data = new NiceAct2(true);
        }

        public NiceAct2 Data { get; set; }

        [Fact(DisplayName = "Equal `1 Token with Str")]
        public void StringEqualTest()
        {
            var v = LeoVisitorFactory.Create(Data);
            v.ValidationEntry.ForMember(x => x.Str, c => c.Equal("StrStr"));

            var r = v.Verify();
            Assert.True(r.IsValid);

            v.ValidationEntry.ForMember(x => x.Str, c => c.Equal("Str").OverwriteRule());

            r = v.Verify();
            Assert.False(r.IsValid);
        }

        [Fact(DisplayName = "Equal `1 Token with Int16")]
        public void Int16EqualTest()
        {
            var v = LeoVisitorFactory.Create(Data);
            v.ValidationEntry.ForMember(x => x.Int16, c => c.Equal(16));

            var r = v.Verify();
            Assert.True(r.IsValid);

            v.ValidationEntry.ForMember(x => x.Int16, c => c.Equal(99).OverwriteRule());

            r = v.Verify();
            Assert.False(r.IsValid);
        }

        [Fact(DisplayName = "Equal `1 Token with Int32")]
        public void Int32EqualTest()
        {
            var v = LeoVisitorFactory.Create(Data);
            v.ValidationEntry.ForMember(x => x.Int32, c => c.Equal(32));

            var r = v.Verify();
            Assert.True(r.IsValid);

            v.ValidationEntry.ForMember(x => x.Int32, c => c.Equal(99).OverwriteRule());

            r = v.Verify();
            Assert.False(r.IsValid);
        }

        [Fact(DisplayName = "Equal `1 Token with Int64")]
        public void Int64EqualTest()
        {
            var v = LeoVisitorFactory.Create(Data);
            v.ValidationEntry.ForMember(x => x.Int64, c => c.Equal(64));

            var r = v.Verify();
            Assert.True(r.IsValid);

            v.ValidationEntry.ForMember(x => x.Int64, c => c.Equal(99).OverwriteRule());

            r = v.Verify();
            Assert.False(r.IsValid);
        }

        [Fact(DisplayName = "Equal `1 Token with Char")]
        public void CharEqualTest()
        {
            var v = LeoVisitorFactory.Create(Data);
            v.ValidationEntry.ForMember(x => x.Char, c => c.Equal('c'));

            var r = v.Verify();
            Assert.True(r.IsValid);

            v.ValidationEntry.ForMember(x => x.Char, c => c.Equal('d').OverwriteRule());

            r = v.Verify();
            Assert.False(r.IsValid);
        }

        [Fact(DisplayName = "Equal `1 Token with Obj")]
        public void ObjEqualTest()
        {
            var v = LeoVisitorFactory.Create(Data);
            v.ValidationEntry.ForMember(x => x.SomeObj, c => c.Equal(Data.SomeObj));

            var r = v.Verify();
            Assert.True(r.IsValid);

            v.ValidationEntry.ForMember(x => x.SomeObj, c => c.Equal(new NiceAct2()).OverwriteRule());

            r = v.Verify();
            Assert.False(r.IsValid);
        }

        [Fact(DisplayName = "Not Equal `1 Token with Str")]
        public void StringNotEqualTest()
        {
            var v = LeoVisitorFactory.Create(Data);
            v.ValidationEntry.ForMember(x => x.Str, c => c.NotEqual("StrStr"));

            var r = v.Verify();
            Assert.False(r.IsValid);

            v.ValidationEntry.ForMember(x => x.Str, c => c.NotEqual("Str").OverwriteRule());

            r = v.Verify();
            Assert.True(r.IsValid);
        }

        [Fact(DisplayName = "Not Equal `1 Token with Int16")]
        public void Int16NotEqualTest()
        {
            var v = LeoVisitorFactory.Create(Data);
            v.ValidationEntry.ForMember(x => x.Int16, c => c.NotEqual(16));

            var r = v.Verify();
            Assert.False(r.IsValid);

            v.ValidationEntry.ForMember(x => x.Int16, c => c.NotEqual(99).OverwriteRule());

            r = v.Verify();
            Assert.True(r.IsValid);
        }

        [Fact(DisplayName = "Not Equal `1 Token with Int32")]
        public void Int32NotEqualTest()
        {
            var v = LeoVisitorFactory.Create(Data);
            v.ValidationEntry.ForMember(x => x.Int32, c => c.NotEqual(32));

            var r = v.Verify();
            Assert.False(r.IsValid);

            v.ValidationEntry.ForMember(x => x.Int32, c => c.NotEqual(99).OverwriteRule());

            r = v.Verify();
            Assert.True(r.IsValid);
        }

        [Fact(DisplayName = "Not Equal `1 Token with Int64")]
        public void Int64NotEqualTest()
        {
            var v = LeoVisitorFactory.Create(Data);
            v.ValidationEntry.ForMember(x => x.Int64, c => c.NotEqual(64));

            var r = v.Verify();
            Assert.False(r.IsValid);

            v.ValidationEntry.ForMember(x => x.Int64, c => c.NotEqual(99).OverwriteRule());

            r = v.Verify();
            Assert.True(r.IsValid);
        }

        [Fact(DisplayName = "Not Equal `1 Token with Char")]
        public void CharNotEqualTest()
        {
            var v = LeoVisitorFactory.Create(Data);
            v.ValidationEntry.ForMember(x => x.Char, c => c.NotEqual('c'));

            var r = v.Verify();
            Assert.False(r.IsValid);

            v.ValidationEntry.ForMember(x => x.Char, c => c.NotEqual('d').OverwriteRule());

            r = v.Verify();
            Assert.True(r.IsValid);
        }

        [Fact(DisplayName = "Not Equal `1 Token with Obj")]
        public void ObjNotEqualTest()
        {
            var v = LeoVisitorFactory.Create(Data);
            v.ValidationEntry.ForMember(x => x.SomeObj, c => c.NotEqual(Data.SomeObj));

            var r = v.Verify();
            Assert.False(r.IsValid);

            v.ValidationEntry.ForMember(x => x.SomeObj, c => c.NotEqual(new NiceAct2()).OverwriteRule());

            r = v.Verify();
            Assert.True(r.IsValid);
        }

        [Fact(DisplayName = "Range`1 Token with int16")]
        public void Int16RangeTest()
        {
            var v = LeoVisitorFactory.Create(Data);
            v.ValidationEntry.ForMember(x => x.Int16, c => c.Range(1, 100));

            var r = v.Verify();
            Assert.True(r.IsValid);

            v.ValidationEntry.ForMember(x => x.Int16, c => c.Range(88, 100));

            r = v.Verify();
            Assert.False(r.IsValid);

            v.ValidationEntry.ForMember(x => x.Int16, c => c.RangeWithCloseInterval(16, 17).OverwriteRule());

            r = v.Verify();
            Assert.True(r.IsValid);

            v.ValidationEntry.ForMember(x => x.Int16, c => c.RangeWithOpenInterval(16, 17).OverwriteRule());

            r = v.Verify();
            Assert.False(r.IsValid);
        }

        [Fact(DisplayName = "Range`1 Token with int32")]
        public void Int32RangeTest()
        {
            var v = LeoVisitorFactory.Create(Data);
            v.ValidationEntry.ForMember(x => x.Int32, c => c.Range(1, 100));

            var r = v.Verify();
            Assert.True(r.IsValid);

            v.ValidationEntry.ForMember(x => x.Int32, c => c.Range(88, 100));

            r = v.Verify();
            Assert.False(r.IsValid);

            v.ValidationEntry.ForMember(x => x.Int32, c => c.RangeWithCloseInterval(32, 33).OverwriteRule());

            r = v.Verify();
            Assert.True(r.IsValid);

            v.ValidationEntry.ForMember(x => x.Int32, c => c.RangeWithOpenInterval(32, 33).OverwriteRule());

            r = v.Verify();
            Assert.False(r.IsValid);
        }

        [Fact(DisplayName = "Range`1 Token with int64")]
        public void Int64RangeTest()
        {
            var v = LeoVisitorFactory.Create(Data);
            v.ValidationEntry.ForMember(x => x.Int64, c => c.Range(1, 100));

            var r = v.Verify();
            Assert.True(r.IsValid);

            v.ValidationEntry.ForMember(x => x.Int64, c => c.Range(88, 100));

            r = v.Verify();
            Assert.False(r.IsValid);

            v.ValidationEntry.ForMember(x => x.Int64, c => c.RangeWithCloseInterval(64, 65).OverwriteRule());

            r = v.Verify();
            Assert.True(r.IsValid);

            v.ValidationEntry.ForMember(x => x.Int64, c => c.RangeWithOpenInterval(64, 65).OverwriteRule());

            r = v.Verify();
            Assert.False(r.IsValid);
        }

        [Fact(DisplayName = "Range`1 Token with Char")]
        public void CharRangeTest()
        {
            var v = LeoVisitorFactory.Create(Data);
            v.ValidationEntry.ForMember(x => x.Char, c => c.Range('a', 'd'));

            var r = v.Verify();
            Assert.True(r.IsValid);

            v.ValidationEntry.ForMember(x => x.Char, c => c.Range('x', 'z'));

            r = v.Verify();
            Assert.False(r.IsValid);

            r = v.Verify();
            Assert.False(r.IsValid);

            v.ValidationEntry.ForMember(x => x.Char, c => c.RangeWithCloseInterval('c', 'd').OverwriteRule());

            r = v.Verify();
            Assert.True(r.IsValid);

            v.ValidationEntry.ForMember(x => x.Char, c => c.RangeWithOpenInterval('c', 'd').OverwriteRule());

            r = v.Verify();
            Assert.False(r.IsValid);
        }

        [Fact(DisplayName = "Range`1 Token with DateTime")]
        public void DateTimeRangeTest()
        {
            var v = LeoVisitorFactory.Create(Data);
            v.ValidationEntry.ForMember(x => x.DateTime, c => c.Range(DateTime.Today.AddDays(-1), DateTime.Today.AddDays(1)));

            var r = v.Verify();
            Assert.True(r.IsValid);

            v.ValidationEntry.ForMember(x => x.DateTime, c => c.Range(DateTime.Today.AddDays(1), DateTime.Today.AddDays(2)));

            r = v.Verify();
            Assert.False(r.IsValid);

            v.ValidationEntry.ForMember(x => x.DateTime, c => c.RangeWithCloseInterval(DateTime.Today, DateTime.Today.AddDays(1)).OverwriteRule());

            r = v.Verify();
            Assert.True(r.IsValid);

            v.ValidationEntry.ForMember(x => x.DateTime, c => c.RangeWithOpenInterval(DateTime.Today, DateTime.Today.AddDays(1)).OverwriteRule());

            r = v.Verify();
            Assert.False(r.IsValid);
        }

        [Fact(DisplayName = "Range`1 Token with DateTimeOffset")]
        public void DateTimeOffsetRangeTest()
        {
            var v = LeoVisitorFactory.Create(Data);
            v.ValidationEntry.ForMember(x => x.DateTimeOffset, c => c.Range(DateTime.Today.AddDays(-1), DateTime.Today.AddDays(1)));

            var r = v.Verify();
            Assert.True(r.IsValid);

            v.ValidationEntry.ForMember(x => x.DateTimeOffset, c => c.Range(DateTime.Today.AddDays(1), DateTime.Today.AddDays(2)));

            r = v.Verify();
            Assert.False(r.IsValid);

            v.ValidationEntry.ForMember(x => x.DateTimeOffset, c => c.RangeWithCloseInterval(DateTime.Today, DateTime.Today.AddDays(1)).OverwriteRule());

            r = v.Verify();
            Assert.True(r.IsValid);

            v.ValidationEntry.ForMember(x => x.DateTimeOffset, c => c.RangeWithOpenInterval(DateTime.Today, DateTime.Today.AddDays(1)).OverwriteRule());

            r = v.Verify();
            Assert.False(r.IsValid);
        }

        [Fact(DisplayName = "Any Token with Bytes")]
        public void BytesAnyTest()
        {
            var v = LeoVisitorFactory.Create(Data);

            v.ValidationEntry.ForMember(x => x.Bytes, c => c.Any(x => x == 0));

            var r = v.Verify();
            Assert.True(r.IsValid);
        }

        [Fact(DisplayName = "Any Token with Array")]
        public void ObjArrayAnyTest()
        {
            var v = LeoVisitorFactory.Create(Data);
            var o = v.GetValue(x => x.SomeObj);

            v.ValidationEntry.ForMember(x => x.SomeNiceActArray, c => c.Any(s => s == o));

            var r = v.Verify();
            Assert.True(r.IsValid);
        }

        [Fact(DisplayName = "Any Token with List")]
        public void ObjListAnyTest()
        {
            var v = LeoVisitorFactory.Create(Data);
            var o = v.GetValue(x => x.SomeObj);

            v.ValidationEntry.ForMember(x => x.SomeNiceActList, c => c.Any<NiceAct2, List<NiceAct>, NiceAct>(s => s == o));

            var r = v.Verify();
            Assert.True(r.IsValid);
        }

        [Fact(DisplayName = "All Token with Bytes")]
        public void BytesAllTest()
        {
            var v = LeoVisitorFactory.Create(Data);

            v.ValidationEntry.ForMember(x => x.Bytes, c => c.All(b => b == 0));

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

            var v = LeoVisitorFactory.Create(d);

            v.ValidationEntry.ForMember(x => x.SomeNiceActArray, c => c.All(s => s == o));

            var r = v.Verify();
            Assert.False(r.IsValid);

            var v2 = LeoVisitorFactory.Create(Data);
            var o2 = v2.GetValue(x => x.SomeObj);
            v.ValidationEntry.ForMember(x => x.SomeNiceActArray, c => c.All(s => s == o2));

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

            var v = LeoVisitorFactory.Create(d);

            v.ValidationEntry.ForMember(x => x.SomeNiceActList, c => c.All<NiceAct2, List<NiceAct>, NiceAct>(s => s == o));

            var r = v.Verify();
            Assert.False(r.IsValid);

            var v2 = LeoVisitorFactory.Create(Data);
            var o2 = v2.GetValue(x => x.SomeObj);
            v.ValidationEntry.ForMember(x => x.SomeNiceActList, c => c.All<NiceAct2, List<NiceAct>, NiceAct>(s => s == o2));

            var r2 = v2.Verify();
            Assert.True(r2.IsValid);
        }

        [Fact(DisplayName = "In/NotIn Token with Str")]
        public void StrInTest()
        {
            var v = LeoVisitorFactory.Create(Data);

            v.ValidationEntry.ForMember(x => x.Str, c => c.In(new List<string> {"Str", "StrStr"}));

            var r = v.Verify();
            Assert.True(r.IsValid);

            v.ValidationEntry.ForMember(x => x.Str, c => c.NotIn(new List<string> {"Str", "StrStr"}).OverwriteRule());

            r = v.Verify();
            Assert.False(r.IsValid);
        }

        [Fact(DisplayName = "In/NotIn Token with Int16")]
        public void Int16InTest()
        {
            var v = LeoVisitorFactory.Create(Data);

            v.ValidationEntry.ForMember(x => x.Int16, c => c.In(new List<Int16> {16, 17}));

            var r = v.Verify();
            Assert.True(r.IsValid);

            v.ValidationEntry.ForMember(x => x.Int16, c => c.NotIn(new List<Int16> {16, 17}).OverwriteRule());

            r = v.Verify();
            Assert.False(r.IsValid);
        }

        [Fact(DisplayName = "In/NotIn Token with Int32")]
        public void Int32InTest()
        {
            var v = LeoVisitorFactory.Create(Data);

            v.ValidationEntry.ForMember(x => x.Int32, c => c.In(new List<Int32> {32, 33}));

            var r = v.Verify();
            Assert.True(r.IsValid);

            v.ValidationEntry.ForMember(x => x.Int32, c => c.NotIn(new List<Int32> {32, 33}).OverwriteRule());

            r = v.Verify();
            Assert.False(r.IsValid);
        }

        [Fact(DisplayName = "In/NotIn Token with Int64")]
        public void Int64InTest()
        {
            var v = LeoVisitorFactory.Create(Data);

            v.ValidationEntry.ForMember(x => x.Int64, c => c.In(new List<Int64> {64, 65}));

            var r = v.Verify();
            Assert.True(r.IsValid);

            v.ValidationEntry.ForMember(x => x.Int64, c => c.NotIn(new List<Int64> {64, 65}).OverwriteRule());

            r = v.Verify();
            Assert.False(r.IsValid);
        }

        [Fact(DisplayName = "In/NotIn Token with Char")]
        public void CharInTest()
        {
            var v = LeoVisitorFactory.Create(Data);

            v.ValidationEntry.ForMember(x => x.Char, c => c.In(new List<Char> {'c', 'd'}));

            var r = v.Verify();
            Assert.True(r.IsValid);

            v.ValidationEntry.ForMember(x => x.Char, c => c.NotIn(new List<Char> {'c', 'd'}).OverwriteRule());

            r = v.Verify();
            Assert.False(r.IsValid);
        }
    }
}