namespace Guify.Models.Components;

internal class Separator : ComponentBase
{
	internal Separator(string label)
	{
		Label = label;
	}

	internal string Label { get; init; }

	internal override string Compile() => string.Empty;
}