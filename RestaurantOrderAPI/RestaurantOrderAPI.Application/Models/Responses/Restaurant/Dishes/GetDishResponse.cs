using Newtonsoft.Json;
using RestaurantOrderAPI.Application.Models.Dtos.Restaurant;

namespace RestaurantOrderAPI.Application.Models.Responses.Dishes
{
    /// <summary>
    /// Represents the response returned 
    /// when retrieving a dish
    /// </summary>
    public class GetDishResponse
    {
        [JsonProperty("dish")]
        /// <summary>
        /// The details of the retrieved dish
        /// </summary>
        public DishDto DishDto { get; set; }
            = null!;
        /// <summary>
        /// Initializes a new instance of the 
        /// <see cref="GetDishResponse"/> class
        /// </summary>
        /// <param name="dishDto">The details of 
        /// the retrieved dish
        /// </param>
        public GetDishResponse(DishDto dishDto)
        {
            DishDto = dishDto;
        }
    }
}
