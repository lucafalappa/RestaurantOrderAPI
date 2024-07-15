using RestaurantOrderAPI.Application.Models.Dtos.Restaurant;
using RestaurantOrderAPI.Entities.Restaurant;

namespace RestaurantOrderAPI.Application.Mappers.Restaurant
{
    /// <summary>
    /// Provides methods for mapping between <see cref="Dish"/> 
    /// and <see cref="DishDto"/> objects
    /// </summary>
    public static class DishMapper
    {
        /// <summary>
        /// Maps a <see cref="Dish"/> entity 
        /// to a <see cref="DishDto"/> object
        /// </summary>
        /// <param name="dish">The dish entity to map from
        /// </param>
        /// <param name="includeRelativeEntities">If set to true, 
        /// includes related entities in the mapping
        /// </param>
        /// <returns>A <see cref="DishDto"/> object mapped 
        /// from the provided dish entity
        /// </returns>
        public static DishDto ToDto
            (Dish dish, bool includeRelativeEntities)
        {
            if (dish == null)
            {
                return null;
            }
            var result = new DishDto();
            result.IdDish = dish.IdDish;
            result.Name = dish.Name;
            result.Price = dish.Price;
            result.Type = dish.Type;
            result.IdOrder = dish.IdOrder;
            if (includeRelativeEntities)
            {
                result.OrderDto = OrderMapper.ToDto
                    (dish.Order, false);
            }
            return result;
        }
        /// <summary>
        /// Maps a <see cref="DishDto"/> object 
        /// to a <see cref="Dish"/> entity
        /// </summary>
        /// <param name="dishDto">The dish DTO to map from
        /// </param>
        /// <param name="includeRelativeEntities">If set to true, 
        /// includes related entities in the mapping
        /// </param>
        /// <returns>A <see cref="Dish"/> entity mapped 
        /// from the provided dish DTO
        /// </returns>
        public static Dish ToEntity
            (DishDto dishDto, bool includeRelativeEntities)
        {
            if (dishDto == null)
            {
                return null;
            }
            var result = new Dish();
            result.IdDish = dishDto.IdDish;
            result.Name = dishDto.Name;
            result.Price = dishDto.Price;
            result.Type = dishDto.Type;
            result.IdOrder = dishDto.IdOrder;
            if (includeRelativeEntities)
            {
                result.Order = OrderMapper.ToEntity
                    (dishDto.OrderDto, false);
            }
            return result;
        }
        /// <summary>
        /// Maps a list of <see cref="Dish"/> entities 
        /// to a list of <see cref="DishDto"/> objects
        /// </summary>
        /// <param name="dishes">The list of dish entities 
        /// to map from
        /// </param>
        /// <param name="includeRelativeEntities">If set to true, 
        /// includes related entities in the mapping
        /// </param>
        /// <returns>A list of <see cref="DishDto"/> objects mapped 
        /// from the provided dish entities
        /// </returns>
        public static List<DishDto> ToDto
            (List<Dish> dishes, bool includeRelativeEntities)
        {
            if (dishes == null)
            {
                return null;
            }
            return dishes.Select(dish =>
                ToDto(dish, includeRelativeEntities)).ToList();
        }
        /// <summary>
        /// Maps a list of <see cref="DishDto"/> objects 
        /// to a list of <see cref="Dish"/> entities
        /// </summary>
        /// <param name="dishDtos">The list of dish DTOs 
        /// to map from
        /// </param>
        /// <param name="includeRelativeEntities">If set to true, 
        /// includes related entities in the mapping
        /// </param>
        /// <returns>A list of <see cref="Dish"/> entities mapped 
        /// from the provided dish DTOs
        /// </returns>
        public static List<Dish> ToEntity
            (List<DishDto> dishDtos, bool includeRelativeEntities)
        {
            if (dishDtos == null)
            {
                return null;
            }
            return dishDtos.Select(dishDto =>
                ToEntity(dishDto, includeRelativeEntities)).ToList();
        }
    }
}
