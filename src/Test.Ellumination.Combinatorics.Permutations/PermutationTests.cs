﻿using System.Collections.Generic;
using System.Linq;

// ReSharper disable IdentifierTypo
namespace Ellumination.Combinatorics.Permutations
{
    using Xunit;
    using Xunit.Abstractions;

    public class PermutationTests : TestFixtureBase
    {
        public PermutationTests(ITestOutputHelper outputHelper)
            : base(outputHelper)
        {
        }

        // ReSharper disable PossibleMultipleEnumeration
        /// <summary>
        /// Reduction to Distinct Sets of Permutations is beyond the scope of what we are doing
        /// here. We want to verify the full range of Permutations, forwards and backwards.
        /// </summary>
        /// <param name="values"></param>
        /// <param name="r"></param>
        /// <param name="expected"></param>
        [Theory, ClassData(typeof(PermutationTestCases))]
        public void Verify_Permutations(IEnumerable<char> values, int? r, IEnumerable<IEnumerable<char>> expected)
        {
            // Sketch in some Factorial calculations.
            int Factorial(int x)
            {
                var result = 1;

                while (x-- > 0)
                {
                    result *= (x + 1);
                }

                return result;
            }

            int CalculateCount(int count, int rActual) => Factorial(count) / Factorial(count - rActual);

            int CalculateTotalCount(int count, int? rActual)
            {
                if (rActual != null)
                {
                    return CalculateCount(count, rActual.Value);
                }

                var total = 0;
                rActual = count;
                while (rActual > 0)
                {
                    total += CalculateCount(count, rActual.Value);
                    rActual--;
                }

                return total;
            }

            // Just work with a simple String Rendering for purposes of Ordering.
            string Render(IEnumerable<char> parts) => parts.Aggregate("", (g, x) => $"{g}{x}");

            // Thereby ensuring that the bits themselves are in the correct order from here on.
            values = values.AssertNotNull().AssertCollectionNotEmpty();
            expected = expected.AssertNotNull().AssertCollectionNotEmpty()
                .AssertEqual(CalculateCount(
                    values.Count(), r ?? values.Count()), x => x.Count())
                .OrderBy(Render).ToArray();

            //var comparer = PermutationComparer.Instance.AssertNotNull();
            var permutations = values.Permute(r).AssertNotNull().AssertCollectionNotEmpty().OrderBy(Render).ToArray();

            permutations.AssertEqual(expected.Count(), x => x.Length)
                .Zip(expected, (a, e) => new {a, e})
                .ToList().ForEach(zipped => zipped.a.AssertEqual(zipped.e));
        }
        // ReSharper restore PossibleMultipleEnumeration
    }
}
