using CommandLine;

namespace Guify.CLI {

	[Verb("add", HelpText = "Add a Guify UI file")]
	class AddUIOptions {

		[Option('n', "command-name", HelpText = "Name of the command", Required = true)]
		public string? Name { get; set; }

		[Option('p', "path", HelpText = "path of the Guify UI file", Required = true)]
		public string? Path { get; set; }
	}

	[Verb("remove", HelpText = "Remove a Guify UI file")]
	class RemoveUIOptions {
		
		[Option('n', "command-name", HelpText = "Name of the command", Required = true)]
		public string? Name { get; set; }
	}
}