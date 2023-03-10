namespace Guify.Models.Components;

internal class Infix : ComponentBase
{
	public Infix(string value)
	{
		_Value = value;
	}

	private readonly string _Value;
	internal override string Compile() => _Value;
}