using Microsoft.AspNetCore.Identity;
using RestaurantOrderAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantOrderAPI.Domain.Users
{
    public class User
    {
        public Guid IdUser { get; set; } 
            = Guid.Empty;
        public string Email { get; set; } 
            = string.Empty;
        public string Name { get; set; } 
            = string.Empty;
        public string Surname { get; set; } 
            = string.Empty;
        public string Password { get; set; } 
            = string.Empty;
        public UserRole Role { get; set; } 
            = UserRole.NoRole;
        public virtual ICollection<Order> Orders { get; set; } 
             = null!;
        public User()
        {
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
            User otherUser = (User)obj;
            return IdUser.Equals(otherUser.IdUser) 
                || Email.Equals(otherUser.Email);
        }
    }
}
