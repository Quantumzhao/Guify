namespace Guify.Models.Components;

class PickValueField : ComponentBase<string>
{
	public PickValueField(string defaultValue, bool isRequired, string? longName, string? shortName
		, string description, string[] candidates) : base(defaultValue, isRequired, longName
		, shortName, description)
	{
		Value = defaultValue;
		Candidates = candidates;
	}

	public string[] Candidates { get; init; }

	public override string Value { get; set; }

	public override string Compile()
	{
		return base.Compile();
	}
}