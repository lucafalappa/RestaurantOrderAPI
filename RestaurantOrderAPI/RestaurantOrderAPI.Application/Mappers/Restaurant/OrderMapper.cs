using RestaurantOrderAPI.Application.Mappers.Users;
using RestaurantOrderAPI.Application.Models.Dtos.Restaurant;
using RestaurantOrderAPI.Entities.Restaurant;

namespace RestaurantOrderAPI.Application.Mappers.Restaurant
{
    /// <summary>
    /// Provides methods for mapping between <see cref="Order"/> 
    /// and <see cref="OrderDto"/> objects
    /// </summary>
    public static class OrderMapper
    {
        /// <summary>
        /// Maps a <see cref="Order"/> entity 
        /// to a <see cref="OrderDto"/> object
        /// </summary>
        /// <param name="order">The order entity to map from
        /// </param>
        /// <param name="includeRelativeEntities">If set to true, 
        /// includes related entities in the mapping
        /// </param>
        /// <returns>A <see cref="OrderDto"/> object mapped 
        /// from the provided order entity
        /// </returns>
        public static OrderDto ToDto
            (Order order, bool includeRelativeEntities)
        {
            if (order == null)
            {
                return null;
            }
            var result = new OrderDto
            {
                IdOrder = order.IdOrder,
                OrderDate = order.OrderDate,
                DeliveryAddress = order.DeliveryAddress,
                TotalPrice = order.TotalPrice,
                IdUser = order.IdUser
            };
            if (includeRelativeEntities)
            {
                result.UserDto = UserMapper.ToDto
                    (order.User, false);
                result.DishDtos = order.Dishes.Select(d =>
                    DishMapper.ToDto(d, false)).ToList();
            }
            return result;
        }
        /// <summary>
        /// Maps a <see cref="OrderDto"/> object 
        /// to a <see cref="Order"/> entity
        /// </summary>
        /// <param name="orderDto">The order DTO to map from
        /// </param>
        /// <param name="includeRelativeEntities">If set to true, 
        /// includes related entities in the mapping
        /// </param>
        /// <returns>A <see cref="Order"/> entity mapped 
        /// from the provided order DTO
        /// </returns>
        public static Order ToEntity
            (OrderDto orderDto, bool includeRelativeEntities)
        {
            if (orderDto == null)
            {
                return null;
            }
            var result = new Order
            {
                IdOrder = orderDto.IdOrder,
                OrderDate = orderDto.OrderDate,
                DeliveryAddress = orderDto.DeliveryAddress,
                TotalPrice = orderDto.TotalPrice,
                IdUser = orderDto.IdUser
            };
            if (includeRelativeEntities)
            {
                result.User = UserMapper.ToEntity
                    (orderDto.UserDto, false);
                result.Dishes = orderDto.DishDtos.Select(d =>
                    DishMapper.ToEntity(d, false)).ToList();
            }
            return result;
        }
        /// <summary>
        /// Maps a list of <see cref="Order"/> entities 
        /// to a list of <see cref="OrderDto"/> objects
        /// </summary>
        /// <param name="orders">The list of order entities 
        /// to map from
        /// </param>
        /// <param name="includeRelativeEntities">If set to true, 
        /// includes related entities in the mapping
        /// </param>
        /// <returns>A list of <see cref="OrderDto"/> objects mapped 
        /// from the provided order entities
        /// </returns>
        public static List<OrderDto> ToDto
            (List<Order> orders, bool includeRelativeEntities)
        {
            if (orders == null)
            {
                return null;
            }
            return orders.Select(order =>
                ToDto(order, includeRelativeEntities)).ToList();
        }
        /// <summary>
        /// Maps a list of <see cref="OrderDto"/> objects 
        /// to a list of <see cref="Order"/> entities
        /// </summary>
        /// <param name="orderDtos">The list of order DTOs 
        /// to map from
        /// </param>
        /// <param name="includeRelativeEntities">If set to true, 
        /// includes related entities in the mapping
        /// </param>
        /// <returns>A list of <see cref="Order"/> entities mapped 
        /// from the provided order DTOs
        /// </returns>
        public static List<Order> ToEntity
            (List<OrderDto> orderDtos, bool includeRelativeEntities)
        {
            if (orderDtos == null)
            {
                return null;
            }
            return orderDtos.Select(orderDto =>
                ToEntity(orderDto, includeRelativeEntities)).ToList();
        }
    }
}
