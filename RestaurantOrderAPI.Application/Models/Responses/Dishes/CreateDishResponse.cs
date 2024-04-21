using RestaurantOrderAPI.Application.Models.Dtos;
using RestaurantOrderAPI.Domain.Entities;

namespace RestaurantOrderAPI.Application.Models.Responses.Dishes
{
    public class CreateDishResponse
    {
        public DishDto Dish { get; set; } 
            = null!;
        public CreateDishResponse(DishDto dishDto)
        {
            Dish = dishDto;
        }
    }
}
