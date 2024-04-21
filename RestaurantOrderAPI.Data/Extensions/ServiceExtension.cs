using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RestaurantOrderAPI.Data.Context;
using RestaurantOrderAPI.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantOrderAPI.Data.Extensions
{
    public static class ServiceExtension
    {
        public static IServiceCollection AddDataServices
            (this IServiceCollection services, IConfiguration configuration)
        {
            //TODO : REMOVE THIS WHEN YOU WON'T USE INMEMORY
            services.AddDbContext<RestaurantOrderAPIDbContext>(conf =>
                conf.UseInMemoryDatabase("InMemory"));
            //  conf.UseSqlServer(configuration.GetConnectionString("RestaurantOrderAPIDbContext"));
            services.AddScoped<DishRepository>();
            services.AddScoped<OrderRepository>();
            services.AddScoped<UserRepository>();
            return services;
        }
    }
}
