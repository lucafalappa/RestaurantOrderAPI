using RestaurantOrderAPI.Application.Models.Dtos;
using RestaurantOrderAPI.Domain.Entities;

namespace RestaurantOrderAPI.Application.Models.Responses.Dishes
{
    public class UpdateDishResponse
    {
        public DishDto Dish { get; set; }
            = null!;
        public UpdateDishResponse(DishDto dishDto)
        {
            Dish = dishDto;
        }
    }
}
