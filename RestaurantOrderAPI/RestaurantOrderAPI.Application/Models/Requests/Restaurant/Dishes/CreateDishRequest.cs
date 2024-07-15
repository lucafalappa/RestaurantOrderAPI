using RestaurantOrderAPI.Application.Models.Dtos.Restaurant;
using RestaurantOrderAPI.Utilities;

namespace RestaurantOrderAPI.Application.Models.Requests.Restaurant.Dishes
{
    /// <summary>
    /// Represents a request to create a new dish
    /// </summary>
    public class CreateDishRequest
    {
        /// <summary>
        /// The name of the dish
        /// </summary>
        public string Name { get; set; }
            = string.Empty;
        /// <summary>
        /// The price of the dish
        /// </summary>
        public decimal Price { get; set; }
            = decimal.Zero;
        /// <summary>
        /// The type of the dish
        /// </summary>
        public DishType Type { get; set; }
            = DishType.NoType;
        /// <summary>
        /// Generates the <see cref="DishDto"/> instance
        /// from a <see cref="CreateDishRequest"/> instance
        /// </summary>
        /// <returns>A <see cref="DishDto"/> 
        /// containing the dish details
        /// </returns>
        public DishDto ToDto()
        {
            var dishDto = new DishDto();
            dishDto.Name = this.Name;
            dishDto.Price = this.Price;
            dishDto.Type = this.Type;
            return dishDto;
        }
    }
}
