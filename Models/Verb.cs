using System.Linq;
using Guify.Models.Components;

namespace Guify.Models;

class Verb
{
	public Verb(string label, string comment, ComponentBase[] items)
	{
		Name = label;
		Description = comment;
		Items = items;
	}

	public string Name { get; init; }
	public string Description { get; init; }
	public ComponentBase[] Items { get; init; }

	public bool IsUsingThis { get; set; } = false;

	public string Compile()
		=> Name + " " + string.Join(' ', Items.Select(i => i.Compile()));
}
