namespace Guify.Models.Components;

class IntField : ComponentBase<int?>
{
	public IntField(int defaultValue, bool isRequired, string? longName, string? shortName
		, string description) : base(defaultValue, isRequired, longName, shortName, description)
	{
		
	}

	public override int? Value { get; set; }
}

class FloatField : ComponentBase<float?>
{
	public FloatField(float defaultValue, bool isRequired, string? longName, string? shortName
		, string description) : base(defaultValue, isRequired, longName, shortName, description)
	{
	}

	public override float? Value { get; set; }
}