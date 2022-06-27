using System.Linq;

namespace Guify.Models.Components;

class OpenFileField : FieldBase<string[]?>
{
	public OpenFileField(string[]? customDefaultFileName, string? customDefaultFolder, 
		bool? allowMultiple, bool isRequired, string description
		, string? longName, string? shortName) : base(
			customDefaultFileName?.Select(d => customDefaultFolder + d).ToArray()
		, isRequired, longName, shortName, description)
	{
		CustomDefaultFileName = customDefaultFileName;
		CustomDefaultFolder = customDefaultFolder;
		AllowMultiple = allowMultiple ?? false;
	}

	public bool AllowMultiple { get; init; }
	public string? CustomDefaultFolder { get; init; }
	public string[]? CustomDefaultFileName { get; init; }

	public override string ValueToString(string[]? value)
		=> string.Join(' ', value ?? Array.Empty<string>());
}