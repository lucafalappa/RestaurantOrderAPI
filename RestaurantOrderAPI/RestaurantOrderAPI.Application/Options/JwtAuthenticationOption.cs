namespace RestaurantOrderAPI.Application.Options
{
    /// <summary>
    /// Represents the JWT authentication options
    /// </summary>
    public class JwtAuthenticationOption
    {
        /// <summary>
        /// The key used for signing the JWT
        /// </summary>
        public string Key { get; set; }
            = string.Empty;
        /// <summary>
        /// The issuer of the JWT
        /// </summary>
        public string Issuer { get; set; }
            = string.Empty;
    }
}
