using System.Collections.Generic;
using System.Linq;

namespace Spoleto.Common.Extensions
{
    /// <summary>
    /// Extensions for <see cref="IEnumerable{T}"/>.
    /// </summary>
    public static class EnumerableExtension
    {
        /// <summary>
        /// Splits the source into the batches with the given size.
        /// </summary>
        public static IEnumerable<IEnumerable<T>> Split<T>(this IEnumerable<T> source, int batchSize)
        {
            while (source.Any())
            {
                yield return source.Take(batchSize);

                source = source.Skip(batchSize);
            }
        }
    }
}
