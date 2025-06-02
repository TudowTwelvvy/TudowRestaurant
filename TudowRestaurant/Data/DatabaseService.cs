using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using TudowRestaurant.Models;

namespace TudowRestaurant.Data
{
    public class DatabaseService : IAsyncDisposable
    {
        private readonly SQLiteAsyncConnection _connection;
        public DatabaseService()
        {
            var dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),"restaurantPOS.db3");

            _connection = new SQLiteAsyncConnection(dbPath, SQLiteOpenFlags.Create | SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.SharedCache);
        }

        public async Task InitializeDatabaseAsync()
        {
            //create table if it does not exist
            await _connection.CreateTableAsync<MenuCategory>();
            await _connection.CreateTableAsync<MenuItem>();
            await _connection.CreateTableAsync<MenuItemCategoryMapping>();
            await _connection.CreateTableAsync<Order>();
            await _connection.CreateTableAsync<OrderItem>();

            await SeedDataAsync();

            //test to see
            var menuItems = await GetMenuItemsByCategoryIdAsync(2);

        }

        private async Task SeedDataAsync()
        {
            var firstCategory = await _connection.Table<MenuCategory>().FirstOrDefaultAsync();

            if (firstCategory != null)
            {
                return; // if database is already seeded
            }

            var categories = SeedData.GetMenuCategories();
            var menuItems = SeedData.GetMenuItems();
            var mappings = SeedData.GetMenuItemCategoryMappings();

            //add to database
            await _connection.InsertAllAsync(categories);
            await _connection.InsertAllAsync(menuItems);
            await _connection.InsertAllAsync(mappings);
        }

        public async Task<MenuCategory[]> GetMenuCategoriesAsync() => await _connection.Table<MenuCategory>().ToArrayAsync();

        public async Task<MenuItem[]> GetMenuItemsByCategoryIdAsync(int categoryId)
        {
            var query = @"
                            SELECT menu.*
                            FROM MenuItem AS menu
                                INNER JOIN MenuItemCategoryMapping AS mapping
                                    ON menu.Id = mapping.MenuItemId
                            WHERE mapping.CategoryId = ?
                        ";
            var menuItems = await _connection.QueryAsync<MenuItem>(query, categoryId);

            return [.. menuItems];
        }

        public async Task<string?> PlaceOrderAsync(OrderModel model)
        {
            var newOrder = new Order
            {
                OrderDate = model.OrderDate,
                PaymentMode = model.PaymentMode,
                TotalAmountPaid = model.TotalAmountPaid,
                TotalItemsCount = model.TotalItemsCount
            };

            if (await _connection.InsertAsync(newOrder) > 0)
            {
                //order inserted succesfully
                //Mow i have newly inserted Order Id in the order.Id
                //now i can add the orderId to the OrderItems AND INSERT OrderItems IN THE DATABASE

                foreach (var item in model.Items)
                {
                    item.OrderId = newOrder.Id;
                }
                if (await _connection.InsertAllAsync(model.Items) == 0)
                {
                    //OrderItems insert operation failed
                    //Remove the newly inserted Order in the method
                    await _connection.DeleteAsync(newOrder);
                    return "Error inserting order items";
                }
            }
            else
            {
                return "Error inserting order";
            }
            model.Id = newOrder.Id;
            return null;
        }


        public async ValueTask DisposeAsync()
        {
            if (_connection != null)
            {
                await _connection.CloseAsync();
            }

        }
    }
}
