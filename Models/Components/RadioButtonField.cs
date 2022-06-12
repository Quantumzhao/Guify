namespace Guify.Models.Components
{
	class RadioButtonField : ComponentBase<bool>
	{
		public RadioButtonField(bool defaultValue, bool isRequired, string longName
			, string shortName, string description) : base(defaultValue, isRequired, longName,
			shortName, description)
		{

		}

		public override bool Value { get; set; }
	}
}