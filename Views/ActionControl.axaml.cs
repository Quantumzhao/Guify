using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace Guify.Views;

public partial class ActionControl : UserControl
{
    public ActionControl()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}