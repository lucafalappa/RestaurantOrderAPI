using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RestaurantOrderAPI.Domain.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantOrderAPI.Data.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");
            builder.HasKey(x => x.IdUser);
            builder.Property(user => user.Email)
                .HasMaxLength(256)
                .IsRequired();
            builder.Property(user => user.Name)    
                .HasMaxLength(255)
                .IsRequired();
            builder.Property(user => user.Surname)
                .HasMaxLength(255)
                .IsRequired();
            builder.Property(user => user.Password)
                .HasMaxLength(16)
                .IsRequired();
            builder.Property(user => user.Role)
                .HasColumnName("UserRole")
                .HasConversion<int>()
                .IsRequired();
                //TODO : CONSIDER THIS IF IN DB I DONT HAVE CORRECT USERROLE
                //.HasConversion(
                //v => v.ToString(), // Convert enum value to string
                //v => (UserRole)Enum.Parse(typeof(UserRole), v)); // Convert string to enum value
        }
    }
}
