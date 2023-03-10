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
			var local = Path.Combine(GetFolderPath(
				SpecialFolder.LocalApplicationData, SpecialFolderOption.DoNotVerify), "guify/UI/");
			// Ensure the directory and all its parents exist.
			return Directory.CreateDirectory(local).FullName;
		}

		public static string CombineName(string name) 
		{
			if (name[^4..^0] != ".xml") name += ".xml";
			return GetUIDirectory() + name;
		}

		public static void AddUI(string pathWithName, bool isLink)
		{
			var file = new FileInfo(pathWithName);
			if (isLink) File.CreateSymbolicLink(CombineName(file.Name), pathWithName);
			else File.Copy(pathWithName, CombineName(file.Name));
		}

		public static void RemoveUI(string name)
		{
			File.Delete(CombineName(name));
		}

		public static string[] GetEntries() => Directory.GetFiles(GetUIDirectory());
	}
}