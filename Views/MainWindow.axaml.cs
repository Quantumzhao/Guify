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
		
		Instance = this;
	}

	// public string HelpText { get; set; }
	
	public object MainPageContent => Program.Root?.Content switch
	{
		ComponentBase[] items => items,
		Verb[] verbs => verbs,
		_ => throw new ArgumentException()
	};
	
	private void Execute(object? sender, RoutedEventArgs e)
	{
		try
		{
			Program.Run();
		}
		catch (WarningException we)
		{
			var msgBox = MessageBoxManager.GetMessageBoxStandardWindow(
				"Missing Fields", we.Message);

			msgBox.Show();
		}
	}
}
