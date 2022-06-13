namespace Guify.Models.Components;

class OpenFileField : ComponentBase<string?>
{
	public OpenFileField(string? defaultValue, bool isRequired, string description, string? longName
		, string? shortName) : base(defaultValue, isRequired, longName, shortName, description)
	{

	}
}