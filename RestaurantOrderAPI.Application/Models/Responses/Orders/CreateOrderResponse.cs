using RestaurantOrderAPI.Application.Models.Dtos;
using RestaurantOrderAPI.Domain.Entities;

namespace RestaurantOrderAPI.Application.Models.Responses.Orders
{
    public class CreateOrderResponse
    {
        public OrderDto Order { get; set; } 
            = null!;
        public decimal DiscountedPrice { get; set; }
        public CreateOrderResponse(OrderDto orderDto, decimal discountedPrice)
        {
            Order = orderDto;
            DiscountedPrice = discountedPrice;
        }
    }
}
