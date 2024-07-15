using System.Globalization;

namespace RestaurantOrderAPI.Domain.Extensions
{
    public static class DateTimeExtension
    {
        public static DateTime? ToNullableDateTime(this string dateString)
        {
            if (string.IsNullOrEmpty(dateString))
            {
                return null;
            }
            if (DateTime.TryParseExact(dateString, "yyyy-MM-dd",
                CultureInfo.InvariantCulture, DateTimeStyles.None,
                out DateTime parsedDate))
            {
                return parsedDate;
            }
            return null;
        }
    }
}
