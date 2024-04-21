using RestaurantOrderAPI.Application.Models.Dtos;

namespace RestaurantOrderAPI.Application.Models.Responses.Users
{
    public class DeleteUserResponse
    {
        public UserDto User { get; set; } 
            = null!;
        public DeleteUserResponse(UserDto userDto)
        {
            User = userDto;
        }
    }
}
