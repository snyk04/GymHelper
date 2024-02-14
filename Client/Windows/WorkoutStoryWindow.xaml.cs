using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
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

        FillWorkoutStoryList();
    }

    private void FillWorkoutStoryList()
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
        addWorkoutWindow.OnDataUpdate += FillWorkoutStoryList;
        addWorkoutWindow.ShowDialog();
    }
}