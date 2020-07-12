using System;
using System.Collections.Generic;

// ReSharper disable IdentifierTypo
namespace Ellumination.Combinatorics.Combinatorials
{
    internal static class CollectionExtensionMethods
    {
        /// <summary>
        /// Provides the For Each algorithm for <see cref="IEnumerable{T}"/>.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="values"></param>
        /// <param name="action"></param>
        /// <remarks>This is a helpful extension method, but w edo not want to necessary pollute
        /// subscriber namespaces with extraneous helper methods.</remarks>
        public static void ForEach<T>(this IEnumerable<T> values, Action<T> action)
        {
            foreach (var x in values)
            {
                action?.Invoke(x);
            }
        }
    }
}
