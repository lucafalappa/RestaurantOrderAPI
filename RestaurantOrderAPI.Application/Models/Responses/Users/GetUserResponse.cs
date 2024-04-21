using RestaurantOrderAPI.Application.Models.Dtos;

namespace RestaurantOrderAPI.Application.Models.Responses.Users
{
    public class GetUserResponse
    {
        public UserDto User { get; set; } 
            = null!;
        public GetUserResponse(UserDto userDto)
        {
            User = userDto;
        }
    }
}
