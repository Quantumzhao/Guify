using System.ComponentModel;

namespace Guify.Models.Components;

internal class SaveFileField : FieldBase<string?> {

	public SaveFileField(string? customDefaultFileName, string? customDefaultFolder
	, string description, bool isRequired, string? longName, string? shortName
	, bool useEqualConnector) : base(customDefaultFolder.Append(customDefaultFileName), isRequired
	, longName, shortName, description, useEqualConnector) 
	{
		CustomDefaultFileName = customDefaultFileName;
		CustomDefaultFolder = customDefaultFolder;
	}

	public string? CustomDefaultFolder { get; }
	public string? CustomDefaultFileName { get; }
}