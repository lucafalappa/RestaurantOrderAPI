using RestaurantOrderAPI.Domain.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RestaurantOrderAPI.Domain.Entities
{
    public class Order
    {
        public Guid IdOrder { get; set; } 
            = Guid.Empty;
        public DateTime OrderDate { get; set; } 
            = DateTime.Now;
        public string DeliveryAddress { get; set; } 
            = string.Empty;
        public Guid IdUser { get; set; } 
            = Guid.Empty;
        public virtual User OrderingUser { get; set; } 
            = null!;
        public virtual ICollection<Dish> OrderedDishes { get; set; } 
             = null!;
        public Order()
        {
            IdOrder = Guid.NewGuid();
            IdUser = Guid.NewGuid();
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
            Order otherOrder = (Order)obj;
            return IdOrder.Equals(otherOrder.IdOrder);
        }
    }
}
