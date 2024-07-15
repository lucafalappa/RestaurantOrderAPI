namespace RestaurantOrderAPI.Extensions
{
    /// <summary>
    /// Provides extension methods for working with enums
    /// </summary>
    public static class EnumExtension
    {
        /// <summary>
        /// Converts a string to the specified enum type, 
        /// or returns a default value if the conversion fails
        /// </summary>
        /// <typeparam name="T">The enum type</typeparam>
        /// <param name="value">The string value to convert</param>
        /// <param name="defaultValue">The default value to return 
        /// if the conversion fails
        /// </param>
        /// <returns>The converted enum value, or the default value 
        /// if the conversion fails
        /// </returns>
        public static T ToEnum<T>
            (this string value, T defaultValue)
            where T : struct
        {
            if (string.IsNullOrEmpty(value))
            {
                return defaultValue;
            }
            return Enum.TryParse(value, true, out T result)
                ? result : defaultValue;
        }
        /// <summary>
        /// Determines whether the specified value is 
        /// a valid value for the enum type
        /// </summary>
        /// <typeparam name="T">The enum type</typeparam>
        /// <param name="value">The value to check</param>
        /// <returns>true if the value is valid for the enum type, 
        /// false otherwise
        /// </returns>
        public static bool IsEnumValueValid<T>
            (this T value)
            where T : struct
        {
            return Enum.IsDefined(typeof(T), value);
        }
        /// <summary>
        /// Converts all values of the specified enum type to a list
        /// </summary>
        /// <typeparam name="T">The enum type</typeparam>
        /// <returns>A list of all values of the specified 
        /// enum type
        /// </returns>
        public static List<T> ToList<T>()
            where T : struct
        {
            return new List<T>((T[])Enum.GetValues(typeof(T)));
        }
    }
}
