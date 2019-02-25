using System;
using System.Collections.Generic;

// ReSharper disable IdentifierTypo
namespace Kingdom.Combinatorics.Combinatorials
{
    using Xunit;
    using Xunit.Abstractions;

    public abstract class TestFixtureBase
    {
        /// <summary>
        /// Gets the OutputHelper.
        /// </summary>
        protected ITestOutputHelper OutputHelper { get; }

        protected TestFixtureBase(ITestOutputHelper outputHelper)
        {
            OutputHelper = outputHelper;
        }

        protected static void VerifyAspect(Combiner combiner, params Action<IEnumerable<object>>[] verifiers)
        {
            Assert.NotNull(combiner);
            Assert.Equal(combiner.AllValues.Count, verifiers.Length);

            for (var i = 0; i < combiner.AllValues.Count; i++)
            {
                verifiers[i].Invoke(combiner.AllValues[i]);
            }
        }
    }
}
