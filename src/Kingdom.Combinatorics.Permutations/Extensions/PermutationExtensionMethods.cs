using System;
using System.Collections.Generic;
using System.Linq;

// ReSharper disable IdentifierTypo
namespace Kingdom.Combinatorics.Permutations
{
    using static Math;

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

        // ReSharper disable PossibleMultipleEnumeration
        /// <summary>
        /// Permutes All of the <paramref name="values"/>, gathering the intermediate subsets
        /// during the <paramref name="inner"/> iterations.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="values"></param>
        /// <param name="inner"></param>
        /// <returns></returns>
        private static IEnumerable<IEnumerable<T>> PermuteAll<T>(this IEnumerable<T> values, ICollection<IEnumerable<T>> inner, int? r)
        {
            IEnumerable<T> AppendCandidate(IEnumerable<T> candidate)
            {
                if (candidate.Count() == (r ?? 0))
                {
                    inner.Add(candidate);
                }

                return candidate;
            }

            var count = values.Count();

            if (count == 1)
            {
                yield return AppendCandidate(GetRange(values.Single()));
            }

            for (var i = 0; i < count; i++)
            {
                var x = values.ElementAt(i);

                // Permute everything around the Element At the Index.
                var available = values.Where((_, j) => j != i).PermuteAll(inner, r).ToArray();

                foreach (var permutation in available)
                {
                    // Sprinkle in the Current Element with the surrounding Available Permutations.
                    yield return AppendCandidate(GetRange(x).Concat(permutation));
                }
            }
        }

        /// <summary>
        /// Returns the Permutations of <paramref name="values"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="values">Provide the Values to be Permuted.</param>
        /// <param name="r">Can be taken R at a time. Leave this unspecified, or Null, in order
        /// to receive the Permutations aligned with the <paramref name="values"/> width.</param>
        /// <returns>A Range of Permuted <paramref name="values"/>.</returns>
        public static IEnumerable<IEnumerable<T>> Permute<T>(this IEnumerable<T> values, int? r = null)
        {
            IEnumerable<IEnumerable<T>> GetOuter(out ICollection<IEnumerable<T>> x)
            {
                // We always want to compute this one.
                var outer = values.PermuteAll(x = new List<IEnumerable<T>>(), r).ToArray();
                // The further issue is whether we are contending for an R.
                return r == null ? outer : null;
            }

            // ReSharper disable once ImplicitlyCapturedClosure
            IEnumerable<IEnumerable<T>> GetInner(IEnumerable<IEnumerable<T>> x, Func<int> getCount)
            {
                // We land here when we want the R subset of the Permutations.
                var count = getCount();
                var normalized = Max(0, Min(r ?? count, count));
                return x.Where(y => y.Count() == normalized);
            }

            return GetOuter(out var inner) ?? GetInner(inner, values.Count);
        }
        // ReSharper restore PossibleMultipleEnumeration
    }
}
