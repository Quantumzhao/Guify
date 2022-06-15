using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace Guify.Views;

public partial class TestStyle : Window
{
    public TestStyle()
    {
        InitializeComponent();
#if DEBUG
        this.AttachDevTools();
#endif
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}