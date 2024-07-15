using RestaurantOrderAPI.Application.Models.Dtos.Users;
using RestaurantOrderAPI.Application.Models.Requests.Users;

namespace RestaurantOrderAPI.Application.Abstractions.Services
{
    /// <summary>
    /// Interface for token service operations
    /// </summary>
    public interface ITokenService
    {
        /// <summary>
        /// Creates a JWT token for the specified user 
        /// and authentication request
        /// </summary>
        /// <param name="idUser">The user unique identifier
        /// </param>
        /// <param name="request">The authentication request
        /// </param>
        /// <returns>The generated JWT token
        /// </returns>
        string CreateToken
            (Guid idUser, AuthenticateRequest request);
        /// <summary>
        /// Creates a JWT token for the specified user 
        /// and registration request
        /// </summary>
        /// <param name="idUser">The user unique identifier
        /// </param>
        /// <param name="request">The registration request
        /// </param>
        /// <returns>The generated JWT token
        /// </returns>
        string CreateToken
            (Guid idUser, RegisterRequest request);
        /// <summary>
        /// Creates a JWT token for the specified user DTO
        /// </summary>
        /// <param name="userDto">The user DTO
        /// </param>
        /// <returns>The generated JWT token
        /// </returns>
        string CreateToken
            (UserDto userDto);
    }
}
