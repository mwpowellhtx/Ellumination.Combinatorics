using System;
using System.Linq;

// ReSharper disable IdentifierTypo
namespace Ellumination.Combinatorics.Combinatorials
{
    using Xunit;
    using Xunit.Abstractions;
    using static Domain;

    public class CombinerTests : TestFixtureBase
    {
        public CombinerTests(ITestOutputHelper outputHelper)
            : base(outputHelper)
        {
        }

        [Fact]
        public void CtorThreeAndTwoYieldSix()
        {
            var first = ThreeObjects.ToArray();
            var second = TwoObjects.ToArray();

            var combiner = new Combiner(first, second);

            combiner.AssertEqual(first.Length * second.Length, x => x.Count);

            VerifyAspect(combiner
                , x => x.AssertEqual(first)
                , x => x.AssertEqual(second)
            );
        }

        [Fact]
        public void ExtensionThreeAndTwoYieldSix()
        {
            var first = ThreeIntegers.ToArray();
            var second = TwoIntegers.ToArray();

            var combiner = first.Combine(second);

            combiner.AssertEqual(first.Length * second.Length, x => x.Count);

            VerifyAspect(combiner
                , x => x.AssertEqual(first.OfType<object>())
                , x => x.AssertEqual(second.OfType<object>())
            );
        }

        [Fact]
        public void EnumerableIterationThreeAndTwoYieldsSix()
        {
            var combiner = new Combiner(ThreeObjects, TwoObjects);

            // So if there were no Combinations, then ActualCount would actually be Null at the outset.
            int? actualCount = null;

            // We must ensure that Overflow is Silent.
            combiner.SilentOverflow = true;

            // Does not matter what the Combination was, we just want to verify that it Counts correctly.
            foreach (var _ in combiner)
            {
                if (!actualCount.HasValue)
                {
                    actualCount = 0;
                }

                actualCount++;
            }

            actualCount.HasValue.AssertTrue();
            combiner.AssertEqual(actualCount, x => x.Count);
        }

        [Fact]
        public void EnumerableThreeAndTwoRangeEquals()
        {
            var combiner = new Combiner(ThreeIntegers.OfType<object>(), TwoIntegers.OfType<object>())
            {
                SilentOverflow = true
            };

            // Which should Iterate over the Enumerable of Object[] Combinations.
            combiner.AssertEqual(ExpectedThreeAndTwoCombos);
        }

        [Fact]
        public void OverflowEventuallyThrows()
        {
            void IterateUntilOverflow()
            {
                var combiner = TwoIntegers.Combine(ThreeIntegers);

                for (; !combiner.Exhausted; ++combiner)
                {
                }

                // TODO: TBD: need to figure out how better to deal with the indexing at the edges... particular when reaching the "maxed out" or "last" edge case.
                // TODO: TBD: for now, the "simplest" thing to do is increment one more time after having shorted out at the last case.
                // ReSharper disable once RedundantAssignment
                combiner++;
            }

            // We can see this as an "Action" although there is no implicit type conversion as such.
            ((Action)IterateUntilOverflow).AssertThrows<InvalidOperationException>();
        }
    }
}
