namespace Guify.Models.Components;

class Separator : ComponentBase
{
	public Separator(string label)
	{
		Label = label;
	}

	public string Label { get; init; }

	public override string Compile() => string.Empty;
}