using Newtonsoft.Json;
using RestaurantOrderAPI.Application.Models.Dtos.Users;
using RestaurantOrderAPI.Utilities;

namespace RestaurantOrderAPI.Application.Models.Requests.Users
{
    /// <summary>
    /// Represents a request to authenticate a user
    /// </summary>
    public class AuthenticateRequest
    {
        /// <summary>
        /// The email address of the user
        /// </summary>
        public string Email { get; set; }
            = string.Empty;
        /// <summary>
        /// The name of the user
        /// </summary>
        public string Name { get; set; }
            = string.Empty;
        /// <summary>
        /// The surname of the user
        /// </summary>
        public string Surname { get; set; }
            = string.Empty;
        /// <summary>
        /// The password of the user
        /// </summary>
        public string Password { get; set; }
            = string.Empty;
        /// <summary>
        /// The role (Administrator) of the user
        /// </summary>
        [JsonIgnore]
        public UserRole UserRole { get; set; }
            = UserRole.Administrator;
        /// <summary>
        /// Generates the <see cref="UserDto"/> instance 
        /// from a <see cref="AuthenticateRequest"/> instance
        /// </summary>
        /// <param name="idUser">The unique identifier 
        /// of the user
        /// </param>
        /// <returns>A <see cref="UserDto"/> 
        /// containing the user details
        /// </returns>
        public UserDto ToDto(Guid idUser)
        {
            var userDto = new UserDto();
            userDto.IdUser = idUser;
            userDto.Email = Email;
            userDto.Name = Name;
            userDto.Surname = Surname;
            userDto.Role = UserRole;
            return userDto;
        }
    }
}
