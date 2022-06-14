namespace Guify.Models.Components;

class Infix : ComponentBase
{
	public Infix(string value)
	{
		Value = value;
	}

	public string Value { get; init; }
	public override string Compile() => Value;
}