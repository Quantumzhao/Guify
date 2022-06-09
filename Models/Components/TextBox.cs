namespace Guify.Models.Components;

class TextBox : ValueComponent<string> {

	public TextBox(string defaultValue, string comment) : base(defaultValue) {
		Comment = comment;
		Value = defaultValue;
	}
	public readonly string Comment;

	public override string Value { get; set; }
}
