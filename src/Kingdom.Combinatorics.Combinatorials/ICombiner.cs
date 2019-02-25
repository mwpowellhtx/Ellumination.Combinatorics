using System.Collections.Generic;

// ReSharper disable IdentifierTypo
namespace Kingdom.Combinatorics.Combinatorials
{
    // ReSharper disable CommentTypo
    /// <summary>
    /// Provides an interface for all things Combinatorials.
    /// </summary>
    /// <inheritdoc />
    public interface ICombiner : IEnumerable<object[]>
    {
        /// <summary>
        /// Gets or sets whether the Combiner SilentOverflow.
        /// </summary>
        bool SilentOverflow { get; set; }

        /// <summary>
        /// Gets whether Combiner is Exhausted.
        /// </summary>
        bool Exhausted { get; }

        /// <summary>
        /// Resets the Combiner.
        /// </summary>
        /// <returns></returns>
        ICombiner Reset();

        /// <summary>
        /// Increments the Combiner by the <paramref name="steps"/>.
        /// </summary>
        /// <param name="steps"></param>
        /// <returns></returns>
        ICombiner Increment(int steps);

        /// <summary>
        /// Gets the number of possible Combiner Combinations.
        /// </summary>
        int Count { get; }

        /// <summary>
        /// Gets the number of possible Combiner Combinations.
        /// </summary>
        long LongCount { get; }

        /// <summary>
        /// Gets the CurrentCombination.
        /// </summary>
        IEnumerable<object> CurrentCombination { get; }
    }
}
