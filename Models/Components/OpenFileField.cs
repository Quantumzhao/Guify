namespace Guify.Models.Components;

class OpenFileField : ComponentBase<string?>
{
	public OpenFileField(string? customDefaultFileName, string? customDefaultFolder, 
		(string[], string)[]? extensions, bool? allowMultiple, bool isRequired, string description
		, string? longName, string? shortName) : base(customDefaultFileName + customDefaultFileName
		, isRequired, longName, shortName, description)
	{
		CustomDefaultFileName = customDefaultFileName;
		CustomDefaultFolder = customDefaultFolder;
		AllowMultiple = allowMultiple ?? false;
		Extensions = extensions ?? new (string[], string)[0];
	}

	public (string[] afterDot, string displayName)[] Extensions { get; set; }
	public bool AllowMultiple { get; set; }
	public string? CustomDefaultFolder { get; init; }
	public string? CustomDefaultFileName { get; init; }
}