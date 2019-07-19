using NCaller.Builder;
using System;
namespace NCaller
{
    public class DynamicCaller
    {
        public static CallerBase Create(Type type)
        {
           return CallerBuilder.Ctor(type);
        }
    }
    public class DynamicCaller<T>
    {
        public static CallerBase Create()
        {
            return CallerBuilder<T>.Ctor();
        }
    }
}
