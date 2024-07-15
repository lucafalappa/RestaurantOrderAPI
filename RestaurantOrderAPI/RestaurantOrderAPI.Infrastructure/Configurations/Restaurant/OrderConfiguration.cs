using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RestaurantOrderAPI.Entities.Restaurant;

namespace RestaurantOrderAPI.Infrastructure.Configurations.Restaurant
{
    /// <summary>
    /// Configures the <see cref="Order"/> entity type for 
    /// correct persistence within the database
    /// </summary>
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        /// <summary>
        /// Configures the <see cref="Order"/> entity type
        /// </summary>
        /// <param name="builder">The builder used to configure 
        /// the <see cref="Order"/> entity to properly work with
        /// Entity Framework Core
        /// </param>
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Orders");
            builder.HasKey(order => order.IdOrder);
            builder.Property(order => order.OrderDate)
                .IsRequired();
            builder.Property(order => order.DeliveryAddress)
                .HasMaxLength(100)
                .IsRequired();
            builder.Property(order => order.TotalPrice)
                .HasPrecision(10, 2)
                .IsRequired();
        }
    }
}
