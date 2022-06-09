using Avalonia;
using Avalonia.ReactiveUI;
using CommandLine;
using Guify.CLI;
using Guify.Models.Components;
using Guify.Models.Containers;
using Guify.Models;
using Guify.IO;
using System.Linq;

namespace Guify;

class Program {

	public static string RootCommand { get; private set; } = string.Empty;
	public static Page[] Pages = new Page[0];

	// Initialization code. Don't use any Avalonia, third-party APIs or any
	// SynchronizationContext-reliant code before AppMain is called: things aren't initialized
	// yet and stuff might break.
	[STAThread]
	public static void Main(string[] args)
		=> Parser.Default.ParseArguments<AddUIOptions, RemoveUIOptions>(args)
			.WithParsed<AddUIOptions>(o => {
				if (o.Name == null || o.Path == null) {
					throw new ArgumentNullException("name or path is null");
				} else {
					ConfigIO.AddEntry(o.Name, o.Path);
				}
			})
			.WithParsed<RemoveUIOptions>(o => {
				if (o.Name == null) {
					throw new ArgumentNullException("name is null");
				} else {
					ConfigIO.RemoveEntry(o.Name);
				}
			})
			.WithNotParsed(_ => {
				RootCommand = string.Join(' ', args);
				InitGUI();
				BuildAvaloniaApp().StartWithClassicDesktopLifetime(args);
			});

	// Avalonia configuration, don't remove; also used by visual designer.
	public static AppBuilder BuildAvaloniaApp()
		=> AppBuilder.Configure<App>()
			.UsePlatformDetect()
			.LogToTrace()
			.UseReactiveUI();

	private static void InitGUI() {
		
		Pages = XMLUtils.LoadFile(ConfigIO.FindPathEntry(RootCommand));
	}

	public static string Compile() {

		var currentPage = Pages.First(p => p.IsUsingThis);
		if (currentPage == null) throw new ArgumentException("Impossible");

		throw new NotImplementedException();
	}
}
