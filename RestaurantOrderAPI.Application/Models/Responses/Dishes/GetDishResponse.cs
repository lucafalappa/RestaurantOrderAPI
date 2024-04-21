using RestaurantOrderAPI.Application.Models.Dtos;
using RestaurantOrderAPI.Domain.Entities;

namespace RestaurantOrderAPI.Application.Models.Responses.Dishes
{
    public class GetDishResponse
    {
        public DishDto Dish { get; set; }
            = null!;
        public GetDishResponse(DishDto dishDto)
        {
            Dish = dishDto;
        }
    }
}
