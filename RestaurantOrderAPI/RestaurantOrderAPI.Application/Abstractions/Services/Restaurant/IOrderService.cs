using RestaurantOrderAPI.Application.Models.Dtos.Restaurant;
using RestaurantOrderAPI.Entities.Restaurant;
using System.Linq.Expressions;

namespace RestaurantOrderAPI.Application.Abstractions.Services.Restaurant
{
    /// <summary>
    /// Interface for order service operations
    /// </summary>
    public interface IOrderService
    {
        /// <summary>
        /// Asynchronously creates a new order
        /// </summary>
        /// <param name="orderDto">The order DTO to add
        /// </param>
        /// <param name="predicate">The predicate to check
        /// for existing orders
        /// </param>
        /// <returns>The created <see cref="OrderDto"/> if successful,
        /// null otherwise
        /// </returns>
        Task<OrderDto?> CreateOrderAsync
            (OrderDto orderDto, Predicate<Order> predicate);
        /// <summary>
        /// Asynchronously gets an order by its unique identifier
        /// </summary>
        /// <param name="id">The unique identifier of the order
        /// </param>
        /// <returns>The <see cref="OrderDto"/> if found,
        /// null otherwise
        /// </returns>
        Task<OrderDto?> GetOrderByIdAsync
            (Guid id);
        /// <summary>
        /// Asynchronously updates an existing order
        /// </summary>
        /// <param name="orderDto">The order DTO to update
        /// </param>
        /// <param name="predicate">The predicate to find
        /// the existing order
        /// </param>
        /// <returns>The updated <see cref="OrderDto"/> if successful,
        /// null otherwise
        /// </returns>
        Task<OrderDto?> UpdateOrderAsync
            (OrderDto orderDto, Predicate<Order> predicate);
        /// <summary>
        /// Asynchronously deletes an order by its unique identifier
        /// </summary>
        /// <param name="id">The unique identifier of the order
        /// </param>
        /// <returns>The deleted <see cref="OrderDto"/> if successful,
        /// null otherwise
        /// </returns>
        Task<OrderDto?> DeleteOrderAsync
            (Guid id);
        /// <summary>
        /// Asynchronously gets a list of orders
        /// based on a query and predicate
        /// </summary>
        /// <param name="queryBuilder">An optional
        /// query builder function
        /// </param>
        /// <param name="predicate">A predicate to filter the orders
        /// </param>
        /// <returns>A list of <see cref="OrderDto"/> objects
        /// </returns>
        Task<List<OrderDto>> GetOrdersAsync
            (Func<IQueryable<Order>, IQueryable<Order>>? queryBuilder,
            Expression<Func<Order, bool>> predicate);
        /// <summary>
        /// Gets a paginated list of orders
        /// </summary>
        /// <param name="from">The starting index
        /// </param>
        /// <param name="num">The number of items to retrieve
        /// </param>
        /// <param name="predicate">A predicate to filter the orders
        /// </param>
        /// <param name="queryCount">The total number of items
        /// that match the predicate
        /// </param>
        /// <returns>A paginated list of <see cref="OrderDto"/> objects
        /// </returns>
        List<OrderDto> GetOrdersWithPagination
            (int from, int num,
            Expression<Func<Order, bool>> predicate, out int queryCount);
        /// <summary>
        /// Applies a full meal discount to the given order
        /// </summary>
        /// <param name="orderDto">The given order DTO
        /// </param>
        /// <returns>The total price after applying 
        /// the discount if applicable, 
        /// the default total price otherwise
        /// </returns>
        decimal ApplyFullMealDiscount(OrderDto orderDto);
    }
}
