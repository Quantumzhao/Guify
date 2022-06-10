using System.Linq;

namespace Guify.Models.Containers;

class ContainerBase : ControlBase {
	public readonly List<ControlBase> Items = new List<ControlBase>();

	public override string Compile()
		=> string.Join(' ', Items.Select(i => i.Compile()));
}
