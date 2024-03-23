using System.Windows;
using BusinessLogic.Interfaces;
using Client.New.ViewModels;
using Client.New.Views;
using Client.Windows.Statistics;
using Client.Windows.WorkoutStory;

namespace Client.Windows;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow
{
    private readonly IDatabase database;
    private readonly ExercisesViewModel exercisesViewModel;
    
    public MainWindow(IDatabase database)
    {
        this.database = database;
        exercisesViewModel = new ExercisesViewModel(database);
        
        InitializeComponent();
        SetLastWorkoutText(database);
    }

    private void SetLastWorkoutText(IDatabase database)
    {
        var lastWorkout = database.Workouts.GetList().OrderBy(workout => workout.DateTime).Last();
        LastWorkoutLabel.Content = $"Последняя тренировка была {lastWorkout.DateTime.ToShortDateString()}";
    }

    private void OpenWorkoutStoryWindow(object sender, RoutedEventArgs routedEventArgs)
    {
        var sellHistoryWindow = new WorkoutStoryWindow(database);
        sellHistoryWindow.ShowDialog();
    }

    private void OpenExerciseListWindow(object sender, RoutedEventArgs routedEventArgs)
    {
        var exerciseListWindow = new ExercisesWindow(exercisesViewModel);
        exerciseListWindow.ShowDialog();
    }
    
    private void OpenStatisticsWindow(object sender, RoutedEventArgs routedEventArgs)
    {
        var statisticsWindow = new StatisticsWindow(database);
        statisticsWindow.ShowDialog();
    }
}