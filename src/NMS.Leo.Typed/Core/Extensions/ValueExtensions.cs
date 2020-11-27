namespace NMS.Leo.Typed.Core.Extensions
{
    internal static class ValueExtensions
    {
        public static TVal As<TVal>(this object value)
        {
            return (TVal) value;
        }
    }
}