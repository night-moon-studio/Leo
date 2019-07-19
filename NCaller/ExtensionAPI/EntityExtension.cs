namespace NCaller
{
    public static class EntityExtension
    {
        public static CallerBase<T> Caller<T>(this T value)
        {
            var caller = (CallerBase<T>)DynamicCaller<T>.Create();
            caller.SetInstance(value);
            return caller;
        }       
    }
}
