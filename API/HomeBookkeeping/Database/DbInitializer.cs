using System;
using System.Collections.Generic;
using System.Linq;
using HomeBookkeeping.Models;
using HomeBookkeeping.Models.Enums;
using Microsoft.AspNetCore.Identity;

namespace HomeBookkeeping.Database
{
    public static class DbInitializer
    {

        public static async void InitializeAsync(
          DatabaseContext context,
          UserManager<User> userManager,
          RoleManager<IdentityRole> roleManager)
        {
            context.Database.EnsureCreated();

            var catHome = new Category { Name = "Дом", Capacity = 15000 };
            var catEat = new Category { Name = "Еда", Capacity = 10000 };
            var catCar = new Category { Name = "Машина", Capacity = 7000 };

            var categories = new List<Category> {  catHome, catEat, catCar };
            if (!context.Categories.Any())
            {
                context.Categories.AddRange(categories);
                context.SaveChanges();
            }

            if (!context.Events.Any())
            {
                var events = new List<Event>
                {
                    new Event
                    {
                      Type = EventType.Income,
                      Amount = 1250,
                      CategoryId = catEat.Id,
                      CreationDate = new DateTime(2017, 11, 1, 10, 15, 30),
                      Description = "Подарили еду в гостях"
                    },
                    new Event
                    {
                      Type = EventType.Income,
                      Amount = 1250,
                      CategoryId = catEat.Id,
                      CreationDate = new DateTime(2017, 11, 1, 14, 33, 30),
                      Description = "Поход в окей с баллами. Бесплатная еда"
                    },
                    new Event
                    {
                      Type = EventType.Outcome,
                      Amount = 3700,
                      CategoryId = catEat.Id,
                      CreationDate = new DateTime(2017, 11, 1, 14, 33, 30),
                      Description = "Купил еду на неделю"
                    },
                    new Event
                    {
                      Type = EventType.Outcome,
                      Amount = 1300,
                      CategoryId = catHome.Id,
                      CreationDate = new DateTime(2017, 11, 1, 16, 47, 30),
                      Description = "Купил посудомоечную машину"
                    },
                    new Event
                    {
                      Type = EventType.Outcome,
                      Amount = 4000,
                      CategoryId = catCar.Id,
                      CreationDate = new DateTime(2017, 11, 2, 14, 33, 30),
                      Description = "Заправка + ТО"
                    }
                };

                context.Events.AddRange(events);
                context.SaveChanges();
            }

            if (!context.Roles.Any(r => r.Name == "Administrator"))
            {
                await roleManager.CreateAsync(new IdentityRole("Administrator"));
            }

            var username = "test@test.com";
            var password = "Test_0";

            if (!context.Users.Any(x => x.Email == username))
            {
                await userManager.CreateAsync(new User { UserName = username, Email = username, EmailConfirmed = true }, password);
                await userManager.AddToRoleAsync(await userManager.FindByNameAsync(username), "Administrator");
            }

            if (!context.Bills.Any())
            {
                var user = userManager.FindByNameAsync(username).Result;

                context.Bills.Add(new Bill { UserId = user.Id, Currency = Currency.RUB, Value = 100000 });
                context.SaveChanges();
            }
        }
    }
}
