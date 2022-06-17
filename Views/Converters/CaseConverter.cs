using System.Globalization;
using Avalonia.Data;
using Avalonia.Data.Converters;

namespace Guify.Views.Converters;

public class CaseConverter : IValueConverter
{
    public static readonly CaseConverter Instance = new();

    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        => value switch
        {
            string str when parameter is "true" => str.ToUpper(),
            string str when parameter is "false" => str.ToLower(),
            _ => throw new ArgumentNullException()
        };

    public object? ConvertBack(object? value, Type targetType, object? parameter
        , CultureInfo culture)
    {
        throw new NotSupportedException();
    }
}