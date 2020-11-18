<p align="center">
<span>中文</span> | 
<a href="README.md">English</a>
</p>

# Leo

一个高性能的类型动态操作库。

[![Member project of Night Moon Studio](https://img.shields.io/badge/member%20project%20of-NMS-9e20c9.svg)](https://github.com/night-moon-studio)
[![NuGet Badge](https://buildstats.info/nuget/NMS.NCaller?includePreReleases=true)](https://www.nuget.org/packages/NMS.NCaller)
![GitHub repo size](https://img.shields.io/github/repo-size/night-moon-studio/ncaller.svg)
[![Badge](https://img.shields.io/badge/link-996.icu-red.svg)](https://996.icu/#/zh_CN)
[![GitHub license](https://img.shields.io/github/license/night-moon-studio/ncaller.svg)](https://github.com/night-moon-studio/NCaller/blob/master/LICENSE)

本项目基于 [NCC Natasha](https://github.com/dotnetcore/natasha) 和 .NET。

通过运行时自动构建高性能操作代理类。为普通类、静态类、动态类、嵌套动态类以及动态生成的静态类等提供了完备的高性能操作。

成员索引及类型缓存均采用高性能算法进行重建。如果反射、Dynamic 等方法都不能满足你的特殊需求，则你可以选择使用本方案。

### 持续构建

| CI Platform | Build Server |  Master Test |
|--------- |------------- | --------|
| Github | ![os](https://img.shields.io/badge/os-all-black.svg) | [![Build status](https://img.shields.io/github/workflow/status/night-moon-studio/leo/.NET%20Core/master)](https://github.com/night-moon-studio/tree/actions) |

## 开始

### 安装

Leo 可通过以下命令安装在你的项目中

```shell
PM> Install-Package NMS.Leo
PM> Install-Package NMS.Leo.Typed
```

### Natasha 初始化

```C#
 // 仅仅注册组件
 NatashaInitializer.Initialize();
 
 // 或者
 // 注册组件+预热组件 , 之后编译会更加快速
 await NatashaInitializer.InitializeAndPreheating();
 ```

### 核心用法

Leo 使用了 NCC BTFindTree Algorithm 作为方法查找算法，并默认选用 `Precision`（精确最小权）来构建属性和字段的索引。

#### 创建字典操作器

使用精确最小权方法构建属性和字段索引：

```c#
var handler = PrecisionDictOperator.CreateFromType(typeof(A));
// or
var handler = PrecisionDictOperator<A>.Create();
```

使用哈希二分查找方法构建属性和字段索引：

```c#
var handler = HashDictOperator.CreateFromType(typeof(A));
// or
var handler = HashDictOperator<A>.Create();
```

使用模糊指针查找方法构建属性和字段索引：

```c#
var handler = FuzzyDictOperator.CreateFromType(typeof(A));
// or
var handler = FuzzyDictOperator<A>.Create();
```

#### 字典操作器的调用

假设有两个类型 A 和 B：

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

则我们可以如此调用字典操作器：

```c#
var handler = PrecisionDictOperator.CreateFromType(typeof(A));
handler.New();

handler["Age"]= 100;                                          // Set operation
handler.Set("Age", 100);                                      // Set operation

Console.WriteLine(handler["Time"]);                           // Get operation
Console.WriteLine(handler.Get<DateTime>("Time"));             // Get operation

((B)handler["Outter"]).Name = "NewName";                      // Link operation
```

### 动态构建类型

我们首先准备一段文本：

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

然后通过 Natasha 构建运行时类型：

```c#
var oop = new AssemblyCSharpBuilder();
oop.Add(text);
Type type = oop.GetTypeFromShortName("Test");
```

最后使用 Leo 来操作这个运行时类型的实例：

```c#
var instance = PrecisionDictOperator.CreateFromType(type);

// 如果使用 NMS.Leo.Typed 的 LeoVisitor，则这两部是自动完成的
var obj = Activator.CreateInstance(type);
instance.SetObjInstance(obj);

instance["Pp"] = 30L;
instance["Rp"] = "ab";
instance.Set("Name", "222");
```

## Leo Visitor

在 `NMS.Leo.Typed` 包中提供了更易使用的封装。

引用命名空间：

```c#
using NMS.Leo.Typed;
```

### 创建 Visitor 实例

Leo Visitor 支持通过类型（Type）和泛型来创建：

```c#
var type = typeof(YourType);
var visitor = LeoVisitorFactory.Create(type); // returns ILeoVisitor instance

// or
var visitor = LeoVisitorFactory.Create<YourType>(); // returns ILeoVisitor<YourType> instance
```

### 设置或获取实例对象

你可以为 Leo Visitor 给定一个已存在的对象：

```c#
var type = typeof(YourType);
var instance = new YourType();

// 直接在工厂方法中给定实例对象：
var visitor = LeoVisitorFactory.Create(type, instance); // returns ILeoVisitor instance

// or
var visitor = LeoVisitorFactory.Create<YourType>(instance); // returns ILeoVisitor<YourType> instance
```

然后从 Leo Visitor 中获取实例对象：

```c#
object instance = visitor.Instance; // 从 ILeoVisitor 中获得 object 对象。

// or
T instance = visitor.Instance; // 从 ILeoVisitor<T> 中获得类型 T 的实例。
```

### 使用字典初始化 LeoVisitor

在创建 Leo Visitor 时，也可以使用字典直接初始化实例：

```c#
var d = new Dictionary<string, object>();
d["Name"] = "YourMidName";
d["Age"] = 25;

var type = typeof(YourType);
var visitor = LeoVisitorFactory.Create(type, d); // returns ILeoVisitor

// or
var visitor = LeoVisitorFactory.Create<YourType>(d); // returns ILeoVisitor<YourType>
```

然后从 Leo Visitor 中获取实例对象：

```c#
object instance = visitor.Instance; // 从 ILeoVisitor 中获得 object 对象。

// or
T instance = visitor.Instance; // 从 ILeoVisitor<T> 中获得类型 T 的实例。
```

### 设置或获取值

可以通过 `GetValue` 或 `SetValue` 方法读写 Leo Visitor 中的值。

#### GetValue

从 Leo Visitor 中读取名为 `Name` 的字段或属性的值：

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
string name = visitor.GetValue(t => t.Name); // 仅支持 ILeoVisitor<YourType>
```

或者通过字典一次获得所有字段或属性的值：

```c#
var d = visitor.ToDictionary(); // Dictionary<string, object>
```

#### SetValue

将值 `YourName` 设置给名为 `Name` 的字段或属性：

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
visitor.SetValue<string>(t => t.Name, "YourName"); // 仅支持 ILeoVisitor<YourType>
```

甚至可以通过字典直接批量操作：

```c#
var d = new Dictionary<string, object>();
d["Name"] = "YourMidName";
d["Age"] = 25;

visitor.SetValue(d);
```

### 选择需要返回的成员

我们可以通过 `Select` 来选择并返回我们需要的成员：

```c#
var visitor = LeoVisitorFactory.Create(typeof(YourType)); // ILeoVisitor

var z0 = v.Select((name, val) => name);                         // returns ILeoSelector<YourType, string>
var z1 = v.Select((name, val, metadata) => name);               // returns ILeoSelector<YourType, string>
var z2 = v.Select(ctx => ctx.Name);                             // returns ILeoSelector<YourType, string>
var z3 = v.Select(ctx => (ctx.Name, ctx.Index));                // returns ILeoSelector<YourType, (Name, Index)>
var z4 = v.Select(ctx => new {ctx.Name, ctx.Value, ctx.Index}); // returns ILeoSelector<YourType, {Name, Value, Index}>
```

此时我们会获得一个 ILeoSelector 接口的实现，我们只需要执行 `FireAndReturn()` 方法便可获得我们所需要的结果：

```c#
var l0 = z0.FireAndReturn(); // returns IEnumerable<string>
var l1 = z1.FireAndReturn(); // returns IEnumerable<string>
var l2 = z2.FireAndReturn(); // returns IEnumerable<string>
var l3 = z3.FireAndReturn(); // returns IEnumerable<(Name, Index)>
var l4 = z4.FireAndReturn(); // returns IEnumerable<{Name, Value, Index}>
```

## Leo 元数据

你可以从字典操作器中获得字段或属性的元数据：

```c#
var instance = PrecisionDictOperator<YourType>.Create(); // DictBase<YourType>

var members = instance.GetMembers(); // IEnumerable<LeoMember>

// 或者只获得可读 / 可写的成员
var members = instance.GetCanReadMembers();
var members = instance.GetCanWriteMembers();

// 或者获取指定名称的属性或字段的元数据
var member = instance.GetMember("Name"); // LeoMember
```

你可以从 Leo Visitor 中获得字段或属性的元数据：

```c#
var visitor = LeoVisitorFactory.Create(typeof(YourType)); // ILeoVisitor

// 使用指定名称来获取对应的属性或字段的元数据
var member = visitor.GetMember("Name"); // LeoMember

// 或者直接指定该类型的成员来获取属性或字段的元数据
var member = visitor.GetMember( t => t.Name ); // 仅支持 ILeoVisitor<YourType>
```

## 历史

- 2019-08-01 ： 发布 v1.0.0.0, 高性能动态调用库。
- 2020-10-12 ： 发布 v1.2.0.0, 使用最新版本 Natasha 与 [DynamicCache](https://github.com/night-moon-studio/DynamicCache) ，并使用函数指针代替系统委托。  

## 算法

- NCC BTFindTree Algorithm: https://github.com/dotnet-lab/BTFindTree

## 性能

![Performance](Image/%E6%80%A7%E8%83%BD%E6%B5%8B%E8%AF%951.png)

## 许可证

[![FOSSA Status](https://app.fossa.io/api/projects/git%2Bgithub.com%2Fnight-moon-studio%2FNCaller.svg?type=large)](https://app.fossa.io/projects/git%2Bgithub.com%2Fnight-moon-studio%2FNCaller?ref=badge_large)

[MIT](LICENSE)