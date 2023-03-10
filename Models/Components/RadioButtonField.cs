using System.Linq;
using System.ComponentModel;
namespace Guify.Models.Components
{
	class RadioButtonField : FieldBase<bool>
	{
		private static readonly Dictionary<string, List<RadioButtonField>> AllGroups = new();

		public RadioButtonField(bool defaultValue, bool? isFlag, bool isRequired, string? longName
			, string? shortName, string description, string group, bool useEqualConnector) 
			: base(defaultValue, isRequired, longName,
			shortName, description, useEqualConnector)
		{
			Group = group;
			_IsFlag = isFlag ?? true;

			if (!AllGroups.ContainsKey(group)) AllGroups.Add(group, new());
			AllGroups[group].Add(this);
		}

		private readonly bool _IsFlag;
		
		public string Group { get; init; }

		internal override bool Value
		{
			get => _Value; 
			set
			{
				// because binded control will automatically set any nullable struct to a non-null default
				// therefore using null as default value is prone to change
				// if (_Value?.Equals(DefaultValue) ?? DefaultValue == null) UsingDefault = true;
				// else UsingDefault = false;

				if (_Value == value) return;
				_Value = value;
				// InvokePropertyChanged(nameof(Value));
				// InvokePropertyChanged(nameof(FulfillRequirement));
				// InvokePropertyChanged(nameof(IsValueChanged));
					
				if (value) AllGroups[Group].ForEach(r => 
				{
					if (r != this) r._Value = false;
					r.InvokePropertyChanged(nameof(Value));
					r.InvokePropertyChanged(nameof(FulfillRequirement));
					r.InvokePropertyChanged(nameof(IsValueChanged));
				});
			}
		}

		public override bool FulfillRequirement
		{
			get
			{
				if (!IsRequired) return true;
				if (DefaultValue) return true;
				if (Value) return true;

				return AllGroups[Group].Any(r => r.Value);
				// if (!IsRequired || DefaultValue) return true;
				// else return Value != null;
			}
		}

		internal override string Compile()
		{
			if (_IsFlag)
			{
				if (!Value) return string.Empty;
				return GetFlag();
			}
			else return base.Compile();
		}

		internal override string ValueToString(bool value)
			=> value ? "true" : "false";

		public override bool IsValueChanged
			=> Value != DefaultValue || AllGroups[Group].Any(r => r.Value != r.DefaultValue);

		public override void Reset()
			=> AllGroups[Group].ForEach(r => r.Value = r.DefaultValue);
	}
}