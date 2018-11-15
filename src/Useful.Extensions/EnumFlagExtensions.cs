using System;
using System.Linq;

namespace Useful.Extensions
{
    public static class EnumFlagExtensions
    {
        /// <summary>
        /// Does the enum contains a given entry
        /// </summary>
        /// <param name="src">The enum value</param>
        /// <param name="entry">The enum entry to check</param>
        /// <returns>A boolean denoting if the entry is set</returns>
        public static bool Contains(this Enum src, Enum entry)
        {
            return src.HasFlag(entry);
        }

        /// <summary>
        /// Are any of the enum entries set
        /// </summary>
        /// <param name="src">The enum value</param>
        /// <param name="entries">The entries to check if set</param>
        /// <returns>A boolean denoting if any of the entries is set</returns>
        public static bool Any(this Enum src, params Enum[] entries)
        {
            return entries.Any(src.HasFlag);
        }

        /// <summary>
        /// Are all the entries given set
        /// </summary>
        /// <param name="src">The enum value</param>
        /// <param name="entries">The entries to check if set</param>
        /// <returns>A boolean denoting if all of the entries are set</returns>
        public static bool All(this Enum src, params Enum[] entries)
        {
            return entries.All(src.HasFlag);
        }

        /// <summary>
        /// Set entries on the value
        /// </summary>
        /// <typeparam name="T">The enum type</typeparam>
        /// <param name="src">The enum value</param>
        /// <param name="entries">The entries to set</param>
        /// <returns>The value with the entries set</returns>
        public static T Set<T>(this Enum src, params Enum[] entries) where T : Enum        
        {
            var type = Enum.GetUnderlyingType(src.GetType());
            dynamic srcValue = Convert.ChangeType(src, type);

            foreach (var entry in entries)
            {
                dynamic entryValue = Convert.ChangeType(entry, type);
                srcValue |= entryValue;
            }

            return (T)srcValue;
        }

        /// <summary>
        /// Unset entries on the value
        /// </summary>
        /// <typeparam name="T">The enum type</typeparam>
        /// <param name="src">The enum value</param>
        /// <param name="entries">The entries to unset</param>
        /// <returns>The value with the entries unset</returns>
        public static T UnSet<T>(this Enum src, params Enum[] entries) where T : Enum        
        {
            var type = Enum.GetUnderlyingType(src.GetType());
            dynamic srcValue = Convert.ChangeType(src, type);

            foreach (var entry in entries)
            {
                dynamic entryValue = Convert.ChangeType(entry, type);
                srcValue &= ~entryValue;
            }

            return (T)srcValue;
        }
    }
}