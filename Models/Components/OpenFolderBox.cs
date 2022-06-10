namespace Guify.Models.Components;

class OpenFolderBox : ComponentBase<string> {

	public OpenFolderBox(string defaultValue, bool isRequired, string description, string? longName
		, string? shortName) : base(defaultValue, isRequired, longName, shortName, description) {
		
		Value = defaultValue;
	}

	public override string Value { get; set; }
}