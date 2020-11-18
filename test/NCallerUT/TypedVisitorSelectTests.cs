using System;
using System.Linq;
using NMS.Leo.Typed;
using Xunit;

namespace NCallerUT
{
    [Trait("TypedVisitor.Select", "LeoSelector")]
    public class TypedVisitorSelectTests : Prepare
    {
        [Fact(DisplayName = "直接实例选择操作测试")]
        public void DirectInstanceSelectTest()
        {
            var act = new NiceAct()
            {
                Name = "Hu",
                Age = 22,
                Country = Country.China,
                Birthday = DateTime.Today
            };

            var type = typeof(NiceAct);
            var v = LeoVisitorFactory.Create(type, act);

            var z0 = v.Select((s, o) => s);
            var z1 = v.Select((s, o, m) => s);
            var z2 = v.Select(c => c.Name);
            var z3 = v.Select<(string, int)>(c => (c.Name, c.Index));
            var z4 = v.Select(c => new {c.Name, c.Value, c.Index});

            Assert.NotNull(z0);
            Assert.NotNull(z1);
            Assert.NotNull(z2);
            Assert.NotNull(z3);
            Assert.NotNull(z4);

            var l0 = z0.FireAndReturn();
            var l1 = z1.FireAndReturn();
            var l2 = z2.FireAndReturn();
            var l3 = z3.FireAndReturn();
            var l4 = z4.FireAndReturn();

            Assert.Equal(5, l0.Count());
            Assert.Equal(5, l1.Count());
            Assert.Equal(5, l2.Count());
            Assert.Equal(5, l3.Count());
            Assert.Equal(5, l4.Count());

            var f0 = l0.First();
            var f1 = l1.First();
            var f2 = l2.First();
            var f3 = l3.First();
            var f4 = l4.First();

            Assert.Equal("Name", f0);
            Assert.Equal("Name", f1);
            Assert.Equal("Name", f2);
            Assert.Equal("Name", f3.Item1);
            Assert.Equal(0, f3.Item2);
            Assert.Equal("Name", f4.Name);
            Assert.Equal("Hu", f4.Value);
            Assert.Equal(0, f4.Index);
        }

        [Fact(DisplayName = "直接类型选择操作测试")]
        public void DirectFutureSelectTest()
        {
            var type = typeof(NiceAct);
            var v = LeoVisitorFactory.Create(type);

            v.SetValue("Name", "Du");
            v.SetValue("Age", 55);
            v.SetValue("Country", Country.USA);
            v.SetValue("Birthday", DateTime.Today.AddDays(-1));
            v.SetValue("IsValid", true);

            var z0 = v.Select((s, o) => s);
            var z1 = v.Select((s, o, m) => s);
            var z2 = v.Select(c => c.Name);
            var z3 = v.Select<(string, int)>(c => (c.Name, c.Index));
            var z4 = v.Select(c => new {c.Name, c.Value, c.Index});

            Assert.NotNull(z0);
            Assert.NotNull(z1);
            Assert.NotNull(z2);
            Assert.NotNull(z3);
            Assert.NotNull(z4);

            var l0 = z0.FireAndReturn();
            var l1 = z1.FireAndReturn();
            var l2 = z2.FireAndReturn();
            var l3 = z3.FireAndReturn();
            var l4 = z4.FireAndReturn();

            Assert.Equal(5, l0.Count());
            Assert.Equal(5, l1.Count());
            Assert.Equal(5, l2.Count());
            Assert.Equal(5, l3.Count());
            Assert.Equal(5, l4.Count());

            var f0 = l0.First();
            var f1 = l1.First();
            var f2 = l2.First();
            var f3 = l3.First();
            var f4 = l4.First();

            Assert.Equal("Name", f0);
            Assert.Equal("Name", f1);
            Assert.Equal("Name", f2);
            Assert.Equal("Name", f3.Item1);
            Assert.Equal(0, f3.Item2);
            Assert.Equal("Name", f4.Name);
            Assert.Equal("Du", f4.Value);
            Assert.Equal(0, f4.Index);
        }

        [Fact(DisplayName = "泛型实例选择操作测试")]
        public void GenericInstanceSelectTest()
        {
            var act = new NiceAct()
            {
                Name = "Hu",
                Age = 22,
                Country = Country.China,
                Birthday = DateTime.Today
            };

            var v = LeoVisitorFactory.Create<NiceAct>(act);

            var z0 = v.Select((s, o) => s);
            var z1 = v.Select((s, o, m) => s);
            var z2 = v.Select(c => c.Name);
            var z3 = v.Select(c => (c.Name, c.Index));
            var z4 = v.Select(c => new {c.Name, c.Value, c.Index});

            Assert.NotNull(z0);
            Assert.NotNull(z1);
            Assert.NotNull(z2);
            Assert.NotNull(z3);
            Assert.NotNull(z4);

            var l0 = z0.FireAndReturn();
            var l1 = z1.FireAndReturn();
            var l2 = z2.FireAndReturn();
            var l3 = z3.FireAndReturn();
            var l4 = z4.FireAndReturn();

            Assert.Equal(5, l0.Count());
            Assert.Equal(5, l1.Count());
            Assert.Equal(5, l2.Count());
            Assert.Equal(5, l3.Count());
            Assert.Equal(5, l4.Count());

            var f0 = l0.First();
            var f1 = l1.First();
            var f2 = l2.First();
            var f3 = l3.First();
            var f4 = l4.First();

            Assert.Equal("Name", f0);
            Assert.Equal("Name", f1);
            Assert.Equal("Name", f2);
            Assert.Equal("Name", f3.Item1);
            Assert.Equal(0, f3.Item2);
            Assert.Equal("Name", f4.Name);
            Assert.Equal("Hu", f4.Value);
            Assert.Equal(0, f4.Index);
        }

        [Fact(DisplayName = "泛型类型选择操作测试")]
        public void GenericFutureSelectTest()
        {
            var v = LeoVisitorFactory.Create<NiceAct>();

            v.SetValue("Name", "Du");
            v.SetValue("Age", 55);
            v.SetValue("Country", Country.USA);
            v.SetValue("Birthday", DateTime.Today.AddDays(-1));
            v.SetValue("IsValid", true);

            var z0 = v.Select((s, o) => s);
            var z1 = v.Select((s, o, m) => s);
            var z2 = v.Select(c => c.Name);
            var z3 = v.Select(c => (c.Name, c.Index));
            var z4 = v.Select(c => new {c.Name, c.Value, c.Index});

            Assert.NotNull(z0);
            Assert.NotNull(z1);
            Assert.NotNull(z2);
            Assert.NotNull(z3);
            Assert.NotNull(z4);

            var l0 = z0.FireAndReturn();
            var l1 = z1.FireAndReturn();
            var l2 = z2.FireAndReturn();
            var l3 = z3.FireAndReturn();
            var l4 = z4.FireAndReturn();

            Assert.Equal(5, l0.Count());
            Assert.Equal(5, l1.Count());
            Assert.Equal(5, l2.Count());
            Assert.Equal(5, l3.Count());
            Assert.Equal(5, l4.Count());

            var f0 = l0.First();
            var f1 = l1.First();
            var f2 = l2.First();
            var f3 = l3.First();
            var f4 = l4.First();

            Assert.Equal("Name", f0);
            Assert.Equal("Name", f1);
            Assert.Equal("Name", f2);
            Assert.Equal("Name", f3.Item1);
            Assert.Equal(0, f3.Item2);
            Assert.Equal("Name", f4.Name);
            Assert.Equal("Du", f4.Value);
            Assert.Equal(0, f4.Index);
        }
    }
}