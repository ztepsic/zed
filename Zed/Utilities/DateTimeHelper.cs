using System;

namespace Zed.Utilities {
    /// <summary>
    /// <see cref="DateTime"/> extension methods
    /// </summary>
    public static class DateTimeHelper {

        /// <summary>
        /// Converts DateTime from UTC to destination time zone
        /// </summary>
        /// <param name="dateTimeUtc">DateTime in UTC</param>
        /// <param name="destinationTimeZoneInfo">Destination time zone</param>
        /// <returns>Converted DateTime in destination time zone</returns>
        public static DateTime ConvertFromUtc(this DateTime dateTimeUtc, TimeZoneInfo destinationTimeZoneInfo) => TimeZoneInfo.ConvertTimeFromUtc(dateTimeUtc, destinationTimeZoneInfo);

        /// <summary>
        /// Converts DateTime from source time zone to UTC
        /// </summary>
        /// <param name="dateTime">DateTime in source time zone</param>
        /// <param name="sourceTimeZoneInfo">Source time zone</param>
        /// <returns>Converted DateTime in UTC</returns>
        public static DateTime ConvertToUtc(this DateTime dateTime, TimeZoneInfo sourceTimeZoneInfo) => TimeZoneInfo.ConvertTimeToUtc(dateTime, sourceTimeZoneInfo);

        /// <summary>
        /// Converts DateTime from source time zone to destination time zone
        /// </summary>
        /// <param name="dateTime">DateTime to be converted</param>
        /// <param name="sourceTimeZoneInfo">Source time zone</param>
        /// <param name="destinationTimeZoneInfo">Source time zone</param>
        /// <returns>Converted DateTime in destination time zone</returns>
        public static DateTime Convert(this DateTime dateTime, TimeZoneInfo sourceTimeZoneInfo, TimeZoneInfo destinationTimeZoneInfo) => TimeZoneInfo.ConvertTime(dateTime, sourceTimeZoneInfo, destinationTimeZoneInfo);

        /// <summary>
        /// This method returns the number of seconds in Unix time. 
        /// The number of seconds that have elapsed since 1970-01-01T00:00:00Z
        /// </summary>
        /// <param name="dateTime">DateTime to convert from</param>
        /// <returns>The number of seconds that have elapsed since 1970-01-01T00:00:00Z.</returns>
        /// <remarks>
        /// Unix time represents the number of seconds that have elapsed since 1970-01-01T00:00:00Z (January 1, 1970, at 12:00 AM UTC).
        /// It does not take leap seconds into account.
        /// 
        /// This method first converts the current instance to UTC before returning its Unix time.
        /// For date and time values before 1970-01-01T00:00:00Z, this method returns a negative value.
        /// </remarks>
        public static long ToUnixTimeSeconds(this DateTime dateTime) => ((DateTimeOffset)dateTime).ToUnixTimeSeconds();

        /// <summary>
        /// Converts a Unix time expressed as the number of seconds that have elapsed since 1970-01-01T00:00:00Z
        /// to a UTC DateTime value.
        /// </summary>
        /// <param name="seconds">
        /// A Unix time, expressed as the number of seconds that have elapsed since 1970-01-01T00:00:00Z (January 1, 1970, at 12:00 AM UTC).
        /// For Unix times before this date, its value is negative.
        /// </param>
        /// <returns>A date and time value that represents the same moment in time as the Unix time.</returns>
        public static DateTime FromUnixTimeSeconds(long seconds) => DateTimeOffset.FromUnixTimeSeconds(seconds).UtcDateTime;

        /// <summary>
        /// This method returns the number of milliseconds in Unix time. 
        /// The number of milliseconds that have elapsed since 1970-01-01T00:00:00.000Z.
        /// </summary>
        /// <param name="dateTime">DateTime to convert from</param>
        /// <returns>The number of milliseconds that have elapsed since 1970-01-01T00:00:00.000Z.</returns>
        /// <remarks>
        /// Unix time represents the number of seconds that have elapsed since 1970-01-01T00:00:00Z (January 1, 1970, at 12:00 AM UTC).
        /// It does not take leap seconds into account. This method returns the number of milliseconds in Unix time.
        /// 
        /// This method first converts the current instance to UTC before returning the number of milliseconds in its Unix time.
        /// For date and time values before 1970-01-01T00:00:00Z, this method returns a negative value.
        /// </remarks>
        public static long ToUnixTimeMilliseconds(this DateTime dateTime) => ((DateTimeOffset)dateTime).ToUnixTimeMilliseconds();

        /// <summary>
        /// Converts a Unix time expressed as the number of milliseconds that have elapsed since 1970-01-01T00:00:00Z
        /// to a UTC DateTime value.
        /// </summary>
        /// <param name="miliseconds">
        /// A Unix time, expressed as the number of milliseconds that have elapsed since 1970-01-01T00:00:00Z (January 1, 1970, at 12:00 AM UTC).
        /// For Unix times before this date, its value is negative.
        /// </param>
        /// <returns>A date and time value that represents the same moment in time as the Unix time.</returns>
        public static DateTime FromUnixTimeMiliseconds(long miliseconds) => DateTimeOffset.FromUnixTimeMilliseconds(miliseconds).UtcDateTime;

    }
}
