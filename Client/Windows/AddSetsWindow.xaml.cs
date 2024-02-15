using System.Windows;
using BusinessLogic.Interfaces;
using BusinessLogic.Models;
using Client.Models;
using Client.Utils.Sorting;

namespace Client.Windows;

public partial class AddSetsWindow
{
    private readonly ListViewSorter listViewSorter;

    public event Action<List<Set>> OnSetsAdded;

    private readonly List<Set> sets = new();
    
    public AddSetsWindow(IDatabase database)
    {
        InitializeComponent();
        
        listViewSorter = new ListViewSorter(SetList);

        ExerciseComboBox.ItemsSource = database.Exercises.GetList();
    }
    
    private void OnColumnHeaderClick(object sender, RoutedEventArgs e)
    {
        listViewSorter.OnColumnHeaderClick(sender);
    }
    
    private void HandleAddButtonClicked(object sender, RoutedEventArgs e)
    {
        var addSetWindow = new AddSetWindow();
        addSetWindow.OnSetAdded += HandleSetAdded;
        addSetWindow.ShowDialog();
    }

    private void HandleSetAdded(Set set)
    {
        sets.Add(set);
        UpdateSetsList();
    }

    private void UpdateSetsList()
    {
        SetList.ItemsSource = sets.Select(ConvertSetToSetView);
    }

    private SetView ConvertSetToSetView(Set set)
    {
        return new SetView
        {
            Id = set.Id,
            Reps = set.Reps,
            Weight = set.Weight
        };
    }

    private void HandleSaveButtonClicked(object sender, RoutedEventArgs e)
    {
        foreach (var set in sets)
        {
            set.Exercise = (Exercise)ExerciseComboBox.SelectedItem;
        }
        
        OnSetsAdded?.Invoke(sets);
        Close();
    }
    
    private void OnDeleteClicked(object sender, RoutedEventArgs e)
    {
        var setView = (SetView)SetList.SelectedItem;

        if (setView != null)
        {
            sets.RemoveAll(set => set.Id == setView.Id);
            UpdateSetsList();
        }
    }
}