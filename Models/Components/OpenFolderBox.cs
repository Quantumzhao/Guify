namespace Guify.Models.Components;

class OpenFolderBox : ComponentBase<string> {

	public OpenFolderBox(string defaultValue, bool isRequired, string comment, string? longName
		, string? shortName) : base(defaultValue, isRequired, longName, shortName) {
		
		Value = defaultValue;
		Comment = comment;
	}

	public readonly string Comment;
	public override string Value { get; set; }

}