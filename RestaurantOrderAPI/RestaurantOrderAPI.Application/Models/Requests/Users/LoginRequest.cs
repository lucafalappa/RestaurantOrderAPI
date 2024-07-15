namespace RestaurantOrderAPI.Application.Models.Requests.Users
{
    /// <summary>
    /// Represents a request to log in a user
    /// </summary>
    public class LoginRequest
    {
        /// <summary>
        /// The email address of the user
        /// </summary>
        public string Email { get; set; }
            = string.Empty;
        /// <summary>
        /// The password of the user
        /// </summary>
        public string Password { get; set; }
            = string.Empty;
    }
}
