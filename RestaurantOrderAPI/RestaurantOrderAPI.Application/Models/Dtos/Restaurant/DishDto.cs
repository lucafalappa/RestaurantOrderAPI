using Newtonsoft.Json;
using RestaurantOrderAPI.Utilities;

namespace RestaurantOrderAPI.Application.Models.Dtos.Restaurant
{
    /// <summary>
    /// Represents a DTO related to the entity Dish. 
    /// It contains all fields with simple types 
    /// and the DTOs of entity fields
    /// </summary>
    public class DishDto
    {
        /// <summary>
        /// The unique identifier of the dish DTO
        /// </summary>
        public Guid IdDish { get; set; }
            = Guid.NewGuid();
        /// <summary>
        /// The name of the dish DTO
        /// </summary>
        public string Name { get; set; }
            = string.Empty;
        /// <summary>
        /// The price of the dish DTO
        /// </summary>
        public decimal Price { get; set; }
            = decimal.Zero;
        /// <summary>
        /// The type of the dish DTO
        /// </summary>
        public DishType Type { get; set; }
            = DishType.NoType;
        /// <summary>
        /// The unique identifier of the order
        /// associated to the dish DTO, which serves
        /// as a foreign key to the relationship
        /// </summary>
        public Guid IdOrder { get; set; }
            = Guid.Empty;
        /// <summary>
        /// The order DTO associated to the dish DTO
        /// </summary>
        [JsonIgnore]
        public OrderDto OrderDto { get; set; }
            = null!;
        /// <summary>
        /// Determines whether the specified object 
        /// is equal to the dish DTO
        /// </summary>
        /// <param name="obj">The object to compare 
        /// with the dish DTO</param>
        /// <returns>
        /// true if the specified object is equal to the dish DTO, 
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
            DishDto otherDish = (DishDto)obj;
            return (this.Name.Equals(otherDish.Name)
                    && this.IdOrder.Equals(otherDish.IdOrder))
                || this.IdDish.Equals(otherDish.IdDish);
        }
    }
}
