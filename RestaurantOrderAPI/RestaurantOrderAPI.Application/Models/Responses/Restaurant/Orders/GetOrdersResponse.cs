using Newtonsoft.Json;
using RestaurantOrderAPI.Application.Models.Dtos.Restaurant;

namespace RestaurantOrderAPI.Application.Models.Responses.Orders
{
    /// <summary>
    /// Represents the response returned when retrieving 
    /// a list of orders with pagination information
    /// </summary>
    public class GetOrdersResponse
    {
        /// <summary>
        /// The total number of pages available
        /// </summary>
        public int PagesNumber { get; set; }
            = 0;
        [JsonProperty("orders")]
        /// <summary>
        /// The list of order details
        /// </summary>
        public List<OrderDto> OrderDtos { get; set; }
            = null!;
        /// <summary>
        /// Initializes a new instance of the 
        /// <see cref="GetOrdersResponse"/> class
        /// </summary>
        public GetOrdersResponse(List<OrderDto> orderDtos)
        {
            OrderDtos = orderDtos;
        }
    }
}
