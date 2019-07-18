

<p align="center">
  <span>中文</span> |  
  <a href="https://github.com/dotnetcore/natasha/tree/master/lang/english">English</a>
</p>

# NCaller

[![Member project of Night Moon Studio](https://img.shields.io/badge/member%20project%20of-NMS-9e20c9.svg)](https://github.com/night-moon-studio)
[![NuGet Badge](https://buildstats.info/nuget/DotNetCore.Natasha?includePreReleases=true)](https://www.nuget.org/packages/DotNetCore.Natasha)
[![Gitter](https://badges.gitter.im/dotnetcore/natasha.svg)](https://gitter.im/dotnetcore/Natasha?utm_source=badge&utm_medium=badge&utm_campaign=pr-badge)
[![Badge](https://img.shields.io/badge/link-996.icu-red.svg)](https://996.icu/#/zh_CN)
[![GitHub license](https://img.shields.io/github/license/dotnetcore/natasha.svg)](https://github.com/dotnetcore/Natasha/blob/master/LICENSE)

&ensp;&ensp;&ensp;&ensp;使用Natasha构建快速动态调用方法，如果您觉得反射太low, dynamic性能还不够，对运行时生成的类不知从何调用，那么欢迎您使用本类库。

<br/>

### 类库信息(Library Info)  

[![GitHub tag (latest SemVer)](https://img.shields.io/github/tag/dotnetcore/natasha.svg)](https://github.com/dotnetcore/Natasha/releases) ![GitHub repo size](https://img.shields.io/github/repo-size/dotnetcore/Natasha.svg) [![GitHub commit activity](https://img.shields.io/github/commit-activity/m/dotnetcore/natasha.svg)](https://github.com/dotnetcore/Natasha/commits/master) [![Codecov](https://img.shields.io/codecov/c/github/dotnetcore/natasha.svg)](https://codecov.io/gh/dotnetcore/Natasha) [![wiki](https://img.shields.io/badge/wiki-ch-blue.svg)](https://github.com/dotnetcore/Natasha/wiki) 
  

<br/>  

### 持续构建(CI Build Status)  

| CI Platform | Build Server | Master Build  | Master Test |
|--------- |------------- |---------| --------|
| Travis | Linux/OSX | [![Build status](https://travis-ci.org/dotnetcore/Natasha.svg?branch=master)](https://travis-ci.org/dotnetcore/Natasha) | |
| AppVeyor | Windows/Linux |[![Build status](https://ci.appveyor.com/api/projects/status/5ydt5yvb9lwfqocw?svg=true)](https://ci.appveyor.com/project/NMSAzulX/natasha)|[![Build status](https://img.shields.io/appveyor/tests/NMSAzulX/Natasha.svg)](https://ci.appveyor.com/project/NMSAzulX/natasha)|
| Azure |  Windows |[![Build Status](https://dev.azure.com/NightMoonStudio/Natasha/_apis/build/status/dotnetcore.Natasha?branchName=master&jobName=Windows)](https://dev.azure.com/NightMoonStudio/Natasha/_build/latest?definitionId=3&branchName=master)|[![Build Status](https://img.shields.io/azure-devops/tests/NightMoonStudio/Natasha/3/master.svg)](https://dev.azure.com/NightMoonStudio/Natasha/_build/latest?definitionId=3&branchName=master) |
| Azure |  Linux |[![Build Status](https://dev.azure.com/NightMoonStudio/Natasha/_apis/build/status/dotnetcore.Natasha?branchName=master&jobName=Linux)](https://dev.azure.com/NightMoonStudio/Natasha/_build/latest?definitionId=3&branchName=master)|[![Build Status](https://img.shields.io/azure-devops/tests/NightMoonStudio/Natasha/3/master.svg)](https://dev.azure.com/NightMoonStudio/Natasha/_build/latest?definitionId=3&branchName=master) | 
| Azure |  Mac |[![Build Status](https://dev.azure.com/NightMoonStudio/Natasha/_apis/build/status/dotnetcore.Natasha?branchName=master&jobName=macOS)](https://dev.azure.com/NightMoonStudio/Natasha/_build/latest?definitionId=3&branchName=master)|[![Build Status](https://img.shields.io/azure-devops/tests/NightMoonStudio/Natasha/3/master.svg)](https://dev.azure.com/NightMoonStudio/Natasha/_build/latest?definitionId=3&branchName=master) | 

<br/>    


### 发布计划(Publish Plan)  
  
 - 2019-08-01 ： 发布v1.0.0.0, 高性能动态调用库。  
 
 <br/>  
 
---------------------  
 <br/>  
 

### 使用方法(User Api)：  
 <br/>  
 
#### 动态调用普通类:  

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

//如果是运行时动态生成类，也同样


//调用方式

var handler EntityOperator.Create(typeof(A));

handler.New();

handler.Set("Age",100);                                           // Set Operator

Console.WriteLine(handler.Get<DateTime>("Time"));                 // Get Operator

handler.Get("Outter")["Name"].Set("NewName");                     // Link Operator
```
<br/>
<br/>  

#### 动态调用静态类:  

```C#

public static class A
{
   public static int Age;
   public static DateTime Time;
   public static B Outter = new B();
}

public class B
{
   public string Name;
   public B()
   {
      Name = "小明";
   }
}

//如果是运行时动态生成类，也同样


//调用方式

var handler = StaticEntityOperator.Create(type);

handler["Age"].Set(100);                                          // Set Operator

Console.WriteLine(handler["Time"].Get<DateTime>());               // Get Operator

handler.Get("Outter").Set(Name,"NewName");                        // Link Operator

```
<br/>
<br/>  
