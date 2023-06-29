namespace Useful.Extensions;

public static class NullableExtensions
{
    /// <summary>
    /// String representation of a nullable value
    /// </summary>
    /// <param name="src">The nullable</param>
    /// <returns>A string representation of the value or an empty string</returns>
    public static string ToStringOrEmpty<T>(this T? src) where T : struct
    {
        return src.HasValue ? src.Value.ToString() : string.Empty;
    }

    /// <summary>
    /// Checks if the nullable value is equal another value
    /// </summary>
    /// <param name="src">The nullable</param>
    /// <param name="compare">The value to compare</param>
    /// <returns>A boolean denoting if the values are the same</returns>
    public static bool IsEqual<T>(this T? src, T compare) where T : struct
    {
        return src.HasValue && src.Value.Equals(compare);
    }

    /// <summary>
    /// Check if the value is null or the default value
    /// </summary>
    /// <param name="src">The nullable</param>
    /// <returns>A boolean representing if the value is null or the default value</returns>
    public static bool IsNullOrDefault<T>(this T? src) where T : struct
    {
        return default(T).Equals(src.GetValueOrDefault());
    }

    /// <summary>
    /// Retrieves the value of the nullable or the default of the type if there is not a value or specified default value
    /// </summary>
    /// <param name="src">The nullable</param>
    /// <param name="defaultValue">(optional) A default to use if there is no value</param>
    /// <returns>The value or the default if there is no value</returns>
    public static T ValueOrDefault<T>(this T? src, T defaultValue = default(T)) where T : struct
    {
        if (src.HasValue)
        {
            return src.Value;
        }

        return defaultValue;
    }
}