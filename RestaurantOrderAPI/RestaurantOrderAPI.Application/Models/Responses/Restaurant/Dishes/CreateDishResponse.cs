using Newtonsoft.Json;
using RestaurantOrderAPI.Application.Models.Dtos.Restaurant;

namespace RestaurantOrderAPI.Application.Models.Responses.Dishes
{
    /// <summary>
    /// Represents the response returned 
    /// after creating a dish
    /// </summary>
    public class CreateDishResponse
    {
        [JsonProperty("dish")]
        /// <summary>
        /// The details of the created dish
        /// </summary>
        public DishDto DishDto { get; set; }
            = null!;
        /// <summary>
        /// Initializes a new instance of the 
        /// <see cref="CreateDishResponse"/> class
        /// </summary>
        /// <param name="dishDto">The details of 
        /// the created dish
        /// </param>
        public CreateDishResponse(DishDto dishDto)
        {
            DishDto = dishDto;
        }
    }
}
