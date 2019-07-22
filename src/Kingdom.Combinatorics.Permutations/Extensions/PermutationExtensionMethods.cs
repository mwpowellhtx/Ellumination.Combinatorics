using System;
using System.Collections.Generic;
using System.Linq;

// ReSharper disable IdentifierTypo
namespace Kingdom.Combinatorics.Permutations
{
    using static Enumerable;

    /// <summary>
    /// Provides a helpful set of Permutation Extension Methods.
    /// </summary>
    /// <see cref="!:https://en.wikipedia.org/wiki/Heap%27s_algorithm">Heap&apos; algorithm</see>
    /// <see cref="!:https://en.wikipedia.org/wiki/Permutation">Permutation</see>
    /// <see cref="!:https://www.geeksforgeeks.org/heaps-algorithm-for-generating-permutations/">Heap&apos;s Algorithm for generating permutations</see>
    /// <see cref="!:https://www.mrexcel.com/forum/excel-questions/959919-permutations-heaps-algorithm.html">Permutation Heap&apos;s Algorithm</see>
    /// <see cref="!:https://ruslanledesma.com/2016/06/17/why-does-heap-work.html">Why does Heap&apos;s algorithm work</see>
    /// <see cref="!:https://www.zrzahid.com/k-th-permutation-sequence/">K-th permutation sequence</see>
    /// <see cref="!:https://rangerway.com/way/algorithm-permutation-combination-subset">Algorithm (Permutation Combination Subset)</see>
    public static class PermutationExtensionMethods
    {
        /// <summary>
        /// Returns the Range of <paramref name="values"/> as a fresh <see cref="IEnumerable{T}"/>
        /// instance.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="values"></param>
        /// <returns></returns>
        private static IEnumerable<T> GetRange<T>(params T[] values)
        {
            // ReSharper disable once LoopCanBeConvertedToQuery
            foreach (var x in values)
            {
                yield return x;
            }
        }

        internal static IEnumerable<T> GetRangeOrDefault<T>(this IEnumerable<T> range) => range ?? GetRange<T>();

        /// <summary>
        /// Which <see cref="GetHashCode"/> forces
        /// <see cref="Equals(IEnumerable{T}, IEnumerable{T})"/> to be invoked.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <inheritdoc />
        private abstract class ForcedEqualityComparer<T> : IEqualityComparer<IEnumerable<T>>
            where T : IComparable<T>
        {
            public abstract bool Equals(IEnumerable<T> x, IEnumerable<T> y);

            public int GetHashCode(IEnumerable<T> obj) => 0;
        }

        /// <inheritdoc />
        private class SequenceEqualityComparer<T> : ForcedEqualityComparer<T>
            where T : IComparable<T>
        {
            /// <summary>
            /// Gets a new instance of the Comparer.
            /// </summary>
            internal static SequenceEqualityComparer<T> Comparer => new SequenceEqualityComparer<T>();

            /// <summary>
            /// Private Constructor.
            /// </summary>
            private SequenceEqualityComparer()
            {
            }

            /// <summary>
            /// 0
            /// </summary>
            private const int Eq = 0;

            // ReSharper disable PossibleMultipleEnumeration
            private static bool PrivateEquals(IEnumerable<T> x, IEnumerable<T> y)
                => x.Count() == y.Count()
                   && x.Zip(y, (first, second) => first.CompareTo(second)).All(z => z == Eq);

            /// <inheritdoc />
            /// <remarks>Which also precludes any Null issues.</remarks>
            public override bool Equals(IEnumerable<T> x, IEnumerable<T> y)
                => PrivateEquals(x.GetRangeOrDefault(), y.GetRangeOrDefault());
        }

        /// <summary>
        /// Swaps <paramref name="a"/> with <paramref name="b"/>.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="a"></param>
        /// <param name="b"></param>
        private static void Swap<T>(ref T a, ref T b)
        {
            var x = a;
            a = b;
            b = x;
        }

        /// <summary>
        /// Obtains the Permutations iteratively, using an Index Swapping algorithm instead of
        /// enumerating the <paramref name="values"/> themselves. Loosely based on the references
        /// algorithm, with a few minor adjustments taking into account the nPr issue, as well as
        /// doing a bit of bookkeeping to track permuted results forwards as well as backwards.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="values"></param>
        /// <param name="r"></param>
        /// <returns></returns>
        /// <see cref="IComparable{T}">Requires for <typeparamref name="T"/> to be
        /// <see cref="IComparable{T}"/> in order for bookkeeping to track properly.</see>
        /// <see cref="!:https://stackoverflow.com/questions/2390954/how-would-you-calculate-all-possible-permutations-of-0-through-n-iteratively/57137858"/>
        public static IEnumerable<IEnumerable<T>> Permute<T>(this IEnumerable<T> values, int? r = null)
            where T : IComparable<T>
        {
            // We just want this slice off the front, and off the reversed tail, that is all.
            var rActual = r ?? values.Count();
            var indices = Range(0, values.Count()).ToArray();

            // ReSharper disable once RedundantEmptyObjectOrCollectionInitializer
            var permuted = new List<IEnumerable<T>> { };
            var comparer = SequenceEqualityComparer<T>.Comparer;

            // TODO: TBD: sprinkle in consideration as to whether what was already permuted, etc.
            // TODO: TBD: and does anything need to be reversed, etc?
            IEnumerable<IEnumerable<T>> EnumerateCurrent()
            {
                {
                    var candidate = indices.Take(rActual).Select(values.ElementAt).ToArray();
                    if (!permuted.Contains(candidate, comparer))
                    {
                        permuted.Add(candidate);
                        yield return candidate;
                    }
                }

                // Bypass the tail when we have the breadth of the Indices.
                if (rActual != indices.Length)
                {
                    var candidate = indices.Reverse().Take(rActual).Select(values.ElementAt).ToArray();
                    if (!permuted.Contains(candidate, comparer))
                    {
                        permuted.Add(candidate);
                        yield return candidate;
                    }
                }
            }

            foreach (var x in EnumerateCurrent())
            {
                yield return x;
            }

            var weights = new int[values.Count()];
            var upper = 1;
            while (upper < values.Count())
            {
                if (weights[upper] < upper)
                {
                    var lower = upper % 2 * weights[upper];
                    Swap(ref indices[lower], ref indices[upper]);

                    foreach (var x in EnumerateCurrent())
                    {
                        yield return x;
                    }

                    weights[upper]++;
                    upper = 1;
                }
                else
                {
                    weights[upper] = 0;
                    upper++;
                }
            }
        }
        // ReSharper restore PossibleMultipleEnumeration
    }
}
