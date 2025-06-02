using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TudowRestaurant.Data;
using CommunityToolkit.Mvvm.ComponentModel;
using TudowRestaurant.Models;
using Microsoft.Maui.Controls;
using CommunityToolkit.Mvvm.Input;
using MenuItem = TudowRestaurant.Data.MenuItem;
using System.Collections.ObjectModel;


namespace TudowRestaurant.ViewModels
{
    public partial class HomeViewModel : ObservableObject
    {
        private readonly DatabaseService _databaseService;
        private readonly OrdersViewModel _ordersViewModel;

        [ObservableProperty]
        private MenuCategoryModel[] _categories = [];

        [ObservableProperty]
        private MenuItem[] _menuItems = [];

        [ObservableProperty]
        private MenuCategoryModel? _selectedCategory = null;

        public ObservableCollection<CartModel> CartItems { get; set; } = new();

        [ObservableProperty]
        private bool _isLoading;

        [ObservableProperty, NotifyPropertyChangedFor(nameof(TaxAmount)), NotifyPropertyChangedFor(nameof(Total))]
        private decimal _subtotal;

        [ObservableProperty, NotifyPropertyChangedFor(nameof(TaxAmount)), NotifyPropertyChangedFor(nameof(Total))]
        private int _taxPrecentage;

        public decimal TaxAmount => (Subtotal * TaxPrecentage) / 100;

        public decimal Total => Subtotal + TaxAmount;

        public HomeViewModel(DatabaseService databaseService, OrdersViewModel ordersViewModel)
        {
            _databaseService = databaseService;
            _ordersViewModel = ordersViewModel;

            CartItems.CollectionChanged += (sender, args) => RecalculateAmounts();
        }

        private bool _isInitialized;
        public async ValueTask InitializeAsync()
        {
            if (_isInitialized) return; //if its already initialized

            _isInitialized = true;

            IsLoading = true;

            //fetch the categories
            Categories = (await _databaseService.GetMenuCategoriesAsync())
                            .Select(MenuCategoryModel.FromEntity)
                            .ToArray();

            Categories[0].IsSelected = true;
            SelectedCategory = Categories[0];

           MenuItems = await _databaseService.GetMenuItemsByCategoryIdAsync(SelectedCategory.Id);


            IsLoading = false;
        }

        [RelayCommand]
        private async Task SelectCategoryAsync(int categoryId)
        {
            if (SelectedCategory?.Id == categoryId)
                return; // Already selected

            IsLoading = true;

            var currentSelectedCategory = Categories.First(c => c.IsSelected);
            currentSelectedCategory.IsSelected = false;

            var newSelectedCategory = Categories.First(c => c.Id == categoryId);
            newSelectedCategory.IsSelected = true;

            SelectedCategory = newSelectedCategory;

            //await Task.Delay(3000);
            MenuItems = await _databaseService.GetMenuItemsByCategoryIdAsync(SelectedCategory.Id);

            IsLoading = false;
        }

        [RelayCommand]
        private void AddToCart(MenuItem menuItem)
        {
            var cartItem = CartItems.FirstOrDefault(c => c.ItemId == menuItem.Id);
            if (cartItem == null)
            {
                //Item doesn't exist in the caet
                //Add item to cart
                cartItem = new CartModel()
                {
                    ItemId = menuItem.Id,
                    Name = menuItem.Name,
                    Price = menuItem.Price,
                    Icon = menuItem.Icon,
                    Quantity = 1
                };
                CartItems.Add(cartItem);
            }
            else
            {
                //This item exists in cart
                //increase the quantity for this in the cart
                cartItem.Quantity++;
                RecalculateAmounts();
            }
        }

        [RelayCommand]
        private void IncreaseQuantity(CartModel cartItem)
        {
            cartItem.Quantity++;
            RecalculateAmounts();
        }

        [RelayCommand]
        private void DecreaseQuantity(CartModel cartItem)
        {
            cartItem.Quantity--;
            if (cartItem.Quantity == 0)
            {
                CartItems.Remove(cartItem);
            }
            else
            {
                RecalculateAmounts();
            }
        }

        [RelayCommand]
        private void RemoveItemFromCart(CartModel cartItem) => CartItems.Remove(cartItem);

        private void RecalculateAmounts() => Subtotal = CartItems.Sum(i => i.Amount);

        [RelayCommand]
        private async Task TaxPercentageClickAsync()
        {
            var result = await Shell.Current.DisplayPromptAsync("Tax Percentage", "Enter tax percentage", placeholder: "10", initialValue: TaxPrecentage.ToString());
            if (!string.IsNullOrWhiteSpace(result))
            {
                if (!int.TryParse(result, out int enteredTaxPercentage))
                {
                    await Shell.Current.DisplayAlert("Invalid value", "Please enter a valid number", "OK");
                    return;
                }

                if (enteredTaxPercentage > 100 || enteredTaxPercentage < 0)
                {
                    await Shell.Current.DisplayAlert("Invalid value", "Tax percentage must be between 0 and 100", "OK");
                    return;
                }

                TaxPrecentage = enteredTaxPercentage;

                
            }

            
        }

        [RelayCommand]
        private async Task ClearCartAsync()
        {
            if (CartItems.Count > 0)
            {
                if (await Shell.Current.DisplayAlert("Clear Order", "Are you sure you want to clear the order?", "Yes", "No"))
                {
                    CartItems.Clear();
                }
            }
        }

        [RelayCommand]
        private async Task PlaceOrderAsync(bool isPaidCash)
        {
            IsLoading = true;
            //await _ordersViewModel.PlaceOrderAsync([..CartItems], isPaidCash);
            if (CartItems.Count == 0)
            {
                return;
            }

            if (await Shell.Current.DisplayAlert("Close Order", "Are you sure you want to close the order?", "Yes", "No"))
            {
                IsLoading = true;
                if (await _ordersViewModel.PlaceOrderAsync([.. CartItems], isPaidCash))
                {
                    //Order creation succesfull
                    //Clear the cart menu
                    CartItems.Clear();
                }
                IsLoading = false;
            }
            IsLoading = false;

        }

        
    }

}
