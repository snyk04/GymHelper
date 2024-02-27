﻿using System.Globalization;
using System.Windows.Controls;
using System.Windows.Data;

namespace Client.Utils.Converters;

public class IndexConverter : IValueConverter
{
    public object Convert(object value, Type TargetType, object parameter, CultureInfo culture)
    {
        var item = (ListViewItem) value;
        var listView = ItemsControl.ItemsControlFromItemContainer(item) as ListView;
        var index = listView.ItemContainerGenerator.IndexFromContainer(item) + 1;
        return index.ToString();
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}