using RestaurantOrderAPI.Application.Models.Dtos;
using RestaurantOrderAPI.Domain.Entities;

namespace RestaurantOrderAPI.Application.Models.Responses.Orders
{
    public class UpdateOrderResponse
    {
        public OrderDto Order { get; set; }
            = null!;
        public UpdateOrderResponse(OrderDto order)
        {
            Order = order;
        }
    }
}
