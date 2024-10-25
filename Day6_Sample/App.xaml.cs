using Day6_Sample.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;

namespace Day6_Sample
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static IServiceCollection _services;
        private static IServiceProvider _serviceProvider;

        public App()
        {
            _services = new ServiceCollection();
            ConfigureServices(_services);
            _serviceProvider = _services.BuildServiceProvider();
        }

        private void ConfigureServices(IServiceCollection services)
        {
            _services.AddSingleton<DatabaseService>();
            _services.AddSingleton<CartService>();
            _services.AddSingleton<ProductService>();
        }

        public static IServiceProvider GetServiceProvider()
        {
            return _serviceProvider;
        }
    }

}
