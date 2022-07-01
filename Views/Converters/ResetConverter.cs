using System.Globalization;
using Avalonia.Data.Converters;
using Guify.Models.Components;

namespace Guify.Views.Converters;

public class ResetConverter : IMultiValueConverter
{
	public static readonly ResetConverter Instance = new();
	
	public object? Convert(IList<object?> values, Type targetType, object? parameter, CultureInfo culture)
		=> (field: values[0], isValueChanged: values[1]) switch
		{
			(RadioButtonField rf, bool b) => b && rf.Value != rf.DefaultValue,
			(FieldBase _, bool b) => b,
			_ => null,
		};
}