using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace Guify.Views.Components;

public partial class StringField : UserControl
{
    public StringField()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}