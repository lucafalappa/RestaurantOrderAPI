using RestaurantOrderAPI.Entities.Restaurant;
using RestaurantOrderAPI.Infrastructure.Contexts;
using System.Linq.Expressions;

namespace RestaurantOrderAPI.Infrastructure.Repositories.Restaurant
{
    /// <summary>
    /// Repository class for managing operations 
    /// related to <see cref="Order"/> entities
    /// </summary>
    public class OrderRepository : GenericRepository<Order>
    {
        /// <summary>
        /// Initializes a new instance of the 
        /// <see cref="OrderRepository"/> class
        /// </summary>
        /// <param name="context">The database context
        /// to associate to the repository
        /// </param>
        public OrderRepository
            (RestaurantOrderAPIDbContext context)
            : base(context)
        {
        }
        /// <summary>
        /// Asynchronously adds a new order
        /// </summary>
        /// <param name="order">The order to add
        /// </param>
        /// <param name="predicate">The predicate to check 
        /// for existing orders
        /// </param>
        /// <returns>true if the order was added, 
        /// false otherwise
        /// </returns>
        public async Task<bool> CreateOrderAsync
            (Order order, Predicate<Order> predicate)
        {
            return await AddEntityAsync(order, predicate);
        }
        /// <summary>
        /// Asynchronously retrieves an order 
        /// by his unique identifier
        /// </summary>
        /// <param name="id">The unique identifier of the order
        /// </param>
        /// <returns>The order if is found, 
        /// null otherwise
        /// </returns>
        public async Task<Order?> GetOrderByIdAsync(Guid id)
        {
            return await GetEntityByIdAsync(id);
        }
        /// <summary>
        /// Asynchronously retrieves a list of orders 
        /// based on optional query builder and search conditions
        /// </summary>
        /// <param name="queryBuilder">Optional query builder
        /// </param>
        /// <param name="predicate">
        /// Optional predicate to filter orders
        /// </param>
        /// <returns>A list of orders
        /// </returns>
        public async Task<List<Order>> GetOrdersAsync
            (Func<IQueryable<Order>, IQueryable<Order>>? queryBuilder,
            Expression<Func<Order, bool>>? predicate)
        {
            return await GetEntitiesAsync
                (queryBuilder, predicate);
        }
        /// <summary>
        /// Asynchronously modifies an order, if a matching one 
        /// exists based on the provided conditions
        /// </summary>
        /// <param name="order">The order to modify
        /// </param>
        /// <param name="predicate">The predicate to find 
        /// the existing order
        /// </param>
        /// <returns>true if the order was modified, 
        /// false otherwise
        /// </returns>
        public async Task<bool> UpdateOrderAsync
            (Order order, Predicate<Order> predicate)
        {
            return await ModifyEntityAsync(order, predicate);
        }
        /// <summary>
        /// Asynchronously deletes an order
        /// by his unique identifier
        /// </summary>
        /// <param name="id">The unique identifier 
        /// of the order to delete
        /// </param>
        /// <returns>true if the order was deleted, 
        /// false otherwise
        /// </returns>
        public async Task<bool> DeleteOrderAsync(Guid id)
        {
            return await DeleteEntityAsync(id);
        }
    }
}
