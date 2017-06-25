using System.Collections.Generic;

namespace Useful.Extensions
{
    /// <summary>
    /// Extensions for Dictionary object
    /// </summary>
    public static class DictionaryExtensions
    {
        /// <summary>
        /// Return the value from a dictionary for a given key or the default value for the type if not found
        /// </summary>
        /// <param name="src">The dictionary to search in</param>
        /// <param name="key">The key to search</param>
        /// <param name="defaultValue">(optional) The default value of the value or override if alternate required</param>
        /// <returns>The value for the given key or a default value</returns>
        public static TValue ValueOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> src, TKey key, TValue defaultValue = default(TValue))
        {
            if (src == null)
            {
                return defaultValue;
            }

            return src.ContainsKey(key) ? src[key] : defaultValue;
        }

        /// <summary>
        /// Return the value from a dictionary for a given key or the default value for the type if not found
        /// Note: Use of TryGetValue does one scan of the hash table and considered faster than ContainsKey
        /// </summary>
        /// <param name="src">The dictionary to search in</param>
        /// <param name="key">The key to search</param>
        /// <param name="defaultValue">(optional) The default value of the value or override if alternate required</param>
        /// <returns>The value for the given key or a default value</returns>
        public static TValue TryGetValueOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> src, TKey key, TValue defaultValue = default(TValue))
        {
            if (src == null)
            {
                return defaultValue;
            }

            TValue value;
            return src.TryGetValue(key, out value) ? value : defaultValue;            
        }
    }
}
