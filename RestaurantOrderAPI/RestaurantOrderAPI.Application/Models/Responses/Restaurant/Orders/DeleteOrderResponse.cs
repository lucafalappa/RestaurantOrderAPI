using Newtonsoft.Json;
using RestaurantOrderAPI.Application.Models.Dtos.Restaurant;

namespace RestaurantOrderAPI.Application.Models.Responses.Orders
{
    /// <summary>
    /// Represents the response returned 
    /// after deleting an order
    /// </summary>
    public class DeleteOrderResponse
    {
        [JsonProperty("order")]
        /// <summary>
        /// The details of the deleted order
        /// </summary>
        public OrderDto OrderDto { get; set; }
            = null!;
        /// <summary>
        /// Initializes a new instance of the 
        /// <see cref="DeleteOrderResponse"/> class
        /// </summary>
        /// <param name="orderDto">The details of 
        /// the deleted order
        /// </param>
        public DeleteOrderResponse(OrderDto orderDto)
        {
            OrderDto = orderDto;
        }
    }
}
