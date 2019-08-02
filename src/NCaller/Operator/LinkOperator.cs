using NCaller.Builder;
using System;


namespace NCaller
{

    public class LinkOperator
    {
        public static LinkBase Create(Type type) => LinkBuilder.Ctor(type);
    }




    public class LinkOperator<T>
    {
        public static LinkBase Create() => LinkBuilder<T>.Ctor();
    }

}
