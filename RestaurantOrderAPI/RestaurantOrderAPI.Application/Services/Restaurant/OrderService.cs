using Microsoft.EntityFrameworkCore;
using RestaurantOrderAPI.Application.Abstractions.Services.Restaurant;
using RestaurantOrderAPI.Application.Mappers.Restaurant;
using RestaurantOrderAPI.Application.Models.Dtos.Restaurant;
using RestaurantOrderAPI.Entities.Restaurant;
using RestaurantOrderAPI.Infrastructure.Repositories.Restaurant;
using System.Linq.Expressions;

namespace RestaurantOrderAPI.Application.Services.Restaurant
{
    /// <summary>
    /// Service class for managing orders
    /// </summary>
    public class OrderService : IOrderService
    {
        /// <summary>
        /// The repository class for managing operations 
        /// related to <see cref="Order"/> entities
        /// </summary>
        private readonly OrderRepository _orderRepository;
        /// <summary>
        /// Initializes a new instance of the 
        /// <see cref="OrderService"/> class
        /// </summary>
        /// <param name="orderRepository">The repository for 
        /// managing order data
        /// </param>
        public OrderService
            (OrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
        public async Task<OrderDto?> CreateOrderAsync
            (OrderDto orderDto, Predicate<Order> predicate)
        {
            var order = OrderMapper.ToEntity(orderDto, true);
            var result = await _orderRepository.CreateOrderAsync
                (order, predicate);
            if (result)
            {
                await _orderRepository.SaveAsync();
                return orderDto;
            }
            return null;
        }
        public async Task<OrderDto?> GetOrderByIdAsync
            (Guid id)
        {
            Func<IQueryable<Order>, IQueryable<Order>>? queryBuilder = (query =>
                query.Include(order => order.User)
                     .Include(order => order.Dishes)
            );
            Expression<Func<Order, bool>> predicate = (order =>
                order.IdOrder.Equals(id));
            var orderDtos = await this.GetOrdersAsync
                (queryBuilder, predicate);
            var result = orderDtos.FirstOrDefault();
            return result;
        }
        public async Task<OrderDto?> UpdateOrderAsync
            (OrderDto orderDto, Predicate<Order> predicate)
        {
            var order = OrderMapper.ToEntity(orderDto, true);
            var result = await _orderRepository.UpdateOrderAsync(order, predicate);
            if (result)
            {
                await _orderRepository.SaveAsync();
                return orderDto;
            }
            return null;
        }
        public async Task<OrderDto?> DeleteOrderAsync
            (Guid id)
        {
            var orderDto = await this.GetOrderByIdAsync(id);
            if (orderDto != null)
            {
                await _orderRepository.DeleteOrderAsync(id);
                await _orderRepository.SaveAsync();
                return orderDto;
            }
            return null;
        }
        public async Task<List<OrderDto>> GetOrdersAsync
            (Func<IQueryable<Order>, IQueryable<Order>>? queryBuilder,
            Expression<Func<Order, bool>> predicate)
        {
            var result = await _orderRepository.GetOrdersAsync
                (queryBuilder, predicate);
            return OrderMapper.ToDto(result, true);
        }
        public List<OrderDto> GetOrdersWithPagination
            (int from, int num,
            Expression<Func<Order, bool>> predicate, out int queryCount)
        {
            var orders = _orderRepository.GetOrdersAsync
                (null, predicate).Result;
            var query = orders.AsQueryable();
            queryCount = query.Count();
            var result = query.OrderBy(order => order.IdOrder)
                              .Skip(from)
                              .Take(num)
                              .ToList();
            return OrderMapper.ToDto(result, true);
        }
        public decimal ApplyFullMealDiscount
            (OrderDto orderDto)
        {
            var defaultTotalPrice = orderDto.DishDtos.Sum(dish => dish.Price);
            var groups = orderDto.DishDtos.GroupBy(dish => dish.Type);
            if (groups.Count() != 4)
            {
                return defaultTotalPrice;
            }
            var sublists = groups.Select(groups => groups.ToList()).ToList();
            for (int i = 0; i < sublists.Count; i++)
            {
                sublists[i] = sublists[i]
                    .OrderByDescending(dish => dish.Price)
                    .ToList();
            }
            decimal newTotalPrice = 0m;
            foreach (var sublist in sublists)
            {
                newTotalPrice += sublist.First().Price * 0.9m;
                sublist.RemoveAt(0);
            }
            foreach (var sublist in sublists)
            {
                newTotalPrice += sublist.Sum(dish => dish.Price);
            }
            return newTotalPrice;
        }
    }
}
