namespace RestaurantOrderAPI.Application.Models.Responses.Users
{
    /// <summary>
    /// Represents the response returned 
    /// upon successful authentication
    /// </summary>
    public class AuthenticateResponse
    {
        /// <summary>
        /// The message about operation result
        /// </summary>
        public string Result { get; set; }
            = string.Empty;
        /// <summary>
        /// The authentication token
        /// </summary>
        public string Token { get; set; }
            = string.Empty;
        /// <summary>
        /// Initializes a new instance of the 
        /// <see cref="AuthenticateResponse"/> class
        /// </summary>
        /// <param name="token">The authentication 
        /// token
        /// </param>
        public AuthenticateResponse(string token)
        {
            Result = "Successful authentication";
            Token = token;
        }
    }
}
