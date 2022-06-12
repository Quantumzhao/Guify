using System.ComponentModel;
using Avalonia.Controls;
using Guify.IO;
using Avalonia;

namespace Guify.Views;

public partial class MainWindow : Window, INotifyPropertyChanged
{
    public MainWindow()
    {
        InitializeComponent();

        if (Program.Root == null) throw new ArgumentNullException();
        else HelpText = ShellUtils.GetHelpInfo(Program.Root);

        HelpTextBlock.Text = HelpText;
    }

    public string HelpText { get; set; }
}
