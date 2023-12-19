using System.ComponentModel;

namespace Guify.Models.Components;

internal abstract class FieldBase : ComponentBase, INotifyPropertyChanged
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
	public List<string> Flags { get; } = [];
	protected char Connector = ' ';
	public abstract void Reset();

	protected void InvokePropertyChanged(string propertyName) 
		=> PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}

internal abstract class FieldBase<T> : FieldBase
{
	internal FieldBase(T defaultValue, bool isRequired, string? longName, string? shortName
		,string description, bool useEqualConnector) : base(description)
	{
		DefaultValue = defaultValue;
		_Value = defaultValue;

		IsRequired = isRequired;
		_LongName = longName;
		_ShortName = shortName;

		if (longName != null) Flags.Add(longName);
		if (shortName != null) Flags.Add(shortName);
		if (useEqualConnector) Connector = '=';
	}

	//                         value
	//                    null   non-null
	// default     null   true   false
	//   value non-null   false  ?? == ??
	public override bool IsValueChanged => !(_Value?.Equals(DefaultValue) ?? DefaultValue is null);
	private readonly string? _LongName;
	private readonly string? _ShortName;

	public T DefaultValue { get; init; }
	public override bool IsRequired { get; init; }

	public override bool FulfillRequirement
	{
		get
		{
			if (!IsRequired || DefaultValue != null) return true;
			else return Value != null;
		}
	}

	private protected T _Value;
	internal virtual T Value
	{
		get => _Value; 
		set
		{
			if (_Value?.Equals(value) ?? false) return;
			_Value = value;
			InvokePropertyChanged(nameof(Value));
			InvokePropertyChanged(nameof(FulfillRequirement));
			InvokePropertyChanged(nameof(IsValueChanged));
			Program.Root?.InvokePropertyChanged(nameof(Program.Root.FulfillRequirement));
		}
	}
	
	internal override string Compile()
	{
		var flag = GetFlag();

		if (Value is not null) return string.Join(Connector, flag, ValueToString(Value));
		else if (DefaultValue != null) return string.Join(Connector, flag, ValueToString(DefaultValue));
		else return string.Empty;
	}

	public override void Reset() => Value = DefaultValue;

	internal string GetFlag() 
		=> (LongName: _LongName, ShortName: _ShortName) switch
		{
			({ } l, _) => l,
			(null, { } s) => s,
			_ => string.Empty
		};

	internal string GetName()
	{
		if (_LongName != null) return _LongName;
		else if (_ShortName != null) return _ShortName;
		else return Description[0..5] + "...";
	}

	internal virtual string ValueToString(T value) => value?.ToString() ?? string.Empty;
}
