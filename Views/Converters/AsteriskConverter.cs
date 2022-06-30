using System.Globalization;
using Avalonia.Data.Converters;
using Avalonia.Media;
using Guify.Models.Components;

namespace Guify.Views.Converters;

public class AsteriskConverter : IMultiValueConverter
{
	public static readonly AsteriskConverter Instance = new();

	public object? Convert(IList<object?> values, Type targetType, object? parameter, CultureInfo culture)
		=> (isValueChanged: values[0], isRequired: values[1]) switch
		{
			(true, true) => App.Current?.Resources["SubtleForeground"],
			(false, true) => App.Current?.Resources["Alert"],
			_ => App.Current?.Resources["TransparentBackground"],
		};
}