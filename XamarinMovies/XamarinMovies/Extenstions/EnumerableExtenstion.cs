using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace System.Linq
{
    public static class EnumerableExtenstions
    {
        public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
        {
            foreach (var item in source)
            {
                action(item);
            }
        }
    }
}