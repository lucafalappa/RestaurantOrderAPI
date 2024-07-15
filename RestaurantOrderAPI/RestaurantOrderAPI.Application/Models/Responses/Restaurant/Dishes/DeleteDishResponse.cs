using Newtonsoft.Json;
using RestaurantOrderAPI.Application.Models.Dtos.Restaurant;

namespace RestaurantOrderAPI.Application.Models.Responses.Dishes
{
    /// <summary>
    /// Represents the response returned 
    /// after deleting a dish
    /// </summary>
    public class DeleteDishResponse
    {
        [JsonProperty("dish")]
        /// <summary>
        /// The details of the deleted dish
        /// </summary>
        public DishDto DishDto { get; set; }
            = null!;
        /// <summary>
        /// Initializes a new instance of the 
        /// <see cref="DeleteDishResponse"/> class
        /// </summary>
        /// <param name="dishDto">The details of 
        /// the deleted dish
        /// </param>
        public DeleteDishResponse(DishDto dishDto)
        {
            DishDto = dishDto;
        }
    }
}
