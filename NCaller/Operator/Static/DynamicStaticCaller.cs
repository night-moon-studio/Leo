using NCaller.Builder;
using System;
namespace NCaller
{
    public class DynamicStaticCaller
    {
        public static CallerBase Create(Type type)
        {
           return StaticCallerBuilder.Ctor(type);
        }
    }
}
