using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RestaurantOrderAPI.Domain.Entities
{
    public class Dish
    {
        public Guid IdDish { get; set; } 
            = Guid.Empty;
        public string Name { get; set; } 
            = string.Empty;
        public decimal Price { get; set; } 
            = decimal.Zero;
        public DishType Type { get; set; } 
            = DishType.NoType;
        public Guid IdOrder { get; set; } 
            = Guid.Empty;
        public virtual Order AssociatedOrder { get; set; } 
            = null!;
        public Dish()
        {
            IdDish = Guid.NewGuid();
            IdOrder = Guid.NewGuid();
        }
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
            return IdDish.Equals(otherDish.IdDish);
        }
    }
}
