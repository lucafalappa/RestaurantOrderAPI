using FluentValidation;
using RestaurantOrderAPI.Application.Abstractions.Services;
using RestaurantOrderAPI.Application.Services;

namespace RestaurantOrderAPI.Application.Extensions
{
    public static class ServiceExtension
    {
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
            return services;
        }
    }
}
