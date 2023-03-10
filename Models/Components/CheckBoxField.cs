namespace Guify.Models.Components
{
	internal sealed class CheckBoxField : FieldBase<bool?>
	{
		public CheckBoxField(bool? defaultValue, bool? isFlag, bool isRequired, string? longName
			, string? shortName, string description, bool connector) : base(defaultValue, isRequired
			, longName, shortName, description, connector)
		{
			_IsFlag = isFlag ?? true;
		}

		private readonly bool _IsFlag;

		internal override bool? Value 
		{ 
			get => base.Value; 
			set
			{
				if (_Value is null && value is false) base.Value = true;
				else base.Value = value;
			}
		}

		internal override string Compile()
		{
			if (_IsFlag)
			{
				if (Value ?? false) return GetFlag();
				else return string.Empty;
			}
			else return base.Compile();
		}

		internal override string ValueToString(bool? value)
			=> (value ?? false) ? "true" : "false";
	}
}