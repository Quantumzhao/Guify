using System.ComponentModel;

namespace Guify.Models.Components;

class SaveFileField : ComponentBase<string?>, INotifyPropertyChanged {

	public SaveFileField(string? defaultValue, string description, bool isRequired, string? longName
	, string? shortName) : base(defaultValue, isRequired, longName, shortName, description) {

		Value = defaultValue;
	}
}