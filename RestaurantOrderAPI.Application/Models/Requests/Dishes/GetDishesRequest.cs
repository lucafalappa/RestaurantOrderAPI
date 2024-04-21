using RestaurantOrderAPI.Domain.Entities;

namespace RestaurantOrderAPI.Application.Models.Requests.Dishes
{
    public class GetDishesRequest
    {
        public string Name { get; set; } 
            = string.Empty;
        public decimal Price { get; set; } 
            = decimal.Zero;
        public DishType Type { get; set; } 
            = DishType.NoType;
    }
}
