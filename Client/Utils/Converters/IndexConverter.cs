using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Client.Utils.Converters;

public class IndexConverter : IValueConverter
{
    private const int StartFrom = 1;
    
    public object Convert(object value, Type TargetType, object parameter, CultureInfo culture)
    {
        var item = (ListViewItem)value;
        var listView = ItemsControl.ItemsControlFromItemContainer(item) as ListView;
        var index = StartFrom + listView.ItemContainerGenerator.IndexFromContainer(item);
        return index.ToString();
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return DependencyProperty.UnsetValue;
    }
}