using Newtonsoft.Json;
using RestaurantOrderAPI.Application.Models.Dtos.Restaurant;
using RestaurantOrderAPI.Utilities;

namespace RestaurantOrderAPI.Application.Models.Dtos.Users
{
    /// <summary>
    /// Represents a DTO related to the entity User. 
    /// It contains all fields with simple types 
    /// and the DTOs of entity fields
    /// </summary>
    public class UserDto
    {
        /// <summary>
        /// The unique identifier of the user DTO
        /// </summary>
        public Guid IdUser { get; set; }
            = Guid.NewGuid();
        /// <summary>
        /// The email of the user DTO
        /// </summary>
        public string Email { get; set; }
            = string.Empty;
        /// <summary>
        /// The name of the user DTO
        /// </summary>
        public string Name { get; set; }
            = string.Empty;
        /// <summary>
        /// The surname of the user DTO
        /// </summary>
        public string Surname { get; set; }
            = string.Empty;
        /// <summary>
        /// The password of the user DTO
        /// </summary>
        [JsonIgnore]
        public string Password { get; set; }
            = string.Empty;
        /// <summary>
        /// The role of the user DTO
        /// </summary>
        public UserRole Role { get; set; }
            = UserRole.NoRole;
        /// <summary>
        /// The set of order DTOs placed by the user DTO
        /// </summary>
        [JsonProperty("orders")]
        public ICollection<OrderDto> OrderDtos { get; set; }
            = new List<OrderDto>();
        /// <summary>
        /// Determines whether the specified object 
        /// is equal to the user DTO
        /// </summary>
        /// <param name="obj">The object to compare 
        /// with the user DTO</param>
        /// <returns>
        /// true if the specified object is equal to the user DTO, 
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
            UserDto otherUser = (UserDto)obj;
            return this.Email.Equals(otherUser.Email)
                || this.IdUser.Equals(otherUser.IdUser);
        }
    }
}
