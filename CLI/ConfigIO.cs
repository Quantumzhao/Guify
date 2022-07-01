using System.IO;
using System.Linq;
using static System.Environment;

namespace Guify.CLI
{
	class ConfigIO
	{
		private static string GetUIDirectory()
		{
			// Use DoNotVerify in case LocalApplicationData doesn’t exist.
			string appData = Path.Combine(Environment.GetFolderPath(
				SpecialFolder.LocalApplicationData, SpecialFolderOption.DoNotVerify), "guify");
			// Ensure the directory and all its parents exist.
			return Directory.CreateDirectory(appData).FullName;
		}

		public static string CombineName(string name) => GetUIDirectory() + "/" + name;

		public static void AddUI(string pathWithName, bool isLink)
		{
			// EnforcePath();
			var file = new FileInfo(pathWithName);
			if (isLink) File.CreateSymbolicLink(CombineName(file.Name), pathWithName);
			else File.Copy(pathWithName, CombineName(file.Name));
		}

		public static void RemoveUI(string name)
		{
			File.Delete(CombineName(name));
		}

		public static string[] GetEntries() => Directory.GetFiles(GetUIDirectory());

		// public static string FindPathEntry(string name)
		// {
		// 	var queryResult = GetEntries().FirstOrDefault(e => e[0] == name);
		// 	if (queryResult == null) throw new ArgumentNullException();
		// 	else return queryResult[1];
		// }
	}
}