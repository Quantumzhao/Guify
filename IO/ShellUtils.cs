using System.Diagnostics;
using System.IO;
using Guify.Models;

namespace Guify.IO;

class ShellUtils 
{
	public static void Bash(string cmd)
	{
		var escapedArgs = cmd.Replace("\"", "\\\"");

		using var process = new Process()
		{
			StartInfo = new ProcessStartInfo
			{
				FileName = "/bin/bash",
				Arguments = $"-c \"{escapedArgs}\"",
				UseShellExecute = false,
				CreateNoWindow = true,
			}
		};

		process.Start();
		// process.Exited += (o, e) => {
		// 	process.Dispose();
		// };
		// string result = process.StandardOutput.ReadToEnd();
		process.WaitForExit();
		//Console.WriteLine(process.StandardOutput.ReadToEnd());
	}

	public static string GetHelpInfo(Root root)
	{
		var escapedArgs = root.Command.Replace("\"", "\\\"") + " " + root.helpCommand;
		//escapedArgs = "dotnet --help";

		using var process = new Process()
		{
			StartInfo = new ProcessStartInfo
			{
				FileName = "/bin/bash",
				Arguments = $"-c \"{escapedArgs}\"",
				RedirectStandardOutput = true,
				UseShellExecute = false,
				CreateNoWindow = true
			}
		};

		process.Start();
		var result = process.StandardOutput.ReadToEnd();
		process.WaitForExit();

		return result;
	}
}