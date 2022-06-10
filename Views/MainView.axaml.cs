using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Interactivity;
using Guify.Models.Components;
using Guify.Models.Containers;
using Guify.Models;
using Guify.IO;
using Guify.CLI;
using System.Linq;

namespace Guify.Views
{
    public partial class MainView : UserControl
    {
        public MainView()
        {
            InitializeComponent();
        }
        
        public void OnMainViewLoaded(object sender, EventArgs args) {

            // var tabControl = new TabControl();
            // tabControl.Items = pages;
            MainTab.Items = Program.Pages;
        }

        private UserControl[] RenderPages(Verb[] pages) {
            // return pages.Select(p => {
            //     var tabItem = new TabItem();
            //     tabItem.Header = p.Label;
            //     tabItem.Content = null;
            // });
			throw new NotImplementedException();
        }

        private void Execute(object? sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}