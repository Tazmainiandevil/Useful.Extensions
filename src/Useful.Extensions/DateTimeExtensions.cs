namespace Useful.Extensions;

/// <summary>
/// Extensions for the DateTime object.
/// </summary>
public static class DateTimeExtensions
{
    /// <summary>
    /// <para>
    /// An extension method that uses a fluent assertion example to check that a date is close to an expected date. I created this
    /// method to return an actual boolean value. The fluent assertions return a fluent assertion, not a boolean value.
    /// </para>
    /// <list type="number">
    /// <item><description> The low value is set to a negative value using the supplied plus or minus value.</description></item>
    /// <item><description> The high value is set to a positive value using the supplied plus or minus value.</description></item>
    /// </list>
    /// </summary>
    /// <param name="currentTime"> The current time being tested. </param>
    /// <param name="expectedTime"> The expected time. </param>
    /// <param name="secondsRange">
    /// The plus and minus value in seconds. To be added to the expected time to get the high and low values. The default is set to 10
    /// seconds.
    /// </param>
    /// <exception cref="ArgumentOutOfRangeException"> The resulting <see cref="DateTime" /> is less than <see cref="DateTime.MinValue" />
    /// or greater than <see cref="DateTime.MaxValue" /> . </exception>
    /// <returns>
    /// If the current date is close to the expected time.
    /// </returns>
    public static bool ShouldBeWithinRangeOf(this DateTime currentTime, DateTime expectedTime, int secondsRange = 10)
    {
        // Setting up the high and low date times.
        var lowDate = expectedTime.AddSeconds(-secondsRange);
        var highDate = expectedTime.AddSeconds(secondsRange);

        return currentTime > lowDate && currentTime < highDate;
    }

    /// <summary>
    /// Determines if the date time is between a specified start and end time.
    /// </summary>
    /// <param name="datetime"> The time to check. </param>
    /// <param name="startTime"> The start of the range. </param>
    /// <param name="endTime"> The end of the range. </param>
    /// <param name="timesInclusive"> If the start and end times are to be included; defaults to <see langword="false"/>. </param>
    /// <returns>
    /// <see langword="True"/> if the date time is between a specified start and end time, else <see langword="false"/>.
    /// </returns>
    public static bool Between(this DateTime datetime, DateTime startTime, DateTime endTime, bool timesInclusive = false)
    {
        return timesInclusive
            ? datetime >= startTime && datetime <= endTime
            : datetime > startTime && datetime < endTime;
    }

    /// <summary>
    /// Determines the current age based on the date of birth / creation supplied.
    /// </summary>
    /// <param name="dateOfBirth"> The date of birth / creation supplied for the person / object we are looking the age for. </param>
    /// <param name="assessmentDate"> The date that we want to use for the age check. </param>
    /// <returns>
    /// The age of the person or object.
    /// </returns>
    public static int GetAge(this DateTime dateOfBirth, DateTime assessmentDate = default)
    {
        var now = assessmentDate == default ? DateTime.Now : assessmentDate;
        var age = now.Year - dateOfBirth.Year;

        if (now.Month < dateOfBirth.Month || now.Month == dateOfBirth.Month && now.Day < dateOfBirth.Day) { age--; }

        return age;
    }
}