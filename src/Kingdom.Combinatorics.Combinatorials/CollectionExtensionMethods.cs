using System;
using System.Collections.Generic;

// ReSharper disable IdentifierTypo
namespace Kingdom.Combinatorics.Combinatorials
{
    internal static class CollectionExtensionMethods
    {
        /// <summary>
        /// Provides the For Each algorithm for <see cref="IEnumerable{T}"/>.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="values"></param>
        /// <param name="action"></param>
        public static void ForEach<T>(this IEnumerable<T> values, Action<T> action)
        {
            foreach (var x in values)
            {
                action?.Invoke(x);
            }
        }
    }
}
