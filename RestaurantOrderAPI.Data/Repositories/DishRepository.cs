using RestaurantOrderAPI.Data.Context;
using RestaurantOrderAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantOrderAPI.Data.Repositories
{
    public class DishRepository : GenericRepository<Dish>
    {
        public DishRepository
            (RestaurantOrderAPIDbContext context) : base(context)
        {

        }
        public async Task<bool> CreateDishAsync
            (Dish dish, Predicate<Dish> predicate)
        {
            return await AddEntityAsync(dish, predicate);
        }
        public async Task<Dish?> GetDishByIdAsync(Guid id)
        {
            return await GetEntityAsync(id);
        }
        public async Task<List<Dish>> GetAllDishAsync
            (Predicate<Dish> predicate)
        {
            return await GetAllEntityAsync(predicate);
        }
        public async Task<bool> UpdateDishAsync
            (Dish dish, Predicate<Dish> predicate)
        {
            return await ModifyEntityAsync(dish, predicate);
        }
        public async Task<bool> DeleteDishAsync(Guid id)
        {
            return await DeleteEntityAsync(id);
        }
        public async Task<bool> DeleteDishAsync
            (Dish dish, Predicate<Dish> predicate)
        {
            return await DeleteEntityAsync(dish, predicate);
        }
    }
}
