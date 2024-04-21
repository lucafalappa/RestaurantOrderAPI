using RestaurantOrderAPI.Application.Abstractions.Services;
using RestaurantOrderAPI.Application.Caching;
using RestaurantOrderAPI.Data.Repositories;
using RestaurantOrderAPI.Domain.Entities;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace RestaurantOrderAPI.Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly OrderRepository _orderRepository;
        public OrderService
            (OrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
        public async Task<bool> CreateOrderAsync
            (Guid key, Order order, Predicate<Order> predicate)
        {
            order.OrderedDishes = Cache<Dish>.Instance.Get(order.IdOrder);
            if (order.OrderedDishes == null) { return false; }
            var result = await _orderRepository.CreateOrderAsync
                (order, predicate);
            if (result == true)
            {
                Cache<Order>.Instance.Add(key, order, predicate);
            }
            await _orderRepository.SaveAsync();
            return result;
        }
        public async Task<Order?> GetOrderByIdAsync(Guid key, Guid id)
        {
            var orders = Cache<Order>.Instance.Get(key);
            if (orders != null)
            {
                var result = orders.FirstOrDefault(d => d.IdOrder.Equals(id));
                if (result != null)
                {
                    return result;
                }
                else
                {
                    result = await _orderRepository.GetOrderByIdAsync(id);
                    return result;
                }
            }
            return null;
        }
        public async Task<List<Order>> GetAllOrderAsync
            (Guid key, Predicate<Order> predicate)
        {
            var result = Cache<Order>.Instance.Get(key);
            if (result != null)
            {
                return result.FindAll(predicate);
            }
            else
            {
                result = await _orderRepository.GetAllOrderAsync(predicate);
                return result;
            }
        }
        public List<Order> GetAllOrder
            (Guid key, int from, int num, 
            Predicate<Order> predicate, out int queryCount)
        {
            var result = Cache<Order>.Instance.Get(key);
            if (result != null)
            {
                result = result.FindAll(predicate);
            }
            else
            {
                result = _orderRepository.GetAllOrderAsync(predicate).Result;
            }
            var query = result.AsQueryable().Where(order => predicate(order));
            queryCount = query.Count();
            return query.OrderBy(order => order.IdOrder)
                        .Skip(from)
                        .Take(num)
                        .ToList();
        }
        //TODO : DUPLICATED CODE
        public List<Order> GetAllOrder
            (int from, int num, Predicate<Order> predicate, out int queryCount)
        {
            List<Order> result = new List<Order>();
            if (Cache<Order>.Instance.GetKeys().Any())
            {
                result = Cache<Order>.Instance.GetKeys()
                                              .SelectMany(key => 
                                              Cache<Order>.Instance.Get(key))
                                              .ToList();
                result = result.FindAll(predicate);
            }
            else
            {
                result = _orderRepository.GetAllOrderAsync(predicate).Result;
            }
            var query = result.AsQueryable();
            queryCount = query.Count();
            return query.OrderBy(order => order.IdOrder)
                        .Skip(from)
                        .Take(num)
                        .ToList();
        }
        public async Task<List<Order>> GetAllOrderAsync
            (Predicate<Order> predicate)
        {
            var result = await _orderRepository.GetAllOrderAsync(predicate);
            return result;
        }
        public async Task<bool> UpdateOrderAsync
            (Guid key, Order order, Predicate<Order> predicate)
        {
            var result = await _orderRepository.UpdateOrderAsync
                (order, predicate);
            if (result == true)
            {
                Cache<Order>.Instance.Add(key, order, predicate);
            }
            await _orderRepository.SaveAsync();
            return result;
        }
        public async Task<bool> DeleteOrderAsync(Guid key, Guid id)
        {
            var order = await _orderRepository.GetOrderByIdAsync(id);
            var result = await _orderRepository.DeleteOrderAsync(id);
            if (result == true)
            {
                Cache<Order>.Instance.RemoveValue(key, order);
                Cache<Dish>.Instance.RemoveKey(id);
            }
            await _orderRepository.SaveAsync();
            return result;
        }
        public async Task<bool> DeleteOrderAsync
            (Guid key, Order order, Predicate<Order> predicate)
        {
            var result = await _orderRepository.DeleteOrderAsync
                (order, predicate);
            if (result == true)
            {
                Cache<Order>.Instance.RemoveValue(key, order);
                Cache<Dish>.Instance.RemoveKey(order.IdOrder);
            }
            await _orderRepository.SaveAsync();
            return result;
        }
    }
}
