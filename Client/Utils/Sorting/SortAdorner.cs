using System.ComponentModel;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;

namespace Client.Utils.Sorting;

/// <summary>
/// Draws 🔼 or 🔽 in list column header, this symbol shows whether sorting is ascending or descending
/// </summary>
public class SortAdorner : Adorner
{
    private const int MinColumnWidth = 20;
    
    private static readonly Geometry AscendingGeometry = Geometry.Parse("M 0 4 L 3.5 0 L 7 4 Z");
    private static readonly Geometry DescendingGeometry = Geometry.Parse("M 0 0 L 3.5 4 L 7 0 Z");
    private readonly SolidColorBrush sortArrowColor = Brushes.Black;
    
    private readonly Dictionary<ListSortDirection, Geometry> geometryBySortDirection = new()
    {
        [ListSortDirection.Ascending] = AscendingGeometry,
        [ListSortDirection.Descending] = DescendingGeometry
    };

    public ListSortDirection Direction { get; }

    public SortAdorner(UIElement element, ListSortDirection direction) : base(element)
    {
        Direction = direction;
    }

    protected override void OnRender(DrawingContext drawingContext)
    {
        base.OnRender(drawingContext);

        if (!IsColumnWideEnough())
        {
            return;
        }

        var transform = GetTranslateTransform();
        drawingContext.PushTransform(transform);
        DrawSortArrow(drawingContext);
    }

    private bool IsColumnWideEnough()
    {
        return AdornedElement.RenderSize.Width > MinColumnWidth;
    }

    private TranslateTransform GetTranslateTransform()
    {
        return new TranslateTransform
        (
            AdornedElement.RenderSize.Width - 15,
            (AdornedElement.RenderSize.Height - 5) / 2
        );
    }

    private void DrawSortArrow(DrawingContext drawingContext)
    {
        var geometry = geometryBySortDirection[Direction];
        drawingContext.DrawGeometry(sortArrowColor, null, geometry);
        drawingContext.Pop();
    }
}