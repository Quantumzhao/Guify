using System.Threading;
using System.Diagnostics;
using System.IO;
using Guify.Models;

namespace Guify.IO;

class ShellUtils 
{
	public static async void Bash(string cmd)
	{
		cmd = "echo input && read name && echo $name && read n && echo $n";

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
			}
		};
		
		
		var thread = new Thread(() => {
			while (true)
			{
				Thread.Sleep(100);

				if (!process.StandardOutput.EndOfStream)
				{
					Console.WriteLine(process.StandardOutput.ReadLine());
				}
			}
		});

		process.Start();
		thread.Start();

		// process.Exited += (o, e) => {
		// 	process.Dispose();
		// };
		//  string result = process.StandardOutput.ReadToEnd();
		await process.WaitForExitAsync();
		thread.Interrupt();
		// Console.WriteLine(process.StandardOutput.ReadToEnd());
	}
}