using RestaurantOrderAPI.Application.Models.Dtos.Restaurant;
using RestaurantOrderAPI.Entities.Restaurant;
using System.Linq.Expressions;

namespace RestaurantOrderAPI.Application.Abstractions.Services.Restaurant
{
    /// <summary>
    /// Interface for dish service operations
    /// </summary>
    public interface IDishService
    {
        /// <summary>
        /// Asynchronously creates a new dish 
        /// </summary>
        /// <param name="dishDto">The dish DTO to add
        /// </param>
        /// <param name="predicate">The predicate to check 
        /// for existing dishes
        /// </param>
        /// <returns>The created <see cref="DishDto"/> if successful, 
        /// null otherwise
        /// </returns>
        Task<DishDto?> CreateDishAsync
            (DishDto dishDto, Predicate<Dish> predicate);
        /// <summary>
        /// Asynchronously gets a dish by its unique identifier
        /// </summary>
        /// <param name="id">The unique identifier of the dish
        /// </param>
        /// <returns>The <see cref="DishDto"/> if found, 
        /// null otherwise
        /// </returns>
        Task<DishDto?> GetDishByIdAsync
            (Guid id);
        /// <summary>
        /// Asynchronously updates an existing dish
        /// </summary>
        /// <param name="dishDto">The dish DTO to update
        /// </param>
        /// <param name="predicate">The predicate to find 
        /// the existing dish
        /// </param>
        /// <returns>The updated <see cref="DishDto"/> if successful, 
        /// null otherwise
        /// </returns>
        Task<DishDto?> UpdateDishAsync
            (DishDto dishDto, Predicate<Dish> predicate);
        /// <summary>
        /// Asynchronously deletes a dish by its unique identifier
        /// </summary>
        /// <param name="id">The unique identifier of the dish
        /// </param>
        /// <returns>The deleted <see cref="DishDto"/> if successful, 
        /// null otherwise
        /// </returns>
        Task<DishDto?> DeleteDishAsync
            (Guid id);
        /// <summary>
        /// Asynchronously gets a list of dishes 
        /// based on a query and predicate
        /// </summary>
        /// <param name="queryBuilder">An optional 
        /// query builder function
        /// </param>
        /// <param name="predicate">A predicate to filter the dishes
        /// </param>
        /// <returns>A list of <see cref="DishDto"/> objects
        /// </returns>
        Task<List<DishDto>> GetDishesAsync
            (Func<IQueryable<Dish>, IQueryable<Dish>>? queryBuilder,
            Expression<Func<Dish, bool>> predicate);
        /// <summary>
        /// Gets a paginated list of dishes
        /// </summary>
        /// <param name="from">The starting index
        /// </param>
        /// <param name="num">The number of items to retrieve
        /// </param>
        /// <param name="predicate">A predicate to filter the dishes
        /// </param>
        /// <param name="queryCount">The total number of items 
        /// that match the predicate
        /// </param>
        /// <returns>A paginated list of <see cref="DishDto"/> objects
        /// </returns>
        List<DishDto> GetDishesWithPagination
            (int from, int num,
            Expression<Func<Dish, bool>> predicate, out int queryCount);
    }
}
