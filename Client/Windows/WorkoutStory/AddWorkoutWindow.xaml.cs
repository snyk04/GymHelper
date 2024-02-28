using System.Windows;
using BusinessLogic.Interfaces;
using BusinessLogic.Models;
using Client.Models;

namespace Client.Windows.WorkoutStory;

public partial class AddWorkoutWindow
{
    public event Action<Workout> OnWorkoutSaved;

    private readonly IDatabase database;
    
    private List<Set> sets = new();

    public AddWorkoutWindow(IDatabase database)
    {
        InitializeComponent();

        this.database = database;
    }

    private void HandleAddButtonClicked(object sender, RoutedEventArgs e)
    {
        var addSetsWindow = new AddSetsWindow(database);
        addSetsWindow.OnSetsAdded += newSets =>
        {
            sets.AddRange(newSets);
            UpdateSetsList();
        };
        addSetsWindow.ShowDialog();
    }

    private void UpdateSetsList()
    {
        var setsByExercises = GetSetsByExercises();
        var setViews = setsByExercises.Values.Select(sets => new SetsView
        {
            Exercise = sets.First().Exercise,
            Sets = sets
        });
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

    private void HandleSaveButtonClicked(object sender, RoutedEventArgs e)
    {
        var workout = new Workout
        {
            DateTime = DateTimePicker.Value.Value,
            Sets = sets
        };
        OnWorkoutSaved?.Invoke(workout);
        Close();
    }
    
    private void OnEditClicked(object sender, RoutedEventArgs e)
    {
        var setsView = (SetsView)SetList.SelectedItem;

        if (setsView == null)
        {
            return;
        }
        
        var editSetsWindow = new EditSetsWindow(database, setsView);
        editSetsWindow.OnSetsUpdated += newSets =>
        {
            sets = newSets;
            UpdateSetsList();
        };
        editSetsWindow.ShowDialog();
    }
    
    private void OnDeleteClicked(object sender, RoutedEventArgs e)
    {
        var setView = (SetsView)SetList.SelectedItem;

        if (setView == null)
        {
            return;
        }
        
        sets.RemoveAll(set => set.Exercise == setView.Exercise);
        UpdateSetsList();
    }
}