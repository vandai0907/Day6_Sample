using CommunityToolkit.Mvvm.Messaging;
using Day6_Sample.Models;
using Day6_Sample.ViewModels;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace Day6_Sample.UserControls
{
    /// <summary>
    /// Interaction logic for ProductDetailUC.xaml
    /// </summary>
    public partial class ProductDetailUC : UserControl
    {
        public ProductDetailUC()
        {
            InitializeComponent();
        }

        private void UIElement_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            WeakReferenceMessenger.Default.Send(new NavigateWindow(@"..\UserControls\FavoriteMenuUC.xaml"));
        }

        private void Retange_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            ((ProductDetailVM)DataContext).TestColor.Execute(null);
        }
        private void Geometry_Changed(object sender, EventArgs e)
        {
            DoubleAnimation animation = new DoubleAnimation(30, 35, TimeSpan.FromSeconds(0.2));
            animation.AutoReverse = true;
            Heart.BeginAnimation(Rectangle.HeightProperty, animation);
        }
    }
}
