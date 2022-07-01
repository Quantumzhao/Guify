namespace Guify.Models.Components
{
	class CheckBoxField : FieldBase<bool?>
	{
		public CheckBoxField(bool? defaultValue, bool? isFlag, bool isRequired, string? longName
			, string? shortName, string description, bool connector) : base(defaultValue, isRequired
			, longName, shortName, description, connector)
		{
			IsFlag = isFlag ?? true;
		}

		public bool IsFlag { get; init; }

		public override string Compile()
			=> IsFlag ? GetFlag() : base.Compile();

		public override string ValueToString(bool? value)
			=> (value ?? false) ? "true" : "false";
	}
}