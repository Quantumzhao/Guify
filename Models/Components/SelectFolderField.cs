using System.ComponentModel;

namespace Guify.Models.Components;

internal class SelectFolderField : FieldBase<string?>
{
	internal SelectFolderField(string? defaultValue, string description, bool isRequired, string? longName
		, string? shortName, bool useEqualConnector) 
		: base(defaultValue, isRequired, longName, shortName, description, useEqualConnector)
	{
	}
}