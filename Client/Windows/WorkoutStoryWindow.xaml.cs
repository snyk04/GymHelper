using System.Windows;
using System.Windows.Controls;
using BusinessLogic.Interfaces;
using BusinessLogic.Models;
using Client.Models;
using Client.Utils.Sorting;

namespace Client.Windows;

public partial class WorkoutStoryWindow
{
    private readonly IDatabase database;
    private readonly ListViewSorter listViewSorter;

    public WorkoutStoryWindow(IDatabase database)
    {
        InitializeComponent();

        this.database = database;
        listViewSorter = new ListViewSorter(WorkoutList);

        UpdateWorkoutList();
    }

    private void UpdateWorkoutList()
    {
        var workouts = database.Workouts.GetList();
        WorkoutList.ItemsSource = workouts.Select(ConvertWorkoutToWorkoutView);
    }

    private WorkoutView ConvertWorkoutToWorkoutView(Workout workout)
    {
        return new WorkoutView
        {
            Id = workout.Id,
            DateTime = workout.DateTime
        };
    }

    private void OnColumnHeaderClick(object sender, RoutedEventArgs e)
    {
        listViewSorter.OnColumnHeaderClick(sender);
    }

    private void HandleAddButtonClicked(object sender, RoutedEventArgs e)
    {
        var addWorkoutWindow = new AddWorkoutWindow(database);
        addWorkoutWindow.OnWorkoutSaved += HandleWorkoutSaved;
        addWorkoutWindow.ShowDialog();
    }

    private void HandleWorkoutSaved(Workout workout)
    {
        database.Workouts.Add(workout);
        UpdateWorkoutList();
    }

    private void OnDeleteClicked(object sender, RoutedEventArgs e)
    {
        var workout = (WorkoutView)WorkoutList.SelectedItem;

        if (workout != null)
        {
            database.Workouts.Remove(workout.Id);
            UpdateWorkoutList();
        }
    }
}