using CommandLine;

namespace Guify.CLI
{
	[Verb("add", HelpText = "Add a Guify UI file")]
	class AddUIOptions
	{
		[Option('p', "path", HelpText = "path of the Guify UI file", Required = true)]
		public string? Path { get; set; }
	}

	[Verb("remove", HelpText = "Remove a Guify UI file")]
	class RemoveUIOptions
	{
		[Option('p', "path", HelpText = "Name of the command", Required = true)]
		public string? Name { get; set; }
	}

	[Verb("run", true, HelpText = "run the following commands (perhaps also arguments)")]
	class WrapperOptions
	{
		[Value(1, Min = 1)]
		public IEnumerable<string>? Properties { get; set; }
	}

	[Verb("list", HelpText = "list all saved UIs")]
	class ListUIOptions
	{
		[Option('s', "substring", HelpText = "show results containing this substring")]
		public string? Substring { get; set; }
	}
}