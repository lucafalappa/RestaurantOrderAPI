using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RestaurantOrderAPI.Entities.Users;

namespace RestaurantOrderAPI.Infrastructure.Configurations.Users
{
    /// <summary>
    /// Configures the <see cref="User"/> entity type for 
    /// correct persistence within the database
    /// </summary>
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        /// <summary>
        /// Configures the <see cref="User"/> entity type
        /// </summary>
        /// <param name="builder">The builder used to configure 
        /// the <see cref="User"/> entity to properly work with
        /// Entity Framework Core
        /// </param>
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");
            builder.HasKey(x => x.IdUser);
            builder.Property(user => user.Email)
                .HasMaxLength(255)
                .IsRequired();
            builder.Property(user => user.Name)
                .HasMaxLength(255)
                .IsRequired();
            builder.Property(user => user.Surname)
                .HasMaxLength(255)
                .IsRequired();
            builder.Property(user => user.Password)
                .HasMaxLength(255)
                .IsRequired();
            builder.Property(user => user.Role)
                .HasColumnName("UserRole")
                .HasConversion<int>()
                .IsRequired();
        }
    }
}
