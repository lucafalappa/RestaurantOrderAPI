using RestaurantOrderAPI.Entities.Restaurant;
using RestaurantOrderAPI.Utilities;

namespace RestaurantOrderAPI.Entities.Users
{
    /// <summary>
    /// Represents a registered user within the platform,
    /// having properties such as email, name, surname and
    /// the role that it has on the platform
    /// </summary>
    public class User
    {
        /// <summary>
        /// The unique identifier of the user
        /// </summary>
        public Guid IdUser { get; set; }
            = Guid.Empty;
        /// <summary>
        /// The email of the user
        /// </summary>
        public string Email { get; set; }
            = string.Empty;
        /// <summary>
        /// The name of the user
        /// </summary>
        public string Name { get; set; }
            = string.Empty;
        /// <summary>
        /// The surname of the user
        /// </summary>
        public string Surname { get; set; }
            = string.Empty;
        /// <summary>
        /// The password of the user
        /// </summary>
        public string Password { get; set; }
            = string.Empty;
        /// <summary>
        /// The role of the user
        /// </summary>
        public UserRole Role { get; set; }
            = UserRole.NoRole;
        /// <summary>
        /// The set of orders placed by the user
        /// </summary>
        public virtual ICollection<Order> Orders { get; set; }
            = new List<Order>();
        /// <summary>
        /// Determines whether the specified object 
        /// is equal to the user
        /// </summary>
        /// <param name="obj">The object to compare 
        /// with the user</param>
        /// <returns>
        /// true if the specified object is equal to the user, 
        /// false otherwise
        /// </returns>
        public override bool Equals(object? obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            if (ReferenceEquals(this, obj))
            {
                return true;
            }
            User otherUser = (User)obj;
            return this.Email.Equals(otherUser.Email)
                || this.IdUser.Equals(otherUser.IdUser);
        }
    }
}
