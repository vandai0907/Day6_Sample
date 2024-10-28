using Day6_Sample.ViewModels;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace Day6_Sample.UserControls
{
    /// <summary>
    /// Interaction logic for MainMenuUC.xaml
    /// </summary>
    public partial class MainMenuUC : UserControl
    {
        public MainMenuUC()
        {
            InitializeComponent();
        }

        private void Geometry_Changed(object sender, EventArgs e)
        {
            DoubleAnimation animation = new DoubleAnimation(30, 35, TimeSpan.FromSeconds(0.2));
            animation.AutoReverse = true;
            Heart.BeginAnimation(Rectangle.HeightProperty, animation);
        }

        private void Rectangle_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            ((MainMenuVM)DataContext).TestColor.Execute(null);
        }
    }
}
