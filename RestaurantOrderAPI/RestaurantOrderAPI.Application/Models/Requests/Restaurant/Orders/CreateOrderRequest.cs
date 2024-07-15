using RestaurantOrderAPI.Application.Models.Dtos.Restaurant;
using RestaurantOrderAPI.Application.Models.Requests.Restaurant.Dishes;

namespace RestaurantOrderAPI.Application.Models.Requests.Restaurant.Orders
{
    /// <summary>
    /// Represents a request to create a new order
    /// </summary>
    public class CreateOrderRequest
    {
        /// <summary>
        /// The delivery address of the order
        /// </summary>
        public string DeliveryAddress { get; set; }
            = string.Empty;
        /// <summary>
        /// The list of dishes of the order
        /// </summary>
        public List<CreateDishRequest> Dishes { get; set; }
            = new List<CreateDishRequest>();
        /// <summary>
        /// Generates the <see cref="OrderDto"/> instance
        /// from a <see cref="CreateOrderRequest"/> instance
        /// </summary>
        /// <returns>A <see cref="OrderDto"/> 
        /// containing the order details
        /// </returns>
        public OrderDto ToDto()
        {
            var orderDto = new OrderDto();
            orderDto.OrderDate = DateTime.Now;
            orderDto.DeliveryAddress = this.DeliveryAddress;
            foreach (var dish in Dishes)
            {
                var dishDto = dish.ToDto();
                dishDto.IdOrder = orderDto.IdOrder;
                dishDto.OrderDto = orderDto;
                orderDto.DishDtos.Add(dishDto);
            }
            return orderDto;
        }
    }
}
