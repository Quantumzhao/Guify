using System.Threading;
using System.Diagnostics;
using System.IO;
using Guify.Models;

namespace Guify.IO;

class ShellUtils 
{
	public static async void Bash(string cmd)
	{
		var flag = true;
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

		process.Disposed += (s, e) => flag = false;
		
		var thread = new Thread(() => {
			while (flag)
			{
				Thread.Sleep(100);
				if (flag)
				{
					Console.WriteLine(process.StandardOutput.ReadLine());
				}
			}
		});

		process.Start();
		thread.Start();

		process.Exited += (o, e) => {
			Console.WriteLine(process.StandardOutput.ReadToEnd());
			// if (flag) process.Dispose();
			var code = process.ExitCode;
			process.Dispose();
			flag = false;

			Console.WriteLine($"Process exited with code {code}");
		};
		// string result = process.StandardOutput.ReadToEnd();
		await process.WaitForExitAsync();
		flag = false;
	}
}