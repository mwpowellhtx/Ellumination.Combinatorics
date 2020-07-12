using System;
using System.Collections.Generic;

// ReSharper disable IdentifierTypo
namespace Ellumination.Combinatorics.Combinatorials
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
            combiner.AssertNotNull().AllValues.AssertEqual(verifiers.Length, x => x.Count);

            for (var i = 0; i < combiner.AllValues.Count; i++)
            {
                verifiers[i].Invoke(combiner.AllValues[i]);
            }
        }
    }
}
