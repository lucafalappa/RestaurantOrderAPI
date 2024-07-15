using FluentValidation;
using RestaurantOrderAPI.Application.Abstractions.Configurations;
using RestaurantOrderAPI.Application.Abstractions.Services;
using RestaurantOrderAPI.Application.Abstractions.Services.Restaurant;
using RestaurantOrderAPI.Application.Abstractions.Services.Users;
using RestaurantOrderAPI.Application.Configurations;
using RestaurantOrderAPI.Application.Services;
using RestaurantOrderAPI.Application.Services.Restaurant;
using RestaurantOrderAPI.Application.Services.Users;

namespace RestaurantOrderAPI.Application.Extensions
{
    /// <summary>
    /// Provides extension methods for configuring 
    /// services in an application
    /// </summary>
    public static class ServiceExtension
    {
        /// <summary>
        /// Configures services required for an application
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> 
        /// collection to add services to
        /// </param>
        /// <param name="configuration">The configuration settings
        /// </param>
        /// <returns>The modified <see cref="IServiceCollection"/> 
        /// collection instance
        /// </returns>
        public static IServiceCollection AddApplicationServices
            (this IServiceCollection services, IConfiguration configuration)
        {
            services.AddValidatorsFromAssembly(
                AppDomain.CurrentDomain.GetAssemblies().
                       SingleOrDefault(assembly =>
                       assembly.GetName().Name == "RestaurantOrderAPI.Application")
                );
            services.AddScoped<IDishService, DishService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IPasswordEncoder, BCryptPasswordEncoder>();
            return services;
        }
    }
}
