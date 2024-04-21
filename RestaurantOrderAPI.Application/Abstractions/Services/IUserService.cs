using RestaurantOrderAPI.Application.Models.Requests.Users;
using RestaurantOrderAPI.Domain.Users;

namespace RestaurantOrderAPI.Application.Abstractions.Services
{
    public interface IUserService
    {
        Task<bool> CreateUserAsync
            (User user, Predicate<User> predicate);
        Task<User> GetUserByIdAsync
            (Guid id);
        Task<List<User>> GetAllUserAsync
            (Predicate<User> predicate);
        //TODO : CONSIDER IMPLEMENTING GETALLUSER WITH
        //SEARCH PARAMS AND PAGINATION
        Task<bool> UpdateUserAsync
            (User user, Predicate<User> predicate);
        Task<bool> DeleteUserAsync
            (Guid id);
        Task<bool> DeleteUserAsync
            (User user, Predicate<User> predicate);
        Task<bool> NotUniqueAdministrator
            (UserRole userRole);
    }
}
