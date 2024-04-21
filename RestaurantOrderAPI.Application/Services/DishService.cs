using Microsoft.EntityFrameworkCore.Metadata.Internal;
using RestaurantOrderAPI.Application.Abstractions.Services;
using RestaurantOrderAPI.Application.Caching;
using RestaurantOrderAPI.Data.Repositories;
using RestaurantOrderAPI.Domain.Entities;

namespace RestaurantOrderAPI.Application.Services
{
    public class DishService : IDishService
    {
        private readonly DishRepository _dishRepository;
        public DishService(DishRepository dishRepository)
        {
            _dishRepository = dishRepository;
        }
        public bool CreateDish
            (Guid key, Dish dish, Predicate<Dish> predicate)
        {
            Cache<Dish>.Instance.Add(key, dish, predicate);
            return true;
        }
        public async Task<Dish?> GetDishByIdAsync(Guid key, Guid id)
        {
            var dishes = Cache<Dish>.Instance.Get(key);
            if (dishes != null)
            {
                var result = dishes.FirstOrDefault
                    (d => d.IdDish.Equals(id));
                if (result != null)
                {
                    return result;
                }
                else
                {
                    result = await _dishRepository.GetDishByIdAsync(id);
                    return result;
                }
            }
            return null;
        }
        public async Task<List<Dish>> GetAllDishAsync
            (Guid key, Predicate<Dish> predicate)
        {
            var result = Cache<Dish>.Instance.Get(key);
            if (result != null)
            {
                return result.FindAll(predicate);
            } else
            {
                result = await _dishRepository.GetAllDishAsync
                    (predicate);
                return result;
            }
        }
        public List<Dish> GetAllDish
            (Guid key, int from, int num, 
            Predicate<Dish> predicate, out int queryCount)
        {
            var result = Cache<Dish>.Instance.Get(key);
            if (result != null)
            {
                result = result.FindAll(predicate);
            }
            else
            {
                result = _dishRepository.GetAllDishAsync(predicate).Result;
            }
            var query = result.AsQueryable().Where(dish => predicate(dish));
            queryCount = query.Count();
            return query.OrderBy(dish => dish.Name)
                        .Skip(from)
                        .Take(num)
                        .ToList();
        }
        public async Task<bool> UpdateDishAsync
            (Guid key, Dish dish, Predicate<Dish> predicate)
        {
            var result = await _dishRepository.UpdateDishAsync(dish, predicate);
            if (result == true)
            {
                Cache<Dish>.Instance.Add(key, dish, predicate);
            }
            await _dishRepository.SaveAsync();
            return result;
        }
        public async Task<bool> DeleteDishAsync(Guid key, Guid id)
        {
            var dish = await GetDishByIdAsync(key, id);
            var result = await _dishRepository.DeleteDishAsync(id);
            Cache<Dish>.Instance.RemoveValue(key, dish);
            await _dishRepository.SaveAsync();
            return result;
        }
        public async Task<bool> DeleteDishAsync
            (Guid key, Dish dish, Predicate<Dish> predicate)
        {
            var result = await _dishRepository.DeleteDishAsync(dish, predicate);
            Cache<Dish>.Instance.RemoveValue(key, dish);
            await _dishRepository.SaveAsync();
            return result;
        }
    }
}
