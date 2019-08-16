

<p align="center">
  <span>中文</span> |  
  <a href="https://github.com/night-moon-studio/ncaller/tree/master/lang/english">English</a>
</p>

# NCaller

[![Member project of Night Moon Studio](https://img.shields.io/badge/member%20project%20of-NMS-9e20c9.svg)](https://github.com/night-moon-studio)
[![NuGet Badge](https://buildstats.info/nuget/DotNetCore.Natasha.NCaller?includePreReleases=true)](https://www.nuget.org/packages/DotNetCore.Natasha.NCaller)
 ![GitHub repo size](https://img.shields.io/github/repo-size/night-moon-studio/ncaller.svg)
[![Codecov](https://img.shields.io/codecov/c/github/night-moon-studio/ncaller.svg)](https://codecov.io/gh/night-moon-studio/ncaller)
[![Badge](https://img.shields.io/badge/link-996.icu-red.svg)](https://996.icu/#/zh_CN)
[![GitHub license](https://img.shields.io/github/license/night-moon-studio/ncaller.svg)](https://github.com/night-moon-studio/NCaller/blob/master/LICENSE)


<br/>
  

### 持续构建(CI Build Status)  

| CI Platform | Build Server | Master Build  | Master Test |
|--------- |------------- |---------| --------|
| Travis | Linux/OSX | [![Build status](https://travis-ci.org/night-moon-studio/ncaller.svg?branch=master)](https://travis-ci.org/night-moon-studio/ncaller) | |
| AppVeyor | Windows/Linux |[![Build status](https://ci.appveyor.com/api/projects/status/4qwm7p9dpy7agdoa?svg=true)](https://ci.appveyor.com/project/NMSAzulX/ncaller)|[![Build status](https://img.shields.io/appveyor/tests/NMSAzulX/ncaller.svg)](https://ci.appveyor.com/project/NMSAzulX/ncaller)|
| Azure |  Windows |[![Build Status](https://dev.azure.com/NightMoonStudio/NCaller/_apis/build/status/night-moon-studio.NCaller?branchName=master&jobName=Windows)](https://dev.azure.com/NightMoonStudio/NCaller/_build/latest?definitionId=4&branchName=master)|[![Azure DevOps tests](https://img.shields.io/azure-devops/tests/NightMoonStudio/ncaller/4.svg)](https://dev.azure.com/NightMoonStudio/NCaller/_build/latest?definitionId=4&branchName=master) |
| Azure |  Linux |[![Build Status](https://dev.azure.com/NightMoonStudio/NCaller/_apis/build/status/night-moon-studio.NCaller?branchName=master&jobName=Linux)](https://dev.azure.com/NightMoonStudio/NCaller/_build/latest?definitionId=4&branchName=master)|[![Azure DevOps tests](https://img.shields.io/azure-devops/tests/NightMoonStudio/ncaller/4.svg)](https://dev.azure.com/NightMoonStudio/NCaller/_build/latest?definitionId=4&branchName=master)  | 
| Azure |  Mac |[![Build Status](https://dev.azure.com/NightMoonStudio/NCaller/_apis/build/status/night-moon-studio.NCaller?branchName=master&jobName=macOS)](https://dev.azure.com/NightMoonStudio/NCaller/_build/latest?definitionId=4&branchName=master)|[![Azure DevOps tests](https://img.shields.io/azure-devops/tests/NightMoonStudio/ncaller/4.svg)](https://dev.azure.com/NightMoonStudio/NCaller/_build/latest?definitionId=4&branchName=master) | 

<br/>    

### 项目简介： 

此项目为[Natasha](https://github.com/dotnetcore/Natasha)的衍生项目，通过运行时自动构建高性能操作代理类，为普通类，静态类，动态类，动态类中的动态类，动态生成的静态类等提供了良好的、完备的、高性能的操作，如果反射、dynamic都不能满足高端的需求，可使用本类库，它将是一个不错的选择。  

<br/>    

### 分支介绍： 

 - BTFindTree分支： 此分支为 HASH2-3查找树的实现分支。
 - PointBTFindTree分支：此分支为 小僧 优化思路的 指针2-3查找树分支。

<br/>    


### 发布计划： 
  
 - 2019-08-01 ： 发布v1.0.0.0, 高性能动态调用库。  
 
 <br/>  
 
---------------------  
 <br/>  
 
### 性能展示：  

> 由于benchmark 不能支持最新的roslyn编译测试，因此基准测试放在3.0版本后。  

![性能对比](https://github.com/night-moon-studio/NCaller/blob/master/Image/Perfomance.png)  

 <br/> 

### 使用方法(User Api)：  
 <br/>  
 
#### 首先编辑您的工程文件：

```C#
  <PropertyGroup>
    <OutputType>Exe</OutputType>
    <TargetFramework>netcoreapp2.2</TargetFramework>
    <PreserveCompilationContext>true</PreserveCompilationContext>   <--- 一定要加上这句话
  </PropertyGroup>
```  
<br/>  

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

var handler = LinkOperator.Create(typeof(A));

handler.New();

handler["Age"].Set(100);                                          // Set Operator
handler.Set("Age", 100);                                          // Set Operator

Console.WriteLine(handler["Time"].Get<DateTime>());               // Get Operator
Console.WriteLine(handler.Get<DateTime>("Time"));                 // Get Operator

handler.Get("Outter")["Name"].Set("NewName");                     // Link Operator
handler.Get<B>("Outter").Name = "NewName";                        // Link Operator


//字典调用
var handler = DictOperator.Create(typeof(A));

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
