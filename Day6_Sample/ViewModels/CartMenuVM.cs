using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Day6_Sample.Interfaces;
using Day6_Sample.Models;
using Day6_Sample.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.ObjectModel;
using System.Windows.Forms;
using System.Windows.Input;

namespace Day6_Sample.ViewModels;

public partial class CartMenuVM : ObservableObject
{
    private double _totalPrice;
    private double _tax;
    private double _serviceCharge;
    private ObservableCollection<Cart> _cart;
    private readonly ICartService _cartService;
    private readonly IProductService _productService;
    public ICommand AddCommand { get; }
    public ICommand PlusQuantityCommand { get; }
    public ICommand SubQuantityCommand { get; }
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
        var serviceProvider = App.GetServiceProvider();
        _cartService = serviceProvider.GetService<CartService>();
        _productService = serviceProvider.GetService<ProductService>();

        Init();
        CalculatePrice();

        AddCommand = new RelayCommand(Add);
        PlusQuantityCommand = new RelayCommand<object>(PlusQuantity);
        SubQuantityCommand = new RelayCommand<object>(SubQuantity);
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

    private void PlusQuantity(object obj)
    {
        if (obj is Cart cart)
        {
            OnCountQuantityClick(1, cart);
        }
    }

    private void SubQuantity(object obj)
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
        if (quantity == 0)
        {
            var decision = MessageBox.Show("Xóa sản phẩm khỏi giỏ hàng", "Xác nhận", MessageBoxButtons.YesNo,
                MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (decision == DialogResult.No) return;
        }
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

    private void Add()
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