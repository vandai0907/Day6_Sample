using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using Application = System.Windows.Application;

namespace Day6_Sample.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Init();
            RenderOptions.SetBitmapScalingMode(this, BitmapScalingMode.Fant);
        }

        private void Init()
        {
            var ni = new NotifyIcon();
            ni.Icon = new System.Drawing.Icon("Images/icon.ico");
            ni.Visible = true;
            ni.DoubleClick += NiOnDoubleClick;

            var menu = new ContextMenu();
            var item = new MenuItem();
            item.Header = "Exit";
            item.Click += Item_Click;
            menu.Items.Add(item);
        }

        private void Item_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void NiOnDoubleClick(object sender, EventArgs e)
        {
            this.ShowInTaskbar = true;
            this.WindowState = WindowState.Normal;
        }

        private void TitleBar_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                System.Windows.Application.Current.MainWindow?.DragMove();
            }
        }

        private void CloseButton_OnClick(object sender, RoutedEventArgs e)
        {
            this.ShowInTaskbar = false;
            this.WindowState = WindowState.Minimized;

        }

        private void MaxButton_OnClick(object sender, RoutedEventArgs e)
        {
            this.WindowState = this.WindowState == WindowState.Normal ? WindowState.Maximized : WindowState.Normal;
        }

        private void MinButton_OnClick(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
    }
}
