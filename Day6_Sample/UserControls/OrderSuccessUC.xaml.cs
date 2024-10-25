using CommunityToolkit.Mvvm.Messaging;
using Day6_Sample.Models;
using System.Windows;
using System.Windows.Controls;

namespace Day6_Sample.UserControls
{
    /// <summary>
    /// Interaction logic for OrderSuccessUC.xaml
    /// </summary>
    public partial class OrderSuccessUC : UserControl
    {
        public OrderSuccessUC()
        {
            InitializeComponent();
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            WeakReferenceMessenger.Default.Send<NavigateWindow>(new NavigateWindow("..\\UserControls\\CartMenuUC.xaml"));
        }

        private void Image_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            WeakReferenceMessenger.Default.Send<NavigateWindow>(new NavigateWindow("..\\UserControls\\CartMenuUC.xaml"));
        }
    }
}
