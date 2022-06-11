namespace Guify.Models.Components;

abstract class ComponentBase : ControlBase { }

abstract class ComponentBase<T> : ComponentBase
{
	public ComponentBase(T defaultValue, bool isRequired, string? longName, string? shortName, string description)
	{
		DefaultValue = defaultValue;
		Value = defaultValue;

		IsRequired = isRequired;
		LongName = longName;
		ShortName = shortName;
		Description = description;

		if (longName != null) Flags.Add(longName);
		if (shortName != null) Flags.Add((shortName));
	}

	public readonly string? LongName = null;
	public readonly string? ShortName = null;
	public List<string> Flags { get; } = new List<string>();

	public bool IsChanged
	{
		get
		{
			if (Value == null) throw new ArgumentNullException("Impossible");
			return Value.Equals(DefaultValue);
		}
	}

	public abstract T Value { get; set; }
	public readonly T DefaultValue;
	public readonly bool IsRequired;
	public string Description { get; init; }

	public override string Compile()
	{
		var flag = (LongName, ShortName) switch
		{
			(string l, _) => l,
			(null, string s) => s,
			_ => string.Empty
		} + " ";

		if (IsRequired) return flag + Value.FuckingToString();
		else if (IsChanged) return flag + Value.FuckingToString();
		else return string.Empty;
	}
}
