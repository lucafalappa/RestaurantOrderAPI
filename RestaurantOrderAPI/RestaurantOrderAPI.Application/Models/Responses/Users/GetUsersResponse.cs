using Newtonsoft.Json;
using RestaurantOrderAPI.Application.Models.Dtos.Users;

namespace RestaurantOrderAPI.Application.Models.Responses.Users
{
    /// <summary>
    /// Represents the response returned when retrieving 
    /// a list of users with pagination information
    /// </summary>
    public class GetUsersResponse
    {
        /// <summary>
        /// The total number of pages available
        /// </summary>
        public int PagesNumber { get; set; }
            = 0;
        [JsonProperty("users")]
        /// <summary>
        /// The list of user details
        /// </summary>
        public List<UserDto> UserDtos { get; set; }
            = null!;
        /// <summary>
        /// Initializes a new instance of the 
        /// <see cref="GetUsersResponse"/> class
        /// </summary>
        public GetUsersResponse(List<UserDto> userDtos)
        {
            UserDtos = userDtos;
        }
    }
}
