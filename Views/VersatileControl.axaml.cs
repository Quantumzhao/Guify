using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Guify.Models.Components;

namespace Guify.Views;

public partial class VersatileControl : UserControl
{
    public VersatileControl()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }

    private async void BrowseFolder(object? sender, RoutedEventArgs e)
    {
        var dialog = new OpenFolderDialog();
        var lifeTime = App.Current?.ApplicationLifetime as IClassicDesktopStyleApplicationLifetime;
        if (lifeTime == null) throw new InvalidOperationException("Wait that's illegal");
        
        var result = await dialog.ShowAsync(lifeTime.MainWindow);

        if (result != null && DataContext is SelectFolderField s)
        {
            s.Value = result;
        }
    }
}