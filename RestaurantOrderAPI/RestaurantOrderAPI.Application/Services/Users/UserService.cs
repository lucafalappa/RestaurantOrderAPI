using Microsoft.EntityFrameworkCore;
using RestaurantOrderAPI.Application.Abstractions.Services.Users;
using RestaurantOrderAPI.Application.Mappers.Users;
using RestaurantOrderAPI.Application.Models.Dtos.Users;
using RestaurantOrderAPI.Entities.Users;
using RestaurantOrderAPI.Infrastructure.Repositories.Users;
using RestaurantOrderAPI.Utilities;
using System.Linq.Expressions;

namespace RestaurantOrderAPI.Application.Services.Users
{
    /// <summary>
    /// Service class for managing users
    /// </summary>
    public class UserService : IUserService
    {
        /// <summary>
        /// The repository class for managing operations 
        /// related to <see cref="User"/> entities
        /// </summary>
        private readonly UserRepository _userRepository;
        /// <summary>
        /// Initializes a new instance of the 
        /// <see cref="UserService"/> class
        /// </summary>
        /// <param name="userRepository">The repository for 
        /// managing user data
        /// </param>
        public UserService
            (UserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<UserDto?> CreateUserAsync
            (UserDto userDto, Predicate<User> predicate)
        {
            var user = UserMapper.ToEntity(userDto, true);
            var result = await _userRepository.CreateUserAsync
                (user, predicate);
            if (result)
            {
                await _userRepository.SaveAsync();
                return userDto;
            }
            return null;
        }
        public async Task<UserDto?> GetUserByIdAsync
            (Guid id)
        {
            Func<IQueryable<User>, IQueryable<User>>? queryBuilder = (query =>
                query.Include(user => user.Orders)
            );
            Expression<Func<User, bool>> predicate = (user =>
                user.IdUser.Equals(id));
            var userDtos = await this.GetUsersAsync
                (queryBuilder, predicate);
            var result = userDtos.FirstOrDefault();
            return result;
        }
        public async Task<UserDto?> UpdateUserAsync
            (UserDto userDto, Predicate<User> predicate)
        {
            var user = UserMapper.ToEntity(userDto, true);
            var result = await _userRepository.UpdateUserAsync
                (user, predicate);
            if (result)
            {
                await _userRepository.SaveAsync();
                return userDto;
            }
            return null;
        }
        public async Task<UserDto?> DeleteUserAsync
            (Guid id)
        {
            var user = await _userRepository.GetUserByIdAsync(id);
            if (user != null)
            {
                var result = await _userRepository.DeleteUserAsync(id);
                if (result)
                {
                    await _userRepository.SaveAsync();
                    return UserMapper.ToDto(user, true);
                }
            }
            return null;
        }
        public async Task<List<UserDto>> GetUsersAsync
            (Func<IQueryable<User>, IQueryable<User>>? queryBuilder,
            Expression<Func<User, bool>> predicate)
        {
            var result = await _userRepository.GetUsersAsync
                (queryBuilder, predicate);
            return UserMapper.ToDto(result, true);
        }
        public List<UserDto> GetUsersWithPagination
            (int from, int num,
            Expression<Func<User, bool>> predicate, out int queryCount)
        {
            var users = _userRepository.GetUsersAsync
                (null, predicate).Result;
            var query = users.AsQueryable();
            queryCount = query.Count();
            var result = query.OrderBy(user => user.Name)
                              .Skip(from)
                              .Take(num)
                              .ToList();
            return UserMapper.ToDto(result, true);
        }
        public async Task<bool> NotUniqueAdministrator
            (UserRole? userRole)
        {
            bool result = await _userRepository.IsAdministratorExisting();
            if (userRole != null)
            {
                result = result && userRole.Equals(UserRole.Administrator);
            }
            return result;
        }
    }
}
