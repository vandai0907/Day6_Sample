using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Day6_Sample.Interfaces;
using Day6_Sample.Models;
using Day6_Sample.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;

namespace Day6_Sample.ViewModels
{
    public partial class RightMenuVM : ObservableObject
    {
        private int _quantity;
        private readonly ICartService _cartService;

        [ObservableProperty]
        private Visibility _haveProducts = Visibility.Hidden;

        public int Quantity
        {
            get => _quantity;
            set
            {
                _quantity = value;
                HaveProducts = value != 0 ? Visibility.Visible : Visibility.Hidden;
                OnPropertyChanged();
            }
        }

        public RightMenuVM()
        {
            var serviceProvider = App.GetServiceProvider();
            _cartService = serviceProvider.GetService<CartService>();

            Quantity = _cartService.GetTotalCart();
            WeakReferenceMessenger.Default.Register<ListCart>(this, OnUpdateCart);
        }

        private void OnUpdateCart(object recipient, ListCart message)
        {
            Quantity = _cartService.GetTotalCart();
        }

        [RelayCommand]
        private void Navigate(string url)
        {
            WeakReferenceMessenger.Default.Send<NavigateWindow>(new NavigateWindow(url));
        }
    }
}
