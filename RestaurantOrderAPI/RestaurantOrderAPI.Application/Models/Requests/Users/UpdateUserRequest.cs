using Newtonsoft.Json;
using RestaurantOrderAPI.Utilities;

namespace RestaurantOrderAPI.Application.Models.Requests.Users
{
    /// <summary>
    /// Represents a request to update an existing user
    /// </summary>
    public class UpdateUserRequest
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
    }
}
