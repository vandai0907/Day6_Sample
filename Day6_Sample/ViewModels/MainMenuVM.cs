using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Day6_Sample.Interfaces;
using Day6_Sample.Models;
using Day6_Sample.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Windows.Input;

namespace Day6_Sample.ViewModels;

public partial class MainMenuVM : ObservableObject
{

    private Product _product;
    private bool _isEnableSub;
    private bool _isEnablePlus;
    private int _quantity;
    private readonly ICartService _cartService;
    private readonly IProductService _productService;
    public ICommand AddCommand { get; }
    public ICommand CountQuantityCommand { get; }

    public Product Product
    {
        get => _product;
        set
        {
            _product = value;
            OnPropertyChanged();
        }
    }

    public bool IsEnableSub
    {
        get => _isEnableSub;
        set
        {
            _isEnableSub = value;
            OnPropertyChanged();
        }
    }

    public bool IsEnablePlus
    {
        get => _isEnablePlus;
        set
        {
            _isEnablePlus = value;
            OnPropertyChanged();
        }
    }

    public int Quantity
    {
        get => _quantity;
        set
        {
            _quantity = value;
            OnPropertyChanged();
        }
    }

    public MainMenuVM()
    {
        var serviceProvider = App.GetServiceProvider();
        _cartService = serviceProvider.GetService<CartService>();
        _productService = serviceProvider.GetService<ProductService>();

        Product = _productService.GetProducts().FirstOrDefault();
        CountQuantity(string.Empty);
        WeakReferenceMessenger.Default.Register<SelectedProduct>(this, OnSelectedProduct);

        AddCommand = new RelayCommand(Add);
        CountQuantityCommand = new RelayCommand<string>(CountQuantity);
    }

    private void OnSelectedProduct(object recipient, SelectedProduct message)
    {
        if (message.Product is Product value)
            Product = new Product()
            {
                Id = value.Id,
                Name = value.Name,
                Description = value.Description,
                Quantity = value.Quantity,
                Image = value.Image,
                Like = value.Like,
                Person = value.Person,
                Price = value.Price,
            };
        Quantity = 0;
        CountQuantity(string.Empty);
    }

    private void CountQuantity(string count)
    {
        int.TryParse(count, out int value);
        Quantity += value;
        Product.Quantity -= value;
        IsEnablePlus = Product.Quantity != 0;
        IsEnableSub = Quantity != 0;
    }

    private void Add()
    {
        var product = new Product()
        {
            Id = _product.Id,
            Name = _product.Name,
            Description = _product.Description,
            Quantity = _quantity,
            Person = _product.Person,
            Image = _product.Image,
            Like = _product.Like,
            Price = _product.Price
        };
        _cartService.AddToCart(product);
        _productService.UpdateQuantity(Product);
        WeakReferenceMessenger.Default.Send<ListProduct>();
        WeakReferenceMessenger.Default.Send<ListCart>(new ListCart());
    }
}