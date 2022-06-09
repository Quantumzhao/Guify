using System.IO;
using System.Linq;

namespace Guify.CLI {
	class ConfigIO {
		private const string CONFIG_PATH = "./config.cfg";
		public static void AddEntry(string name, string path) {
			if (!File.Exists(CONFIG_PATH)) File.Create(CONFIG_PATH);
			File.AppendAllLines(CONFIG_PATH, new string[] { name + "," + path });
		}

		public static void RemoveEntry(string name) {
			var entries = File.ReadAllLines(CONFIG_PATH)?.Select(l => l.Split(','));

			if (entries == null) {
				throw new FileNotFoundException("the config file may be deleted, moved or renamed");
			} else {
				var result = entries.Where(e => e[0] != name).Select(e => e[0] + "," + e[1]);
				File.Create(CONFIG_PATH);
				File.WriteAllLines(CONFIG_PATH, result);
			}
		}
	}
}