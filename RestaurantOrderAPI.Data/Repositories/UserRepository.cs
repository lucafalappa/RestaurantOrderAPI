using Microsoft.EntityFrameworkCore;
using RestaurantOrderAPI.Data.Context;
using RestaurantOrderAPI.Domain.Entities;
using RestaurantOrderAPI.Domain.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantOrderAPI.Data.Repositories
{
    public class UserRepository : GenericRepository<User>
    {
        public UserRepository
            (RestaurantOrderAPIDbContext context) : base(context)
        {
            
        }
        public async Task<bool> CreateUserAsync
            (User user, Predicate<User> predicate)
        {
            return await AddEntityAsync(user, predicate);
        }
        public async Task<User?> GetUserByIdAsync(Guid id)
        {
            return await GetEntityAsync(id);
        }
        public async Task<List<User>> GetAllUserAsync
            (Predicate<User> predicate)
        {
            return await GetAllEntityAsync(predicate);
        }
        public async Task<bool> UpdateUserAsync
            (User user, Predicate<User> predicate)
        {
            return await ModifyEntityAsync(user, predicate);
        }
        public async Task<bool> DeleteUserAsync(Guid id)
        {
            return await DeleteEntityAsync(id);
        }
        public async Task<bool> DeleteUserAsync
            (User user, Predicate<User> predicate)
        {
            return await DeleteEntityAsync(user, predicate);
        }
        public async Task<bool> IsAdministratorExisting()
        {
            return await AnyEntitiesAsync(user =>
            user.Role.Equals(UserRole.Administrator));
        }
    }
}
