using System;

namespace NCaller.ExtensionAPI
{
    internal static class ListExtension
    {
        internal static void For<T>(this T[] list,Action<T> action)
        {
            for (int i = 0; i < list.Length; i+=1)
            {
                action(list[i]);
            }
        }
    }
}
