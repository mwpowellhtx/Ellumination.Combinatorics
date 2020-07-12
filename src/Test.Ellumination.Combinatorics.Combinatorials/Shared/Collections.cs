using System.Collections.Generic;

// ReSharper disable IdentifierTypo
namespace Ellumination.Combinatorics.Combinatorials
{
    internal static class Collections
    {
        public static IEnumerable<T> GetRange<T>(params T[] values)
        {
            // ReSharper disable once LoopCanBeConvertedToQuery
            foreach (var value in values)
            {
                yield return value;
            }
        }

        public static IEnumerable<object> GetObjectRange(params object[] values)
        {
            // ReSharper disable once LoopCanBeConvertedToQuery
            foreach (var value in values)
            {
                yield return value;
            }
        }
    }
}
