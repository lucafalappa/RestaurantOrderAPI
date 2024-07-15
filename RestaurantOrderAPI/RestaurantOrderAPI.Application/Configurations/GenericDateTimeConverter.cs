using Newtonsoft.Json.Converters;

namespace RestaurantOrderAPI.Application.Configurations
{
    /// <summary>
    /// Converter class for serializing and deserializing 
    /// DateTime objects to and from a specific ISO 8601 
    /// date and time format
    /// </summary>
    public class GenericDateTimeConverter
        : IsoDateTimeConverter
    {
        /// <summary>
        /// Initializes a new instance of the 
        /// <see cref="GenericDateTimeConverter"/> class
        /// and sets the DateTime format to 
        /// "yyyy-MM-ddTHH:mm:ss"
        /// </summary>
        public GenericDateTimeConverter()
        {
            base.DateTimeFormat = "yyyy-MM-ddTHH:mm:ss";
        }
    }
}
