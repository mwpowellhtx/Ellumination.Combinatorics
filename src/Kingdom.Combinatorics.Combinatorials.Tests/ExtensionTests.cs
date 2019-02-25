using System.Collections.Generic;
using System.Linq;

// ReSharper disable IdentifierTypo
namespace Kingdom.Combinatorics.Combinatorials
{
    using Xunit;
    using Xunit.Abstractions;
    using static Domain;

    public class ExtensionTests : TestFixtureBase
    {
        public ExtensionTests(ITestOutputHelper outputHelper)
            : base(outputHelper)
        {
        }

        /// <summary>
        /// Verifies that Strongly Typed Collections may be Combined.
        /// </summary>
        /// <see cref="CombinerExtensionMethods.Combine{T,TOther}"/>
        [Fact]
        public void VerifyStronglyTypedCombine()
        {
            // ReSharper disable RedundantTypeArgumentsOfMethod
            var combiner = FourCharacters.Combine<char, int>(TwoIntegers);

            VerifyAspect(combiner
                , x => Assert.Equal(FourCharacters.OfType<object>(), x)
                , x => Assert.Equal(TwoIntegers.OfType<object>(), x)
            );
        }

        /// <summary>
        /// Verifies that Weakly Typed Collections may be Combined.
        /// </summary>
        /// <see cref="CombinerExtensionMethods.Combine(IEnumerable{object},IEnumerable{object}[])"/>
        [Fact]
        public void VerifyWeaklyTypedCombine()
        {
            var combiner = ThreeObjects.Combine(TwoObjects, FourCharacters.OfType<object>().ToArray());

            VerifyAspect(combiner
                , x => Assert.Equal(ThreeObjects, x)
                , x => Assert.Equal(TwoObjects, x)
                , x => Assert.Equal(FourCharacters.OfType<object>(), x)
            );
        }

        /// <summary>
        /// Verifies that Strongly Typed Collections may be Appended.
        /// </summary>
        /// <see cref="CombinerExtensionMethods.Append{T}"/>
        [Fact]
        public void VerifyStronglyTypedAppend()
        {
            var combiner = ThreeObjects.Combine(TwoObjects);
            // Make sure that we are using the Strongly Typed Version.
            var appended = combiner.Append<char>(FourCharacters);

            Assert.NotNull(appended);
            Assert.NotSame(combiner, appended);

            VerifyAspect(appended
                , x => Assert.Equal(ThreeObjects, x)
                , x => Assert.Equal(TwoObjects, x)
                , x => Assert.Equal(FourCharacters.OfType<object>(), x)
            );
        }

        /// <summary>
        /// Verifies that Weakly Typed Collections may be Appended.
        /// </summary>
        /// <see cref="CombinerExtensionMethods.Append(ICombiner,IEnumerable{object},IEnumerable{object}[])"/>
        [Fact]
        public void VerifyWeaklyTypedAppend()
        {
            var combiner = ThreeObjects.Combine(TwoObjects);
            // Make sure that we are using the Object[] params version.
            var appended = combiner.Append(FourCharacters.OfType<object>(), FiveBooleans.OfType<object>());

            Assert.NotNull(appended);
            Assert.NotSame(combiner, appended);

            VerifyAspect(appended
                , x => Assert.Equal(ThreeObjects, x)
                , x => Assert.Equal(TwoObjects, x)
                , x => Assert.Equal(FourCharacters.OfType<object>(), x)
                , x => Assert.Equal(FiveBooleans.OfType<object>(), x)
            );
        }
    }
}
