using System.Threading;
using System.Diagnostics;
using System.IO;
using Guify.Models;

namespace Guify.IO;

class ShellUtils 
{
	private static bool _IsCommandRunning = false;
	public static bool IsCommandRunning
	{
		get => _IsCommandRunning;
		set
		{
			if (value == _IsCommandRunning) return;
			_IsCommandRunning = value;
			Program.Root?.InvokePropertyChanged(nameof(Program.Root.IsCommandRunning));
		}
	}
	public static async void Bash(string cmd)
	{
		IsCommandRunning = true;
		var escapedArgs = cmd.Replace("\"", "\\\"");

		using var process = new Process()
		{
			StartInfo = new ProcessStartInfo
			{
				FileName = "/bin/bash",
				Arguments = $"-c \"{escapedArgs}\"",
				UseShellExecute = false,
				CreateNoWindow = true,
				RedirectStandardOutput = true,
				RedirectStandardError = true,
			}
		};

		// process.ErrorDataReceived += (s, e) => 
		// {
		// 	Console.WriteLine($"{cmd} failed with the following error");

		// 	var c = Console.ForegroundColor;
		// 	Console.ForegroundColor = ConsoleColor.Red;
		// 	Console.WriteLine(e.Data);
		// 	Console.ForegroundColor = c;

		// 	process.Dispose();

		// 	Console.WriteLine("Process Disposed due to error. ");
		// };

		process.Disposed += (s, e) => IsCommandRunning = false;
		
		var thread = new Thread(() => {
			while (true)
			{
				Thread.Sleep(100);
				if (_IsCommandRunning) Console.WriteLine(process.StandardOutput.ReadLine());
				else return;
			}
		});

		process.Start();
		thread.Start();

		process.Exited += (o, e) => {
			Console.WriteLine(process.StandardOutput.ReadToEnd());
			var code = process.ExitCode;
			process.Dispose();
			IsCommandRunning = false;

			Console.WriteLine($"Process exited with code {code}");
		};
		// string result = process.StandardOutput.ReadToEnd();
		await process.WaitForExitAsync();
		IsCommandRunning = false;
	}
}