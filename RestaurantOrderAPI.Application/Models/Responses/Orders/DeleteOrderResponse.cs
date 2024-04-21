using RestaurantOrderAPI.Application.Models.Dtos;
using RestaurantOrderAPI.Domain.Entities;

namespace RestaurantOrderAPI.Application.Models.Responses.Orders
{
    public class DeleteOrderResponse
    {
        public OrderDto Order { get; set; }
            = null!;
        public DeleteOrderResponse(OrderDto orderDto)
        {
            Order = orderDto;
        }
    }
}
