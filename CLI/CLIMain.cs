using CommandLine;

namespace Guify.CLI {
	class CLIMain {
		public static void EntryPoint(string[] args)
        {
            Parser.Default.ParseArguments<AddUIOptions, RemoveUIOptions>(args)
				.WithParsed<AddUIOptions>(o => {
					if (o.Name == null || o.Path == null) {
						throw new ArgumentNullException("name or path is null");
					} else {
						ConfigIO.AddEntry(o.Name, o.Path);
					}
				})
				.WithParsed<RemoveUIOptions>(o => {
					if (o.Name == null) {
						throw new ArgumentNullException("name is null");
					} else {
						ConfigIO.RemoveEntry(o.Name);
					}
				});
        }
	}
}