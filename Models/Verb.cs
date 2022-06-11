using System.Linq;

namespace Guify.Models;

class Verb : ControlBase {

	public Verb(string label, string comment, ControlBase[] items) {
		Name = label;
		Description = comment;
		Items = items;
	}

	public string Name { get; init; }
	public string Description { get; init; }
	public ControlBase[] Items { get; init; }

	public bool IsUsingThis { get; set; } = false;

	public override string Compile() 
		=> Name + " " + string.Join(' ', Items.Select(i => i.Compile()));
}
