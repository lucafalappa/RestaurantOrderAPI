namespace RestaurantOrderAPI.Utilities
{
    /// <summary>
    /// Represents the role of a user
    /// </summary>
    public enum UserRole
    {
        /// <summary>
        /// Indicates that the role 
        /// of the user is not specified
        /// </summary>
        NoRole = 0,
        /// <summary>
        /// Indicates that the user 
        /// is an administrator
        /// </summary>
        Administrator = 1,
        /// <summary>
        /// Indicates that the user 
        /// is a customer
        /// </summary>
        Customer = 2
    }
}
