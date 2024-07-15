using RestaurantOrderAPI.Application.Abstractions.Configurations;

namespace RestaurantOrderAPI.Application.Configurations
{
    /// <summary>
    /// Utility class for password 
    /// encoding and verification
    /// </summary>
    public class BCryptPasswordEncoder : IPasswordEncoder
    {
        public bool VerifyPassword
            (string requestPassword, string userPassword)
        {
            return BCrypt.Net.BCrypt.Verify
                (requestPassword, userPassword);
        }
        public string EncodePassword
            (string requestPassword)
        {
            return BCrypt.Net.BCrypt.HashPassword
                (requestPassword);
        }
    }
}
