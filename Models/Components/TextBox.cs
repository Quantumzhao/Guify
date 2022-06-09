namespace Guify.Models.Components;

class TextBox : ValueComponent<string> {

	public TextBox(string defaultValue, string comment, bool isRequired, string? longName
		, string? shortName) : base(defaultValue, isRequired, longName, shortName) {
		Comment = comment;
		Value = defaultValue;
	}
	public readonly string Comment;

	public override string Value { get; set; }
}
