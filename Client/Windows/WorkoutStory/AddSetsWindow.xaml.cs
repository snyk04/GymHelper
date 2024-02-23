using System.Windows;
using BusinessLogic.Interfaces;
using BusinessLogic.Models;
using Client.Models;
using Client.Utils.Sorting;

namespace Client.Windows.WorkoutStory;

public partial class AddSetsWindow
{
    public event Action<List<Set>> OnSetsAdded;

    private readonly ListViewSorter listViewSorter;

    private readonly List<Set> sets = new();
    private readonly Dictionary<SetView, Set> setsBySetViews = new();

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
        addSetWindow.OnSetAdded += set =>
        {
            sets.Add(set);
            UpdateSetsList();
        };
        addSetWindow.ShowDialog();
    }

    private void UpdateSetsList()
    {
        setsBySetViews.Clear();
        SetList.ItemsSource = sets.Select(set =>
        {
            var setView = new SetView
            {
                Id = set.Id,
                Reps = set.Reps,
                Weight = set.Weight
            };
            setsBySetViews.Add(setView, set);
            return setView;
        });
    }

    private void HandleSaveButtonClicked(object sender, RoutedEventArgs e)
    {
        foreach (var (_, set) in setsBySetViews)
        {
            set.Exercise = (Exercise)ExerciseComboBox.SelectedItem;
        }

        OnSetsAdded?.Invoke(sets.ToList());
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