using RestaurantOrderAPI.Entities.Users;
using RestaurantOrderAPI.Infrastructure.Contexts;
using RestaurantOrderAPI.Utilities;
using System.Linq.Expressions;

namespace RestaurantOrderAPI.Infrastructure.Repositories.Users
{
    /// <summary>
    /// Repository class for managing operations 
    /// related to <see cref="User"/> entities
    /// </summary>
    public class UserRepository : GenericRepository<User>
    {
        /// <summary>
        /// Initializes a new instance of the 
        /// <see cref="UserRepository"/> class
        /// </summary>
        /// <param name="context">The database context
        /// to associate to the repository
        /// </param>
        public UserRepository
            (RestaurantOrderAPIDbContext context)
            : base(context)
        {
        }
        /// <summary>
        /// Asynchronously adds a new user
        /// </summary>
        /// <param name="user">The user to add
        /// </param>
        /// <param name="predicate">The predicate to check 
        /// for existing users
        /// </param>
        /// <returns>true if the user was added, 
        /// false otherwise
        /// </returns>
        public async Task<bool> CreateUserAsync
            (User user, Predicate<User> predicate)
        {
            return await AddEntityAsync(user, predicate);
        }
        /// <summary>
        /// Asynchronously retrieves a user 
        /// by his unique identifier
        /// </summary>
        /// <param name="id">The unique identifier of the user
        /// </param>
        /// <returns>The user if is found, 
        /// null otherwise
        /// </returns>
        public async Task<User?> GetUserByIdAsync(Guid id)
        {
            return await GetEntityByIdAsync(id);
        }
        /// <summary>
        /// Asynchronously retrieves a list of users 
        /// based on optional query builder and search conditions
        /// </summary>
        /// <param name="queryBuilder">Optional query builder
        /// </param>
        /// <param name="predicate">
        /// Optional predicate to filter users
        /// </param>
        /// <returns>A list of users
        /// </returns>
        public async Task<List<User>> GetUsersAsync
            (Func<IQueryable<User>, IQueryable<User>>? queryBuilder,
            Expression<Func<User, bool>>? predicate)
        {
            return await GetEntitiesAsync(queryBuilder, predicate);
        }
        /// <summary>
        /// Asynchronously modifies a user, if a matching one 
        /// exists based on the provided conditions
        /// </summary>
        /// <param name="user">The user to modify
        /// </param>
        /// <param name="predicate">The predicate to find 
        /// the existing user
        /// </param>
        /// <returns>true if the user was modified, 
        /// false otherwise
        /// </returns>
        public async Task<bool> UpdateUserAsync
            (User user, Predicate<User> predicate)
        {
            return await ModifyEntityAsync(user, predicate);
        }
        /// <summary>
        /// Asynchronously deletes a user
        /// by his unique identifier
        /// </summary>
        /// <param name="id">The unique identifier 
        /// of the user to delete
        /// </param>
        /// <returns>true if the user was deleted, 
        /// false otherwise
        /// </returns>
        public async Task<bool> DeleteUserAsync(Guid id)
        {
            return await DeleteEntityAsync(id);
        }
        /// <summary>
        /// Asynchronously checks if an administrator user 
        /// exists in the repository
        /// </summary>
        /// <returns>true if an administrator user exists, 
        /// false otherwise
        /// </returns>
        public async Task<bool> IsAdministratorExisting()
        {
            return await AnyEntitiesAsync(user =>
            user.Role.Equals(UserRole.Administrator));
        }
    }
}
