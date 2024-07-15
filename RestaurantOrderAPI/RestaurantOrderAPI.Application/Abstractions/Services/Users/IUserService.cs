using RestaurantOrderAPI.Application.Models.Dtos.Users;
using RestaurantOrderAPI.Entities.Users;
using RestaurantOrderAPI.Utilities;
using System.Linq.Expressions;

namespace RestaurantOrderAPI.Application.Abstractions.Services.Users
{
    /// <summary>
    /// Interface for user service operations
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Asynchronously creates a new user 
        /// </summary>
        /// <param name="userDto">The user DTO to add
        /// </param>
        /// <param name="predicate">The predicate to check 
        /// for existing users
        /// </param>
        /// <returns>The created <see cref="UserDto"/> if successful, 
        /// null otherwise
        /// </returns>
        Task<UserDto?> CreateUserAsync
            (UserDto userDto, Predicate<User> predicate);
        /// <summary>
        /// Asynchronously gets a user by its unique identifier
        /// </summary>
        /// <param name="id">The unique identifier of the user
        /// </param>
        /// <returns>The <see cref="UserDto"/> if found, 
        /// null otherwise
        /// </returns>
        Task<UserDto?> GetUserByIdAsync(Guid id);
        /// <summary>
        /// Asynchronously updates an existing user
        /// </summary>
        /// <param name="userDto">The user DTO to update
        /// </param>
        /// <param name="predicate">The predicate to find 
        /// the existing user
        /// </param>
        /// <returns>The updated <see cref="UserDto"/> if successful, 
        /// null otherwise
        /// </returns>
        Task<UserDto?> UpdateUserAsync
            (UserDto userDto, Predicate<User> predicate);
        /// <summary>
        /// Asynchronously deletes a user by its unique identifier
        /// </summary>
        /// <param name="id">The unique identifier of the user
        /// </param>
        /// <returns>The deleted <see cref="UserDto"/> if successful, 
        /// null otherwise
        /// </returns>
        Task<UserDto?> DeleteUserAsync
            (Guid id);
        /// <summary>
        /// Asynchronously gets a list of users 
        /// based on a query and predicate
        /// </summary>
        /// <param name="queryBuilder">An optional 
        /// query builder function
        /// </param>
        /// <param name="predicate">A predicate to filter the users
        /// </param>
        /// <returns>A list of <see cref="UserDto"/> objects
        /// </returns>
        Task<List<UserDto>> GetUsersAsync
            (Func<IQueryable<User>, IQueryable<User>>? queryBuilder,
            Expression<Func<User, bool>> predicate);
        /// <summary>
        /// Gets a paginated list of users
        /// </summary>
        /// <param name="from">The starting index
        /// </param>
        /// <param name="num">The number of items to retrieve
        /// </param>
        /// <param name="predicate">A predicate to filter the users
        /// </param>
        /// <param name="queryCount">The total number of items 
        /// that match the predicate
        /// </param>
        /// <returns>A paginated list of <see cref="UserDto"/> objects
        /// </returns>
        List<UserDto> GetUsersWithPagination
            (int from, int num,
            Expression<Func<User, bool>> predicate, out int queryCount);
        /// <summary>
        /// Determines if the specified user role would result 
        /// in more than one administrator in the system
        /// </summary>
        /// <param name="userRole">The user role to check. 
        /// If null, only checks if an administrator already exists
        /// </param>
        /// <returns>A boolean indicating if the specified user role 
        /// is not unique as an administrator
        /// </returns>
        Task<bool> NotUniqueAdministrator
            (UserRole? userRole);
    }
}
