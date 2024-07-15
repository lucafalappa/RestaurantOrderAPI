using RestaurantOrderAPI.Application.Extensions;
using RestaurantOrderAPI.Infrastructure.Extensions;
using RestaurantOrderAPI.WebAPI.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddWebAPIServices(builder.Configuration)
    .AddApplicationServices(builder.Configuration)
    .AddInfrastructureServices(builder.Configuration);

var app = builder.Build();

app.AddWebAPIMiddleware()
   .AddApplicationMiddleware();

app.Run();