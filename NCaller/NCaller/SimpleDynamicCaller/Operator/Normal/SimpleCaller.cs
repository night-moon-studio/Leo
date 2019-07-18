using NCaller.Builder;
using System;
namespace NCaller
{
    public class SimpleCaller
    {
        public static CallerBase Create(Type type)
        {
           return SimpleCallerBuilder.Ctor(type);
        }
    }
    public class SimpleCaller<T>
    {
        public static CallerBase Create()
        {
            return SimpleCallerBuilder<T>.Ctor();
        }
    }
}
