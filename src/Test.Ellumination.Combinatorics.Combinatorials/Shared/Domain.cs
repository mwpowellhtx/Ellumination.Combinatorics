using System.Collections.Generic;
using System.Linq;

// ReSharper disable IdentifierTypo
namespace Ellumination.Combinatorics.Combinatorials
{
    using static Collections;

    internal static class Domain
    {
        public static readonly IEnumerable<object> ThreeObjects = GetObjectRange(1, 2, 3);

        public static readonly IEnumerable<object> TwoObjects = GetObjectRange(4, 5);

        public static readonly IEnumerable<int> ThreeIntegers = GetRange(1, 2, 3);

        public static readonly IEnumerable<int> TwoIntegers = GetRange(4, 5);

        public static readonly IEnumerable<char> FourCharacters = GetRange('a', 'b', 'c', 'd');

        public static readonly IEnumerable<bool> FiveBooleans = GetRange(true, false, true, false, true);

        /// <summary>
        /// Gets the Expected Value Combinations, minding the Inner and Outer loop values.
        /// </summary>
        /// <see cref="ThreeIntegers"/>
        /// <see cref="TwoIntegers"/>
        public static IEnumerable<object[]> ExpectedThreeAndTwoCombos
            => from y in TwoIntegers
                from x in ThreeIntegers
                select GetObjectRange(x, y).ToArray();
    }
}
