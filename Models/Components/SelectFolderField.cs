using System.ComponentModel;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;

namespace Guify.Models.Components;

class SelectFolderField : ComponentBase<string>, INotifyPropertyChanged
{
	public event PropertyChangedEventHandler? PropertyChanged;

	public SelectFolderField(string defaultValue, string description, bool isRequired, string? longName
		, string? shortName) : base(defaultValue, isRequired, longName, shortName, description)
	{

		Value = defaultValue;
	}

	private string _Value;
	public override string Value
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