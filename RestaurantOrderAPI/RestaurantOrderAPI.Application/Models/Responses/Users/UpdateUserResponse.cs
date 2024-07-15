using Newtonsoft.Json;
using RestaurantOrderAPI.Application.Models.Dtos.Users;

namespace RestaurantOrderAPI.Application.Models.Responses.Users
{
    /// <summary>
    /// Represents the response returned 
    /// after updating an existing user
    /// </summary>
    public class UpdateUserResponse
    {
        [JsonProperty("user")]
        /// <summary>
        /// The details of the updated user
        /// </summary>
        public UserDto UserDto { get; set; }
            = null!;
        /// <summary>
        /// Initializes a new instance of the 
        /// <see cref="UpdateUserResponse"/> class
        /// </summary>
        /// <param name="userDto">The details of 
        /// the updated user
        /// </param>
        public UpdateUserResponse(UserDto userDto)
        {
            UserDto = userDto;
        }
    }
}
