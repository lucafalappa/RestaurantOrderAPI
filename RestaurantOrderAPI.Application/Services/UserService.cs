using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using RestaurantOrderAPI.Application.Abstractions.Services;
using RestaurantOrderAPI.Application.Models.Requests.Users;
using RestaurantOrderAPI.Application.Options;
using RestaurantOrderAPI.Data.Repositories;
using RestaurantOrderAPI.Domain.Users;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RestaurantOrderAPI.Application.Services
{
    public class UserService : IUserService
    {
        private readonly UserRepository _userRepository;
        public UserService(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<bool> CreateUserAsync
            (User user, Predicate<User> predicate)
        {

            var result = await _userRepository.CreateUserAsync
                (user, predicate);
            await _userRepository.SaveAsync();
            return result;
        }
        public async Task<User> GetUserByIdAsync(Guid id)
        {
            var result = await _userRepository.GetUserByIdAsync(id);
            return result;
        }
        public async Task<List<User>> GetAllUserAsync
            (Predicate<User> predicate)
        {
            var result = await _userRepository.GetAllUserAsync
                (predicate);
            return result;
        }
        public async Task<bool> UpdateUserAsync
            (User user, Predicate<User> predicate)
        {
            var result = await _userRepository.UpdateUserAsync
                (user, predicate);
            await _userRepository.SaveAsync();
            return result;
        }
        public async Task<bool> DeleteUserAsync(Guid id)
        {
            var result = await _userRepository.DeleteUserAsync(id);
            await _userRepository.SaveAsync();
            return result;
        }
        public async Task<bool> DeleteUserAsync
            (User user, Predicate<User> predicate)
        {
            var result = await _userRepository.DeleteUserAsync
                (user, predicate);
            await _userRepository.SaveAsync();
            return result;
        }
        public async Task<bool> NotUniqueAdministrator
            (UserRole userRole)
        {
            return userRole.Equals(UserRole.Administrator) 
                && await _userRepository.IsAdministratorExisting();
        }
    }
}
