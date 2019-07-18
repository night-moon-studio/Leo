using NCaller.Builder;
using System;
namespace NCaller
{
    public class StaticEntityOperator
    {
        public static CallerBase Create(Type type)
        {
           return SimpleStaticCallerBuilder.Ctor(type);
        }
    }
}
