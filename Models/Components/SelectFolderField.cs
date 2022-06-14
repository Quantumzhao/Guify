using System.ComponentModel;

namespace Guify.Models.Components;

class SelectFolderField : FieldBase<string?>, INotifyPropertyChanged
{
	public SelectFolderField(string? defaultValue, string description, bool isRequired, string? longName
		, string? shortName) : base(defaultValue, isRequired, longName, shortName, description)
	{
	}
}