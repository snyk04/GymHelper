using System.Windows;
using BusinessLogic.Interfaces;
using Client.New.ViewModels;
using Client.New.Views;
using Client.Windows.WorkoutStory;
using StatisticsWindow = Client.New.Views.StatisticsWindow;

namespace Client.Windows;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow
{
    private readonly IDatabase database;
    private readonly ExercisesViewModel exercisesViewModel;
    private readonly StatisticsViewModel statisticsViewModel;
    
    public MainWindow(IDatabase database)
    {
        this.database = database;
        exercisesViewModel = new ExercisesViewModel(database);
        statisticsViewModel = new StatisticsViewModel(database);
        
        InitializeComponent();
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
        var statisticsWindow = new StatisticsWindow(statisticsViewModel);
        statisticsWindow.ShowDialog();
    }
}