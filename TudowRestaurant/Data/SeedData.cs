using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TudowRestaurant.Data
{
    public static class SeedData
    {
        public static List<MenuCategory> GetMenuCategories()
        {
            return new List<MenuCategory>
            {
            new MenuCategory { Id = 1, Name = "Beverages", Icon = "drink.png" },
            new MenuCategory { Id = 2, Name = "Main Course", Icon = "meal.png" },
            new MenuCategory { Id = 3, Name = "Snacks", Icon = "snacks.png" },
            new MenuCategory { Id = 4, Name = "Desserts", Icon = "cake.png" },
            new MenuCategory { Id = 5, Name = "Fast Food", Icon = "fast_food.png" }
            };
        }
        public static List<MenuItem> GetMenuItems()
        {
            return new List<MenuItem>
            {
            new MenuItem { Id = 1, Name = "Black Label", Icon = "blacklabel.png", Description = "Champions beer 500ml", Price = 18.99m },
           new MenuItem { Id = 2, Name = "Spannish Cake", Icon = "cake.png", Description = "Delicious chocolate cake", Price = 49.99m },
           new MenuItem { Id = 3, Name = "Cakepan", Icon = "cakepan.jpg", Description = "Freshly baked cakes", Price = 34.99m },
           new MenuItem { Id = 4, Name = "Corona", Icon = "coronabeer.png", Description = "Extra Cold 6-pack", Price = 120.99m },
           new MenuItem { Id = 5, Name = "Italian ice-cream Cakes", Icon = "dessert.jpeg", Description = "Delicious italian cake", Price = 30.99m },
           new MenuItem { Id = 6, Name = "VIP dessert", Icon = "fulldessert.jpg", Description = "Rich chocolate dessert for rich people", Price = 249.99m },
           new MenuItem { Id = 7, Name = "Russian Kota", Icon = "headerkota.png", Description = "Delicious Kota", Price = 39.99m },
           new MenuItem { Id = 8, Name = "Soda", Icon = "soda.png", Description = "Refreshing soda", Price = 5.49m },
           new MenuItem { Id = 9, Name = "Jack Danials", Icon = "jackisky.png", Description = "Jennessee Whiskey No.7", Price = 300.49m },
           new MenuItem { Id = 10, Name = "Jonny Walker", Icon = "jjwalker.png", Description = "Red Label for kings", Price = 500.49m },
           new MenuItem { Id = 11, Name = "Beef burger", Icon = "kotabegger.jpg", Description = " Juicy hamburger", Price = 26.99m },
           new MenuItem { Id = 12, Name = "Chicken Meal", Icon = "kotachickenmeal.jpg", Description = "Quick and tasty full meal", Price = 170.99m },
           new MenuItem { Id = 13, Name = "Japan burger", Icon = "kotajapan.jpg", Description = "Japannes burger", Price = 79.99m },
           new MenuItem { Id = 14, Name = "Large Kota", Icon = "kotalag.jpeg", Description = "large kota for friends", Price = 60.99m },
           new MenuItem { Id = 15, Name = "French burger", Icon = "kotamaxi.jpg", Description = "Crispy french burger", Price = 120.99m },
           new MenuItem { Id = 16, Name = "Kota (Medium)", Icon = "kotanormal.jpg", Description = "best kota", Price = 32.99m },
           new MenuItem { Id = 17, Name = "Audialle", Icon = "kotarec.jpeg", Description = "Tasty and juicy", Price = 53.49m },
           new MenuItem { Id = 18, Name = "Beacon and Chips", Icon = "kotasmall.jpg", Description = "Best Beacon and Chips", Price = 34.99m },
           new MenuItem { Id = 19, Name = "Jonny Walker ", Icon = "jjwalkerblack.png", Description = "BlackLabel Premium", Price = 700.99m },
           new MenuItem { Id = 20, Name = "Lager", Icon = "lagerbeer.png", Description = "Castel Larger beer 360ml", Price = 16.49m },
           new MenuItem { Id = 21, Name = "Oreo", Icon = "oreodessertcups.jpg", Description = "Oreo Dessert Cup Cakes", Price = 50.99m },
           new MenuItem { Id = 22, Name = "PannaCotta", Icon = "pannacotta.jpg", Description = "South Indian dessert", Price = 60.99m },
           new MenuItem { Id = 23, Name = "Heineken", Icon = "nekenbeer.png", Description = "Grilled kebab", Price = 29.99m },
           new MenuItem { Id = 24, Name = "Pie", Icon = "peanutbutterpretzelpie.jpg", Description = "Peanutbutter pretzel pie", Price = 49.99m },
           new MenuItem { Id = 25, Name = "Tiramisu", Icon = "tiramisucheesecake", Description = "Sweet cheesecake", Price = 70.99m },
           new MenuItem { Id = 27, Name = "Biryani", Icon = "biryani.png", Description = "Spicy chicken biryani", Price = 7.99m },
           new MenuItem { Id = 28, Name = "Fish and Chips", Icon = "fish_and_chips.png", Description = "Crispy fish and chips", Price = 60.99m },
           new MenuItem { Id = 29, Name = "Fish", Icon = "fish.png", Description = "Grilled fish", Price = 20.99m },
           new MenuItem { Id = 30, Name = "Buns", Icon = "buns.png", Description = "Freshly baked buns", Price = 15.99m },
           new MenuItem { Id = 31, Name = "French Fries", Icon = "french_fries.png", Description = "Crispy french fries", Price = 20.99m },
           new MenuItem { Id = 32, Name = "Fried Chicken", Icon = "fried_chicken.png", Description = "Crispy fried chicken", Price = 50.99m },
           new MenuItem { Id = 33, Name = "Fried Egg", Icon = "fried_egg.png", Description = "Sunny_side_up fried egg", Price = 1.49m },
           new MenuItem { Id = 34, Name = "Hamburger", Icon = "hamburger.png", Description = "Juicy hamburger", Price = 50.99m },
           new MenuItem { Id = 35, Name = "Noodles", Icon = "noodles.png", Description = "Stir_fried noodles", Price = 46.99m },
           

            };
        }

        public static List<MenuItemCategoryMapping> GetMenuItemCategoryMappings()
        {
            return new List<MenuItemCategoryMapping>
            {
                new MenuItemCategoryMapping { CategoryId = 1, MenuItemId = 1 },
                new MenuItemCategoryMapping { CategoryId = 1, MenuItemId = 4 },
                new MenuItemCategoryMapping { CategoryId = 1, MenuItemId = 8 },
                new MenuItemCategoryMapping { CategoryId = 1, MenuItemId = 9 },
                new MenuItemCategoryMapping { CategoryId = 1, MenuItemId = 10 },
                new MenuItemCategoryMapping { CategoryId = 1, MenuItemId = 19 },
                new MenuItemCategoryMapping { CategoryId = 1, MenuItemId = 20 },
                new MenuItemCategoryMapping { CategoryId = 1, MenuItemId = 23 },
                new MenuItemCategoryMapping { CategoryId = 2, MenuItemId = 12 },
                new MenuItemCategoryMapping { CategoryId = 2, MenuItemId = 27 },
                new MenuItemCategoryMapping { CategoryId = 2, MenuItemId = 28 },
                new MenuItemCategoryMapping { CategoryId = 2, MenuItemId = 29 },
                new MenuItemCategoryMapping { CategoryId = 3, MenuItemId = 30 },
                new MenuItemCategoryMapping { CategoryId = 3, MenuItemId = 31 },
                new MenuItemCategoryMapping { CategoryId = 3, MenuItemId = 32 },
                new MenuItemCategoryMapping { CategoryId = 3, MenuItemId = 33 },
                new MenuItemCategoryMapping { CategoryId = 3, MenuItemId = 34 },
                new MenuItemCategoryMapping { CategoryId = 3, MenuItemId = 35 },
                new MenuItemCategoryMapping { CategoryId = 4, MenuItemId = 2 },
                new MenuItemCategoryMapping { CategoryId = 4, MenuItemId = 3 },
                new MenuItemCategoryMapping { CategoryId = 4, MenuItemId = 5 },
                new MenuItemCategoryMapping { CategoryId = 4, MenuItemId = 6 },
                new MenuItemCategoryMapping { CategoryId = 4, MenuItemId = 21 },
                new MenuItemCategoryMapping { CategoryId = 4, MenuItemId = 22},
                new MenuItemCategoryMapping { CategoryId = 4, MenuItemId = 25 },
                new MenuItemCategoryMapping { CategoryId = 4, MenuItemId = 26 },
                new MenuItemCategoryMapping { CategoryId = 5, MenuItemId = 7 },
                new MenuItemCategoryMapping { CategoryId = 5, MenuItemId = 11 },
                new MenuItemCategoryMapping { CategoryId = 5, MenuItemId = 13 },
                new MenuItemCategoryMapping { CategoryId = 5, MenuItemId = 14 },
                new MenuItemCategoryMapping { CategoryId = 5, MenuItemId = 15 },
                new MenuItemCategoryMapping { CategoryId = 5, MenuItemId = 16 },
                new MenuItemCategoryMapping { CategoryId = 5, MenuItemId = 17 },
                new MenuItemCategoryMapping { CategoryId = 5, MenuItemId = 18 },
                new MenuItemCategoryMapping { CategoryId = 5, MenuItemId = 24 }
            
            
            };
        }
    }
}
