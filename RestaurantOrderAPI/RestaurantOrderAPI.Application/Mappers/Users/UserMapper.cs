using RestaurantOrderAPI.Application.Mappers.Restaurant;
using RestaurantOrderAPI.Application.Models.Dtos.Users;
using RestaurantOrderAPI.Entities.Users;

namespace RestaurantOrderAPI.Application.Mappers.Users
{
    /// <summary>
    /// Provides methods for mapping between <see cref="User"/> 
    /// and <see cref="UserDto"/> objects
    /// </summary>
    public static class UserMapper
    {
        /// <summary>
        /// Maps a <see cref="User"/> entity 
        /// to a <see cref="UserDto"/> object
        /// </summary>
        /// <param name="user">The user entity to map from
        /// </param>
        /// <param name="includeRelativeEntities">If set to true, 
        /// includes related entities in the mapping
        /// </param>
        /// <returns>A <see cref="UserDto"/> object mapped 
        /// from the provided user entity
        /// </returns>
        public static UserDto ToDto
            (User user, bool includeRelativeEntities)
        {
            if (user == null)
            {
                return null;
            }
            var result = new UserDto
            {
                IdUser = user.IdUser,
                Email = user.Email,
                Name = user.Name,
                Surname = user.Surname,
                Password = user.Password,
                Role = user.Role
            };
            if (includeRelativeEntities)
            {
                result.OrderDtos = user.Orders.Select(o =>
                    OrderMapper.ToDto(o, false)).ToList();
            }

            return result;
        }
        /// <summary>
        /// Maps a <see cref="UserDto"/> object 
        /// to a <see cref="User"/> entity
        /// </summary>
        /// <param name="userDto">The user DTO to map from
        /// </param>
        /// <param name="includeRelativeEntities">If set to true, 
        /// includes related entities in the mapping
        /// </param>
        /// <returns>A <see cref="User"/> entity mapped 
        /// from the provided user DTO
        /// </returns>
        public static User ToEntity
            (UserDto userDto, bool includeRelativeEntities)
        {
            if (userDto == null)
            {
                return null;
            }
            var result = new User
            {
                IdUser = userDto.IdUser,
                Email = userDto.Email,
                Name = userDto.Name,
                Surname = userDto.Surname,
                Password = userDto.Password,
                Role = userDto.Role
            };
            if (includeRelativeEntities)
            {
                result.Orders = userDto.OrderDtos.Select(o =>
                    OrderMapper.ToEntity(o, false)).ToList();
            }
            return result;
        }
        /// <summary>
        /// Maps a list of <see cref="User"/> entities 
        /// to a list of <see cref="UserDto"/> objects
        /// </summary>
        /// <param name="users">The list of user entities 
        /// to map from
        /// </param>
        /// <param name="includeRelativeEntities">If set to true, 
        /// includes related entities in the mapping
        /// </param>
        /// <returns>A list of <see cref="UserDto"/> objects mapped 
        /// from the provided user entities
        /// </returns>
        public static List<UserDto> ToDto
            (List<User> users, bool includeRelativeEntities)
        {
            if (users == null)
            {
                return null;
            }
            return users.Select(user =>
                ToDto(user, includeRelativeEntities)).ToList();
        }
        /// <summary>
        /// Maps a list of <see cref="UserDto"/> objects 
        /// to a list of <see cref="User"/> entities
        /// </summary>
        /// <param name="userDtos">The list of user DTOs 
        /// to map from
        /// </param>
        /// <param name="includeRelativeEntities">If set to true, 
        /// includes related entities in the mapping
        /// </param>
        /// <returns>A list of <see cref="User"/> entities mapped 
        /// from the provided user DTOs
        /// </returns>
        public static List<User> ToEntity
            (List<UserDto> userDtos, bool includeRelativeEntities)
        {
            if (userDtos == null)
            {
                return null;
            }
            return userDtos.Select(userDto =>
                ToEntity(userDto, includeRelativeEntities)).ToList();
        }
    }
}
