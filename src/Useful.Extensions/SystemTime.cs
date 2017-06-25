using System;

namespace Useful.Extensions
{
    /// <summary>
    /// Class for getting the system time
    /// This allows for easy unit testing of Now and UtcNow
    /// Inspiration for this from https://ayende.com/blog/3408/dealing-with-time-in-tests
    /// </summary>
    public static class SystemTime
    {
        /// <summary>
        /// The current date/time
        /// </summary>
        public static Func<DateTime> Now = () => DateTime.Now;

        /// <summary>
        /// The current Utc date/time
        /// </summary>
        public static Func<DateTime> UtcNow = () => DateTime.UtcNow;
    }
}
