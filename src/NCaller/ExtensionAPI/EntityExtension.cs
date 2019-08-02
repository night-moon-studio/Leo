using NCaller.Builder;


namespace NCaller
{

    public static class EntityExtension
    {

        public static LinkBase<T> LinkCaller<T>(this T value)
        {

            var caller = (LinkBase<T>)LinkBuilder<T>.Ctor();
            caller.SetInstance(value);


            return caller;

        }



        public static DictBase<T> DictCaller<T>(this T value)
        {

            var caller = (DictBase<T>)DictBuilder<T>.Ctor();
            caller.SetInstance(value);


            return caller;

        }

    }

}
