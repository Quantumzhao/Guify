using System.Globalization;
using Avalonia.Data;
using Avalonia.Data.Converters;

namespace Guify.Views.Converters;

public class ToUpperConverter : IValueConverter
{
    public static readonly ToUpperConverter Instance = new();
    
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is not string str) 
            return new BindingNotification(new InvalidCastException(), BindingErrorType.Error);
        else return str.ToUpper();
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter
        , CultureInfo culture)
    {
        throw new NotSupportedException();
    }
}