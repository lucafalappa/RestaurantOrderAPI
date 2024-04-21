using RestaurantOrderAPI.Application.Models.Dtos;

namespace RestaurantOrderAPI.Application.Models.Responses.Users
{
    public class UpdateUserResponse
    {
        public UserDto User { get; set; } 
            = null!;
        public UpdateUserResponse(UserDto userDto)
        {
            User = userDto;
        } 
    }
}
