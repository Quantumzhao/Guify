using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Guify.Models.Components;

namespace Guify.Views;

public partial class FieldControl : UserControl
{
    public FieldControl()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }

    private void Reset(object? sender, RoutedEventArgs e)
        => (DataContext as ComponentBase)?.Reset();
}