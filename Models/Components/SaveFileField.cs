using System.ComponentModel;

namespace Guify.Models.Components;

class SaveFileField : FieldBase<string?>, INotifyPropertyChanged {

	public SaveFileField(string? customDefaultFileName, string? customDefaultFolder, string description, bool isRequired, string? longName
	, string? shortName) : base(customDefaultFolder + customDefaultFileName, isRequired, longName, shortName, description) 
	{
		CustomDefaultFileName = customDefaultFileName;
		CustomDefaultFolder = customDefaultFolder;
	}

	public string? CustomDefaultFolder { get; init; }
	public string? CustomDefaultFileName { get; init; }
}