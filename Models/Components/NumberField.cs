using System.ComponentModel;
namespace Guify.Models.Components;

class NumberField : FieldBase<float?>
{

	public NumberField(float? defaultValue, float? max, float? min, bool isRequired
		, string? longName, string? shortName, string description) : base(defaultValue, isRequired
		, longName, shortName, description)
	{
		var maximum = max ?? float.MaxValue;
		var minimum = min ?? float.MinValue;

		if (maximum < minimum) throw new ArgumentOutOfRangeException();

		Max = maximum;
		Min = minimum;
	}

	public float Max { get; init; }
	public float Min { get; init; }
}