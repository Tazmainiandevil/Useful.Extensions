namespace Useful.Extensions;

/// <summary>
/// Extensions for the char object
/// </summary>
public static class CharacterExtensions
{
    #region Equals

    /// <summary>
    /// Does one character equal another
    /// Default is case insensitive
    /// </summary>
    /// <param name="src">The character to perform the test on</param>
    /// <param name="compare">The character to compare to</param>
    /// <param name="caseCompare">(optional)The type of compare to use the default is to ignore case</param>
    /// <returns>A boolean denoting if the characters are equal depending on case (if selected)</returns>
    public static bool EqualTo(this char src, char compare, StringComparison caseCompare = StringComparison.OrdinalIgnoreCase)
    {
        return src.ToString().Equals(compare.ToString(), caseCompare);
    }

    #endregion Equals
}