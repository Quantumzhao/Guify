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
	public MainWindow()
	{
		InitializeComponent();

		if (Program.Root == null) throw new ArgumentNullException();
		// else HelpText = ShellUtils.GetHelpInfo(Program.Root);

		// HelpTextBlock.Text = HelpText;
		MainTitle.Text = Program.ProfileName ?? string.Empty;
		MainView.DataContext = MainPageContent;
	}

	// public string HelpText { get; set; }
	
	public object MainPageContent => (Program.Root?.FlatItems, Program.Root?.Verbs) switch
	{
		(ComponentBase[] items, null) => items,
		(null, Verb[] verbs) => verbs,
		_ => throw new ArgumentException()
	};

	private void Execute(object? sender, RoutedEventArgs e)
	{
		try
		{
			Program.Run();
		}
		catch (WarningException)
		{
			var msgBox = MessageBoxManager.GetMessageBoxStandardWindow(
				"Missing Fields", "Some required fields are missing!!!");

			msgBox.Show();
		}
	}
}
