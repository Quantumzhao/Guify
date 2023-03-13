using System.Threading;
using System.Diagnostics;
using CliWrap;
using CliWrap.EventStream;

namespace Guify.IO;

class ShellUtils 
{
	private static bool _IsCommandRunning = false;
	public static bool IsCommandRunning
	{
		get => _IsCommandRunning;
		private set
		{
			if (value == _IsCommandRunning) return;
			_IsCommandRunning = value;
			Program.Root?.InvokePropertyChanged(nameof(Program.Root.IsCommandRunning));
		}
	}
	public static async void Run(string cmd, string args)
	{
		var prefab = Cli.Wrap(cmd).WithArguments(args).WithStandardInputPipe(PipeSource.FromStream(Console.OpenStandardInput()));
		await foreach (var e in prefab.ListenAsync())
		{
			switch (e)
			{
				case StartedCommandEvent started:
					Console.WriteLine(cmd + " " + args);
					Console.WriteLine($"Process started. PID: {started.ProcessId}");
					IsCommandRunning = true;
					break;

				case ExitedCommandEvent exited:
					Console.WriteLine($"Process exited. Code: {exited.ExitCode}");
					IsCommandRunning = false;
					break;

				case StandardErrorCommandEvent error:
					var c = Console.ForegroundColor;
					Console.ForegroundColor = ConsoleColor.DarkRed;
					Console.Write("ERR> ");
					Console.ForegroundColor = c;
					Console.WriteLine(error.Text);
					break;

				case StandardOutputCommandEvent output:
					Console.WriteLine($"OUT> {output.Text}");
					break;

				default:
					break;
			}
		}

		await prefab.ExecuteAsync();

		// IsCommandRunning = true;
		// var escapedArgs = cmd.Replace("\"", "\\\"");

		// using var process = new Process()
		// {
		// 	StartInfo = new ProcessStartInfo
		// 	{
		// 		FileName = "/bin/bash",
		// 		Arguments = $"-c \"{escapedArgs}\"",
		// 		UseShellExecute = false,
		// 		CreateNoWindow = true,
		// 		RedirectStandardOutput = true,
		// 		RedirectStandardError = true,
		// 	}
		// };

		// process.Disposed += (s, e) => IsCommandRunning = false;
		
		// var thread = new Thread(() => {
		// 	while (true)
		// 	{
		// 		Thread.Sleep(100);
		// 		if (_IsCommandRunning) Console.WriteLine(process.StandardOutput.ReadLine());
		// 		else return;
		// 	}
		// });

		// process.Start();
		// thread.Start();

		// process.Exited += (o, e) => {
		// 	Console.WriteLine(process.StandardOutput.ReadToEnd());
		// 	var code = process.ExitCode;
		// 	process.Dispose();
		// 	IsCommandRunning = false;

		// 	Console.WriteLine($"Process exited with code {code}");
		// };
		// // string result = process.StandardOutput.ReadToEnd();
		// await process.WaitForExitAsync();
		// IsCommandRunning = false;
	}
}