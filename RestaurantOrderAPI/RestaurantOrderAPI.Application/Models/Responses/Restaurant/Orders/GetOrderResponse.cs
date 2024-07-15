using Newtonsoft.Json;
using RestaurantOrderAPI.Application.Models.Dtos.Restaurant;

namespace RestaurantOrderAPI.Application.Models.Responses.Orders
{
    /// <summary>
    /// Represents the response returned 
    /// when retrieving an order
    /// </summary>
    public class GetOrderResponse
    {
        [JsonProperty("order")]
        /// <summary>
        /// The details of the retrieved order
        /// </summary>
        public OrderDto OrderDto { get; set; }
            = null!;
        /// <summary>
        /// Initializes a new instance of the 
        /// <see cref="GetOrderResponse"/> class
        /// </summary>
        /// <param name="orderDto">The details of 
        /// the retrieved order
        /// </param>
        public GetOrderResponse(OrderDto orderDto)
        {
            OrderDto = orderDto;
        }
    }
}
