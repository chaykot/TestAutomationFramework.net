using System;

namespace Infrastructure.Utils
{
    public static class DateTimeUtil
    {
        public static DateTime SetEndOfDay(this DateTime date)
        {
            return date.AddHours(23).AddMinutes(59).AddSeconds(59);
        }

        public static DateTime? ConvertDateToUtc(this DateTime? date)
        {
            return date == null ? date : TimeZoneInfo.ConvertTimeToUtc(date.Value);
        }

        public static TimeSpan GetSeconds(int seconds)
        {
            return new(0, 0, 0, seconds);
        }

        public static TimeSpan GetMilliseconds(int milliseconds)
        {
            return new(0, 0, 0, 0, milliseconds);
        }
    }
}