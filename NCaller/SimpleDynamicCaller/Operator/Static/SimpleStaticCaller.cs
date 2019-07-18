using NCaller.Builder;
using System;
namespace NCaller
{
    public class SimpleStaticCaller
    {
        public static CallerBase Create(Type type)
        {
           return SimpleStaticCallerBuilder.Ctor(type);
        }
    }
}
