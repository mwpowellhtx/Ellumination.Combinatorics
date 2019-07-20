using System.Collections.Generic;
using System.Linq;

// ReSharper disable IdentifierTypo
namespace Kingdom.Combinatorics.Permutations
{
    using Xunit;
    using Xunit.Abstractions;

    public class PermutationTests : TestFixtureBase
    {
        public PermutationTests(ITestOutputHelper outputHelper)
            : base(outputHelper)
        {
        }

        /// <inheritdoc />
        private class PermutationComparer : IComparer<IEnumerable<char>>
        {
            /// <summary>
            /// Gets an Instance of the <see cref="IComparer{T}"/>.
            /// </summary>
            internal static PermutationComparer Instance => new PermutationComparer();

            /// <summary>
            /// Private Constructor.
            /// </summary>
            private PermutationComparer()
            {
            }

            /// <summary>
            /// Returns the result after Comparing <paramref name="a"/> to <paramref name="b"/>.
            /// </summary>
            /// <param name="a"></param>
            /// <param name="b"></param>
            /// <returns></returns>
            private static int Compare(char a, char b) => a.CompareTo(b);

            // ReSharper disable PossibleMultipleEnumeration
            /// <inheritdoc />
            public int Compare(IEnumerable<char> x, IEnumerable<char> y)
            {
                const int gt = 1, lt = -1;

                int? EvaluateCollections()
                    => x != null && y == null
                        ? gt
                        : y != null && x == null
                            ? lt
                            : x == null
                                ? gt
                                : x.Count() > y.Count()
                                    ? gt
                                    : x.Count() < y.Count()
                                        ? (int?) lt
                                        : null;

                const int eq = 0;

                int EvaluatePairs()
                {
                    // ReSharper disable AssignNullToNotNullAttribute
                    var comparisons = x.Zip(y, (first, second) => new {first, second})
                        .Select(zipped => (int?) Compare(zipped.first, zipped.second)).ToArray();
                    // ReSharper restore AssignNullToNotNullAttribute

                    return comparisons.FirstOrDefault(comparison => comparison != eq) ?? eq;
                }

                return EvaluateCollections() ?? EvaluatePairs();
            }
            // ReSharper restore PossibleMultipleEnumeration
        }

        // ReSharper disable PossibleMultipleEnumeration
        /// <summary>
        /// 
        /// </summary>
        /// <param name="values"></param>
        /// <param name="r"></param>
        /// <param name="expected"></param>
        [Theory, ClassData(typeof(PermutationTestCases))]
        public void Verify_Permutations(IEnumerable<char> values, int? r, IEnumerable<IEnumerable<char>> expected)
        {
            values.AssertNotNull().AssertNotEmpty();
            var length = r ?? values.Count();

            expected.AssertNotNull().AssertNotEmpty().ToList().ForEach(
                x => x.AssertNotNull().AssertNotEmpty().AssertEqual(length, y => y.Count()));

            var comparer = PermutationComparer.Instance.AssertNotNull();
            var permutations = values.Permute(r).AssertNotNull().AssertNotEmpty().ToArray();

            permutations.AssertEqual(expected.Count(), x => x.Length).OrderBy(x => x, comparer)
                .Zip(expected.OrderBy(y => y, comparer), (a, e) => new {a, e})
                .ToList().ForEach(zipped => zipped.a.AssertEqual(zipped.e));
        }
        // ReSharper restore PossibleMultipleEnumeration
    }
}
