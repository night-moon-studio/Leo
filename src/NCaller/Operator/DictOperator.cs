using NCaller.Builder;
using System;


namespace NCaller
{

    public class DictOperator
    {
        public static DictBase Create(Type type) => DictBuilder.Ctor(type);
    }




    public class DictOperator<T>
    {
        public static DictBase Create() => DictBuilder<T>.Ctor();
    }

}
