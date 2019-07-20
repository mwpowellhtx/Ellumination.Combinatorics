// ReSharper disable IdentifierTypo

using System.Collections.Generic;

namespace Kingdom.Combinatorics.Permutations
{
    using Xunit.Abstractions;

    public abstract class TestFixtureBase
    {
        /// <summary>
        /// Gets the OutputHelper.
        /// </summary>
        protected ITestOutputHelper OutputHelper { get; }

        protected TestFixtureBase(ITestOutputHelper outputHelper)
        {
            OutputHelper = outputHelper;
        }

        /// <summary>
        /// Returns the <see cref="IEnumerable{T}"/> Range corresponding with the
        /// <paramref name="values"/>.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="values"></param>
        /// <returns></returns>
        protected static IEnumerable<T> GetRange<T>(params T[] values)
        {
            // ReSharper disable once LoopCanBeConvertedToQuery
            foreach (var x in values)
            {
                yield return x;
            }
        }
    }
}
