using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

namespace Useful.Extensions
{
    /// <summary>
    /// Extensions for string object
    /// </summary>
    public static class StringExtensions
    {
        #region ContainsValue / HasValue

        /// <summary>
        /// Find
        /// </summary>
        /// <param name="src">The string to find a src in</param>
        /// <param name="find">The string to find</param>
        /// <param name="caseCompare">(optional)The type of compare to use the default is to ignore case</param>
        /// <returns>A boolean denoting if the find src is in the string</returns>
        public static bool ContainsValue(this string src, string find, StringComparison caseCompare = StringComparison.OrdinalIgnoreCase)
        {
            if (string.IsNullOrEmpty(src) || string.IsNullOrEmpty(find))
            {
                return false;
            }

            var result = src.IndexOf(find, caseCompare) >= 0;
            return result;
        }

        /// <summary>
        /// Find
        /// </summary>
        /// <param name="src">The string to find a src in</param>
        /// <param name="find">The string to find</param>
        /// <param name="caseCompare">(optional)The type of compare to use the default is to ignore case</param>
        /// <returns>A boolean denoting if the find src is in the string</returns>
        public static bool HasValue(this string src, string find, StringComparison caseCompare = StringComparison.OrdinalIgnoreCase)
        {
            return ContainsValue(src, find, caseCompare);
        }

        #endregion ContainsValue / HasValue

        #region Equals Ignore Case

        /// <summary>
        /// Wraps equals to remove the need to specify the case for ignoring case
        /// </summary>
        /// <param name="src">The string to perform equality on</param>
        /// <param name="compare">The string to compare</param>
        /// <returns>A boolean denoting the two strings are equal ignoring case</returns>
        public static bool EqualsIgnoreCase(this string src, string compare)
        {
            if (src == null && compare == null)
            {
                return true;
            }

            return (src ?? string.Empty).Equals(compare, StringComparison.OrdinalIgnoreCase);
        }

        #endregion Equals Ignore Case

        #region Safe StartsWith

        /// <summary>
        /// Perform a Starts With even if the src is null
        /// </summary>
        /// <param name="src">The string to perform start with on</param>
        /// <param name="find">The value to look for</param>
        /// <returns>A boolean denoting if value starts with given value</returns>
        public static bool SafeStartsWith(this string src, string find)
        {
            return (src ?? string.Empty).StartsWith(find);
        }

        /// <summary>
        /// Perform a Starts With even if the src is null
        /// </summary>
        /// <param name="src">The string to perform start with on</param>
        /// <param name="find">The value to look for</param>
        /// <param name="comparison">The string comparison type (defaults to Ordinal)</param>
        /// <returns>A boolean denoting if value starts with given value</returns>
        public static bool SafeStartsWith(this string src, string find, StringComparison comparison)
        {
            return (src ?? string.Empty).StartsWith(find, comparison);
        }

        /// <summary>
        /// Perform a Starts With even if the src is null
        /// </summary>
        /// <param name="src">The string to perform start with on</param>
        /// <param name="find">The value to look for</param>
        /// <param name="ignoreCase"></param>
        /// <param name="culture"></param>
        /// <returns>A boolean denoting if value starts with given value</returns>
        public static bool SafeStartsWith(this string src, string find, bool ignoreCase, CultureInfo culture)
        {
            return (src ?? string.Empty).StartsWith(find, ignoreCase, culture);
        }

        #endregion Safe StartsWith

        #region Safe EndsWith

        /// <summary>
        /// Perform a Ends With even if the src is null
        /// </summary>
        /// <param name="src">The string to perform start with on</param>
        /// <param name="find">The value to look for</param>
        /// <returns>A boolean denoting if value ends with given value</returns>
        public static bool SafeEndsWith(this string src, string find)
        {
            return (src ?? string.Empty).EndsWith(find);
        }

        /// <summary>
        /// Perform a Ends With even if the src is null
        /// </summary>
        /// <param name="src">The string to perform start with on</param>
        /// <param name="find">The value to look for</param>
        /// <param name="comparison">The string comparison type (defaults to Ordinal)</param>
        /// <returns>A boolean denoting if value ends with given value</returns>
        public static bool SafeEndsWith(this string src, string find, StringComparison comparison)
        {
            return (src ?? string.Empty).EndsWith(find, comparison);
        }

