using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using RestaurantOrderAPI.Entities.Restaurant;
using RestaurantOrderAPI.Entities.Users;

namespace RestaurantOrderAPI.Infrastructure.Contexts
{
    /// <summary>
    /// Represents the database context for 
    /// the RestaurantOrderAPI platform
    /// </summary>
    public class RestaurantOrderAPIDbContext : DbContext
    {
        /// <summary>
        /// The set of Dish entities in the database
        /// </summary>
        public DbSet<Dish> _dishes { get; set; }
        /// <summary>
        /// The set of Order entities in the database
        /// </summary>
        public DbSet<Order> _orders { get; set; }
        /// <summary>
        /// The set of User entities in the database
        /// </summary>
        public DbSet<User> _users { get; set; }
        /// <summary>
        /// Initializes a new instance of the 
        /// <see cref="RestaurantOrderAPIDbContext"/> class 
        /// using the specified options
        /// </summary>
        /// <param name="config">The options to be used 
        /// for configuring the <see cref="DbContext"/>
        /// </param>
        public RestaurantOrderAPIDbContext
            (DbContextOptions<RestaurantOrderAPIDbContext> config)
            : base(config)
        {
            ConstructorInstructions();
        }
        /// <summary>
        /// Initializes a new instance of the 
        /// <see cref="RestaurantOrderAPIDbContext"/> class
        /// </summary>
        public RestaurantOrderAPIDbContext() : base()
        {
            ConstructorInstructions();
        }
        protected override void OnModelCreating
            (ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly
                (this.GetType().Assembly);
            modelBuilder.Entity<Order>()
                        .HasMany(order => order.Dishes)
                        .WithOne(dish => dish.Order)
                        .HasForeignKey(dish => dish.IdOrder)
                        .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<User>()
                        .HasMany(user => user.Orders)
                        .WithOne(order => order.User)
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