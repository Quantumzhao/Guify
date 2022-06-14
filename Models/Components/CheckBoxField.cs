namespace Guify.Models.Components
{
	class CheckBoxField : FieldBase<bool?>
	{
		public CheckBoxField(bool? defaultValue, bool isRequired, string? longName
			, string? shortName, string description) : base(defaultValue, isRequired, longName,
			shortName, description)
		{

		}

		public override string ValueToString()
			=> (Value ?? false) ? "true" : "false";
	}
}