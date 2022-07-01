using System.Linq;
using System.ComponentModel;
namespace Guify.Models.Components
{
	class RadioButtonField : FieldBase<bool>, INotifyPropertyChanged
	{
		private static readonly Dictionary<string, List<RadioButtonField>> AllGroups = new();

		public RadioButtonField(bool defaultValue, bool? isFlag, bool isRequired, string? longName
			, string? shortName, string description, string group, bool useEqualConnector) 
			: base(defaultValue, isRequired, longName,
			shortName, description, useEqualConnector)
		{
			Group = group;
			IsFlag = isFlag ?? true;

			if (!AllGroups.ContainsKey(group)) AllGroups.Add(group, new());
			AllGroups[group].Add(this);
		}

		public bool IsFlag { get; init; }
		
		public string Group { get; init; }

		public override bool Value
		{
			get => _Value; 
			set
			{
				// because binded control will automatically set any nullable struct to a non-null default
				// therefore using null as default value is prone to change
				// if (_Value?.Equals(DefaultValue) ?? DefaultValue == null) UsingDefault = true;
				// else UsingDefault = false;

				if (_Value != value)
				{
					_Value = value;
					InvokePropertyChanged(nameof(Value));
					InvokePropertyChanged(nameof(IsValueChanged));
					AllGroups[Group].ForEach(r => 
					{
						if (r != this) r.InvokePropertyChanged(nameof(IsValueChanged)); 
					});
				}
			}
		}


		public override string Compile()
		{
			if (IsFlag)
			{
				if (Value) return GetFlag();
				else return string.Empty;
			}
			else return base.Compile();
		}

		public override string ValueToString(bool value)
			=> value ? "true" : "false";

		public override bool IsValueChanged
			=> Value || AllGroups[Group].Any(r => r.Value);
	}
}