<p align="center">
<a href="README.zh-CN.md">中文</a> | 
<span>English</span>
</p>

# Leo

A high-performance type dynamic operation library.

[![Member project of Night Moon Studio](https://img.shields.io/badge/member%20project%20of-NMS-9e20c9.svg)](https://github.com/night-moon-studio)
[![NuGet Badge](https://buildstats.info/nuget/NMS.Leo?includePreReleases=true)](https://www.nuget.org/packages/NMS.Leo)
 ![GitHub repo size](https://img.shields.io/github/repo-size/night-moon-studio/leo.svg)
[![Badge](https://img.shields.io/badge/link-996.icu-red.svg)](https://996.icu/#/zh_CN)
[![GitHub license](https://img.shields.io/github/license/night-moon-studio/leo.svg)](https://github.com/night-moon-studio/Leo/blob/master/LICENSE)


This project is based on [NCC Natasha](https://github.com/dotnetcore/natasha) and .NET.

Automatically build high-performance operation agent classes through runtime. Provides complete high-performance operations for ordinary classes, static classes, dynamic classes, nested dynamic classes, and dynamically generated static classes.

Member index and type cache are rebuilt using high-performance algorithms. If reflection, Dynamic and other methods cannot meet your special needs, you can choose to use this solution.

### CI Build Status  

| CI Platform | Build Server |  Master Test |
|--------- |------------- | --------|
| Github | ![os](https://img.shields.io/badge/os-all-black.svg) | [![Build status](https://img.shields.io/github/workflow/status/night-moon-studio/leo/.NET%20Core/master)](https://github.com/night-moon-studio/tree/actions) |

## Getting Started

### Install

Leo can be installed in your project with the following command.

```shell
PM> Install-Package NMS.Leo
PM> Install-Package NMS.Leo.Typed
```

### Natasha initialization

```c#
// Only register components
NatashaInitializer.Initialize();

// or
// Register the component and warm up the component, 
// the runtime compilation speed will be faster.
await NatashaInitializer.InitializeAndPreheating();
```

### Core Usage

Leo uses NCC BTFindTree Algorithm as the method search algorithm, and uses `Precision` by default to build the index of properties and fields.

#### Create Dictionary Operator

Use precision minimum weight to build properties and fields index:

```c#
var handler = PrecisionDictOperator.CreateFromType(typeof(A));
// or
var handler = PrecisionDictOperator<A>.Create();
```

Use hash binary search to build properties and fields index:

```c#
var handler = HashDictOperator.CreateFromType(typeof(A));
// or
var handler = HashDictOperator<A>.Create();
```

Use fuzzy pointer search to build properties and fields index:

```c#
var handler = FuzzyDictOperator.CreateFromType(typeof(A));
// or
var handler = FuzzyDictOperator<A>.Create();
```

#### How to use the dictionary operator

Suppose there are two types A and B:

```c#
public class A
{
   public int Age;
   public DateTime Time;
   public B Outter = new B();
}

public class B
{
   public string Name;
   public B()
   {
      Name = "小明"
   }
}
```

Then we can call the dictionary operator like this:

```c#
var handler = PrecisionDictOperator.CreateFromType(typeof(A));
handler.New();

handler["Age"]= 100;                                          // Set operation
handler.Set("Age", 100);                                      // Set operation

Console.WriteLine(handler["Time"]);                           // Get operation
Console.WriteLine(handler.Get<DateTime>("Time"));             // Get operation

((B)handler["Outter"]).Name = "NewName";                      // Link operation
```

### Build Dynamic Type

We first prepare a piece of text:

```c#
string text = @"
using System;
using System.Collections;
using System.Linq;
using System.Text;
 
namespace HelloWorld
{
    public class Test
    {
        public Test(){
            Name=""111"";
            Pp = 10;
            Rp=""aa"";
        }
        private long Pp;
        private readonly string Rp;
        public string Name;
        public int Age{get;set;}
    }
}";
```

Then build the runtime type through Natasha:

```c#
var oop = new AssemblyCSharpBuilder();
oop.Add(text);
Type type = oop.GetTypeFromShortName("Test");
```

Finally, use Leo to manipulate instances of this runtime type:

```c#
var instance = PrecisionDictOperator.CreateFromType(type);

// If you use LeoVisitor in NMS.Leo.Typed, these two parts are done automatically
var obj = Activator.CreateInstance(type);
instance.SetObjInstance(obj);

instance["Pp"] = 30L;
instance["Rp"] = "ab";
instance.Set("Name", "222");
```

## Leo Visitor

An easier-to-use package is provided in the `NMS.Leo.Typed` package.

Reference namespace:

```c#
using NMS.Leo.Typed;
```

### Create Visitor Instance

Leo Visitor supports creation through Type and Generic parameter:

```c#
var type = typeof(YourType);
var visitor = LeoVisitorFactory.Create(type); // returns ILeoVisitor instance

// or
var visitor = LeoVisitorFactory.Create<YourType>(); // returns ILeoVisitor<YourType> instance
```

### Initialize Leo Visitor with an Object Instance

You can specify an existing object for Leo Visitor:

```c#
var type = typeof(YourType);
var instance = new YourType();

// Give the instance object directly in the factory method.
var visitor = LeoVisitorFactory.Create(type, instance); // returns ILeoVisitor instance

// or
var visitor = LeoVisitorFactory.Create<YourType>(instance); // returns ILeoVisitor<YourType> instance
```

Then get the instance object from Leo Visitor:

```c#
object instance = visitor.Instance; // Obtain the object object from ILeoVisitor.

// or
T instance = visitor.Instance; // Obtain an instance of type T from ILeoVisitor<T>.
```

### Initialize Leo Visitor with a Dictionary

When creating Leo Visitor, you can also use a dictionary to directly initialize the instance:

```c#
var d = new Dictionary<string, object>();
d["Name"] = "YourMidName";
d["Age"] = 25;

var type = typeof(YourType);
var visitor = LeoVisitorFactory.Create(type, d); // returns ILeoVisitor

// or
var visitor = LeoVisitorFactory.Create<YourType>(d); // returns ILeoVisitor<YourType>
```

Then get the instance object from Leo Visitor:

```c#
object instance = visitor.Instance; // Obtain the object object from ILeoVisitor.

// or
T instance = visitor.Instance; // Obtain an instance of type T from ILeoVisitor<T>.
```

### Set or Get Value

The value in Leo Visitor can be read and written through the `GetValue` or `SetValue` method.

#### GetValue

Read the value of the field or property named `Name` from Leo Visitor:

```c#
var visitor = LeoVisitorFactory.Create(typeof(YourType)); // ILeoVisitor

object name = visitor.GetValue("Name");

// or
string name = visitor.GetValue<string>("Name");

// or
object name = visitor["Name"];

// or
object name = visitor.GetValue<YourType>(t => t.Name);

// or
string name = visitor.GetValue<YourType, string>(t => t.Name);

// or
string name = visitor.GetValue(t => t.Name); // only for ILeoVisitor<YourType>
```

Or get the values of all fields or properties at once through a dictionary:

```c#
var d = visitor.ToDictionary(); // Dictionary<string, object>
```

#### SetValue

Set the value `YourName` to the field or property named `Name`.

```c#
var visitor = LeoVisitorFactory.Create(typeof(YourType)); // ILeoVisitor

visitor.SetValue("Name", "YourName");

// or
visitor["Name"] = "YourName";

// or
visitor.SetValue<YourType>(t => t.Name, "YourName");

// or
visitor.SetValue<YourType, string>(t => t.Name, "YourName");

// or
visitor.SetValue<string>(t => t.Name, "YourName"); // only for ILeoVisitor<YourType>
```

You can even batch operations directly through the dictionary:

```c#
var d = new Dictionary<string, object>();
d["Name"] = "YourMidName";
d["Age"] = 25;

visitor.SetValue(d);
```

### Select the members to be returned

We can use `Select` to select and return the members we need:

```c#
var visitor = LeoVisitorFactory.Create(typeof(YourType)); // ILeoVisitor

var z0 = v.Select((name, val) => name);                         // returns ILeoSelector<YourType, string>
var z1 = v.Select((name, val, metadata) => name);               // returns ILeoSelector<YourType, string>
var z2 = v.Select(ctx => ctx.Name);                             // returns ILeoSelector<YourType, string>
var z3 = v.Select(ctx => (ctx.Name, ctx.Index));                // returns ILeoSelector<YourType, (Name, Index)>
var z4 = v.Select(ctx => new {ctx.Name, ctx.Value, ctx.Index}); // returns ILeoSelector<YourType, {Name, Value, Index}>
```

At this point we will get an implementation of the ILeoSelector interface, we only need to execute the `FireAndReturn()` method to get the result we need:

```c#
var l0 = z0.FireAndReturn(); // returns IEnumerable<string>
var l1 = z1.FireAndReturn(); // returns IEnumerable<string>
var l2 = z2.FireAndReturn(); // returns IEnumerable<string>
var l3 = z3.FireAndReturn(); // returns IEnumerable<(Name, Index)>
var l4 = z4.FireAndReturn(); // returns IEnumerable<{Name, Value, Index}>
```

## Leo Getter and Setter

We can use the built-in `LeoGetter` and `LeoSetter` to fluently create a Getter or a Setter for an instance or value.

### Instance Reader: `ILeoGetter`

We can get the value of its member (properties or fields) from the instance through the instance reader (exposed as `ILeoGetter`).

```c#
var type = typeof(YourType);
var act = new YourType()
{
    Name = "YourName",
    Age = 22,
    Country = Country.China,
    Birthday = DateTime.Today
};

var getter = LeoGetter.Type(type).Instance(act); // returns ILeoGetter

// or
var getter = LeoGetter.Type<YourType>().Instance(act); // return ILeoGetter<YourType>
```

When creating an instance reader, we can initialize it through a dictionary. At this time, the instance reader will construct an object by itself.

```c#
var d = new Dictionary<string, object>();
d["Name"] = "YourName";

var getter = LeoGetter.Type(type).InitialValues(d); // returns ILeoGetter
```

Then, we can read the value within the instance:

```c#
var val = getter.GetValue<string>("Name");
```

Essentially, the instance reader is a read-only `ILeoVisitor`.



### Instance Setter: `ILeoSetter`

We can set values to the members (properties or fields) of the instance through the built-in instance setter (exposed as `ILeoSetter`).

```c#
var type = typeof(YourType);
```

You can pass the instance directly into the instance setter:

```c#
var act = new YourType()
{
    Name = "YourName",
    Age = 22,
    Country = Country.China,
    Birthday = DateTime.Today
};

var setter = LeoSetter.Type(type).Instance(act); // ILeoSetter

// or
var setter = LeoSetter.Type<YourType>().Instance(act); // ILeoSetter<YourType>
```

Or use a dictionary:

```c#
var d = new Dictionary<string, object>();
d["Name"] = "YourName";

var setter = LeoSetter.Type(type).InitialValues(d); // ILeoSetter
```

Or automatically create a new object:

```c#
var setter = LeoSetter.Type(type).NewInstance(); // ILeoSetter

// or
var setter = LeoSetter.Type<NiceAct>().NewInstance(); // ILeoSetter<YourType>
```

Then, we can set the value in the instance:

```c#
setter.SetValue("Name", "YourMidName");
```

Essentially, the instance setter is a write-only `ILeoVisitor`.



### Value Reader: `ILeoValueGetter`

Through the value reader, we can read a member (property or field) in the instance at a finer granularity, and the value reader can prevent users from reading the value of an unrelated member (properties or fields) .

```c#
var type = typeof(YourType);

var fluentGetter = LeoGetter.Type(type).Value("Name"); // returns IFluentValueGetter

// or
var fluentGetter = LeoGetter.Type<YourType>().Value("Name"); // returns IFluentValueGetter<YourType>

// or
var fluentGetter = LeoGetter.Type<YourType>().Value(t => t.Name); // returns IFluentValueGetter<YourType>

// or
var fluentGetter = LeoGetter.Type<YourType>().Value<string>(t => t.Name); // returns IFluentValueGetter<YourType>
```

Then, we specify a specific instance for `fluentGetter`:

```c#
var act = new YourType()
{
    Name = "YourName",
    Age = 22,
    Country = Country.China,
    Birthday = DateTime.Today
};

var getter = fluentGetter.Instance(act); // ILeoValueGetter
```

Finally, we can read the specified member (property or field) from the instance:

```c#
var val = getter.Value;
```



### Value Setter: `ILeoValueSetter`

Through the value setter, we can write to a member (property or field) in the instance with finer granularity, and the value setter can prevent users from setting irrelevant members (properties or fields).

```c#
var type = typeof(YourType);

var fluentSetter = LeoSetter.Type(type).Value("Name"); // return IFluentValueSetter

// or
var fluentSetter = LeoSetter.Type<NiceAct>().Value("Name"); // return IFluentValueSetter<YourType>

// or
var fluentSetter = LeoSetter.Type<NiceAct>().Value(t => t.Name); // return IFluentValueSetter<YourType>

// or
var fluentSetter = LeoSetter.Type<NiceAct>().Value<string>(t => t.Name); // return IFluentValueSetter<YourType>
```

Then, we specify a specific instance for `fluentSetter`:

```c#
var act = new YourType()
{
    Name = "YourName",
    Age = 22,
    Country = Country.China,
    Birthday = DateTime.Today
};

var setter = fluentSetter.Instance(act); // ILeoValueSetter
```

Finally, we can set values to the specified member (property or field) of the instance:

```c#
setter.Value("YourLastName");
```



## Leo Metadata

You can get the metadata of the field or property from the dictionary operator:

```c#
var instance = PrecisionDictOperator<YourType>.Create(); // DictBase<YourType>

var members = instance.GetMembers(); // IEnumerable<LeoMember>

// Or get only readable/writable members
var members = instance.GetCanReadMembers();
var members = instance.GetCanWriteMembers();

// Or get the metadata of the property or field of the specified name
var member = instance.GetMember("Name"); // LeoMember
```

You can get the metadata of fields or properties from Leo Visitor:

```c#
var visitor = LeoVisitorFactory.Create(typeof(YourType)); // ILeoVisitor

// Use the specified name to get the metadata of the corresponding property or field
var member = visitor.GetMember("Name"); // LeoMember

// Or directly specify the member of this type to get the metadata of the property or field
var member = visitor.GetMember( t => t.Name ); // Only for ILeoVisitor<YourType>
```

## Release Notes

- 2019-08-01: Release v1.0.0.0, a high-performance dynamic calling library.
- 2020-10-12: Release v1.2.0.0, use the latest version of Natasha and [DynamicCache](https://github.com/night-moon-studio/DynamicCache), and use function pointers instead of system delegates.

## Algorithm

- NCC BTFindTree Algorithm: https://github.com/dotnet-lab/BTFindTree

## Performance 

![Performance](https://images.gitee.com/uploads/images/2020/1201/170347_7d3e36c3_1478282.png)  

## License

[![FOSSA Status](https://app.fossa.io/api/projects/git%2Bgithub.com%2Fnight-moon-studio%2FNCaller.svg?type=large)](https://app.fossa.io/projects/git%2Bgithub.com%2Fnight-moon-studio%2FNCaller?ref=badge_large) 

[MIT](LICENSE)
