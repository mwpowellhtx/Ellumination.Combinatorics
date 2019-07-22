using System.Collections.Generic;
using System.Linq;

// ReSharper disable IdentifierTypo
namespace Kingdom.Combinatorics.Permutations
{
    using Xunit.Theoretically;
    using static Characters;

    public class PermutationTestCases : TestCasesBase
    {
        private static IEnumerable<object[]> _privateCases;

        /// <inheritdoc />
        /// <see cref="!:https://www.calculatorsoup.com/calculators/discretemathematics/permutations.php"/>
        protected override IEnumerable<object[]> Cases
        {
            get
            {
                /* Four elements is about the limit of anything I would test manually,
                 * and then I had a little `help´ organizing in the form of an OpenOffice Spread Sheet.*/
                IEnumerable<object[]> GetAll()
                {
                    // Expecting n elements taken r at a time: n! / (n - r)! for n=4, r=[4,3,2,1], that is 24+24+12+4 = 64.
                    yield return GetRange<object>(
                        GetRange(a, b, c, d).ToArray(), (int?) null, GetRange(
                            GetRange(a, b, c, d), GetRange(a, b, d, c)
                            , GetRange(a, c, b, d), GetRange(a, c, d, b)
                            , GetRange(a, d, b, c), GetRange(a, d, c, b)
                            , GetRange(b, a, c, d), GetRange(b, a, d, c)
                            , GetRange(b, c, a, d), GetRange(b, c, d, a)
                            , GetRange(b, d, a, c), GetRange(b, d, c, a)
                            , GetRange(c, a, b, d), GetRange(c, a, d, b)
                            , GetRange(c, b, a, d), GetRange(c, b, d, a)
                            , GetRange(c, d, a, b), GetRange(c, d, b, a)
                            , GetRange(d, a, b, c), GetRange(d, a, c, b)
                            , GetRange(d, b, a, c), GetRange(d, b, c, a)
                            , GetRange(d, c, a, b), GetRange(d, c, b, a)
                            , GetRange(a, b, c), GetRange(a, c, b)
                            , GetRange(b, a, c), GetRange(b, c, a)
                            , GetRange(c, b, a), GetRange(c, a, b)
                            , GetRange(a, b, d), GetRange(a, d, b)
                            , GetRange(b, a, d), GetRange(b, d, a)
                            , GetRange(d, b, a), GetRange(d, a, b)
                            , GetRange(a, c, d), GetRange(a, d, c)
                            , GetRange(c, a, d), GetRange(c, d, a)
                            , GetRange(d, c, a), GetRange(d, a, c)
                            , GetRange(b, c, d), GetRange(b, d, c)
                            , GetRange(c, b, d), GetRange(c, d, b)
                            , GetRange(d, c, b), GetRange(d, b, c)
                            , GetRange(a, b), GetRange(b, a)
                            , GetRange(a, c), GetRange(c, a)
                            , GetRange(a, d), GetRange(d, a)
                            , GetRange(b, c), GetRange(c, b)
                            , GetRange(b, d), GetRange(d, b)
                            , GetRange(c, d), GetRange(d, c)
                            , GetRange(a), GetRange(b), GetRange(c), GetRange(d)
                        ).ToArray()
                    ).ToArray();

                    // And derivative cases...
                    yield return GetRange<object>(
                        GetRange(a, b, c, d).ToArray(), (int?) 4, GetRange(
                            GetRange(a, b, c, d), GetRange(a, b, d, c)
                            , GetRange(a, c, b, d), GetRange(a, c, d, b)
                            , GetRange(a, d, b, c), GetRange(a, d, c, b)
                            , GetRange(b, a, c, d), GetRange(b, a, d, c)
                            , GetRange(b, c, a, d), GetRange(b, c, d, a)
                            , GetRange(b, d, a, c), GetRange(b, d, c, a)
                            , GetRange(c, a, b, d), GetRange(c, a, d, b)
                            , GetRange(c, b, a, d), GetRange(c, b, d, a)
                            , GetRange(c, d, a, b), GetRange(c, d, b, a)
                            , GetRange(d, a, b, c), GetRange(d, a, c, b)
                            , GetRange(d, b, a, c), GetRange(d, b, c, a)
                            , GetRange(d, c, a, b), GetRange(d, c, b, a)
                        ).ToArray()
                    ).ToArray();

                    yield return GetRange<object>(
                        GetRange(a, b, c, d).ToArray(), (int?) 3, GetRange(
                            GetRange(a, b, c), GetRange(a, c, b)
                            , GetRange(b, a, c), GetRange(b, c, a)
                            , GetRange(c, b, a), GetRange(c, a, b)
                            , GetRange(a, b, d), GetRange(a, d, b)
                            , GetRange(b, a, d), GetRange(b, d, a)
                            , GetRange(d, b, a), GetRange(d, a, b)
                            , GetRange(a, c, d), GetRange(a, d, c)
                            , GetRange(c, a, d), GetRange(c, d, a)
                            , GetRange(d, c, a), GetRange(d, a, c)
                            , GetRange(b, c, d), GetRange(b, d, c)
                            , GetRange(c, b, d), GetRange(c, d, b)
                            , GetRange(d, c, b), GetRange(d, b, c)
                        ).ToArray()
                    ).ToArray();

                    yield return GetRange<object>(
                        GetRange(a, b, c, d).ToArray(), (int?) 2, GetRange(
                            GetRange(a, b), GetRange(b, a)
                            , GetRange(a, c), GetRange(c, a)
                            , GetRange(a, d), GetRange(d, a)
                            , GetRange(b, c), GetRange(c, b)
                            , GetRange(b, d), GetRange(d, b)
                            , GetRange(c, d), GetRange(d, c)
                        ).ToArray()
                    ).ToArray();

                    // Expecting n elements taken r at a time: n! / (n - r)! for n=3, r=[3,2,1], that is 6+6+3 = 15.
                    yield return GetRange<object>(
                        GetRange(a, b, c).ToArray(), (int?) null, GetRange(
                            GetRange(a, b, c), GetRange(a, c, b)
                            , GetRange(b, a, c), GetRange(b, c, a)
                            , GetRange(c, a, b), GetRange(c, b, a)
                            , GetRange(a, b), GetRange(a, c)
                            , GetRange(b, a), GetRange(b, c)
                            , GetRange(c, a), GetRange(c, b)
                            , GetRange(a), GetRange(b), GetRange(c)
                        ).ToArray()
                    ).ToArray();

                    // And derivative cases...
                    yield return GetRange<object>(
                        GetRange(a, b, c).ToArray(), (int?) 3, GetRange(
                            GetRange(a, b, c), GetRange(a, c, b)
                            , GetRange(b, a, c), GetRange(b, c, a)
                            , GetRange(c, a, b), GetRange(c, b, a)
                        ).ToArray()
                    ).ToArray();

                    // Here we just want for r=2, 3! / (3 - 2)! = 6.
                    yield return GetRange<object>(
                        GetRange(a, b, c).ToArray(), (int?) 2, GetRange(
                            GetRange(a, b), GetRange(a, c)
                            , GetRange(b, a), GetRange(b, c)
                            , GetRange(c, a), GetRange(c, b)
                        ).ToArray()
                    ).ToArray();
                }

                return _privateCases ?? (_privateCases = GetAll().ToArray());
            }
        }
    }
}
