using RestaurantOrderAPI.Application.Models.Dtos;
using RestaurantOrderAPI.Domain.Entities;

namespace RestaurantOrderAPI.Application.Models.Responses.Dishes
{
    public class DeleteDishResponse
    {
        public DishDto Dish { get; set; }
            = null!;
        public DeleteDishResponse(DishDto dishDto)
        {
            Dish = dishDto;
        }
    }
}
