global using System.Collections.Generic;
global using System;

using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Guify.ViewModels;
using Guify.Views;

namespace Guify;

public partial class App : Application
{
	public override void Initialize()
	{
		AvaloniaXamlLoader.Load(this);
	}

	public override void OnFrameworkInitializationCompleted()
	{
		if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
		{
			desktop.MainWindow = new MainWindow
			{
				DataContext = new MainWindowViewModel(),
			};
		}

		base.OnFrameworkInitializationCompleted();
	}
}
