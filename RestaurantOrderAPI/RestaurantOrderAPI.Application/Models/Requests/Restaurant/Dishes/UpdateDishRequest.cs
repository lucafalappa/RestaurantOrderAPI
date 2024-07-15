using RestaurantOrderAPI.Utilities;

namespace RestaurantOrderAPI.Application.Models.Requests.Restaurant.Dishes
{
    /// <summary>
    /// Represents a request to update a dish
    /// </summary>
    public class UpdateDishRequest
    {
        /// <summary>
        /// The new name of the dish
        /// </summary>
        public string Name { get; set; }
            = string.Empty;
        /// <summary>
        /// The new price of the dish
        /// </summary>
        public decimal Price { get; set; }
            = decimal.Zero;
        /// <summary>
        /// The new type of the dish
        /// </summary>
        public DishType Type { get; set; }
            = DishType.NoType;
    }
}
