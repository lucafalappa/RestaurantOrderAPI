using RestaurantOrderAPI.Application.Models.Dtos;
using RestaurantOrderAPI.Domain.Entities;

namespace RestaurantOrderAPI.Application.Models.Responses.Dishes
{
    public class GetDishesResponse
    {
        public int PagesNumber { get; set; } 
            = 0;
        public List<DishDto> Dishes { get; set; } 
            = null!;
        public GetDishesResponse() { }
    }
}
