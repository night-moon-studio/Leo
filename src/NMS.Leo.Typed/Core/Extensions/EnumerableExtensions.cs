using System.Collections.Generic;
using System.Linq;

namespace NMS.Leo.Typed.Core.Extensions
{
    internal static class EnumerableExtensions
    {
        public static bool ContainsAny<T>(this IEnumerable<T> source, IEnumerable<T> other)
        {
            return source.Any(other.Contains);
        }
    }
}