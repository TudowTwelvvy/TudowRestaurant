using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Controls;
using TudowRestaurant.Data;
using TudowRestaurant.Models;

namespace TudowRestaurant.ViewModels
{
    public partial class OrdersViewModel : ObservableObject
    {
        private readonly DatabaseService _databaseService;

        public OrdersViewModel(DatabaseService databaseService)
        {
            _databaseService = databaseService;
        }


        //return true if the order creation was succesful or false if NOT
        public async Task<bool> PlaceOrderAsync(CartModel[] cartItems, bool isPaidCash)
        {
            var orderItems = cartItems
                .Select(item => new OrderItem
                {
                    ItemId = item.ItemId,
                    Name = item.Name,
                    Icon = item.Icon,
                    Price = item.Price,
                    Quantity = item.Quantity
                }).ToArray();

            var order = new OrderModel
            {
                OrderDate = DateTime.Now,
                PaymentMode = isPaidCash ? "Cash" : "Online",
                TotalAmountPaid = cartItems.Sum(i => i.Amount),
                TotalItemsCount = cartItems.Length,
                Items = orderItems
            };

            var errorMessage = await _databaseService.PlaceOrderAsync(order);

            if (!string.IsNullOrEmpty(errorMessage))
            {
                //order creation succesfull
                await Shell.Current.DisplayAlert("Error", errorMessage, "OK");
                return false;
            }
            //order creation was succesfull
            await Toast.Make("Order created successfully").Show();
            //Orders.Add(order);
            return true;
        }

    }
}
