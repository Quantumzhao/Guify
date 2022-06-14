using System.ComponentModel;

namespace Guify.Models.Components;

abstract class ComponentBase 
{ 
	public abstract string Compile();
}

abstract class ComponentBase<T> : ComponentBase, INotifyPropertyChanged
{
	public ComponentBase(T defaultValue, bool isRequired, string? longName, string? shortName, string description)
	{
		DefaultValue = defaultValue;
		_Value = defaultValue;

		IsRequired = isRequired;
		LongName = longName;
		ShortName = shortName;
		Description = description;

		if (longName != null) Flags.Add(longName);
		if (shortName != null) Flags.Add((shortName));
	}

	public event PropertyChangedEventHandler? PropertyChanged;


	public readonly string? LongName = null;
	public readonly string? ShortName = null;
	public List<string> Flags { get; } = new List<string>();

	public virtual T DefaultValue { get; init; }
	public bool IsRequired { get; init; }
	public string Description { get; init; }

	private T _Value;
	public T Value
	{
		get => _Value; 
		set
		{
			if (!(_Value?.Equals(value) ?? false))
			{
				_Value = value;
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Value)));
			}
		}
	}

	public override string Compile()
	{
		var flag = (LongName, ShortName) switch
		{
			(string l, _) => l,
			(null, string s) => s,
			_ => string.Empty
		} + " ";

		if (IsRequired) 
		{
			if (!string.IsNullOrEmpty(Value?.ToString())) return flag + ValueToString();				
			else throw new ArgumentNullException(nameof(Value)
				, $"Value of {GetName()} is required");
		}
		else
		{
			if (Value != null) return flag + Value.ToString();
			else return DefaultValue?.ToString() ?? string.Empty;
		}
	}

	public string GetName()
	{
		if (LongName != null) return LongName;
		else if (ShortName != null) return ShortName;
		else return Description[0..5] + "...";
	}

	public virtual string ValueToString() => Value?.ToString() ?? string.Empty;
}
