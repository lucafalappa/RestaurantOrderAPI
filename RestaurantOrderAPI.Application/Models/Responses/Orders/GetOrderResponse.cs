using RestaurantOrderAPI.Application.Models.Dtos;
using RestaurantOrderAPI.Domain.Entities;

namespace RestaurantOrderAPI.Application.Models.Responses.Orders
{
    public class GetOrderResponse
    {
        public OrderDto Order { get; set; }
            = null!;
        public GetOrderResponse(OrderDto orderDto)
        {
            Order = orderDto;
        }
    }
}
