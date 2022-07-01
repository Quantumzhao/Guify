using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Guify.Models;
using Guify.Models.Components;

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

	private async void BrowseFolder(object? sender, RoutedEventArgs e)
	{
		var dialog = new OpenFolderDialog();
		
		var result = await dialog.ShowAsync(MainWindow.Instance);

		if (result != null && DataContext is SelectFolderField s)
		{
			s.Value = result;
		}
	}

	private async void OpenFile(object? sender, RoutedEventArgs e)
	{
		if ((sender as IControl)?.DataContext is not OpenFileField of) return;

		var dialog = new OpenFileDialog();
		dialog.Directory = of.CustomDefaultFolder;
		dialog.InitialFileName = of.CustomDefaultFileName;
		dialog.AllowMultiple = of.AllowMultiple;
		
		var result = await dialog.ShowAsync(MainWindow.Instance);

		if (result != null && DataContext is OpenFileField o)
		{
			o.Value = result;
		}
	}

	private async void SaveFile(object? sender, RoutedEventArgs e)
	{
		if ((sender as IControl)?.DataContext is not SaveFileField sf) return;

		var dialog = new SaveFileDialog();
		dialog.Directory = sf.CustomDefaultFolder;
		dialog.InitialFileName = sf.CustomDefaultFileName;

		var result = await dialog.ShowAsync(MainWindow.Instance);

		if (result != null)
		{
			sf.Value = result;
		}
	}
}