using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Day6_Sample.Interfaces;
using Day6_Sample.Models;
using Day6_Sample.Services;
using System.Windows;
using System.Windows.Input;

namespace Day6_Sample.ViewModels
{
    public class RightMenuVM : ObservableObject
    {
        private Visibility _haveProducts = Visibility.Hidden;
        private int _quantity;
        private readonly ICartService _cartService = new CartService();
        public ICommand NavigateCommand { get; }

        public Visibility HaveProducts
        {
            get => _haveProducts;
            set
            {
                _haveProducts = value;
                OnPropertyChanged();
            }
        }

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
            NavigateCommand = new RelayCommand<string>(OnNavigateClick);
            Quantity = _cartService.GetTotalCart();
            WeakReferenceMessenger.Default.Register<ListCart>(this, OnUpdateCart);
        }

        private void OnUpdateCart(object recipient, ListCart message)
        {
            Quantity = _cartService.GetTotalCart();
        }

        private void OnNavigateClick(string url)
        {
            WeakReferenceMessenger.Default.Send<NavigateWindow>(new NavigateWindow(url));
        }
    }
}
