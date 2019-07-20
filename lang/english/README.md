

<p align="center">
  <a href="https://github.com/night-moon-studio/ncaller">中文</a> |  <span>English</span>
  
</p>

# NCaller

[![Member project of Night Moon Studio](https://img.shields.io/badge/member%20project%20of-NMS-9e20c9.svg)](https://github.com/night-moon-studio)
[![NuGet Badge](https://buildstats.info/nuget/DotNetCore.Natasha?includePreReleases=true)](https://www.nuget.org/packages/DotNetCore.Natasha)
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

### Project profile：
This project is a derivative of [Natasha] (https://github.com/dotnetcore/Natasha). Through the automatic construction of high-performance operation proxy classes at run time, it provides good, complete and high-performance operations for ordinary classes, static classes, dynamic classes in dynamic classes, dynamically generated static classes, etc. If reflection, dynamic can not meet your needs, you can use this class library, it will be a good choice.  


<br/>    

### Publish Plan  
  
 - 2019-08-01 ： v1.0.0.0, Stable Version。  
 
 <br/>  
 
---------------------  
 <br/>  
 
### Performance Display：  

> Because benchmark does not support the latest roslyn compilation tests, benchmark tests are placed after version 3.0.  

![Performance Comparison](https://github.com/night-moon-studio/NCaller/blob/master/Image/Perfomance.png)  

 <br/> 

### User Api：  
 <br/>  
 
#### Dynamic call to normal classes:  

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
      Name = "XXX"
   }
}



//CASE

var handler = DynamicCaller.Create(typeof(A));

handler.New();

handler.Set("Age",100);                                           // Set Operator

Console.WriteLine(handler.Get<DateTime>("Time"));                 // Get Operator

handler.Get("Outter")["Name"].Set("NewName");                     // Link Operator
```
<br/>
<br/>  

#### Dynamic call to static class:  

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
      Name = "XXX";
   }
}



//CASE

var handler = StaticDynamicCaller.Create(type);

handler["Age"].Set(100);                                          // Set Operator

Console.WriteLine(handler["Time"].Get<DateTime>());               // Get Operator

handler.Get("Outter").Set(Name,"NewName");                        // Link Operator

```
<br/>
<br/>  
