using System;

namespace Zed.Extensions {
    /// <summary>
    /// <see cref="DateTime"/> extension methods
    /// </summary>
    public static class DateTimeExtensions {

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

    }
}
