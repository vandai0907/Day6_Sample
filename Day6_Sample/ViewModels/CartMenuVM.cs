using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Day6_Sample.Interfaces;
using Day6_Sample.Models;
using Day6_Sample.Services;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Day6_Sample.ViewModels;

public class CartMenuVM : ObservableObject
{
    private ObservableCollection<Cart> _cart;
    private double _totalPrice;
    private double _tax;
    private double _serviceCharge;
    private readonly ICartService _cartService = new CartService();
    private readonly IProductService _productService = new ProductService();
    public ICommand SubQuantity { get; }
    public ICommand PlusQuantity { get; }
    public ICommand AddCommand { get; }

    public ObservableCollection<Cart> Cart
    {
        get => _cart;
        set
        {
            _cart = value;
            OnPropertyChanged();
        }
    }

    public double TotalPrice
    {
        get => _totalPrice;
        set
        {
            _totalPrice = Math.Round(value, 2);
            OnPropertyChanged();
        }
    }

    public double Tax
    {
        get => _tax;
        set
        {
            _tax = Math.Round(value, 2);
            OnPropertyChanged();
        }
    }

    public double ServiceCharge
    {
        get => _serviceCharge;
        set
        {
            _serviceCharge = Math.Round(value, 2);
            OnPropertyChanged();
        }
    }

    public CartMenuVM()
    {
        Init();
        CalculatePrice();
        AddCommand = new RelayCommand(OnAddProduct);
        SubQuantity = new RelayCommand<object>(OnSubClick);
        PlusQuantity = new RelayCommand<object>(OnPlusClick);
    }

    private void Init()
    {
        Cart = _cartService.GetCart();
        var products = _productService.GetProducts();
        foreach (var cart in Cart)
        {
            if (products.Exists(p => p.Id == cart.ProductId))
            {
                cart.IsEnablePlus = true;
            }
        }
    }

    private void OnPlusClick(object obj)
    {
        if (obj is Cart cart)
        {
            OnCountQuantityClick(1, cart);
        }
    }

    private void OnSubClick(object obj)
    {
        if (obj is Cart cart)
        {
            OnCountQuantityClick(-1, cart);
        }
    }

    private void OnCountQuantityClick(int value, Cart cart)
    {
        var product = _productService.GetProductById(int.Parse(cart.ProductId));
        int quantity = cart.Quantity + value;
        product.Quantity -= value;
        _productService.UpdateQuantity(product);
        _cartService.UpdateCart(cart, quantity);
        if (quantity == 0)
        {
            _cartService.DeleteCart(cart);
        }

        Init();

        WeakReferenceMessenger.Default.Send<ListProduct>();
        WeakReferenceMessenger.Default.Send<ListCart>(new ListCart());
        CalculatePrice();
    }

    private void OnAddProduct()
    {
        _cartService.Order();
        WeakReferenceMessenger.Default.Send<ListCart>(new ListCart());
        WeakReferenceMessenger.Default.Send<NavigateWindow>(new NavigateWindow("..\\UserControls\\OrderSuccessUC.xaml"));
    }

    private void CalculatePrice()
    {
        double sum = _cart.Sum(c => c.TotalPrice);
        Tax = sum - (sum * 0.89);
        TotalPrice = sum + _tax + _serviceCharge;
    }

}