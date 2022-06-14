using System.ComponentModel;
namespace Guify.Models.Components
{
	class RadioButtonField : ComponentBase<bool?>, INotifyPropertyChanged
	{
		public RadioButtonField(bool defaultValue, bool isRequired, string? longName
			, string? shortName, string description, string group) : base(defaultValue, isRequired, longName,
			shortName, description)
		{
			Group = group;
		}
		
		public string Group { get; init; }
	}
}