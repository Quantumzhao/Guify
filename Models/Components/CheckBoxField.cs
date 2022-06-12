namespace Guify.Models.Components
{
	class CheckBoxField : ComponentBase<bool?>
	{
		public CheckBoxField(bool? defaultValue, bool isRequired, string? longName
			, string? shortName, string description) : base(defaultValue, isRequired, longName,
			shortName, description)
		{

		}

		public override bool? Value { get; set; }

		public override string ValueToString()
			=> (Value ?? false) ? "true" : "false";
	}
}