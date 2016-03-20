using System;
using System.Collections.Generic;
using System.Linq;

namespace Useful.Extensions
{
    /// <summary>
    /// Extensions for IEnumerables
    /// </summary>
    public static class EnumerableExtensions
    {
        #region Partition extension

        /// <summary>
        /// Partition a collection of data
        /// </summary>
        /// <param name="src">The collection</param>
        /// <param name="partitionSize">(optional) The size of each partition required, default is 10</param>
        /// <returns></returns>
        public static IEnumerable<IEnumerable<T>> Partition<T>(this IEnumerable<T> src, int partitionSize = 10)
        {
            if (partitionSize <= 0)
            {
                throw new ArgumentOutOfRangeException("partitionSize");
            }

            var list = src.ToList();
            for (var skipCount = 0; skipCount < list.Count; skipCount += partitionSize)
            {
                yield return list.Skip(skipCount).Take(partitionSize);
            }
        }

        /// <summary>
        /// Partition a collection of data
        /// </summary>
        /// <param name="src">The collection</param>
        /// <param name="partitionSize">(optional) The size of each partition required, default is 10</param>
        /// <returns>The src in the required size batch</returns>
        public static IEnumerable<IQueryable<T>> Partition<T>(this IQueryable<T> src, int partitionSize = 10)
        {
            if (partitionSize <= 0)
            {
                throw new ArgumentOutOfRangeException("partitionSize");
            }

            for (var skipCount = 0; skipCount < src.Count(); skipCount += partitionSize)
            {
                yield return src.Skip(skipCount).Take(partitionSize);
            }
        }

        #endregion Partition extension

        #region Page extension

        /// <summary>
        /// Get a page of data for given start and end
        /// </summary>
        /// <param name="src">The collection</param>
        /// <param name="start">The start index</param>
        /// <param name="length">The number of items to return</param>
        /// <returns>The subset of the collection</returns>
        public static IEnumerable<T> Page<T>(this IEnumerable<T> src, int start, int length)
        {
            return src.Skip(start).Take(length);
        }

        /// <summary>
        /// Get a page of data for given start and end
        /// </summary>
        /// <param name="src">The collection</param>
        /// <param name="start">The start index</param>
        /// <param name="length">The number of items to return</param>
        /// <returns>The subset of the collection</returns>
        public static IQueryable<T> Page<T>(this IQueryable<T> src, int start, int length)
        {
            return src.Skip(start).Take(length);
        }

        #endregion Page extension
    }
}