using System.ComponentModel;
using Avalonia.Controls;
using Guify.IO;
using Avalonia;
using Guify.Models;
using Guify.Models.Components;

namespace Guify.Views;

public partial class MainWindow : Window, INotifyPropertyChanged
{
    public MainWindow()
    {
        InitializeComponent();

        if (Program.Root == null) throw new ArgumentNullException();
        else HelpText = ShellUtils.GetHelpInfo(Program.Root);

        HelpTextBlock.Text = HelpText;
        MainView.DataContext = MainPageContent;
        // debug.Text = MainPageContent.ToString();
    }

    public string HelpText { get; set; }

    public object MainPageContent => (Program.Root?.FlatItems, Program.Root?.Verbs) switch
    {
        (ComponentBase[] items, null) => items,
        (null, Verb[] verbs) => verbs,
        _ => throw new ArgumentException()
    };
}
