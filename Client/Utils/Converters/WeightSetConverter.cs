using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using BusinessLogic.Models;

namespace Client.Utils.Converters;

public class WeightSetConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var sets = (ObservableCollection<Set>)value;
        return string.Join(", ", sets.Select(set => set.Weight.ToString(CultureInfo.InvariantCulture)));
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return DependencyProperty.UnsetValue;
    }
}