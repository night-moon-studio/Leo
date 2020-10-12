

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
 - 2020-10-12 ： 发布v1.2.0.0, 使用最新版 Natasha 和 最新版快速缓存；使用函数指针代替系统委托。  
 
 <br/>  
 
---------------------  
 <br/>  
 
### 性能展示：  

![性能对比](https://github.com/night-moon-studio/NCaller/blob/master/Image/%E6%80%A7%E8%83%BD%E6%B5%8B%E8%AF%951.png)  

 <br/> 

#### 使用方法(User Api)：  

 <br/>  
 
 - 引入 动态构件库： NMS.NCaller

 - Natasha 初始化

  ```C#
  //仅仅注册组件
  NatashaInitializer.Initialize();
  //注册组件+预热组件 , 之后编译会更加快速
  await NatashaInitializer.InitializeAndPreheating();
  ```

 - 敲代码  
 
<br/>  

#### 算法引用

https://github.com/dotnet-lab/BTFindTree


<br/>
  

#### 选用操作类：

```C#

//DictOperator : 字典风格的操作，默认使用的是精确最小权方法构建属性/字段索引。
var handler = PrecisionDictOperator.CreateFromType(typeof(A));
var handler = PrecisionDictOperator<A>.Create();


 
//HashDictOperator : 字典风格的操作，默认使用的哈希二分查找方法构建属性/字段索引。
var handler = HashDictOperator.CreateFromType(typeof(A));
var handler = HashDictOperator<A>.Create();



//FuzzyDictOperator : 字典风格的操作，默认使用的模糊指针查找方法构建属性/字段索引。
var handler = FuzzyDictOperator.CreateFromType(typeof(A));
var handler = FuzzyDictOperator<A>.Create();


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


//字典调用
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

## License
[![FOSSA Status](https://app.fossa.io/api/projects/git%2Bgithub.com%2Fnight-moon-studio%2FNCaller.svg?type=large)](https://app.fossa.io/projects/git%2Bgithub.com%2Fnight-moon-studio%2FNCaller?ref=badge_large) 
