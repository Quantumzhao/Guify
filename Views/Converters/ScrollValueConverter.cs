using System.Globalization;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data;
using Avalonia.Data.Converters;
using Avalonia.Media;

#pragma warning disable

namespace Guify.Views.Converters;

public class ScrollValueConverter : IMultiValueConverter
{
	private static int _Flag = 1;

	public object? Convert(IList<object?> values, Type targetType, object? parameter, CultureInfo culture)
	{
		var res = (values[0], values[1]) switch
		{
			(ScrollViewer s, double n) when n <= 1 => Colors.White,
			(ScrollViewer s, double n) => Colors.Transparent,
			_ => throw new ArgumentException()
		};

		// MainWindow.Instance.Height += _Flag;
		// _Flag *= -1;

		return res;
	}
}