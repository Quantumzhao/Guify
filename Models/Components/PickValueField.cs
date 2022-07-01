namespace Guify.Models.Components;

class PickValueField : FieldBase<string?>
{
	public PickValueField(string? defaultValue, bool isRequired, string? longName, string? shortName
		, string description, string[] candidates, bool useEqualConnector) : base(defaultValue
		, isRequired, longName, shortName, description, useEqualConnector)
	{
		Candidates = candidates;
	}

	public string[] Candidates { get; init; }
}