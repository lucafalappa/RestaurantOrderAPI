using Newtonsoft.Json;
using RestaurantOrderAPI.Application.Models.Dtos.Restaurant;

namespace RestaurantOrderAPI.Application.Models.Responses.Dishes
{
    /// <summary>
    /// Represents the response returned 
    /// after updating an existing dish
    /// </summary>
    public class UpdateDishResponse
    {
        [JsonProperty("dish")]
        /// <summary>
        /// The details of the updated dish
        /// </summary>
        public DishDto DishDto { get; set; }
            = null!;
        /// <summary>
        /// Initializes a new instance of the 
        /// <see cref="UpdateDishResponse"/> class
        /// </summary>
        /// <param name="dishDto">The details of 
        /// the updated dish
        /// </param>
        public UpdateDishResponse(DishDto dishDto)
        {
            DishDto = dishDto;
        }
    }
}
