// stfu MainWindow.Instance can't be null!
#pragma warning disable CS8618

using System.ComponentModel;
using Avalonia.Controls;
using Guify.IO;
using Avalonia;
using Avalonia.Interactivity;
using Guify.Models;
using Guify.Models.Components;
using MessageBox.Avalonia;

namespace Guify.Views;

public partial class MainWindow : Window, INotifyPropertyChanged
{
	public static MainWindow Instance;

	public MainWindow()
	{
		InitializeComponent();

		if (Program.Root == null) return;
		// else HelpText = ShellUtils.GetHelpInfo(Program.Root);

		// HelpTextBlock.Text = HelpText;
		// MainTitle.Text = Program.ProfileName ?? string.Empty;
		MainView.DataContext = Program.Root?.Content;
		MainButton.DataContext = Program.Root;
		Ring.DataContext = Program.Root;
		
		Instance = this;
	}

	private void Execute(object? sender, RoutedEventArgs e) => Program.Run();
}
