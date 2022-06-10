using System.Linq;

namespace Guify.Models.Containers;

class Verb : ControlBase {

	public Verb(string label, string comment, ControlBase[] items) {
		_Name = label;
		_Description = comment;
		Items = items;
	}

	private readonly string _Name;
	public string Name => _Name;
	private readonly string _Description;
	public string Description => _Description;
	public readonly ControlBase[] Items;

	public bool IsUsingThis { get; set; } = false;

	public override string Compile() 
		=> _Name + " " + string.Join(' ', Items.Select(i => i.Compile()));
}
