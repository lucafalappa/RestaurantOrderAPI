using Newtonsoft.Json;
using RestaurantOrderAPI.Application.Configurations;
using RestaurantOrderAPI.Application.Models.Dtos.Users;

namespace RestaurantOrderAPI.Application.Models.Dtos.Restaurant
{
    /// <summary>
    /// Represents a DTO related to the entity Order. 
    /// It contains all fields with simple types 
    /// and the DTOs of entity fields
    /// </summary>
    public class OrderDto
    {
        /// <summary>
        /// The unique identifier of the order DTO
        /// </summary>
        public Guid IdOrder { get; set; }
            = Guid.NewGuid();
        /// <summary>
        /// The order date of the order DTO
        /// </summary>
        [JsonConverter(typeof(GenericDateTimeConverter))]
        public DateTime OrderDate { get; set; }
            = DateTime.Now;
        /// <summary>
        /// The delivery address of the order DTO
        /// </summary>
        public string DeliveryAddress { get; set; }
            = string.Empty;
        /// <summary>
        /// The total price of the order DTO
        /// </summary>
        public decimal TotalPrice { get; set; }
        /// <summary>
        /// The unique identifier of the user
        /// associated to the order DTO, which serves
        /// as a foreign key to the relationship
        /// </summary>
        public Guid IdUser { get; set; }
            = Guid.Empty;
        /// <summary>
        /// The user DTO associated to the order DTO
        /// </summary>
        [JsonIgnore]
        public UserDto UserDto { get; set; }
            = null!;
        /// <summary>
        /// The set of dish DTOs that are part of the order DTO
        /// </summary>
        [JsonProperty("dishes")]
        public ICollection<DishDto> DishDtos { get; set; }
            = new List<DishDto>();
        /// <summary>
        /// Determines whether the specified object
        /// is equal to the order DTO
        /// </summary>
        /// <param name="obj">The object to compare
        /// with the order DTO</param>
        /// <returns>
        /// true if the specified object is equal to the order DTO,
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
            OrderDto otherOrder = (OrderDto)obj;
            return this.IdOrder.Equals(otherOrder.IdOrder);
        }
    }
}
