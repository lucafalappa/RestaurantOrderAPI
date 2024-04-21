using RestaurantOrderAPI.Application.Models.Dtos;
using RestaurantOrderAPI.Domain.Entities;

namespace RestaurantOrderAPI.Application.Models.Requests.Orders
{
    public class UpdateOrderRequest
    {
        public string DeliveryAddress { get; set; } 
            = string.Empty;
    }
}
