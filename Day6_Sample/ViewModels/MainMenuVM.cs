using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Day6_Sample.Interfaces;
using Day6_Sample.Models;
using Day6_Sample.Services;
using System.Windows.Input;

namespace Day6_Sample.ViewModels;

public class MainMenuVM : ObservableObject
{
    private Product _product;
    private bool _isEnableSub;
    private bool _isEnablePlus;
    private int _quantity;
    private readonly ICartService _cartService = new CartService();
    private readonly IProductService _productService = new ProductService();

    public ICommand CountQuantity { get; set; }

    public ICommand AddCommand { get; set; }

    public Product Product
    {
        get => _product;
        set
        {
            if (Equals(value, _product)) return;
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
        CountQuantity = new RelayCommand<string>(OnCountQuantityClick);
        Product = _productService.GetProducts().FirstOrDefault();
        OnCountQuantityClick(string.Empty);
        WeakReferenceMessenger.Default.Register<SelectedProduct>(this, OnSelectedProduct);
        AddCommand = new RelayCommand(OnAddProduct);

    }

    private void OnSelectedProduct(object recipient, SelectedProduct message)
    {
        Product = message.Product;
        Quantity = 0;
        OnCountQuantityClick(string.Empty);
    }

    private void OnCountQuantityClick(string count)
    {
        if (Product is null) return;
        int.TryParse(count, out int value);
        Quantity += value;
        Product.Quantity -= value;
        IsEnablePlus = Product.Quantity != 0;
        IsEnableSub = Quantity != 0;
    }

    private void OnAddProduct()
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