using System.Globalization;
using System.Windows.Data;
using BusinessLogic.Models;

namespace Client.Utils.Converters;

public class ExerciseNameConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return ((Exercise)value).Name;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}