using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using RestaurantOrderAPI.Application.Abstractions.Services;
using RestaurantOrderAPI.Application.Models.Requests.Users;
using RestaurantOrderAPI.Application.Options;
using RestaurantOrderAPI.Domain.Users;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RestaurantOrderAPI.Application.Services
{
    public class TokenService : ITokenService
    {
        private readonly JwtAuthenticationOption _jwtAuthenticationOption;
        public TokenService(IOptions<JwtAuthenticationOption> options)
        {
            _jwtAuthenticationOption = options.Value;
        }
        public string CreateToken(Guid idUser, RegisterRequest request)
        {
            var claims = new List<Claim>()
            {
                new Claim("IdUser", idUser.ToString()),
                new Claim("Email", request.Email),
                new Claim("Name", request.Name),
                new Claim("Surname", request.Surname),
                new Claim("Password", request.Password),
                new Claim("Role", request.UserRole)
            };
            return CreateToken(claims);
        }
        public string CreateToken(User user)
        {
            var claims = new List<Claim>()
            {
                new Claim("IdUser", user.IdUser.ToString()),
                new Claim("Email", user.Email),
                new Claim("Name", user.Name),
                new Claim("Surname", user.Surname),
                new Claim("Password", user.Password),
                new Claim("Role", user.Role.ToString()),
            };
            return CreateToken(claims);
        }
        private string CreateToken(List<Claim> claims)
        {
            var securityKey = new SymmetricSecurityKey
                (Encoding.UTF8.GetBytes
                (_jwtAuthenticationOption.Key));
            var securityToken = new JwtSecurityToken(
                issuer: _jwtAuthenticationOption.Issuer,
                audience: null,
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: new SigningCredentials
                (securityKey, SecurityAlgorithms.HmacSha256)
            );
            var token = new JwtSecurityTokenHandler().WriteToken
                (securityToken);
            return token;
        }
    }
}