        /// <summary>
        /// Perform a Ends With even if the src is null
        /// </summary>
        /// <param name="src">The string to perform start with on</param>
        /// <param name="find">The value to look for</param>
        /// <param name="ignoreCase"></param>
        /// <param name="culture"></param>
        /// <returns>A boolean denoting if value starts with given value</returns>
        public static bool SafeEndsWith(this string src, string find, bool ignoreCase, CultureInfo culture)
        {
            return (src ?? string.Empty).EndsWith(find, ignoreCase, culture);
        }

        #endregion Safe EndsWith

        #region Substring

        /// <summary>
        /// SubstringOrEmpty allows substring to be used without throwing an exception if out of range
        /// </summary>
        /// <param name="src">The string to substring</param>
        /// <param name="start">The substring start point</param>
        /// <param name="length">(optional)The length to substring, the default is the remaining length from the start point</param>
        /// <returns>The requested substring or an empty string if the out of range or empty</returns>
        public static string SubstringOrEmpty(this string src, int start, int length = 0)
        {
            if (string.IsNullOrEmpty(src))
            {
                return string.Empty;
            }

            if (length.Equals(0))
            {
                length = src.Length;
            }

            // Get the requested substring
            return new string(src.Skip(start).Take(length).ToArray());
        }

        /// <summary>
        /// Return the substring after a given character
        /// </summary>
        /// <param name="src">The string containing the content</param>
        /// <param name="find">The character to find</param>
        /// <param name="comparison">The string comparison type (defaults to OrdinalIgnoreCase)</param>
        /// <returns>The substring value</returns>
        public static string SubstringAfterValue(this string src, char find, StringComparison comparison = StringComparison.OrdinalIgnoreCase)
        {
            return src.SubstringAfterValue(find.ToString(), comparison);
        }

        /// <summary>
        /// Return the substring after a given character or string
        /// </summary>
        /// <param name="src">The string containing the content</param>
        /// <param name="find">The string to find</param>
        /// <param name="comparison">The string comparison type (defaults to OrdinalIgnoreCase)</param>
        /// <returns>The substring value</returns>
        public static string SubstringAfterValue(this string src, string find, StringComparison comparison = StringComparison.OrdinalIgnoreCase)
        {
            if (string.IsNullOrWhiteSpace(src))
            {
                return string.Empty;
            }

            var index = src.IndexOf(find ?? string.Empty, comparison);

            return index < 0 ? src : new string(src.Skip(index + (find ?? string.Empty).Length).Take(src.Length).ToArray());
        }

        /// <summary>
        /// Return the substring after the last occurrence of a given character
        /// </summary>
        /// <param name="src">The string containing the content</param>
        /// <param name="find">The string to find</param>
        /// <param name="comparison">The string comparison type (defaults to OrdinalIgnoreCase)</param>
        /// <returns>The substring value</returns>
        public static string SubstringAfterLastValue(this string src, char find, StringComparison comparison = StringComparison.OrdinalIgnoreCase)
        {
            return src.SubstringAfterLastValue(find.ToString(), comparison);
        }

        /// <summary>
        /// Return the substring after the last occurrence of a given character or string
        /// </summary>
        /// <param name="src">The string containing the content</param>
        /// <param name="find">The string to find</param>
        /// <param name="comparison">The string comparison type (defaults to OrdinalIgnoreCase)</param>
        /// <returns>The substring value</returns>
        public static string SubstringAfterLastValue(this string src, string find, StringComparison comparison = StringComparison.OrdinalIgnoreCase)
        {
            if (string.IsNullOrWhiteSpace(src))
            {
                return string.Empty;
            }

            if (string.IsNullOrEmpty(find))
            {
                return src;
            }

            var index = src.LastIndexOf(find, comparison);

            return index < 0 ? src : new string(src.Skip(index + find.Length).Take(src.Length).ToArray());
        }

        /// <summary>
        /// Return the substring before a given character
        /// </summary>
        /// <param name="src">The string containing the content</param>
        /// <param name="find">The character to find</param>
        /// <param name="comparison">The string comparison type (defaults to OrdinalIgnoreCase)</param>
        /// <returns>The substring value</returns>
        public static string SubstringBeforeValue(this string src, char find, StringComparison comparison = StringComparison.OrdinalIgnoreCase)
        {
            return src.SubstringBeforeValue(find.ToString(), comparison);
        }

        /// <summary>
        /// Return the substring before a given character or string
        /// </summary>
        /// <param name="src">The string containing the content</param>
        /// <param name="find">The string to find</param>
        /// <param name="comparison">The string comparison type (defaults to OrdinalIgnoreCase)</param>
        /// <returns>The substring value</returns>
        public static string SubstringBeforeValue(this string src, string find, StringComparison comparison = StringComparison.OrdinalIgnoreCase)
        {
            if (string.IsNullOrEmpty(src))
            {
                return string.Empty;
            }

            var index = src.IndexOf(find ?? string.Empty, comparison);

            if (string.IsNullOrEmpty(find) || index < 0)
            {
                return src;
            }

            return new string(src.Take(index).ToArray());
        }

