namespace Guify.Models.Components;

class StringField : FieldBase<string?>
{
	public StringField(string? defaultValue, string description, bool isRequired, string? longName
		, string? shortName) : base(defaultValue, isRequired, longName, shortName, description)
	{
		
	}
}
