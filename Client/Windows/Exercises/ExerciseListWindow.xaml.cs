using System.Windows;
using System.Windows.Controls;
using BusinessLogic.Interfaces;
using BusinessLogic.Models;
using Client.Utils.Sorting;

namespace Client.Windows.Exercises;

public sealed class ExerciseInfoView
{
    public DateTime DateTime { get; init; }
    public Exercise Exercise { get; init; }
    public List<Set> Sets { get; init; }
}

public partial class ExerciseListWindow
{
    private readonly IDatabase database;
    private readonly ListViewSorter listViewSorter;
    
    public ExerciseListWindow(IDatabase database)
    {
        InitializeComponent();

        this.database = database;
        listViewSorter = new ListViewSorter(ExerciseList);

        UpdateExerciseList();
    }

    private void UpdateExerciseList()
    {
        ExerciseList.ItemsSource = database.Exercises.GetList().OrderBy(exercise => exercise.Name);
    }

    private void HandleAddButtonClicked(object sender, RoutedEventArgs e)
    {
        var addExerciseWindow = new AddExerciseWindow(database);
        addExerciseWindow.OnExerciseAdded += exercise =>
        {
            database.Exercises.Add(exercise);
            UpdateExerciseList();
        };
        addExerciseWindow.ShowDialog();
    }

    private void OnEditClicked(object sender, RoutedEventArgs e)
    {
        var exerciseToEdit = (Exercise)ExerciseList.SelectedItem;
        var editExerciseWindow = new EditExerciseWindow(exerciseToEdit, database);
        editExerciseWindow.OnExerciseSaved += exercise =>
        {
            database.Exercises.Update(exercise);
            UpdateExerciseList();
        };
        editExerciseWindow.ShowDialog();
    }

    private void OnColumnHeaderClick(object sender, RoutedEventArgs e)
    {
        listViewSorter.OnColumnHeaderClick(sender);
    }

    private void ExerciseList_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var exercise = (Exercise)ExerciseList.SelectedItem;

        var workouts = database.Workouts.GetList();
        var workoutsWithExercise = workouts.Where(workout => workout.Sets.Any(set => set.Exercise == exercise));

        var result = new List<ExerciseInfoView>();
        foreach (var workout in workoutsWithExercise)
        {
            var exerciseInfoView = new ExerciseInfoView()
            {
                DateTime = workout.DateTime,
                Exercise = exercise,
                Sets = new List<Set>()
            };
            foreach (var set in workout.Sets)
            {
                if (set.Exercise == exercise)
                {
                    exerciseInfoView.Sets.Add(set);
                }
            }
            result.Add(exerciseInfoView);
        }
        
        ExerciseHistoryListView.ItemsSource = result;
    }
}