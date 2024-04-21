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
    public class OrderRepository : GenericRepository<Order>
    {
        public OrderRepository
            (RestaurantOrderAPIDbContext context) : base(context)
        {

        }
        public async Task<bool> CreateOrderAsync
            (Order order, Predicate<Order> predicate)
        {
            return await AddEntityAsync(order, predicate);
        }
        public async Task<Order?> GetOrderByIdAsync(Guid id)
        {
            return await GetEntityAsync(id);
        }
        public async Task<List<Order>> GetAllOrderAsync
            (Predicate<Order> predicate)
        {
            return await GetAllEntityAsync(predicate);
        }
        public async Task<bool> UpdateOrderAsync
            (Order order, Predicate<Order> predicate)
        {
            return await ModifyEntityAsync(order, predicate);
        }
        public async Task<bool> DeleteOrderAsync(Guid id)
        {
            return await DeleteEntityAsync(id);
        }
        public async Task<bool> DeleteOrderAsync
            (Order order, Predicate<Order> predicate)
        {
            return await DeleteEntityAsync(order, predicate);
        }
    }
}
