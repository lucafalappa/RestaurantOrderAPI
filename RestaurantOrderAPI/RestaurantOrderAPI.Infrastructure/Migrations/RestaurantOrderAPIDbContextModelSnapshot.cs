﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RestaurantOrderAPI.Infrastructure.Contexts;

#nullable disable

namespace RestaurantOrderAPI.Infrastructure.Migrations
{
    [DbContext(typeof(RestaurantOrderAPIDbContext))]
    partial class RestaurantOrderAPIDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("RestaurantOrderAPI.Entities.Restaurant.Dish", b =>
                {
                    b.Property<Guid>("IdDish")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("IdOrder")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<decimal>("Price")
                        .HasPrecision(10, 2)
                        .HasColumnType("decimal(10,2)");

                    b.Property<int>("Type")
                        .HasColumnType("int")
                        .HasColumnName("DishType");

                    b.HasKey("IdDish");

                    b.HasIndex("IdOrder");

                    b.ToTable("Dishes", (string)null);
                });

            modelBuilder.Entity("RestaurantOrderAPI.Entities.Restaurant.Order", b =>
                {
                    b.Property<Guid>("IdOrder")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("DeliveryAddress")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<Guid>("IdUser")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("OrderDate")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("TotalPrice")
                        .HasPrecision(10, 2)
                        .HasColumnType("decimal(10,2)");

                    b.HasKey("IdOrder");

                    b.HasIndex("IdUser");

                    b.ToTable("Orders", (string)null);
                });

            modelBuilder.Entity("RestaurantOrderAPI.Entities.Users.User", b =>
                {
                    b.Property<Guid>("IdUser")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<int>("Role")
                        .HasColumnType("int")
                        .HasColumnName("UserRole");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("IdUser");

                    b.ToTable("Users", (string)null);
                });

            modelBuilder.Entity("RestaurantOrderAPI.Entities.Restaurant.Dish", b =>
                {
                    b.HasOne("RestaurantOrderAPI.Entities.Restaurant.Order", "Order")
                        .WithMany("Dishes")
                        .HasForeignKey("IdOrder")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");
                });

            modelBuilder.Entity("RestaurantOrderAPI.Entities.Restaurant.Order", b =>
                {
                    b.HasOne("RestaurantOrderAPI.Entities.Users.User", "User")
                        .WithMany("Orders")
                        .HasForeignKey("IdUser")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("RestaurantOrderAPI.Entities.Restaurant.Order", b =>
                {
                    b.Navigation("Dishes");
                });

            modelBuilder.Entity("RestaurantOrderAPI.Entities.Users.User", b =>
                {
                    b.Navigation("Orders");
                });
#pragma warning restore 612, 618
        }
    }
}
