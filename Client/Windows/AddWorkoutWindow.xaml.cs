using System.Windows;
using BusinessLogic.Interfaces;
using BusinessLogic.Models;
using Client.Models;
using Client.Utils.Sorting;

namespace Client.Windows;

public partial class AddWorkoutWindow
{
    private readonly IDatabase database;
    private readonly ListViewSorter listViewSorter;

    public event Action<Workout> OnWorkoutSaved;

    private readonly List<Set> sets = new();

    public AddWorkoutWindow(IDatabase database)
    {
        InitializeComponent();

        this.database = database;
        listViewSorter = new ListViewSorter(SetList);
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
        FillSetsList();
    }

    private void FillSetsList()
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
        var workout = new Workout
        {
            DateTime = DateTimePicker.Value.Value,
            Sets = sets
        };
        OnWorkoutSaved?.Invoke(workout);
        
        Close();
    }
}