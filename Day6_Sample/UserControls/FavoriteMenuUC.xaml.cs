using CommunityToolkit.Mvvm.Messaging;
using Day6_Sample.Models;
using System.Windows.Controls;
using System.Windows.Input;

namespace Day6_Sample.UserControls
{
    /// <summary>
    /// Interaction logic for FavoriteMenuUC.xaml
    /// </summary>
    public partial class FavoriteMenuUC : UserControl
    {
        public FavoriteMenuUC()
        {
            InitializeComponent();
        }

        private void UIElement_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            WeakReferenceMessenger.Default.Send(new NavigateWindow(@"..\UserControls\ProductsUC.xaml"));
        }
    }
}
