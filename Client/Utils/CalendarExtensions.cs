using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Media;

namespace Client.Utils;

public static class CalendarExtensions
{
    private const string DateBindingPath = "Date";
    
    public static void HighlightDates(this Calendar calendar, IEnumerable<DateTime> dates, SolidColorBrush highlightColor, 
        DependencyProperty backgroundProperty, DependencyProperty foregroundProperty)
    {
        var style = new Style(typeof(CalendarDayButton));
        
        foreach (var date in dates)
        {
            var trigger = new DataTrigger
            {
                Binding = new Binding(DateBindingPath),
                Value = date.Date
            };

            trigger.Setters.Add(new Setter(backgroundProperty, highlightColor));
            trigger.Setters.Add(new Setter(foregroundProperty, Brushes.Black));

            style.Triggers.Add(trigger);
        }

        calendar.CalendarDayButtonStyle = style;
    }
}