        /// <summary>
        /// Return the substring before the last occurrence of a given character
        /// </summary>
        /// <param name="src">The string containing the content</param>
        /// <param name="find">The character to find</param>
        /// <param name="comparison">The string comparison type (defaults to OrdinalIgnoreCase)</param>
        /// <returns>The substring value</returns>
        public static string SubstringBeforeLastValue(this string src, char find, StringComparison comparison = StringComparison.OrdinalIgnoreCase)
        {
            return src.SubstringBeforeLastValue(find.ToString(), comparison);
        }

        /// <summary>
        /// Return the substring before the last occurrence of a given character or string
        /// </summary>
        /// <param name="src">The string containing the content</param>
        /// <param name="find">The string to find</param>
        /// <param name="comparison">The string comparison type (defaults to OrdinalIgnoreCase)</param>
        /// <returns>The substring value</returns>
        public static string SubstringBeforeLastValue(this string src, string find, StringComparison comparison = StringComparison.OrdinalIgnoreCase)
        {
            if (string.IsNullOrEmpty(src))
            {
                return string.Empty;
            }

            var index = src.LastIndexOf(find ?? string.Empty, comparison);

            if (string.IsNullOrEmpty(find) || index < 0)
            {
                return src;
            }

            return new string(src.Take(index).ToArray());
        }

        #endregion Substring

        #region Safe Trim

        /// <summary>
        /// Trim a string and return even if null or empty
        /// </summary>
        /// <param name="src">The string to perform a trim on</param>
        /// <returns>The trimmed string or null or empty</returns>
        public static string SafeTrim(this string src)
        {
            return string.IsNullOrEmpty(src) ? src : src.Trim();
        }

        #endregion Safe Trim

        #region Is Base64

        /// <summary>
        /// Check if a string is base 64 encoded
        /// </summary>
        /// <param name="src">The string to test</param>
        /// <returns>A boolean denoting if a string is base 64 encoded</returns>
        public static bool IsBase64(this string src)
        {
            if (string.IsNullOrWhiteSpace(src))
            {
                return false;
            }

            const string pattern = "^([A-Za-z0-9+/]{4})*([A-Za-z0-9+/]{4}|[A-Za-z0-9+/]{3}=|[A-Za-z0-9+/]{2}==)$";

            return Regex.IsMatch(src, pattern);
        }

        #endregion Is Base64

        #region IsAllNumbers

        private const string IsNumericPattern = @"^\d+$";

        /// <summary>
        /// Does the whole string contain only numeric characters
        /// </summary>
        /// <param name="src">The input string</param>
        /// <returns>A boolean denoting if all the characters are numeric</returns>
        public static bool IsAllNumber(this string src)
        {
            if (string.IsNullOrWhiteSpace(src))
            {
                return false;
            }

            return Regex.IsMatch(src, IsNumericPattern, RegexOptions.Compiled);
        }

        #endregion IsAllNumbers

        #region IsAllAlpha

        private const string IsAlphaPattern = @"^[a-zA-Z]+$";

        /// <summary>
        /// Does the whole string contain only alpha characters
        /// </summary>
        /// <param name="src">The input string</param>
        /// <returns>A boolean denoting if all the characters are alpha</returns>
        public static bool IsAllAlpha(this string src)
        {
            if (string.IsNullOrWhiteSpace(src))
            {
                return false;
            }

            return Regex.IsMatch(src, IsAlphaPattern, RegexOptions.Compiled);
        }

        #endregion IsAllAlpha

        #region IsAllAlphaOrNumbers

        private const string IsAlphaNumericPattern = @"^[a-zA-Z0-9]+$";

        /// <summary>
        /// Does the whole string contain only alpha and numeric characters, no special characters
        /// </summary>
        /// <param name="src">The input string</param>
        /// <returns>A boolean denoting if all the characters are alpha and/or numeric</returns>
        public static bool IsAllAlphaOrNumbers(this string src)
        {
            if (string.IsNullOrWhiteSpace(src))
            {
                return false;
            }

            return Regex.IsMatch(src, IsAlphaNumericPattern, RegexOptions.Compiled);
        }

        #endregion IsAllAlphaOrNumbers

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