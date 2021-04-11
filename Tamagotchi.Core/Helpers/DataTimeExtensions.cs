using System;

namespace Tamagotchi.Core.Helpers
{
    public static class DataTimeExtensions
    {
        public static DateTime Flatten(this DateTime dateTime)
        {
            return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, dateTime.Minute,
                dateTime.Second);
        }
    }
}