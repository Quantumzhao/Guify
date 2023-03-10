namespace Guify.Models.Components;

internal class PickValueField : FieldBase<string?>
{
	internal PickValueField(string? defaultValue, bool isRequired, string? longName, string? shortName
		, string description, string[] candidates, bool useEqualConnector) : base(defaultValue
		, isRequired, longName, shortName, description, useEqualConnector)
	{
		Candidates = candidates;
	}

	internal string[] Candidates { get; init; }
}