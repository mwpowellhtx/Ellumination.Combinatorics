using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

// ReSharper disable IdentifierTypo
namespace Kingdom.Combinatorics.Combinatorials
{
    /// <summary>
    /// The Combiner implementation class accepts multiple jagged collections, meaning not all
    /// necessarily the same size, and incrementally delivers the combination of value from the
    /// cross section of values.
    /// </summary>
    /// <inheritdoc />
    public class Combiner : ICombiner
    {
        /// <inheritdoc />
        public bool SilentOverflow { get; set; }

        internal IList<IEnumerable<object>> AllValues { get; }

        private IList<int> Indices { get; set; }

        /// <summary>
        /// Public Constructor accepting <paramref name="allValues"/>.
        /// </summary>
        /// <param name="allValues"></param>
        public Combiner(params IEnumerable<object>[] allValues)
        {
            AllValues = allValues.ToList();
            Reset();
        }

        /// <inheritdoc />
        public IEnumerator<object[]> GetEnumerator()
        {
            Reset();

            for (var i = 0; i < Count; i++, IncrementOne())
            {
                yield return CurrentCombination.ToArray();
            }
        }

        /// <inheritdoc />
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        /// Resets the <see cref="Indices"/> based on the current <see cref="AllValues"/>.
        /// </summary>
        /// <returns></returns>
        public Combiner Reset()
        {
            Indices = AllValues.Select(x => 0).ToList();
            return this;
        }

        /// <inheritdoc />
        ICombiner ICombiner.Reset() => Reset();

        /// <summary>
        /// Increments the <see cref="Indices"/> from the previous state.
        /// </summary>
        private void IncrementOne()
        {
            if (Indices.Zip(AllValues, (i, x) => i == x.Count() - 1).All(y => y))
            {
                if (SilentOverflow)
                {
                    return;
                }

                throw new InvalidOperationException(
                    "Combiner already maxed out. Try calling Reset before Incrementing.");
            }

            bool WouldOverflow(int i) => Indices[i] == AllValues.ElementAt(i).Count() - 1;

            for (var i = 0; i < Indices.Count; ++i)
            {
                if (WouldOverflow(i))
                {
                    Indices[i] = 0;
                    continue;
                }

                // Also the terminal case. Finished the Increment. Nothing further to overflow.
                ++Indices[i];
                break;
            }
        }

        /// <summary>
        /// Increments the <see cref="Indices"/> a given number of <paramref name="steps"/>.
        /// </summary>
        /// <param name="steps"></param>
        /// <returns></returns>
        public Combiner Increment(int steps)
        {
            // TODO: TBD: what to do about Empty members...
            while (steps-- > 0)
            {
                IncrementOne();
            }

            return this;
        }

        /// <inheritdoc />
        ICombiner ICombiner.Increment(int steps) => Increment(steps);

        /// <summary>
        /// Indicates whether All of the <see cref="Indices"/> have been Exhausted. That is,
        /// whether there is any room for subsequent <see cref="Increment"/>. If any of the
        /// <see cref="AllValues"/> are Empty, then the Combinations are, by definition,
        /// Exhausted.
        /// </summary>
        /// <inheritdoc />
        public bool Exhausted => Indices.Zip(AllValues, (i, x)
            // ReSharper disable once PossibleMultipleEnumeration
            => !x.Any()
               // ReSharper disable once PossibleMultipleEnumeration
               || i == x.Count() - 1).All(y => y);

        /// <inheritdoc />
        public int Count => AllValues.Aggregate(1, (g, x) => g * x.Count());

        /// <inheritdoc />
        public long LongCount => AllValues.Aggregate(1L, (g, x) => g * x.LongCount());

        /// <summary>
        /// The Increment Operator Overload advances the <see cref="CurrentCombination"/> by One
        /// Step.
        /// </summary>
        /// <param name="combiner"></param>
        /// <returns></returns>
        public static Combiner operator ++(Combiner combiner) => combiner + 1;

        /// <summary>
        /// Advances the <see cref="CurrentCombination"/> by the given number of
        /// <paramref name="steps"/>.
        /// </summary>
        /// <param name="combiner"></param>
        /// <param name="steps"></param>
        /// <returns></returns>
        public static Combiner operator +(Combiner combiner, int steps) => combiner.Increment(steps);

        /// <summary>
        /// Gets the CurrentCombination based on the <see cref="AllValues"/> and cross cutting
        /// <see cref="Indices"/>.
        /// </summary>
        /// <inheritdoc />
        public IEnumerable<object> CurrentCombination
            // ReSharper disable once PossibleMultipleEnumeration
            => AllValues.Zip(Indices, (x, i) => !x.Any()
                ? null
                // ReSharper disable once PossibleMultipleEnumeration
                : x.ElementAt(i));
    }
}
