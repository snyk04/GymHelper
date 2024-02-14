using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Client.Utils.Converters;

public class DateTimeConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return ((DateTime)value).ToString("HH:mm:ss dd.MM.yyyy");
    }
     
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return DependencyProperty.UnsetValue;
    }  
}