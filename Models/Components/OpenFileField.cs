using System.IO;
using System.Linq;

namespace Guify.Models.Components;

class OpenFileField : FieldBase<string[]?>
{
	public OpenFileField(string? customDefaultFileName, string? customDefaultFolder, 
		bool? allowMultiple, bool isRequired, string description
		, string? longName, string? shortName, bool useEqualConnector) : base(
			JoinPath(customDefaultFolder, customDefaultFileName)
		, isRequired, longName, shortName, description, useEqualConnector)
	{
		CustomDefaultFileName = customDefaultFileName;
		CustomDefaultFolder = customDefaultFolder;
		AllowMultiple = allowMultiple ?? false;
	}

	public bool AllowMultiple { get; init; }
	public string? CustomDefaultFolder { get; init; }
	public string? CustomDefaultFileName { get; init; }

	public override string ValueToString(string[]? value)
		=> string.Join(' ', (value ?? Array.Empty<string>()).Select(Escape));

	private static string[]? JoinPath(string? folder, string? file)
		=> (folder, file) switch
		{
			(not null, not null) => new[] { Path.Join(folder, file) },
			_ => null
		};

	private static string Escape(string s)
		=> '\"' + s.Replace("\"", "\\\"") + '\"';
}