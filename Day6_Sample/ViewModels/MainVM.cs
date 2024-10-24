using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Day6_Sample.Models;
using System.Windows;
using System.Windows.Input;

namespace Day6_Sample.ViewModels;

public class MainVM : ObservableObject
{
    private string _url = "..\\UserControls\\ProductsUC.xaml";
    private WindowState _windowState;
    private bool _showInTaskbar;
    public ICommand CloseCommand { get; }

    public string Url
    {
        get => _url;
        set
        {
            if (value == _url) return;
            _url = value;
            OnPropertyChanged();
        }
    }

    public WindowState WindowState
    {
        get => _windowState;
        set
        {
            ShowInTaskbar = true;
            SetProperty(ref _windowState, value);
            ShowInTaskbar = value != WindowState.Minimized;
        }
    }

    public bool ShowInTaskbar
    {
        get => _showInTaskbar;
        set
        {
            _showInTaskbar = value;
            OnPropertyChanged();
        }
    }

    public MainVM()
    {
        Data.InitializeDatabase();
        Data.Init();
        var listProduct = new List<Product>
        {
            new Product { Id = "1", Name = "Product 1", Description = "Description 1", Price = 100, Like = 43.5, Person = 10, Quantity = 3, Image = "../Images/Food1.png"},
            new Product { Id = "2", Name = "Product 2", Description = "Description 2", Price = 150, Like = 53.2, Person = 8, Quantity = 1, Image = "../Images/Food2.png" },
            new Product { Id = "3", Name = "Product 3", Description = "Description 3", Price = 130, Like = 40.7, Person = 12, Quantity = 2, Image = "../Images/Food3.png" },
            new Product { Id = "4", Name = "Product 4", Description = "Description 4", Price = 140, Like = 53.2, Person = 13, Quantity = 4, Image = "../Images/Food4.png" },
            new Product { Id = "5", Name = "Product 5", Description = "Description 5", Price = 120, Like = 12.3, Person = 15, Quantity = 5, Image = "../Images/Food5.png" },
        };
        var data = Data.GetData();
        if (!data.Any())
        {
            foreach (var product in listProduct)
            {
                Data.AddData(product);
            }
        }
        WeakReferenceMessenger.Default.Register<NavigateWindow>(this, OnChangedPage);
        CloseCommand = new RelayCommand(OnCloseClick);
    }

    private void OnCloseClick()
    {
        WindowState = WindowState.Minimized;
    }

    private void OnChangedPage(object recipient, NavigateWindow message)
    {
        Url = message.Url;
    }
}