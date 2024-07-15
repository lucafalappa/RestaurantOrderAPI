using Newtonsoft.Json;
using RestaurantOrderAPI.Application.Models.Dtos.Restaurant;

namespace RestaurantOrderAPI.Application.Models.Responses.Orders
{
    /// <summary>
    /// Represents the response returned 
    /// after creating an order
    /// </summary>
    public class CreateOrderResponse
    {
        [JsonProperty("order")]
        /// <summary>
        /// The details of the created order
        /// </summary>
        public OrderDto OrderDto { get; set; }
            = null!;
        /// <summary>
        /// Initializes a new instance of the 
        /// <see cref="CreateOrderResponse"/> class
        /// </summary>
        /// <param name="orderDto">The details of 
        /// the created order
        /// </param>
        /// <param name="totalPrice">The total price
        /// of the created order</param>
        public CreateOrderResponse(OrderDto orderDto)
        {
            OrderDto = orderDto;
        }
    }
}
