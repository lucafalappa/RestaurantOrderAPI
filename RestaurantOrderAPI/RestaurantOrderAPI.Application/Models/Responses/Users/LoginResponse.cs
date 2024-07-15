namespace RestaurantOrderAPI.Application.Models.Responses.Users
{
    /// <summary>
    /// Represents the response returned 
    /// upon successful log in
    /// </summary>
    public class LoginResponse
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
        /// <see cref="LoginResponse"/> class
        /// </summary>
        /// <param name="token">The authentication 
        /// token
        /// </param>
        public LoginResponse(string token)
        {
            Result = "Successful log in";
            Token = token;
        }
    }
}
