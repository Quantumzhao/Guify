using System.ComponentModel;

namespace Guify.Models.Components;

abstract class FieldBase : ComponentBase, INotifyPropertyChanged
{
	protected FieldBase(string description)
	{
		Description = description;
	}
	public event PropertyChangedEventHandler? PropertyChanged;

	public abstract bool IsValueChanged { get; }
	public abstract bool FulfillRequirement { get; }
	public abstract bool IsRequired { get; init; }
	public string Description { get; init; }
	public List<string> Flags { get; } = new List<string>();
	protected char Connector = ' ';
	public abstract void Reset();

	public void InvokePropertyChanged(string propertyName) 
		=> PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}

abstract class FieldBase<T> : FieldBase, INotifyPropertyChanged
{
	public FieldBase(T defaultValue, bool isRequired, string? longName, string? shortName
		,string description, bool useEqualConnector) : base(description)
	{
		DefaultValue = defaultValue;
		_Value = defaultValue;

		IsRequired = isRequired;
		LongName = longName;
		ShortName = shortName;

		if (longName != null) Flags.Add(longName);
		if (shortName != null) Flags.Add((shortName));
		if (useEqualConnector) Connector = '=';
	}

	//                         value
	//                    null   non-null
	// default     null   true   false
	//   value non-null   false  ?? == ??
	public override bool IsValueChanged => !(_Value?.Equals(DefaultValue) ?? DefaultValue is null);
	private readonly string? LongName = null;
	private readonly string? ShortName = null;

	public virtual T DefaultValue { get; init; }
	public override bool IsRequired { get; init; }

	public override bool FulfillRequirement
	{
		get
		{
			if (!IsRequired || DefaultValue != null) return true;
			else return Value != null;
		}
	}

	protected T _Value;
	public virtual T Value
	{
		get => _Value; 
		set
		{
			if (!(_Value?.Equals(value) ?? false))
			{
				_Value = value;
				InvokePropertyChanged(nameof(Value));
				InvokePropertyChanged(nameof(FulfillRequirement));
				InvokePropertyChanged(nameof(IsValueChanged));
				Program.Root?.InvokePropertyChanged(nameof(Program.Root.FulfillRequirement));
			}
		}
	}

	private bool _ShouldSkip;
	public bool ShouldSkip
	{
		get => _ShouldSkip;
		set
		{
			if (_ShouldSkip != value)
			{
				_ShouldSkip = value;
				InvokePropertyChanged(nameof(ShouldSkip));
			}
		}
	}

	public override string Compile()
	{
		var flag = GetFlag();

		if (Value is not null) return string.Join(Connector, flag, ValueToString(Value));
		else if (DefaultValue != null) return string.Join(Connector, flag, ValueToString(DefaultValue));
		else return string.Empty;
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
