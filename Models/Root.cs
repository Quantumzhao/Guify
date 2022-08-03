using System.ComponentModel;
using Guify.Models.Components;
using System.Linq;
using Guify.IO;

namespace Guify.Models;

	class Root : INotifyPropertyChanged
	{
		public Root(string command, bool? isReusable, Verb[]? verbs = null, ComponentBase[]? components = null)
		{
			if (verbs != null)
			{
				Verbs = verbs;
				Verbs[0].IsUsingThis = true;
			}
			else if (components != null) FlatItems = components;
			else throw new InvalidOperationException();

			Command = command;
			IsReusable = isReusable ?? false;
		}

		public event PropertyChangedEventHandler? PropertyChanged;

		public Verb[]? Verbs { get; init; }
		public string Command { get; init; }
		public ComponentBase[]? FlatItems { get; init; }
		public bool IsReusable { get; init; }

		public object Content => (FlatItems, Verbs) switch
		{
			(ComponentBase[] items, null) => items,
			(null, Verb[] verbs) => verbs,
			_ => throw new ArgumentException()
		};

		public bool FulfillRequirement => Content switch
		{
			ComponentBase[] items => 
				items.All(i => (i as FieldBase)?.FulfillRequirement ?? true),
			Verb[] verbs => 
				verbs.Single(v => v.IsUsingThis).Items
					 .All(i => (i as FieldBase)?.FulfillRequirement ?? true),
			_ => throw new ArgumentException()
		};

		public bool IsCommandRunning => ShellUtils.IsCommandRunning;

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

		public void InvokePropertyChanged(string name)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
		}
	}
