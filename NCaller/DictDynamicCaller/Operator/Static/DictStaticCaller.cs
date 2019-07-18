using NCaller.Builder;
using System;
namespace NCaller
{
    public class DictStaticCaller
    {
        public static CallerBase Create(Type type)
        {
           return DictStaticCallerBuilder.Ctor(type);
        }
    }
}
