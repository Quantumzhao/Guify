using System.ComponentModel;

namespace Guify.Models.Components;

class SaveFileField : FieldBase<string?>, INotifyPropertyChanged {

	public SaveFileField(string? customDefaultFileName, string? customDefaultFolder,(string[], string)[]? extensions , string description, bool isRequired, string? longName
	, string? shortName) : base(customDefaultFolder + customDefaultFileName, isRequired, longName, shortName, description) 
	{
		CustomDefaultFileName = customDefaultFileName;
		CustomDefaultFolder = customDefaultFolder;
		Extensions = extensions ?? Array.Empty<(string[], string)>();
	}

	public (string[] afterDot, string displayName)[] Extensions { get; init; }
	public bool AllowMultiple { get; init; }
	public string? CustomDefaultFolder { get; init; }
	public string? CustomDefaultFileName { get; init; }

}