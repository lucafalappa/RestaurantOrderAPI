using Newtonsoft.Json;
using RestaurantOrderAPI.Application.Models.Dtos.Restaurant;

namespace RestaurantOrderAPI.Application.Models.Responses.Dishes
{
    /// <summary>
    /// Represents the response returned when retrieving 
    /// a list of dishes with pagination information
    /// </summary>
    public class GetDishesResponse
    {
        /// <summary>
        /// The total number of pages available
        /// </summary>
        public int PagesNumber { get; set; }
            = 0;
        [JsonProperty("dishes")]
        /// <summary>
        /// The list of dish details
        /// </summary>
        public List<DishDto> DishDtos { get; set; }
            = null!;
        /// <summary>
        /// Initializes a new instance of the 
        /// <see cref="GetDishesResponse"/> class
        /// </summary>
        public GetDishesResponse(List<DishDto> dishDtos)
        {
            DishDtos = dishDtos;
        }
    }
}
