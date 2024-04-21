using RestaurantOrderAPI.Application.Models.Dtos;
using RestaurantOrderAPI.Domain.Entities;

namespace RestaurantOrderAPI.Application.Models.Requests.Orders
{
    public class CreateOrderRequest
    {
        public string DeliveryAddress { get; set; } 
            = string.Empty;
        public Order ToEntity(Guid idOrder)
        {
            var order = new Order();
            order.IdOrder = idOrder;
            order.OrderDate = DateTime.Now;
            order.DeliveryAddress = DeliveryAddress;
            return order;
        }
    }
}
