using System.Collections.Generic;
using System.Linq;

// ReSharper disable IdentifierTypo
namespace Kingdom.Combinatorics.Permutations
{
    using Xunit;
    using Xunit.Abstractions;
    using Xunit.Theoretically;
    using static Characters;

    public class PermutationTestCases : TestCasesBase
    {
        private static IEnumerable<object[]> _privateCases;

        private static IEnumerable<object[]> PrivateCases
        {
            get
            {
                IEnumerable<object[]> GetAll()
                {
                    yield return GetRange<object>(
                        GetRange(a, b, c).ToArray(), (int?) null, GetRange(
                            GetRange(a, b, c), GetRange(a, c, b)
                            , GetRange(b, a, c), GetRange(b, c, a)
                            , GetRange(c, a, b), GetRange(c, b, a)
                        ).ToArray()
                    ).ToArray();

                    yield return GetRange<object>(
                        GetRange(a, b, c), (int?) 2, GetRange(
                            GetRange(a, b), GetRange(a, c)
                            , GetRange(b, a), GetRange(b, c)
                            , GetRange(c, a), GetRange(c, b)
                        ).ToArray()
                    ).ToArray();
                }

                return _privateCases ?? (_privateCases = GetAll().ToArray());
            }
        }

        protected override IEnumerable<object[]> Cases => PrivateCases;
    }
}
