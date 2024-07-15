using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RestaurantOrderAPI.Entities.Restaurant;

namespace RestaurantOrderAPI.Infrastructure.Configurations.Restaurant
{
    /// <summary>
    /// Configures the <see cref="Dish"/> entity type for 
    /// correct persistence within the database
    /// </summary>
    public class DishConfiguration : IEntityTypeConfiguration<Dish>
    {
        /// <summary>
        /// Configures the <see cref="Dish"/> entity type
        /// </summary>
        /// <param name="builder">The builder used to configure 
        /// the <see cref="Dish"/> entity to properly work with
        /// Entity Framework Core
        /// </param>
        public void Configure(EntityTypeBuilder<Dish> builder)
        {
            builder.ToTable("Dishes");
            builder.HasKey(dish => dish.IdDish);
            builder.Property(dish => dish.Name)
                .HasMaxLength(50)
                .IsRequired();
            builder.Property(dish => dish.Price)
                .HasPrecision(10, 2)
                .IsRequired();
            builder.Property(dish => dish.Type)
                .HasColumnName("DishType")
                .HasConversion<int>()
                .IsRequired();
        }
    }
}
