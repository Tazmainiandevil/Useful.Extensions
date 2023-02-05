using System;
using System.Collections.Generic;
using System.Linq;

namespace Useful.Extensions
{
    /// <summary>
    /// Extensions for IEnumerable
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
                throw new ArgumentOutOfRangeException(nameof(partitionSize));
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
        /// <returns>The source in the required size batch</returns>
        public static IEnumerable<IQueryable<T>> Partition<T>(this IQueryable<T> src, int partitionSize = 10)
        {
            if (partitionSize <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(partitionSize));
            }

            for (var skipCount = 0; skipCount < src.Count(); skipCount += partitionSize)
            {
                yield return src.Skip(skipCount).Take(partitionSize) as IQueryable<T>;
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
            return src.Skip(start).Take(length) as IQueryable<T>;
        }

        #endregion Page extension

        #region IsNullOrEmpty

        /// <summary>
        /// Is Null or Empty for IEnumerable types
        /// </summary>
        /// <param name="src">The collection</param>
        /// <returns>A boolean denoting if null or empty</returns>
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> src)
        {
            if (src == null)
            {
                return true;
            }

            return !src.Any();
        }

        /// <summary>
        /// Is Null or Empty for ICollection types
        /// </summary>
        /// <param name="src">The collection</param>
        /// <returns>A boolean denoting if null or empty</returns>
        public static bool IsNullOrEmpty<T>(this ICollection<T> src)
        {
            if (src == null)
            {
                return true;
            }

            return src.Count < 1;
        }

        #endregion IsNullOrEmpty

        #region IsValueInList

        public static bool IsValueInList(this IEnumerable<string> src, string find)
        {
            return src != null && src.Any(x => x.EqualsIgnoreCase(find));
        }

        public static bool IsValueInList(this IEnumerable<string> src, string find, StringComparison compare)
        {
            return src != null && src.Any(x => x.Equals(find, compare));
        }

        public static bool IsValueInList<T>(this IEnumerable<T> src, T find)
        {
            return src != null && src.Any(x => x.Equals(find));
        }

        #endregion IsValueInList

        #region String.Join

        public static string Join(this IEnumerable<string> source, char separator)
        {
            var result = string.Join(separator, source);
            return result;
        }

        public static string Join(this IEnumerable<string> source, string separator)
        {
            var result = string.Join(separator, source);
            return result;
        }

        // public static string Join(this IEnumerable<string> source, char separator, int startIndex, int count)
        // {
        //     var result = string.Join(separator, source, startIndex, count);
        //     return result;
        // }
        //
        // public static string Join(this IEnumerable<string> source, string separator, int startIndex, int count)
        // {
        //     var result = string.Join(separator, source, startIndex, count);
        //     return result;
        // }
        
        // public static string Join(
        //     this string[] source,
        //     char separator)
        // {
        //     var result = string.Join(separator, source);
        //     return result;
        // }
        //
        // public static string Join(
        //     this string[] source,
        //     string separator)
        // {
        //     var result = string.Join(separator, source);
        //     return result;
        // }
        //
        // public static string Join(
        //     this string[] source,
        //     char separator,
        //     int startIndex,
        //     int count)
        // {
        //     var result = string.Join(
        //         separator,
        //         source,
        //         startIndex,
        //         count);
        //     return result;
        // }
        //
        // public static string Join(
        //     this string[] source,
        //     string separator,
        //     int startIndex,
        //     int count)
        // {
        //     var result = string.Join(
        //         separator,
        //         source,
        //         startIndex,
        //         count);
        //     return result;
        // }

        #endregion String.Join
    }
}