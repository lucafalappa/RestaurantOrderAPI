using RestaurantOrderAPI.Domain.Entities;

namespace RestaurantOrderAPI.Application.Abstractions.Services
{
    public interface IOrderService
    {
        Task<bool> CreateOrderAsync
            (Guid key, Order order, Predicate<Order> predicate);
        Task<Order> GetOrderByIdAsync(Guid key, Guid id);
        Task<List<Order>> GetAllOrderAsync
            (Guid key, Predicate<Order> predicate);
        List<Order> GetAllOrder
            (Guid key, int from, int num, 
            Predicate<Order> predicate, out int queryCount);
        List<Order> GetAllOrder
            (int from, int num,
            Predicate<Order> predicate, out int queryCount);
        Task<bool> UpdateOrderAsync
            (Guid key, Order order, Predicate<Order> predicate);
        Task<bool> DeleteOrderAsync(Guid key, Guid id);
        Task<bool> DeleteOrderAsync
            (Guid key, Order order, Predicate<Order> predicate);
    }
}
