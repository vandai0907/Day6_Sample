using System.Diagnostics;
using System.Windows.Controls;
using System.Windows.Input;

namespace Day6_Sample.UserControls
{
    /// <summary>
    /// Interaction logic for RightMenuUC.xaml
    /// </summary>
    public partial class RightMenuUC : UserControl
    {
        public RightMenuUC()
        {
            InitializeComponent();
        }

        private void FB_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            Process.Start(new ProcessStartInfo("http://www.facebook.com") { UseShellExecute = true });
        }

        private void Ins_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            Process.Start(new ProcessStartInfo("https://www.instagram.com") { UseShellExecute = true });
        }
    }
}
