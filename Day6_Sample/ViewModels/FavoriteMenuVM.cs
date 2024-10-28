using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Day6_Sample.Interfaces;
using Day6_Sample.Models;
using Day6_Sample.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Windows.Input;

namespace Day6_Sample.ViewModels
{
    internal partial class FavoriteMenuVM : ObservableObject
    {
        private List<string> _test;
        private string _selectedFilter;
        private List<Product> _products;
        private readonly IProductService _productService;
        private readonly int _itemsPerPage = 6;
        private readonly List<Product> _defaultList;

        public ICommand Command { get; }
        public ICommand DetailCommand { get; }

        public List<string> Test
        {
            get => _test;
            set
            {
                _test = value;
                OnPropertyChanged();
            }
        }

        public string SelectedFilter
        {
            get => _selectedFilter;
            set
            {
                _selectedFilter = value;
                OnPropertyChanged();
            }
        }

        public List<Product> Products
        {
            get => _products;
            set
            {
                _products = value;
                OnPropertyChanged();
            }
        }


        public FavoriteMenuVM()
        {
            var serviceProvider = App.GetServiceProvider();
            _productService = serviceProvider.GetService<ProductService>();

            Command = new RelayCommand<string>(OnClick);
            DetailCommand = new RelayCommand<object>(Detail);
            _defaultList = _productService.GetProductLiked();
            var totalPages = (int)Math.Ceiling((double)_defaultList.Count() / _itemsPerPage);
            var listFilter = new List<string>();
            for (int i = 1; i <= totalPages; i++)
            {
                listFilter.Add(i.ToString());
            }
            Test = listFilter;
            OnClick("1");
        }

        private void Detail(object obj)
        {
            if (obj is Product value)
            {
                ProductDetail.Product = value;
                WeakReferenceMessenger.Default.Send<NavigateWindow>(new NavigateWindow("..\\UserControls\\ProductDetailUC.xaml"));
            }
        }

        private void OnClick(string obj)
        {
            SelectedFilter = obj;
            int.TryParse(obj, out int value);
            var pageData = _defaultList.Skip((value - 1) * _itemsPerPage).Take(_itemsPerPage);
            Products = pageData.ToList();
        }
    }
}
