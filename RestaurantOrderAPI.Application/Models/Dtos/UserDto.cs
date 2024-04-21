using RestaurantOrderAPI.Domain.Users;

namespace RestaurantOrderAPI.Application.Models.Dtos
{
    public class UserDto
    {
        public Guid IdUser { get; set; } 
            = Guid.Empty;
        public string Email { get; set; } 
            = string.Empty;
        public string Name { get; set; } 
            = string.Empty;
        public string Surname { get; set; } 
            = string.Empty;
        public UserRole Role { get; set; } 
            = UserRole.NoRole;
        public UserDto(User user)
        {
            IdUser = user.IdUser;
            Email = user.Email;
            Name = user.Name;
            Surname = user.Surname;
            Role = user.Role;
        }
    }
}
