using System.Globalization;
using System.Windows.Data;
using BusinessLogic.Models;

namespace Client.Utils.Converters;

public class WeightSetConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var sets = (List<Set>)value;
        return string.Join(", ", sets.Select(set => set.Weight.ToString(CultureInfo.InvariantCulture)));
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}