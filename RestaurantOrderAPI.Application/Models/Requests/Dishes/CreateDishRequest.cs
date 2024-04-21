using RestaurantOrderAPI.Domain.Entities;

namespace RestaurantOrderAPI.Application.Models.Requests.Dishes
{
    public class CreateDishRequest
    {
        public string Name { get; set; } 
            = string.Empty;
        public decimal Price { get; set; }
            = decimal.Zero;
        public DishType Type { get; set; }
            = DishType.NoType;
        public Dish ToEntity(Guid idOrder)
        {
            var dish = new Dish();
            dish.Name = Name;
            dish.Price = Price;
            dish.Type = Type;
            dish.IdOrder = idOrder;
            return dish;
        }
    }
}
