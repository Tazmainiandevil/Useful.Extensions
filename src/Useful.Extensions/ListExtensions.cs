namespace Useful.Extensions;

/// <summary>
/// Extensions for Lists
/// </summary>
public static class ListExtensions
{
    /// <summary>
    /// Combine multiple lists together
    /// </summary>
    /// <param name="src">The list to operate on</param>
    /// <param name="otherLists">The lists to add to the src</param>
    public static void Combine<T>(this List<T> src, params IEnumerable<T>[] otherLists)
    {
        if (src == null)
        {
            throw new ArgumentNullException(nameof(src), "The list cannot be null");
        }

        foreach (var element in otherLists)
        {
            src.AddRange(element);
        }
    }

    /// <summary>
    /// Add Many items to a list
    /// </summary>
    /// <param name="src">The list to operate on</param>
    /// <param name="items">The items to add to the src</param>
    public static void AddMany<T>(this List<T> src, params T[] items)
    {
        if (src == null)
        {
            throw new ArgumentNullException(nameof(src), "The list cannot be null");
        }

        src.AddRange(items);
    }
}