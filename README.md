

<p align="center">
  <span>中文</span> |  
  <a href="https://github.com/night-moon-studio/ncaller/tree/master/lang/english">English</a>
</p>

# NCaller

[![Member project of Night Moon Studio](https://img.shields.io/badge/member%20project%20of-NMS-9e20c9.svg)](https://github.com/night-moon-studio)
[![NuGet Badge](https://buildstats.info/nuget/DotNetCore.Natasha.NCaller?includePreReleases=true)](https://www.nuget.org/packages/DotNetCore.Natasha.NCaller)
 ![GitHub repo size](https://img.shields.io/github/repo-size/night-moon-studio/ncaller.svg)
[![Badge](https://img.shields.io/badge/link-996.icu-red.svg)](https://996.icu/#/zh_CN)
[![GitHub license](https://img.shields.io/github/license/night-moon-studio/ncaller.svg)](https://github.com/night-moon-studio/NCaller/blob/master/LICENSE)


<br/>
  

### 持续构建(CI Build Status)  

| CI Platform | Build Server |  Master Test |
|--------- |------------- | --------|
| Github | linux/mac/windows | [![Build status](https://img.shields.io/github/workflow/status/night-moon-studio/ncaller/.NET%20Core/master)](https://github.com/night-moon-studio/NCaller/actions) |


<br/>    

### 项目简介： 

此项目为[Natasha](https://github.com/dotnetcore/Natasha)的衍生项目，通过运行时自动构建高性能操作代理类，为普通类，静态类，动态类，动态类中的动态类，动态生成的静态类等提供了良好的、完备的、高性能的操作，成员索引及类型缓存均采用高性能算法进行重建，如果反射、dynamic都不能满足高端的需求，可使用本类库，它将是一个不错的选择。  

<br/>    


### 发布计划： 
  
 - 2019-08-01 ： 发布v1.0.0.0, 高性能动态调用库。  
 
 <br/>  
 
---------------------  
 <br/>  
 
### 性能展示：  

![性能对比](https://github.com/night-moon-studio/NCaller/blob/master/Image/%E6%80%A7%E8%83%BD%E6%B5%8B%E8%AF%951.png)  

 <br/> 

### 使用方法(User Api)：  
 <br/>  
 
#### 首先编辑您的工程文件：


```C#

  <PropertyGroup>
  
    <OutputType>Exe</OutputType>
    <TargetFramework>netcoreapp2.2</TargetFramework>
    
    //控制台/桌面如下
    <PreserveCompilationContext>true</PreserveCompilationContext>
    
    //老版WEB需要
    <MvcRazorExcludeRefAssembliesFromPublish>false</MvcRazorExcludeRefAssembliesFromPublish>
    
    //3.1 新版WEB要加
    <PreserveCompilationReferences>true</PreserveCompilationReferences>
    //3.1 如果不加上面节点也可以引用Razor的编译服务
    Microsoft.AspNetCore.Mvc.Razor.RuntimeCompilation
    
    //如果你觉得发布文件夹下关于本地化的文件夹太多，您可以选择如下节点
    //选项：cs / de / es / fr / it / ja / ko / pl / ru / tr / zh-Hans / zh-Hant
    <SatelliteResourceLanguages>en</SatelliteResourceLanguages>
    
  </PropertyGroup>
 
```  
<br/>  

#### 算法引用

https://github.com/dotnet-lab/BTFindTree


<br/>
  

#### 选用操作类：

```C#

//DictOperator : 字典风格的操作，默认使用的是精确最小权方法构建属性/字段索引。
CallerManagement.AddType(typeof(A));
var handler = DictOperator.CreateFromType(typeof(A));
var handler = DictOperator.CreateFromString("A");
var handler = DictOperator<A>.Create();


 
//HashDictOperator : 字典风格的操作，默认使用的哈希二分查找方法构建属性/字段索引。
CallerManagement.AddType(typeof(A));
var handler = HashDictOperator.CreateFromType(typeof(A));
var handler = HashDictOperator.CreateFromString("A");
var handler = HashDictOperator<A>.Create();



//FuzzyDictOperator : 字典风格的操作，默认使用的模糊指针查找方法构建属性/字段索引。
CallerManagement.AddType(typeof(A));
var handler = FuzzyDictOperator.CreateFromType(typeof(A));
var handler = FuzzyDictOperator.CreateFromString("A");
var handler = FuzzyDictOperator<A>.Create();



//LinkOperator : 链式风格的操作，默认使用的是精确最小权方法构建属性/字段索引。
CallerManagement.AddType(typeof(A));
var handler = LinkOperator.CreateFromType(typeof(A));
var handler = LinkOperator.CreateFromString("A");
var handler = LinkOperator<A>.Create();



 
//HashLinkOperator : 链式风格的操作，默认使用的哈希二分查找方法构建属性/字段索引。
CallerManagement.AddType(typeof(A));
var handler = HashLinkOperator.CreateFromType(typeof(A));
var handler = HashLinkOperator.CreateFromString("A");
var handler = HashLinkOperator<A>.Create();



//FuzzyLinkOperator : 链式风格的操作，默认使用的模糊指针查找方法构建属性/字段索引。
CallerManagement.AddType(typeof(A));
var handler = FuzzyLinkOperator.CreateFromType(typeof(A));
var handler = FuzzyLinkOperator.CreateFromString("A");
var handler = FuzzyLinkOperator<A>.Create();

```

#### 动态调用普通/静态类:  

```C#

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


//Link调用
CallerManagement.AddType(typeof(A));
var handler = LinkOperator.CreateFromType(typeof(A));

handler.New();

handler["Age"].Set(100);                                          // Set Operator
handler.Set("Age", 100);                                          // Set Operator

Console.WriteLine(handler["Time"].Get<DateTime>());               // Get Operator
Console.WriteLine(handler.Get<DateTime>("Time"));                 // Get Operator

handler.Get("Outter")["Name"].Set("NewName");                     // Link Operator
handler.Get<B>("Outter").Name = "NewName";                        // Link Operator


//字典调用
CallerManagement.AddType(typeof(A));
var handler = DictOperator.CreateFromType(typeof(A));
handler.New();

handler["Age"]= 100;                                          // Set Operator
handler.Set("Age", 100);                                      // Set Operator

Console.WriteLine(handler["Time"]);                           // Get Operator
Console.WriteLine(handler.Get<DateTime>("Time"));             // Get Operator

((B)handler["Outter"]).Name = "NewName";                      // Link Operator
```

<br/>
<br/>  

#### 动态代理类:

```C# 
//可以对 接口/虚方法/抽象方法 进行覆盖及实现。
//缓存采用精确查找树实现，以自实现的类名做Key, 提高查询性能。
ProxyOperator<ITest> builder = new ProxyOperator<ITest>();
builder.OopName("ITestClass");
//builder["MethodName"] = "MethodBody";
builder["SayHello"] = "Console.WriteLine(\"Hello World!\");";
var test = builder.CreateProxy("ITestClass");
test.SayHello();
```

<br/>
<br/>  

## License
[![FOSSA Status](https://app.fossa.io/api/projects/git%2Bgithub.com%2Fnight-moon-studio%2FNCaller.svg?type=large)](https://app.fossa.io/projects/git%2Bgithub.com%2Fnight-moon-studio%2FNCaller?ref=badge_large) 
