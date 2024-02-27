using System.Windows;
using BusinessLogic.Interfaces;
using Client.Models;
using Client.Utils.Sorting;

namespace Client.Windows.WorkoutStory;

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
        var workouts = database.Workouts.GetList().OrderBy(workout => workout.DateTime);
        WorkoutList.ItemsSource = workouts.Select(workout => new WorkoutView
        {
            Id = workout.Id,
            DateTime = workout.DateTime
        });
    }

    private void OnColumnHeaderClick(object sender, RoutedEventArgs e)
    {
        listViewSorter.OnColumnHeaderClick(sender);
    }

    private void HandleAddButtonClicked(object sender, RoutedEventArgs e)
    {
        var addWorkoutWindow = new AddWorkoutWindow(database);
        addWorkoutWindow.OnWorkoutSaved += workout =>
        {
            foreach (var set in workout.Sets)
            {
                database.Sets.Add(set);
            }
            database.Workouts.Add(workout);
            UpdateWorkoutList();
        };
        addWorkoutWindow.ShowDialog();
    }

    private void OnEditClicked(object sender, RoutedEventArgs e)
    {
        var workoutView = (WorkoutView)WorkoutList.SelectedItem;

        if (workoutView != null)
        {
            var editWorkoutWindow = new EditWorkoutWindow(database, database.Workouts.Get(workoutView.Id));
            editWorkoutWindow.OnWorkoutUpdated += workout =>
            {
                foreach (var set in workout.Sets)
                {
                    if (database.Sets.GetList().Any(setInDb => setInDb.Id == set.Id))
                    {
                        database.Sets.Update(set);
                    }
                    else
                    {
                        database.Sets.Add(set);
                    }
                }
                database.Workouts.Update(workout);
                UpdateWorkoutList();
            };
            editWorkoutWindow.ShowDialog();
        }
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