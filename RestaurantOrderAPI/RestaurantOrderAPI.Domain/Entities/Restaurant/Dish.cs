using RestaurantOrderAPI.Utilities;

namespace RestaurantOrderAPI.Entities.Restaurant
{
    /// <summary>
    /// Represents an orderable dish, having properties 
    /// such as name, price and type of dish
    /// </summary>
    public class Dish
    {
        /// <summary>
        /// The unique identifier of the dish
        /// </summary>
        public Guid IdDish { get; set; }
            = Guid.Empty;
        /// <summary>
        /// The name of the dish
        /// </summary>
        public string Name { get; set; }
            = string.Empty;
        /// <summary>
        /// The price of the dish
        /// </summary>
        public decimal Price { get; set; }
            = decimal.Zero;
        /// <summary>
        /// The type of the dish
        /// </summary>
        public DishType Type { get; set; }
            = DishType.NoType;
        /// <summary>
        /// The unique identifier of the order 
        /// associated to the dish, which serves 
        /// as a foreign key to the relationship
        /// </summary>
        public Guid IdOrder { get; set; }
            = Guid.Empty;
        /// <summary>
        /// The order associated to the dish
        /// </summary>
        public virtual Order Order { get; set; }
            = null!;
        /// <summary>
        /// Determines whether the specified object 
        /// is equal to the dish
        /// </summary>
        /// <param name="obj">The object to compare 
        /// with the dish</param>
        /// <returns>
        /// true if the specified object is equal to the dish, 
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
            Dish otherDish = (Dish)obj;
            return (this.Name.Equals(otherDish.Name)
                    && this.IdOrder.Equals(otherDish.IdOrder))
                || this.IdDish.Equals(otherDish.IdDish);
        }
    }
}
