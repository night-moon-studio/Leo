# NCaller
使用Natasha构建快速动态调用方法

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


//调用方式一
var handler = DynamicOperator.GetOperator(typeof(A));

handler["Age"].IntValue = 100;                                    // Set Operator

Console.WriteLine(handler["Time"].DateTime);                      // Get Operator

handler["Outter"].OperatorValue["Name"].StringValue = "NewName"   // Link Operator



//调用方式二

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


//调用方式一

DynamicStaticOperator handler = typeof(A);

handler["Age"].IntValue = 100;                                        // Set Operator

Console.WriteLine(handler["Time"].DateTime);                          // Get Operator

handler.Get["Outter"].OperatorValue["Name"].StringValue = "NewName"   // Link Operator


//调用方式二

var handler = StaticEntityOperator.Create(type);

handler["Age"].Set(100);                                          // Set Operator

Console.WriteLine(handler["Time"].Get<DateTime>());               // Get Operator

handler.Get("Outter").Set(Name,"NewName");                        // Link Operator

```
<br/>
<br/>  
