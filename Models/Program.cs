using Avalonia;
using Avalonia.ReactiveUI;
using Guify.CLI;
using Guify.Models.Components;
using Guify.Models;
using Guify.IO;
using System.Linq;
using System.Diagnostics;
using McMaster.Extensions.CommandLineUtils;

namespace Guify;

class Program
{
	private static string Postfix { get; set; } = string.Empty;
	public static Root? Root { get; private set; }
	private static string? ProfileName { get; set; }

	// Initialization code. Don't use any Avalonia, third-party APIs or any
	// SynchronizationContext-reliant code before AppMain is called: things aren't initialized
	// yet and stuff might break.
	[STAThread]
	public static void Main(string[] args)
	{
		var app = new CommandLineApplication
		{
			Name = "guify",
			Description = "Give your CLI programs a GUI"
		};

		app.HelpOption();

		app.Command("add", cmd => {
			var path = cmd.Argument("path", "path of the Guify UI file").IsRequired();
			var option = cmd.Option(
				"-l | --make-link", "Create a symbolic link", 
				CommandOptionType.NoValue);
			cmd.OnExecute(() => {
				var isLink = cmd.GetOptions().FirstOrDefault()?.ShortName == "l";
				var pathValue = path?.Value;
				if (pathValue is null) throw new UnreachableException();
				else ConfigIO.AddUI(pathValue, option.HasValue());
			});
		});

		app.Command("remove", cmd => {
			var name = cmd.Argument("name", "Name of the command").IsRequired();
			cmd.OnExecute(() => {
				var nameValue = name?.Value;
				if (nameValue is null) throw new ArgumentException(nameof(name), "name is empty");
				else ConfigIO.RemoveUI(nameValue);
			});
		});

		app.Command("list", cmd => {
			var substr = cmd.Argument("substring", "show results containing this substring");
			cmd.OnExecute(() => {
				var substrValue = substr?.Value;
				IEnumerable<string> names;
				if (substrValue is null) names = ConfigIO.GetEntries();
				else names = ConfigIO.GetEntries().Where(n => n.Contains(substrValue));
				foreach (var n in names) Console.WriteLine(n);
			});
		});

		app.Argument(
			"run", 
			"run the following commands (perhaps also arguments)", 
			multipleValues: true);

		app.OnExecute(() => {
			var value0 = app.Arguments[0].Value;
			if (app.Arguments.Count == 0 || value0 == null) app.ShowHelp();
			else
			{
				ProfileName = value0;
				Postfix = app.Arguments.Skip(1).Aggregate("", (acc, a) => acc + " " + a.Value);
				Root = XMLUtils.LoadRoot(ConfigIO.CombineName(ProfileName));
				BuildAvaloniaApp().StartWithClassicDesktopLifetime(args);
			}
		});
		app.Execute(args);
	}

	// Avalonia configuration, don't remove; also used by visual designer.
	public static AppBuilder BuildAvaloniaApp()
		=> AppBuilder.Configure<App>()
			.UsePlatformDetect()
			.LogToTrace();
			// .UseReactiveUI();

	public static void Run()
	{
		if (Root == null) throw new InvalidOperationException("Impossible");

		var args = Root.Compile() + " " + Postfix;

		ShellUtils.Run(Root.Command, args);

		if (Root.IsReusable) return;
		Console.WriteLine("Done.");
		Environment.Exit(0);
	}
}
