using System.ComponentModel;

namespace Guify.Models.Components;

abstract class FieldBase : ComponentBase, INotifyPropertyChanged
{
	public event PropertyChangedEventHandler? PropertyChanged;

	public abstract bool IsValueChanged { get; }
	public abstract bool IsRequired { get; init; }
	public abstract void Reset();

	public void InvokePropertyChanged(PropertyChangedEventArgs e) 
		=> PropertyChanged?.Invoke(this, e);
}

abstract class FieldBase<T> : FieldBase, INotifyPropertyChanged
{
	public FieldBase(T defaultValue, bool isRequired, string? longName, string? shortName, string description)
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

	public override bool IsValueChanged => !(_Value?.Equals(DefaultValue) ?? DefaultValue is null);
	private readonly string? LongName = null;
	private readonly string? ShortName = null;

	public List<string> Flags { get; } = new List<string>();
	public virtual T DefaultValue { get; init; }
	public override bool IsRequired { get; init; }
	public string Description { get; init; }

	private T _Value;
	public T Value
	{
		get => _Value; 
		set
		{
			// because binded control will automatically set any nullable struct to a non-null default
			// therefore using null as default value is prone to change
			// if (_Value?.Equals(DefaultValue) ?? DefaultValue == null) UsingDefault = true;
			// else UsingDefault = false;

			if (!(_Value?.Equals(value) ?? false))
			{
				_Value = value;
				InvokePropertyChanged(new PropertyChangedEventArgs(nameof(Value)));
				InvokePropertyChanged(new PropertyChangedEventArgs(nameof(IsValueChanged)));
			}
		}
	}

	public override string Compile()
	{
		var flag = GetFlag();

		if (IsRequired) 
		{
			if (!string.IsNullOrEmpty(Value?.ToString())) return $"{flag} {ValueToString(Value)}";				
			else throw new WarningException(nameof(Value)
				, $"Value of {GetName()} is required");
		}
		else
		{
			if (Value != null) return $"{flag} {ValueToString(Value)}";
			else if (DefaultValue != null) return $"{flag} {ValueToString(DefaultValue)}";
			else return string.Empty;
		}
	}

	public override void Reset() => Value = DefaultValue;

	protected string GetFlag() 
		=> (LongName, ShortName) switch
		{
			(string l, _) => l,
			(null, string s) => s,
			_ => string.Empty
		};

	public string GetName()
	{
		if (LongName != null) return LongName;
		else if (ShortName != null) return ShortName;
		else return Description[0..5] + "...";
	}

	public virtual string ValueToString(T value) => value?.ToString() ?? string.Empty;
}
