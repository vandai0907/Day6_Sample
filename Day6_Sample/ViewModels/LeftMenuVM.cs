using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using Day6_Sample.Interfaces;
using Day6_Sample.Models;
using Day6_Sample.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Day6_Sample.ViewModels;

public partial class LeftMenuVM : ObservableObject
{
    private Product _selectedProduct;
    private List<Product> _products;
    private readonly IProductService _productService;

    public List<Product> Products
    {
        get => _products;
        set
        {
            _products = value;
            OnPropertyChanged();
        }
    }

    public Product SelectedProduct
    {
        get => _selectedProduct;
        set
        {
            _selectedProduct = value;
            OnPropertyChanged();
            WeakReferenceMessenger.Default.Send<SelectedProduct>(new SelectedProduct(value));
        }
    }

    public LeftMenuVM()
    {
        var serviceProvider = App.GetServiceProvider();
        _productService = serviceProvider.GetService<ProductService>();

        Init();
        WeakReferenceMessenger.Default.Register<ListProduct>(this, OnProductsUpdate);
    }

    private void OnProductsUpdate(object recipient, ListProduct message)
    {
        Init();
    }

    private void Init()
    {
        Products = _productService.GetProducts();
        SelectedProduct = Products.FirstOrDefault();

    }
}