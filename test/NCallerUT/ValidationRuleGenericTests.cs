using System;
using NCallerUT.Model;
using NMS.Leo.Typed;
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
    }
}