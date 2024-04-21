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
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Orders");
            builder.HasKey(order => order.IdOrder);
            builder.Property(order => order.OrderDate)
                .IsRequired();
            builder.Property(order => order.DeliveryAddress)
                .HasMaxLength(100)
                .IsRequired();
            builder.HasOne(order => order.OrderingUser)
               .WithMany(user => user.Orders)
               .HasForeignKey(order => order.IdUser)
               .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
