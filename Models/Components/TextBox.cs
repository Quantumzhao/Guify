namespace Guify.Models.Components;

class TextBox : ComponentBase<string> {

	public TextBox(string defaultValue, string description, bool isRequired, string? longName
		, string? shortName) : base(defaultValue, isRequired, longName, shortName, description) {

		Value = defaultValue;
	}

	public override string Value { get; set; }
}
