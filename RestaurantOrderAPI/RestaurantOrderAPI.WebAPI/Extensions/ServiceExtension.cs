using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using RestaurantOrderAPI.Application.Options;
using System.Text;

namespace RestaurantOrderAPI.WebAPI.Extensions
{
    /// <summary>
    /// Provides extension methods for configuring 
    /// services in a web application
    /// </summary>
    public static class ServiceExtension
    {
        /// <summary>
        /// Configures services required for a web application
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> 
        /// collection to add services to
        /// </param>
        /// <param name="configuration">The configuration settings
        /// </param>
        /// <returns>The modified <see cref="IServiceCollection"/> 
        /// collection instance
        /// </returns>
        public static IServiceCollection AddWebAPIServices
            (this IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers().AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling =
                Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "RestaurantOrderAPI",
                    Version = "v1"
                });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme. " +
                    "\r\n\r\n Enter 'Bearer' [space] and then your token in the text " +
                    "input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\"",
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                {
                    new OpenApiSecurityScheme {
                        Reference = new OpenApiReference {
                            Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                        }
                    },
                    new string[] {}
                }
                });
            });
            services.AddSwaggerGenNewtonsoftSupport();
            services.AddFluentValidationAutoValidation();
            services.Configure<JwtAuthenticationOption>
                (configuration.GetSection("JwtAuthentication"));
            var jwtAuthenticationOption = new JwtAuthenticationOption();
            configuration.GetSection("JwtAuthentication")
                .Bind(jwtAuthenticationOption);
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters =
                    new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                    {
                        ValidateIssuer = true,
                        ValidateLifetime = true,
                        ValidateAudience = false,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = jwtAuthenticationOption.Issuer,
                        IssuerSigningKey = new SymmetricSecurityKey
                            (Encoding.UTF8.GetBytes
                            (jwtAuthenticationOption.Key))
                    };
                });
            services.AddAuthorization(options =>
            {
                options.AddPolicy("AdministratorPolicy", policy =>
                    policy.RequireClaim("Role", "Administrator"));
            });
            services.AddRouting(options => options.LowercaseUrls = true);
            return services;
        }
    }
}
