using RestaurantOrderAPI.Domain.Entities;

namespace RestaurantOrderAPI.Application.Abstractions.Services
{
    public interface IDishService
    {
        bool CreateDish
            (Guid key, Dish dish, Predicate<Dish> predicate);
        Task<Dish?> GetDishByIdAsync(Guid key, Guid id);
        Task<List<Dish>> GetAllDishAsync
            (Guid key, Predicate<Dish> predicate);
        List<Dish> GetAllDish
            (Guid key, int from, int num, 
            Predicate<Dish> predicate, out int queryCount);
        Task<bool> UpdateDishAsync
            (Guid key, Dish dish, Predicate<Dish> predicate);
        Task<bool> DeleteDishAsync(Guid key, Guid id);
        Task<bool> DeleteDishAsync
            (Guid key, Dish dish, Predicate<Dish> predicate);
    }
}
