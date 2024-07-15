using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using RestaurantOrderAPI.Application.Abstractions.Services;
using RestaurantOrderAPI.Application.Models.Dtos.Users;
using RestaurantOrderAPI.Application.Models.Requests.Users;
using RestaurantOrderAPI.Application.Options;
using RestaurantOrderAPI.Utilities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RestaurantOrderAPI.Application.Services
{
    /// <summary>
    /// Service for managing JWT tokens
    /// </summary>
    public class TokenService : ITokenService
    {
        /// <summary>
        /// The class that represents the JWT authentication options
        /// </summary>
        private readonly JwtAuthenticationOption _jwtAuthenticationOption;
        /// <summary>
        /// Initializes a new instance of the 
        /// <see cref="TokenService"/> class
        /// </summary>
        /// <param name="options">The JWT authentication options
        /// </param>
        public TokenService
            (IOptions<JwtAuthenticationOption> options)
        {
            _jwtAuthenticationOption = options.Value;
        }
        public string CreateToken
            (Guid idUser, AuthenticateRequest request)
        {
            var claims = new List<Claim>()
            {
                new Claim("IdUser", idUser.ToString()),
                new Claim("Email", request.Email),
                new Claim("Name", request.Name),
                new Claim("Surname", request.Surname),
                new Claim("Password", request.Password),
                new Claim("Role", UserRole.Administrator.ToString())
            };
            return CreateToken(claims);
        }
        public string CreateToken
            (Guid idUser, RegisterRequest request)
        {
            var claims = new List<Claim>()
            {
                new Claim("IdUser", idUser.ToString()),
                new Claim("Email", request.Email),
                new Claim("Name", request.Name),
                new Claim("Surname", request.Surname),
                new Claim("Password", request.Password),
                new Claim("Role", UserRole.Customer.ToString())
            };
            return CreateToken(claims);
        }
        public string CreateToken
            (UserDto userDto)
        {
            var claims = new List<Claim>()
            {
                new Claim("IdUser", userDto.IdUser.ToString()),
                new Claim("Email", userDto.Email),
                new Claim("Name", userDto.Name),
                new Claim("Surname", userDto.Surname),
                new Claim("Password", userDto.Password),
                new Claim("Role", userDto.Role.ToString()),
            };
            return CreateToken(claims);
        }
        /// <summary>
        /// Creates a JWT token with the specified claims
        /// </summary>
        /// <param name="claims">The claims to include in the token
        /// </param>
        /// <returns>The generated JWT token
        /// </returns>
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
