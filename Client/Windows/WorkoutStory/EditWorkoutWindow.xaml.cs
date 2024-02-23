using System.Windows;
using BusinessLogic.Interfaces;
using BusinessLogic.Models;
using Client.Models;
using Client.Utils.Sorting;

namespace Client.Windows.WorkoutStory;

public partial class EditWorkoutWindow
{
    private readonly IDatabase database;
    private readonly ListViewSorter listViewSorter;

    public event Action<Workout> OnWorkoutUpdated;

    private readonly List<Set> sets;
    private readonly Workout workout;

    public EditWorkoutWindow(IDatabase database, Workout workout)
    {
        InitializeComponent();

        this.database = database;
        this.workout = workout;
        listViewSorter = new ListViewSorter(SetList);

        DateTimePicker.Value = workout.DateTime;
        sets = workout.Sets;
        UpdateSetsList();
    }

    private void OnColumnHeaderClick(object sender, RoutedEventArgs e)
    {
        listViewSorter.OnColumnHeaderClick(sender);
    }

    private void HandleAddButtonClicked(object sender, RoutedEventArgs e)
    {
        var addSetsWindow = new AddSetsWindow(database);
        addSetsWindow.OnSetsAdded += HandleSetsAdded;
        addSetsWindow.ShowDialog();
    }

    private void HandleSetsAdded(IEnumerable<Set> newSets)
    {
        sets.AddRange(newSets);
        UpdateSetsList();
    }

    private void UpdateSetsList()
    {
        var setsByExercises = GetSetsByExercises();
        var setViews = setsByExercises.Values.Select(ConvertSetsToSetView);
        SetList.ItemsSource = setViews;
    }

    private Dictionary<Exercise, List<Set>> GetSetsByExercises()
    {
        var setsByExercises = new Dictionary<Exercise, List<Set>>();
        foreach (var set in sets)
        {
            if (!setsByExercises.ContainsKey(set.Exercise))
            {
                setsByExercises[set.Exercise] = new List<Set> { set };
                continue;
            }

            setsByExercises[set.Exercise].Add(set);
        }
        return setsByExercises;
    }

    private SetsView ConvertSetsToSetView(List<Set> sets)
    {
        return new SetsView
        {
            Exercise = sets.First().Exercise,
            Sets = sets
        };
    }

    private void HandleSaveButtonClicked(object sender, RoutedEventArgs e)
    {
        workout.DateTime = DateTimePicker.Value.Value;
        workout.Sets = sets;
        OnWorkoutUpdated?.Invoke(workout);
        Close();
    }
    
    private void OnEditClicked(object sender, RoutedEventArgs e)
    {
        var setsView = (SetsView)SetList.SelectedItem;

        if (setsView != null)
        {
            var editSetsWindow = new EditSetsWindow(database, setsView);
            editSetsWindow.OnSetsUpdated += sets =>
            {
                workout.Sets = sets;
                UpdateSetsList();
            };
            editSetsWindow.ShowDialog();
        }
    }
    
    private void OnDeleteClicked(object sender, RoutedEventArgs e)
    {
        var setView = (SetsView)SetList.SelectedItem;

        if (setView != null)
        {
            sets.RemoveAll(set => set.Exercise == setView.Exercise);
            UpdateSetsList();
        }
    }
}