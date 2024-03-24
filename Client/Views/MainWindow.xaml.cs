using System.Windows;
using BusinessLogic.Interfaces;
using Client.New.ViewModels;

namespace Client.New.Views;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow
{
    private readonly ExercisesViewModel exercisesViewModel;
    private readonly StatisticsViewModel statisticsViewModel;
    private readonly WorkoutsViewModel workoutsViewModel;
    
    public MainWindow(IDatabase database)
    {
        exercisesViewModel = new ExercisesViewModel(database);
        statisticsViewModel = new StatisticsViewModel(database);
        workoutsViewModel = new WorkoutsViewModel(database);
        
        InitializeComponent();
    }

    private void OpenWorkoutStoryWindow(object sender, RoutedEventArgs routedEventArgs)
    {
        var workoutsWindow = new WorkoutsWindow(workoutsViewModel);
        workoutsWindow.ShowDialog();
    }

    private void OpenExerciseListWindow(object sender, RoutedEventArgs routedEventArgs)
    {
        var exercisesWindow = new ExercisesWindow(exercisesViewModel);
        exercisesWindow.ShowDialog();
    }
    
    private void OpenStatisticsWindow(object sender, RoutedEventArgs routedEventArgs)
    {
        var statisticsWindow = new StatisticsWindow(statisticsViewModel);
        statisticsWindow.ShowDialog();
    }
}