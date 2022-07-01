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
			}
		};

		process.Disposed += (s, e) => flag = false;
		
		var thread = new Thread(() => {
			while (flag)
			{
				Thread.Sleep(100);

				if (flag && !process.StandardOutput.EndOfStream)
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
		flag = false;
		// Console.WriteLine(process.StandardOutput.ReadToEnd());
	}
}