using System.Windows;
using System.Windows.Controls;

namespace Client.Utils;

public sealed class MarginSetter
{
    public static readonly DependencyProperty MarginProperty = DependencyProperty.RegisterAttached("Margin",
        typeof(Thickness), typeof(MarginSetter), new UIPropertyMetadata(new Thickness(), MarginChangedCallback));

    public static Thickness GetMargin(DependencyObject obj)
    {
        return (Thickness)obj.GetValue(MarginProperty);
    }

    public static void SetMargin(DependencyObject obj, Thickness value)
    {
        obj.SetValue(MarginProperty, value);
    }

    private static void MarginChangedCallback(object sender, DependencyPropertyChangedEventArgs e)
    {
        if (sender is not Panel panel)
        {
            return;
        }

        panel.Loaded += PanelLoaded;
    }

    private static void PanelLoaded(object sender, RoutedEventArgs e)
    {
        var panel = sender as Panel;

        foreach (var child in panel.Children)
        {
            if (child is not FrameworkElement fe)
            {
                continue;
            }

            fe.Margin = GetMargin(panel);
        }
    }
}