using Newtonsoft.Json;
using RestaurantOrderAPI.Application.Models.Dtos.Restaurant;

namespace RestaurantOrderAPI.Application.Models.Responses.Orders
{
    /// <summary>
    /// Represents the response returned 
    /// after updating an existing order
    /// </summary>
    public class UpdateOrderResponse
    {
        [JsonProperty("order")]
        /// <summary>
        /// The details of the updated order
        /// </summary>
        public OrderDto OrderDto { get; set; }
            = null!;
        /// <summary>
        /// Initializes a new instance of the 
        /// <see cref="UpdateOrderResponse"/> class
        /// </summary>
        /// <param name="orderDto">The details of 
        /// the updated order
        /// </param>
        public UpdateOrderResponse(OrderDto orderDto)
        {
            OrderDto = orderDto;
        }
    }
}
