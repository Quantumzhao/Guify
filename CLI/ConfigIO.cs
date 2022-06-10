using System.IO;
using System.Linq;

namespace Guify.CLI {
	class ConfigIO {
		private const string CONFIG_PATH = "./CLI/config.cfg";
		public static void AddEntry(string name, string path) {

			if (!File.Exists(CONFIG_PATH)) File.Create(CONFIG_PATH);

			File.AppendAllLines(CONFIG_PATH, new string[] { name + "," + path });
		}

		public static void RemoveEntry(string name) {

			var result = GetEntries().Where(e => e[0] != name).Select(e => e[0] + "," + e[1]);
			File.Create(CONFIG_PATH);
			File.WriteAllLines(CONFIG_PATH, result);
		}

		private static IEnumerable<string[]> GetEntries() {

			var entries = File.ReadAllLines(CONFIG_PATH)?.Select(l => l.Split(','));

			if (entries == null) throw new FileNotFoundException(
				"the config file may be deleted, moved or renamed");
			else return entries;
		}

		public static string FindPathEntry(string name) {

			var queryResult = GetEntries().FirstOrDefault(e => e[0] == name);
			if (queryResult == null) throw new ArgumentNullException(
				"this command has not be configured yet");
			else return queryResult[1];
		}
	}
}