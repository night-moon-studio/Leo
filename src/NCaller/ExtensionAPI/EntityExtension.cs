using NCaller.Builder;


namespace NCaller
{

    public static class EntityExtension
    {

        public static CallerBase<T> Caller<T>(this T value)
        {

            var caller = (CallerBase<T>)CallerBuilder<T>.Ctor();
            caller.SetInstance(value);


            return caller;

        }

    }

}
