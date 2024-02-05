using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlashFood.Models;
using Microsoft.EntityFrameworkCore;

namespace User.Management.Data.Data
{
    public static class ModelBuilderExtension
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            var restaurant1Id = Guid.NewGuid();
            var restaurant2Id = Guid.NewGuid();
            var restaurant3Id = Guid.NewGuid();
            var restaurant4Id = Guid.NewGuid();
            var restaurant5Id = Guid.NewGuid();
            var menu1Id = Guid.NewGuid();
            var menu2Id = Guid.NewGuid();
            var menu3Id = Guid.NewGuid();
            var menu4Id = Guid.NewGuid();
            var menu5Id = Guid.NewGuid();

            modelBuilder.Entity<Restaurant>().HasData
                (
                    new Restaurant { Id = restaurant1Id, Name = "Trattoria Roz Cafe" },
                    new Restaurant { Id = restaurant2Id, Name = "Shaormeria Baneasa" },
                    new Restaurant { Id = restaurant3Id, Name = "Olga's Caffe" },
                    new Restaurant { Id = restaurant4Id, Name = "Calif Kebab" },
                    new Restaurant { Id = restaurant5Id, Name = "Jerry's Pizza" }
                );

            modelBuilder.Entity<Menu>().HasData
                (
                    new Menu { Id = menu1Id, RestaurantId = restaurant1Id },
                    new Menu { Id = menu2Id, RestaurantId = restaurant2Id },
                    new Menu { Id = menu3Id, RestaurantId = restaurant3Id },
                    new Menu { Id = menu4Id, RestaurantId = restaurant4Id },
                    new Menu { Id = menu5Id, RestaurantId = restaurant5Id }
                );

            modelBuilder.Entity<Dish>().HasData(
                new Dish
                {
                    Id = Guid.NewGuid(),
                    Name = "Pizza Classica",
                    Price = 40m,
                    Quantity = 500,
                    MenuId = menu5Id
                },
                new Dish
                {
                    Id = Guid.NewGuid(),
                    Name = "Pizza Diavola",
                    Price = 25m,
                    Quantity = 400,
                    MenuId = menu5Id
                },
                new Dish
                {
                    Id = Guid.NewGuid(),
                    Name = "Burger de Vita",
                    Price = 50m,
                    Quantity = 650,
                    MenuId = menu1Id
                },
                new Dish
                {
                    Id = Guid.NewGuid(),
                    Name = "Pizza Pollo",
                    Price = 25m,
                    Quantity = 400,
                    MenuId = menu3Id
                },
                new Dish
                {
                    Id = Guid.NewGuid(),
                    Name = "Shaorma de Pui",
                    Price = 30m,
                    Quantity = 650,
                    MenuId = menu2Id
                },
                new Dish
                {
                    Id = Guid.NewGuid(),
                    Name = "Cheese Kebab de Pui",
                    Price = 50m,
                    Quantity = 500,
                    MenuId = menu4Id
                }
            );
        }

    }
}
