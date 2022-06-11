using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

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
}