using System.Collections.Generic;
using System.Linq;

// ReSharper disable IdentifierTypo
namespace Ellumination.Combinatorics.Combinatorials
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

#pragma warning disable IDE0001 // Name can be simplified
            // ReSharper disable RedundantTypeArgumentsOfMethod
            var combiner = FourCharacters.Combine<char, int>(TwoIntegers);
#pragma warning restore IDE0001 // Name can be simplified

            VerifyAspect(combiner
                , x => x.AssertEqual(FourCharacters.OfType<object>())
                , x => x.AssertEqual(TwoIntegers.OfType<object>())
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
                , x => x.AssertEqual(ThreeObjects)
                , x => x.AssertEqual(TwoObjects)
                , x => x.AssertEqual(FourCharacters.OfType<object>())
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
#pragma warning disable IDE0001 // Name can be simplified
            // ReSharper disable RedundantTypeArgumentsOfMethod
            var appended = combiner.Append<char>(FourCharacters);
#pragma warning restore IDE0001 // Name can be simplified

            combiner.AssertNotNull().AssertNotSame(appended);

            VerifyAspect(appended
                , x => x.AssertEqual(ThreeObjects)
                , x => x.AssertEqual(TwoObjects)
                , x => x.AssertEqual(FourCharacters.OfType<object>())
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

            appended.AssertNotNull().AssertNotSame(combiner);

            VerifyAspect(appended
                , x => x.AssertEqual(ThreeObjects)
                , x => x.AssertEqual(TwoObjects)
                , x => x.AssertEqual(FourCharacters.OfType<object>())
                , x => x.AssertEqual(FiveBooleans.OfType<object>())
            );
        }
    }
}
