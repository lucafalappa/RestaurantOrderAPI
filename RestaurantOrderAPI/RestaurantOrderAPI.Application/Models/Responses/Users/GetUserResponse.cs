using Newtonsoft.Json;
using RestaurantOrderAPI.Application.Models.Dtos.Users;

namespace RestaurantOrderAPI.Application.Models.Responses.Users
{
    /// <summary>
    /// Represents the response returned 
    /// when retrieving a user
    /// </summary>
    public class GetUserResponse
    {
        [JsonProperty("user")]
        /// <summary>
        /// The details of the retrieved user
        /// </summary>
        public UserDto UserDto { get; set; }
            = null!;
        /// <summary>
        /// Initializes a new instance of the 
        /// <see cref="GetUserResponse"/> class
        /// </summary>
        /// <param name="userDto">The details of 
        /// the retrieved user
        /// </param>
        public GetUserResponse(UserDto userDto)
        {
            UserDto = userDto;
        }
    }
}
