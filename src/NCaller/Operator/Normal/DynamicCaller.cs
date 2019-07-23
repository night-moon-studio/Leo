using NCaller.Builder;
using System;


namespace NCaller
{

    public class DynamicCaller
    {
        public static CallerBase Create(Type type) => CallerBuilder.Ctor(type);
    }




    public class DynamicCaller<T>
    {
        public static CallerBase Create() => CallerBuilder<T>.Ctor();
    }

}
