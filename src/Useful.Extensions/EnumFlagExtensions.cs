namespace Useful.Extensions;

public static class EnumFlagExtensions
{
    /// <summary>
    /// Does the enum contains a given entry
    /// </summary>
    /// <param name="src">The enum value</param>
    /// <param name="entry">The enum entry to check</param>
    /// <returns>A boolean denoting if the entry is set</returns>
    public static bool Contains<TEnum>(this TEnum src, TEnum entry) where TEnum : Enum
    {
        return entry != null && src.HasFlag(entry);
    }

    /// <summary>
    /// Are any of the enum entries set
    /// </summary>
    /// <param name="src">The enum value</param>
    /// <param name="entries">The entries to check if set</param>
    /// <returns>A boolean denoting if any of the entries is set</returns>
    public static bool HasAnyOf<TEnum>(this TEnum src, params TEnum[] entries) where TEnum : Enum
    {
        return !entries.IsNullOrEmpty() && entries.Any(x => src.HasFlag(x));
    }

    /// <summary>
    /// Are all the entries given set
    /// </summary>
    /// <param name="src">The enum value</param>
    /// <param name="entries">The entries to check if set</param>
    /// <returns>A boolean denoting if all of the entries are set</returns>
    public static bool HasAllOf<TEnum>(this TEnum src, params TEnum[] entries) where TEnum : Enum
    {
        return !entries.IsNullOrEmpty() && entries.All(x => src.HasFlag(x));
    }

    /// <summary>
    /// Set entries on the value
    /// </summary>
    /// <typeparam name="TEnum">The enum type</typeparam>
    /// <param name="src">The enum value</param>
    /// <param name="entries">The entries to set</param>
    /// <returns>The value with the entries set</returns>
    public static TEnum Set<TEnum>(this TEnum src, params TEnum[] entries) where TEnum : Enum
    {
        if (entries.IsNullOrEmpty())
        {
            return src;
        }

        var type = Enum.GetUnderlyingType(src.GetType());
        dynamic srcValue = Convert.ChangeType(src, type);

        foreach (var entry in entries)
        {
            dynamic entryValue = Convert.ChangeType(entry, type);
            srcValue |= entryValue;
        }

        return (TEnum)srcValue;
    }

    /// <summary>
    /// Unset entries on the value
    /// </summary>
    /// <typeparam name="TEnum">The enum type</typeparam>
    /// <param name="src">The enum value</param>
    /// <param name="entries">The entries to unset</param>
    /// <returns>The value with the entries unset</returns>
    public static TEnum UnSet<TEnum>(this TEnum src, params TEnum[] entries) where TEnum : Enum
    {
        if (entries.IsNullOrEmpty())
        {
            return src;
        }

        var type = Enum.GetUnderlyingType(src.GetType());
        dynamic srcValue = Convert.ChangeType(src, type);

        foreach (var entry in entries)
        {
            dynamic entryValue = Convert.ChangeType(entry, type);
            srcValue &= ~entryValue;
        }

        return (TEnum)srcValue;
    }
}