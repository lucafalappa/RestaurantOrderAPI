using RestaurantOrderAPI.Application.Models.Dtos;
using RestaurantOrderAPI.Domain.Entities;

namespace RestaurantOrderAPI.Application.Models.Responses.Orders
{
    public class GetOrdersResponse
    {
        public int PagesNumber { get; set; } 
            = 0;
        public List<OrderDto> Orders { get; set; } 
            = null!;
        public GetOrdersResponse() { }
    }
}
