using Guify.Models.Components;
using System.Linq;

namespace Guify.Models;

	class Root
	{
		public Root(string command, string? help, Verb[]? verbs = null, ComponentBase[]? components = null)
		{
			if (verbs != null) Verbs = verbs;
			else if (components != null) FlatItems = components;
			else throw new InvalidOperationException();

			Command = command;
			helpCommand = help;
		}

		public Verb[]? Verbs { get; init; }
		public string Command { get; init; }
		public string? helpCommand { get; init; }
		public ComponentBase[]? FlatItems { get; init; }

		public string Compile()
		{
			if (Verbs != null)
			{
				var currentPage = Verbs.First(p => p.IsUsingThis);
				if (currentPage == null) throw new ArgumentException("Impossible");

				return currentPage.Compile();
			}
			else if (FlatItems != null)
			{
				return string.Join(' ', FlatItems.Select(i => i.Compile()));
			}
			else throw new InvalidOperationException();
		}
	}
