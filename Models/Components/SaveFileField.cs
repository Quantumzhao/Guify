using System.ComponentModel;

namespace Guify.Models.Components;

class SaveFileField : ComponentBase<string?>, INotifyPropertyChanged {

	public SaveFileField(string defaultValue, string description, bool isRequired, string? longName
	, string? shortName) : base(defaultValue, isRequired, longName, shortName, description) {

		Value = defaultValue;
	}

	public event PropertyChangedEventHandler? PropertyChanged;

	private string? _Value;
	public override string? Value 
	{ 
		get => _Value;
		set
		{
			if (_Value != value)
			{
				_Value = value;
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Value)));
			}
		}
	}
}