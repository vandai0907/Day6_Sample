using CommunityToolkit.Mvvm.Messaging;
using Day6_Sample.Models;
using System.Windows.Controls;
using System.Windows.Input;

namespace Day6_Sample.UserControls
{
    /// <summary>
    /// Interaction logic for CartMenuUC.xaml
    /// </summary>
    public partial class CartMenuUC : UserControl
    {
        public CartMenuUC()
        {
            InitializeComponent();
        }

        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            WeakReferenceMessenger.Default.Send<NavigateWindow>(new NavigateWindow("..\\UserControls\\ProductsUC.xaml"));
        }
    }
}
