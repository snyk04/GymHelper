using System.Windows;
using BusinessLogic.Interfaces;
using BusinessLogic.Models;
using Client.Models;
using Client.Utils.Sorting;

namespace Client.Windows.WorkoutStory;

public partial class EditSetsWindow
{
    private readonly ListViewSorter listViewSorter;

    public event Action<List<Set>> OnSetsUpdated;

    private readonly List<Set> sets;
    private readonly Dictionary<SetView, Set> setsBySetViews = new();

    public EditSetsWindow(IDatabase database, SetsView setsView)
    {
        InitializeComponent();

        listViewSorter = new ListViewSorter(SetList);

        ExerciseComboBox.ItemsSource = database.Exercises.GetList();

        ExerciseComboBox.SelectedItem = setsView.Exercise;
        sets = setsView.Sets;
        UpdateSetsList();
    }

    private void OnColumnHeaderClick(object sender, RoutedEventArgs e)
    {
        listViewSorter.OnColumnHeaderClick(sender);
    }

    private void HandleAddButtonClicked(object sender, RoutedEventArgs e)
    {
        var addSetWindow = new AddSetWindow();
        addSetWindow.OnSetAdded += set =>
        {
            setsBySetViews.Clear();
            sets.Add(set);
            UpdateSetsList();
        };
        addSetWindow.ShowDialog();
    }

    private void UpdateSetsList()
    {
        SetList.ItemsSource = sets.Select(set =>
        {
            var setView = new SetView
            {
                Id = set.Id,
                Reps = set.Reps,
                Weight = set.Weight
            };
            setsBySetViews[setView] = set;
            return setView;
        });
    }

    private void HandleSaveButtonClicked(object sender, RoutedEventArgs e)
    {
        foreach (var set in sets)
        {
            set.Exercise = (Exercise)ExerciseComboBox.SelectedItem;
        }

        OnSetsUpdated?.Invoke(sets);
        Close();
    }

    private void OnEditClicked(object sender, RoutedEventArgs e)
    {
        var setView = (SetView)SetList.SelectedItem;

        if (setView != null)
        {
            var editSetWindow = new EditSetWindow(setsBySetViews[setView]);
            editSetWindow.OnSetUpdated += set =>
            {
                var oldSet = setsBySetViews[setView];
                sets.Remove(oldSet);
                sets.Add(set);
                setsBySetViews[setView] = set;
                UpdateSetsList();
            };
            editSetWindow.ShowDialog();
        }
    }

    private void OnDeleteClicked(object sender, RoutedEventArgs e)
    {
        var setView = (SetView)SetList.SelectedItem;

        if (setView != null)
        {
            sets.Remove(setsBySetViews[setView]);
            setsBySetViews.Remove(setView);
            UpdateSetsList();
        }
    }
}