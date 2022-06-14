using System.ComponentModel;
namespace Guify.Models.Components
{
	class RadioButtonField : FieldBase<bool?>, INotifyPropertyChanged
	{
		public RadioButtonField(bool defaultValue, bool? isFlag, bool isRequired, string? longName
			, string? shortName, string description, string group) : base(defaultValue, isRequired, longName,
			shortName, description)
		{
			Group = group;
			IsFlag = isFlag ?? true;
		}

		public bool IsFlag { get; init; }
		
		public string Group { get; init; }

		public override string Compile()
			=> IsFlag ? GetFlag() : base.Compile();
			// => ((Func<string>)(() => { Value = null; return "";}))();

		public override string ValueToString(bool? value)
			=> value ?? false ? "true" : "false";
	}
}