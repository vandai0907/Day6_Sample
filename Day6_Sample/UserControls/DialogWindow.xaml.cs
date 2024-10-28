using System.Windows;

namespace Day6_Sample.UserControls
{
    /// <summary>
    /// Interaction logic for DialogWindow.xaml
    /// </summary>
    public partial class DialogWindow : Window
    {
        public DialogWindow()
        {
            InitializeComponent();
        }

        private void Border_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            DialogResult = true;
            this.Close();
        }

        private void Border_MouseDown_1(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            DialogResult = false;
            this.Close();
        }
    }
}
