using System;
using System.Collections.Generic;
using System.Linq;

// ReSharper disable IdentifierTypo
namespace Kingdom.Combinatorics.Combinatorials
{
    /// <summary>
    /// 
    /// </summary>
    public static class CombinerExtensionMethods
    {
        /// <summary>
        /// Combines the strongly typed <paramref name="values"/> with the
        /// <paramref name="other"/>.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TOther"></typeparam>
        /// <param name="values"></param>
        /// <param name="other"></param>
        /// <returns></returns>
        public static Combiner Combine<T, TOther>(this IEnumerable<T> values, IEnumerable<TOther> other)
            => new Combiner(
                values.OfType<object>().ToArray()
                , other.OfType<object>().ToArray()
            );

        /// <summary>
        /// Combines the weakly typed <paramref name="values"/> with the
        /// <paramref name="others"/>.
        /// </summary>
        /// <param name="values"></param>
        /// <param name="others"></param>
        /// <returns></returns>
        public static Combiner Combine(this IEnumerable<object> values, params IEnumerable<object>[] others)
            => new Combiner(new[] {values}.Concat(others).ToArray());

        /// <summary>
        /// Appends the <paramref name="values"/> to those contained by the
        /// <paramref name="combiner"/>.
        /// </summary>
        /// <param name="combiner"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        private static Combiner AppendValues(Combiner combiner, params IEnumerable<object>[] values)
        {
            if (combiner is null)
            {
                throw new ArgumentException($"'{typeof(Combiner).FullName}' instance expected."
                    , nameof(combiner));
            }

            return new Combiner(combiner.AllValues.Concat(values).ToArray());
        }

        /// <summary>
        /// Returns a brand new <see cref="Combiner"/> instance including the given
        /// <see cref="Combiner.AllValues"/> and the strongly typed <paramref name="values"/>.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="combiner">Should provide an instance of <see cref="Combiner"/>
        /// seen through the interface by the same name.</param>
        /// <param name="values"></param>
        /// <returns>A brand new Combiner instance with the Combined Arrays.</returns>
        public static Combiner Append<T>(this ICombiner combiner, IEnumerable<T> values)
            => AppendValues(combiner as Combiner, values.OfType<object>().ToArray());

        /// <summary>
        /// Returns a brand new <see cref="Combiner"/> instance including the given
        /// <see cref="Combiner.AllValues"/> and weakly typed <paramref name="values"/> and
        /// <paramref name="others"/>.
        /// </summary>
        /// <param name="combiner">Should provide an instance of <see cref="Combiner"/>
        /// seen through the interface by the same name.</param>
        /// <param name="values"></param>
        /// <param name="others"></param>
        /// <returns></returns>
        public static Combiner Append(this ICombiner combiner, IEnumerable<object> values, params IEnumerable<object>[] others)
            => AppendValues(combiner as Combiner, new[] {values.ToArray()}.Concat(others).ToArray());
    }
}
