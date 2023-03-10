namespace Guify.Models.Components;

internal class StringField : FieldBase<string?>
{
	public StringField(string? defaultValue, string description, bool isRequired, string? longName
		, string? shortName, bool useEqualConnector) 
		: base(defaultValue, isRequired, longName, shortName, description, useEqualConnector)
	{
		
	}
}
