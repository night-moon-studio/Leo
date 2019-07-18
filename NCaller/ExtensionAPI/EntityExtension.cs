namespace NCaller
{
    public static class EntityExtension
    {
        public static CallerBase<T> Caller<T>(this T value)
        {
            var caller = (CallerBase<T>)SimpleCaller<T>.Create();
            caller.SetInstance(value);
            return caller;
        }
    }
}
