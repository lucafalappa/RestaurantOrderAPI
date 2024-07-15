using Newtonsoft.Json;
using RestaurantOrderAPI.Application.Models.Dtos.Users;

namespace RestaurantOrderAPI.Application.Models.Responses.Users
{
    /// <summary>
    /// Represents the response returned 
    /// after deleting a user
    /// </summary>
    public class DeleteUserResponse
    {
        [JsonProperty("user")]
        /// <summary>
        /// The details of the deleted user
        /// </summary>
        public UserDto UserDto { get; set; }
            = null!;
        /// <summary>
        /// Initializes a new instance of the 
        /// <see cref="DeleteUserResponse"/> class
        /// </summary>
        /// <param name="userDto">The details of 
        /// the deleted user
        /// </param>
        public DeleteUserResponse(UserDto userDto)
        {
            UserDto = userDto;
        }
    }
}
