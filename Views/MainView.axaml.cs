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

        private Page[] Pages { get; set; } = new Page[0];

        public void OnMainViewLoaded(object sender, EventArgs args) {

            // var tabControl = new TabControl();
            // tabControl.Items = pages;
        }

        private UserControl[] RenderPages(Page[] pages) {
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