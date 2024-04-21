using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using RestaurantOrderAPI.Domain.Entities;
using RestaurantOrderAPI.Domain.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantOrderAPI.Data.Context
{
    public class RestaurantOrderAPIDbContext : DbContext
    {
        public DbSet<Dish>? Dishes { get; set; }
        public DbSet<Order>? Orders { get; set; }
        public DbSet<User>? Users { get; set; }
        public RestaurantOrderAPIDbContext
            (DbContextOptions<RestaurantOrderAPIDbContext> config) 
            : base(config)
        {
            ConstructorInstructions();
        }
        public RestaurantOrderAPIDbContext() : base()
        {
            ConstructorInstructions();
        }
        //TODO : missing override OnConfiguring
        protected override void OnModelCreating
            (ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly
                (this.GetType().Assembly);
            modelBuilder.Entity<Order>()
                        .HasMany(order => order.OrderedDishes)
                        .WithOne(dish => dish.AssociatedOrder)
                        .HasForeignKey(dish => dish.IdOrder)
                        .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<User>()
                        .HasMany(user => user.Orders)
                        .WithOne(order => order.OrderingUser)
                        .HasForeignKey(order => order.IdUser)
                        .OnDelete(DeleteBehavior.Cascade);           
            base.OnModelCreating(modelBuilder);
        }
        private void ConstructorInstructions()
        {
            try
            {
                var databaseCreator =
                    Database.GetService<IDatabaseCreator>() 
                    as RelationalDatabaseCreator;
                if (databaseCreator != null)
                {
                    if (!databaseCreator.CanConnect()) 
                        { databaseCreator.Create(); }
                    if (!databaseCreator.HasTables()) 
                        { databaseCreator.CreateTables(); }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
