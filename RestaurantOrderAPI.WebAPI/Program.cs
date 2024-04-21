using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using RestaurantOrderAPI.Application.Abstractions.Services;
using RestaurantOrderAPI.Application.Caching;
using RestaurantOrderAPI.Application.Extensions;
using RestaurantOrderAPI.Application.Options;
using RestaurantOrderAPI.Application.Services;
using RestaurantOrderAPI.Data.Context;
using RestaurantOrderAPI.Data.Extensions;
using RestaurantOrderAPI.Data.Repositories;
using RestaurantOrderAPI.Domain.Entities;
using RestaurantOrderAPI.WebAPI.Extensions;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddWebAPIServices(builder.Configuration)
    .AddApplicationServices(builder.Configuration)
    .AddDataServices(builder.Configuration);

var app = builder.Build();

app.AddWebAPIMiddleware()
   .AddApplicationMiddleware();

app.Run();
