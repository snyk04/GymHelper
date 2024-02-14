using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Documents;

namespace Client.Utils.Sorting;

/// <summary>
/// Sorts list view after clicking on column headers. Also draws sort adorners in column headers (🔼 or 🔽)
/// </summary>
public class ListViewSorter
{
    private readonly ListView listView;
    
    private GridViewColumnHeader listViewSortingColumn;
    private SortAdorner listViewSortAdorner;

    public ListViewSorter(ListView listView)
    {
        this.listView = listView;
    }

    public void OnColumnHeaderClick(object sender)
    {
        if (sender is not GridViewColumnHeader column)
        {
            return;
        }
        
        if (listViewSortingColumn != null)
        {
            DisableCurrentSorting();
        }

        EnableNewSorting(column);
    }

    private void DisableCurrentSorting()
    {
        AdornerLayer.GetAdornerLayer(listViewSortingColumn)?.Remove(listViewSortAdorner);
        listView.Items.SortDescriptions.Clear();
    }

    private void EnableNewSorting(GridViewColumnHeader column)
    {
        var newDirection = GetNewDirection(column);

        listViewSortingColumn = column;
        listViewSortAdorner = new SortAdorner(listViewSortingColumn, newDirection);
        AdornerLayer.GetAdornerLayer(listViewSortingColumn)?.Add(listViewSortAdorner);
        var sortingOptionName = column.Tag.ToString();
        listView.Items.SortDescriptions.Add(new SortDescription(sortingOptionName, newDirection));
    }

    private ListSortDirection GetNewDirection(GridViewColumnHeader column)
    {
        return listViewSortingColumn == column && listViewSortAdorner.Direction == ListSortDirection.Ascending
            ? ListSortDirection.Descending
            : ListSortDirection.Ascending;
    }
}