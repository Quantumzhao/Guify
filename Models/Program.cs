using Avalonia;
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
	public static string Postfix { get; private set; } = string.Empty;
	public static Root? Root { get; set; }
	public static string? ProfileName { get; set; }

	// Initialization code. Don't use any Avalonia, third-party APIs or any
	// SynchronizationContext-reliant code before AppMain is called: things aren't initialized
	// yet and stuff might break.
	[STAThread]
	public static void Main(string[] args)
		=> Parser.Default.ParseArguments
			<AddUIOptions, RemoveUIOptions, WrapperOptions, ListUIOptions>(args)
			.WithParsed<AddUIOptions>(o =>
			{
				if (o.Path == null)
				{
					throw new ArgumentNullException(nameof(o.Path), "path is empty");
				}
				else
				{
					ConfigIO.AddUI(o.Path, o.IsLink is true);
				}
			})
			.WithParsed<RemoveUIOptions>(o =>
			{
				if (o.Name == null)
				{
					throw new ArgumentNullException("name is empty");
				}
				else
				{
					ConfigIO.RemoveUI(o.Name);
				}
			})
			.WithParsed<ListUIOptions>(o =>
			{
				IEnumerable<string> names;
				if (o.Substring == null) names = ConfigIO.GetEntries();
				else names = ConfigIO.GetEntries().Where(n => n.Contains(o.Substring));

				foreach (var n in names) Console.WriteLine(n);
			})
			.WithParsed<WrapperOptions>(o =>
			{
				if (o.Properties == null || o.Properties.Count() == 0)
					throw new InvalidOperationException();

				var ps = new LinkedList<string>(o.Properties);
				ProfileName = ps.First?.Value ?? string.Empty;
				ps.RemoveFirst();
				Postfix = string.Join(' ', ps);

				Root = XMLUtils.LoadRoot(ConfigIO.CombineName(ProfileName));

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

	public static void Run()
	{
		if (Root == null) throw new InvalidOperationException("Impossible");

		var command = $"{Root.Command} {Root.Compile()} {Postfix}";

		ShellUtils.Bash(command);

		Console.WriteLine(command);

		if (!Root.IsReusable) 
		{
			Console.WriteLine("Done.");
			Environment.Exit(0);
		}
	}
}
