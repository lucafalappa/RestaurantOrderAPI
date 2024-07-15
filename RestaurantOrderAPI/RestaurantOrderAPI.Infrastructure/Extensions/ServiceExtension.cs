using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RestaurantOrderAPI.Infrastructure.Contexts;
using RestaurantOrderAPI.Infrastructure.Repositories.Restaurant;
using RestaurantOrderAPI.Infrastructure.Repositories.Users;

namespace RestaurantOrderAPI.Infrastructure.Extensions
{
    /// <summary>
    /// Provides extension methods for configuring services 
    /// related to the infrastructure of the platform
    /// </summary>
    public static class ServiceExtension
    {
        /// <summary>
        /// Adds infrastructure services to the specified 
        /// <see cref="IServiceCollection"/>
        /// </summary>
        /// <param name="services">
        /// The collection of services of the platform
        /// </param>
        /// <param name="configuration">
        /// The configuration settings of the platform
        /// </param>
        /// <returns>
        /// The updated 
        /// <see cref="IServiceCollection"/> services
        /// </returns>
        public static IServiceCollection AddInfrastructureServices
            (this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<RestaurantOrderAPIDbContext>(conf =>
                conf.UseSqlServer(configuration.GetConnectionString
                    ("RestaurantOrderAPIDbContext"))
            );
            services.AddScoped<DishRepository>();
            services.AddScoped<OrderRepository>();
            services.AddScoped<UserRepository>();
            return services;
        }
    }
}
