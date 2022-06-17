using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

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
}