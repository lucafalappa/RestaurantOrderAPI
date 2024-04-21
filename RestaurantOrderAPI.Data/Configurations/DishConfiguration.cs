using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RestaurantOrderAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantOrderAPI.Data.Configurations
{
    public class DishConfiguration : IEntityTypeConfiguration<Dish>
    {
        public void Configure(EntityTypeBuilder<Dish> builder)
        {
            builder.ToTable("Dishes");
            builder.HasKey(dish => dish.IdDish);
            builder.Property(dish => dish.Name)
                .HasMaxLength(50)
                .IsRequired();
            //TODO : maybe i should remove this property?
            builder.Property(dish => dish.Price)
                .IsRequired();
            builder.Property(dish => dish.Type)
                .HasColumnType("DishType")
                .HasConversion<int>()
                .IsRequired();
            //TODO : CONSIDER THIS IF IN DB I DONT HAVE CORRECT DISHTYPE
            //.HasConversion(
            //v => v.ToString(), // Convert enum value to string
            //v => (DishType)Enum.Parse(typeof(DishType), v)); // Convert string to enum value
            builder.HasOne(dish => dish.AssociatedOrder)
                .WithMany(order => order.OrderedDishes)
                .HasForeignKey(dish => dish.IdOrder)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
