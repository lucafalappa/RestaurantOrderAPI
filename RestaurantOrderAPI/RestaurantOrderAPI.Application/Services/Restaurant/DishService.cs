using RestaurantOrderAPI.Application.Abstractions.Services.Restaurant;
using RestaurantOrderAPI.Application.Mappers.Restaurant;
using RestaurantOrderAPI.Application.Models.Dtos.Restaurant;
using RestaurantOrderAPI.Entities.Restaurant;
using RestaurantOrderAPI.Infrastructure.Repositories.Restaurant;
using System.Linq.Expressions;

namespace RestaurantOrderAPI.Application.Services.Restaurant
{
    /// <summary>
    /// Service class for managing dishes
    /// </summary>
    public class DishService : IDishService
    {
        /// <summary>
        /// The repository class for managing operations 
        /// related to <see cref="Dish"/> entities
        /// </summary>
        private readonly DishRepository _dishRepository;
        /// <summary>
        /// Initializes a new instance of the 
        /// <see cref="DishService"/> class
        /// </summary>
        /// <param name="dishRepository">The repository for 
        /// managing dish data
        /// </param>
        public DishService
            (DishRepository dishRepository)
        {
            _dishRepository = dishRepository;
        }
        public async Task<DishDto?> CreateDishAsync
            (DishDto dishDto, Predicate<Dish> predicate)
        {
            var dish = DishMapper.ToEntity(dishDto, true);
            var result = await _dishRepository.CreateDishAsync
                (dish, predicate);
            if (result)
            {
                await _dishRepository.SaveAsync();
                return dishDto;
            }
            return null;
        }
        public async Task<DishDto?> GetDishByIdAsync
            (Guid id)
        {
            Func<IQueryable<Dish>, IQueryable<Dish>>? queryBuilder = null;
            Expression<Func<Dish, bool>> predicate = (dish =>
                dish.IdDish.Equals(id));
            var dishDtos = await this.GetDishesAsync
                (queryBuilder, predicate);
            var result = dishDtos.FirstOrDefault();
            return result;
        }
        public async Task<DishDto?> UpdateDishAsync
            (DishDto dishDto, Predicate<Dish> predicate)
        {
            var dish = DishMapper.ToEntity(dishDto, true);
            var result = await _dishRepository.UpdateDishAsync
                (dish, predicate);
            if (result)
            {
                await _dishRepository.SaveAsync();
                return dishDto;
            }
            return null;
        }
        public async Task<DishDto?> DeleteDishAsync
            (Guid id)
        {
            var dish = await _dishRepository.GetDishByIdAsync(id);
            if (dish != null)
            {
                await _dishRepository.DeleteDishAsync(id);
                await _dishRepository.SaveAsync();
                return DishMapper.ToDto(dish, true);
            }
            return null;
        }
        public async Task<List<DishDto>> GetDishesAsync
            (Func<IQueryable<Dish>, IQueryable<Dish>>? queryBuilder,
            Expression<Func<Dish, bool>> predicate)
        {
            var result = await _dishRepository.GetDishesAsync
                (queryBuilder, predicate);
            return DishMapper.ToDto(result, true);
        }
        public List<DishDto> GetDishesWithPagination
            (int from, int num,
            Expression<Func<Dish, bool>> predicate, out int queryCount)
        {
            var dishes = _dishRepository.GetDishesAsync
                (null, predicate).Result;
            var query = dishes.AsQueryable();
            queryCount = query.Count();
            var result = query.OrderBy(dish => dish.Name)
                              .Skip(from)
                              .Take(num)
                              .ToList();
            return DishMapper.ToDto(result, true);
        }
    }
}
