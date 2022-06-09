using Avalonia.Controls;

namespace Guify.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            var t = new TextBlock();
            t.Text = "test";
            this.Content = t;
        }
    }
}