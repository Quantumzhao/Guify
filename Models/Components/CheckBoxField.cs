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

		public override bool? Value 
		{ 
			get => base.Value; 
			set
			{
				if (_Value is null && value is false) base.Value = true;
				else base.Value = value;
			}
		}

		public override string Compile()
		{
			if (IsFlag)
			{
				if (Value ?? false) return GetFlag();
				else return string.Empty;
			}
			else return base.Compile();
		}

		public override string ValueToString(bool? value)
			=> (value ?? false) ? "true" : "false";
	}
}