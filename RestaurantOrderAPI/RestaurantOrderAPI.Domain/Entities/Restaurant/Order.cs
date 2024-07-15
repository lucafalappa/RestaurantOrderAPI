using RestaurantOrderAPI.Entities.Users;

namespace RestaurantOrderAPI.Entities.Restaurant
{
    /// <summary>
    /// Represents an order made by a user, having properties
    /// such as date of order and delivery address
    /// </summary>
    public class Order
    {
        /// <summary>
        /// The unique identifier of the order
        /// </summary>
        public Guid IdOrder { get; set; }
            = Guid.Empty;
        /// <summary>
        /// The order date of the order
        /// </summary>
        public DateTime OrderDate { get; set; }
            = DateTime.Now;
        /// <summary>
        /// The delivery address of the order
        /// </summary>
        public string DeliveryAddress { get; set; }
            = string.Empty;
        /// <summary>
        /// The total price of the order
        /// </summary>
        public decimal TotalPrice { get; set; }
        /// <summary>
        /// The unique identifier of the user
        /// associated to the order, which serves
        /// as a foreign key to the relationship
        /// </summary>
        public Guid IdUser { get; set; }
            = Guid.Empty;
        /// <summary>
        /// The user associated to the order
        /// </summary>
        public virtual User User { get; set; }
            = null!;
        /// <summary>
        /// The set of dishes that are part of the order
        /// </summary>
        public virtual ICollection<Dish> Dishes { get; set; }
            = new List<Dish>();
        /// <summary>
        /// Determines whether the specified object
        /// is equal to the order
        /// </summary>
        /// <param name="obj">The object to compare
        /// with the order</param>
        /// <returns>
        /// true if the specified object is equal to the order,
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
            Order otherOrder = (Order)obj;
            return this.IdOrder.Equals(otherOrder.IdOrder);
        }
    }
}
