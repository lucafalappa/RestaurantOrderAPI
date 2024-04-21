using RestaurantOrderAPI.Application.Models.Requests.Users;
using RestaurantOrderAPI.Domain.Users;

namespace RestaurantOrderAPI.Application.Abstractions.Services
{
    public interface ITokenService
    {
        string CreateToken(Guid idUser, RegisterRequest request);
        string CreateToken(User user);
    }
}
