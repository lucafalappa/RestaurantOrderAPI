namespace RestaurantOrderAPI.Application.Abstractions.Configurations
{
    /// <summary>
    /// Interface for password encoding and verification
    /// </summary>
    public interface IPasswordEncoder
    {
        /// <summary>
        /// Verifies if a provided password matches 
        /// the user's stored password
        /// </summary>
        /// <param name="requestPassword">The password 
        /// provided in the request
        /// </param>
        /// <param name="userPassword">The user's stored 
        /// password for comparison
        /// </param>
        /// <returns>true if the passwords match, 
        /// false otherwise
        /// </returns>
        bool VerifyPassword
            (string requestPassword, string userPassword);
        /// <summary>
        /// Encodes a password for secure storage
        /// </summary>
        /// <param name="requestPassword">The password 
        /// to be encoded
        /// </param>
        /// <returns>The encoded password
        /// </returns>
        string EncodePassword
            (string requestPassword);
    }
}
