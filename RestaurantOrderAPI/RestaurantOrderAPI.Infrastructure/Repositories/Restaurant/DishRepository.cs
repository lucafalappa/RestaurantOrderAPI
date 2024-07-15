using RestaurantOrderAPI.Entities.Restaurant;
using RestaurantOrderAPI.Infrastructure.Contexts;
using System.Linq.Expressions;

namespace RestaurantOrderAPI.Infrastructure.Repositories.Restaurant
{
    /// <summary>
    /// Repository class for managing operations 
    /// related to <see cref="Dish"/> entities
    /// </summary>
    public class DishRepository : GenericRepository<Dish>
    {
        /// <summary>
        /// Initializes a new instance of the 
        /// <see cref="DishRepository"/> class
        /// </summary>
        /// <param name="context">The database context
        /// to associate to the repository
        /// </param>
        public DishRepository
            (RestaurantOrderAPIDbContext context)
            : base(context)
        {
        }
        /// <summary>
        /// Asynchronously adds a new dish
        /// </summary>
        /// <param name="dish">The dish to add
        /// </param>
        /// <param name="predicate">The predicate to check 
        /// for existing dishes
        /// </param>
        /// <returns>true if the dish was added, 
        /// false otherwise
        /// </returns>
        public async Task<bool> CreateDishAsync
            (Dish dish, Predicate<Dish> predicate)
        {
            return await AddEntityAsync(dish, predicate);
        }
        /// <summary>
        /// Asynchronously retrieves a dish 
        /// by his unique identifier
        /// </summary>
        /// <param name="id">The unique identifier of the dish
        /// </param>
        /// <returns>The dish if is found, 
        /// null otherwise
        /// </returns>
        public async Task<Dish?> GetDishByIdAsync(Guid id)
        {
            return await GetEntityByIdAsync(id);
        }
        /// <summary>
        /// Asynchronously retrieves a list of dishes 
        /// based on optional query builder and search conditions
        /// </summary>
        /// <param name="queryBuilder">Optional query builder
        /// </param>
        /// <param name="predicate">
        /// Optional predicate to filter dishes
        /// </param>
        /// <returns>A list of dishes
        /// </returns>
        public async Task<List<Dish>> GetDishesAsync
            (Func<IQueryable<Dish>, IQueryable<Dish>>? queryBuilder,
            Expression<Func<Dish, bool>>? predicate)
        {
            return await GetEntitiesAsync
                (queryBuilder, predicate);
        }
        /// <summary>
        /// Asynchronously modifies a dish, if a matching one 
        /// exists based on the provided conditions
        /// </summary>
        /// <param name="dish">The dish to modify
        /// </param>
        /// <param name="predicate">The predicate to find 
        /// the existing dish
        /// </param>
        /// <returns>true if the dish was modified, 
        /// false otherwise
        /// </returns>
        public async Task<bool> UpdateDishAsync
            (Dish dish, Predicate<Dish> predicate)
        {
            return await ModifyEntityAsync(dish, predicate);
        }
        /// <summary>
        /// Asynchronously deletes a dish
        /// by his unique identifier
        /// </summary>
        /// <param name="id">The unique identifier 
        /// of the dish to delete
        /// </param>
        /// <returns>true if the dish was deleted, 
        /// false otherwise
        /// </returns>
        public async Task<bool> DeleteDishAsync(Guid id)
        {
            return await DeleteEntityAsync(id);
        }
    }
}
