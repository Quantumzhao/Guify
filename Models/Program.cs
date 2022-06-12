﻿using Avalonia;
using Avalonia.ReactiveUI;
using CommandLine;
using Guify.CLI;
using Guify.Models.Components;
using Guify.Models;
using Guify.IO;
using System.Linq;
using System.Diagnostics;

namespace Guify;

class Program
{
	public static string RootCommand { get; private set; } = string.Empty;
	public static Root? Root { get; set; }

	// Initialization code. Don't use any Avalonia, third-party APIs or any
	// SynchronizationContext-reliant code before AppMain is called: things aren't initialized
	// yet and stuff might break.
	[STAThread]
	public static void Main(string[] args)
		=> Parser.Default.ParseArguments<AddUIOptions, RemoveUIOptions, WrapperOptions>(args)
			.WithParsed<AddUIOptions>(o =>
			{
				if (o.Name == null || o.Path == null)
				{
					throw new ArgumentNullException("name or path is null");
				}
				else
				{
					ConfigIO.AddEntry(o.Name, o.Path);
				}
			})
			.WithParsed<RemoveUIOptions>(o =>
			{
				if (o.Name == null)
				{
					throw new ArgumentNullException("name is null");
				}
				else
				{
					ConfigIO.RemoveEntry(o.Name);
				}
			})
			.WithParsed<WrapperOptions>(o =>
			{
				if (o.Properties == null || o.Properties.Count() == 0) { }
				else
					RootCommand = string.Join(' ', args);
				InitGUI();
				BuildAvaloniaApp().StartWithClassicDesktopLifetime(args);
				// Console.WriteLine("here");
				// ShellUtils.Bash("apt list");
			});

	// Avalonia configuration, don't remove; also used by visual designer.
	public static AppBuilder BuildAvaloniaApp()
		=> AppBuilder.Configure<App>()
			.UsePlatformDetect()
			.LogToTrace()
			.UseReactiveUI();

	private static void InitGUI()
	{
		Root = XMLUtils.LoadRoot(ConfigIO.FindPathEntry(RootCommand));
	}

	public static void Run()
	{
		if (Root == null) throw new InvalidOperationException("Impossible");

		var command = Root.Command + " " + Root.Compile();

		ShellUtils.Bash(command);
	}
}
