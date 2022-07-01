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
				if (_Value != value)
				{
					_Value = value;
					InvokePropertyChanged(nameof(Value));
					InvokePropertyChanged(nameof(IsValueChanged));

					// Value is false -> true
					if (value) AllGroups[Group].ForEach(r => 
					{
						if (r != this) r.InvokePropertyChanged(nameof(IsValueChanged)); 
					});
					// otherwise (i.e. Value is true -> false) do nothing
					// because the button b1 itself can't be set to false by itself, 
					// so there exist b2 such that b2 is set to true, which in turn
					// propagates the change to b1
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
			=> Value != DefaultValue || AllGroups[Group].Any(r => r.Value != r.DefaultValue);

		// manually invoke the events because for a deselected option, 
		// false (manually deselected by selecting other options)
		// -> false (reset by Reset()) won't trigger Value.setter
		public override void Reset()
		{
			_Value = DefaultValue;
			InvokePropertyChanged(nameof(Value));
			InvokePropertyChanged(nameof(IsValueChanged));

			AllGroups[Group].ForEach(r => 
			{
				if (r == this) return;
				r._Value = r.DefaultValue;
				r.InvokePropertyChanged(nameof(Value));
				r.InvokePropertyChanged(nameof(IsValueChanged));
			});
		}
	}
}