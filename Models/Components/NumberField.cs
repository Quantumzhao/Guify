using System.ComponentModel;
namespace Guify.Models.Components;

class NumberField : FieldBase<float>
{

	public NumberField(float defaultValue, float? max, float? min, bool isRequired
		, string? longName, string? shortName, string description, bool useEqualConnector) 
		: base(defaultValue, isRequired, longName, shortName, description, useEqualConnector)
	{
		var maximum = max ?? float.MaxValue;
		var minimum = min ?? float.MinValue;

		if (maximum < minimum) throw new ArgumentOutOfRangeException();

		Max = maximum;
		Min = minimum;
	}

	public float Max { get; init; }
	public float Min { get; init; }

	// number box cannot have null as a valid value, so if the default value is null, 
	// i.e. don't care, there must be an extra flag storing the info that "0" is indeed 
	// representing the null value
	// public override float Value 
	// { 
	// 	get => base.Value; 
	// 	set
	// 	{
	// 		if (value == _Value) return;

	// 		if (value == DefaultValue)
	// 		{
	// 			_IsUsingDefault = true;
	// 			base.Value = DefaultValue;

	// 			// if (_Value == 0) InvokePropertyChanged(nameof(IsValueChanged));
	// 		}
	// 		else
	// 		{
	// 			_IsUsingDefault = false;
	// 			base.Value = value;
	// 		}
	// 	}
	// }

	// private bool _IsUsingDefault = true;
	// public override bool IsValueChanged => !_IsUsingDefault;

	public override bool FulfillRequirement
	{
		get
		{
			if (!IsRequired || DefaultValue != float.PositiveInfinity) return true;
			else return Value != float.PositiveInfinity;
		}
	}


	public override string Compile()
	{
		var flag = GetFlag();

		if (Value != float.PositiveInfinity) 
			return string.Join(Connector, flag, ValueToString(Value));
		else return string.Empty;
	}
}