using System.Diagnostics;

namespace Guify.IO;

class ShellUtils {

	public static void Bash(string cmd)
	{
		var escapedArgs = cmd.Replace("\"", "\\\"");

		var process = new Process()
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

}