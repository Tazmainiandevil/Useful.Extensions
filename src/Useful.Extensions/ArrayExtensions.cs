namespace Useful.Extensions;

public static class ArrayExtensions
{
    /// <summary>
    /// Gets the element from an array, safely.  We check the array is not null, and ensure we don't go out of bounds.
    /// The element returned can have an additional function applied, as long as that function takes in the type of element in the array
    /// and returns the same type after.
    /// </summary>
    /// <typeparam name="T"> The type used. </typeparam>
    /// <param name="arrayOfValues"> The array we are extracting the element from. </param>
    /// <param name="arrayLocation"> The index of the element in the array. </param>
    /// <param name="defaultElement"> The default value we will return if we can't get the element from the array. </param>
    /// <param name="additionalAction"> The additional function to be applied to the element before we return it. </param>
    /// <returns>
    /// The element found in the array, or the default element.
    /// </returns>
    public static T SafeGetElement<T>(this T[] arrayOfValues, int arrayLocation, T defaultElement = default, Func<T, T> additionalAction = null)
    {
        var result = defaultElement;

        if (arrayOfValues != null && arrayLocation >= 0 && arrayOfValues.Length > arrayLocation && arrayOfValues[arrayLocation] != null)
        {
            result = additionalAction == null
                ? arrayOfValues[arrayLocation]
                : additionalAction(arrayOfValues[arrayLocation]);
        }

        return result;
    }
}