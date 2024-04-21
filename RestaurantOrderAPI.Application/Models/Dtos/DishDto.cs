using RestaurantOrderAPI.Domain.Entities;
using System.Text.Json.Serialization;

namespace RestaurantOrderAPI.Application.Models.Dtos
{
    public class DishDto
    {
        public Guid IdDish { get; set; } 
            = Guid.Empty;
        public string Name { get; set; } 
            = string.Empty;
        public decimal Price { get; set; } 
            = decimal.Zero;
        public DishType Type { get; set; } 
            = DishType.NoType;
        public DishDto()
        {

        }
        public DishDto(Dish dish)
        {
            IdDish = dish.IdDish;
            Name = dish.Name;
            Price = dish.Price;
            Type = dish.Type;
        }
        public static List<Dish> FromDtoToEntity
            (List<DishDto> dtos, Guid idOrder)
        {
            var result = new List<Dish>();
            foreach (var dto in dtos)
            {
                result.Add(new Dish()
                {
                    IdDish = dto.IdDish,
                    Name = dto.Name,
                    Price = dto.Price,
                    Type = dto.Type,
                    IdOrder = idOrder
                });
            }
            return result;
        }
        public static List<DishDto> FromEntityToDto
            (List<Dish> dishes)
        {
            if (dishes != null)
            {
                var result = new List<DishDto>();
                foreach (var dish in dishes)
                {
                    result.Add(new DishDto()
                    {
                        IdDish = dish.IdDish,
                        Name = dish.Name,
                        Price = dish.Price,
                        Type = dish.Type,
                    });
                }
                return result;
            }
            return new List<DishDto>();
        }
    }
}
