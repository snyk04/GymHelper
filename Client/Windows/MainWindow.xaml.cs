using System.Windows;
using BusinessLogic.Interfaces;
using Client.Windows.Exercises;
using Client.Windows.WorkoutStory;

namespace Client.Windows;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow
{
    private readonly IDatabase database;
    
    public MainWindow(IDatabase database)
    {
        this.database = database;
        InitializeComponent();
    }
        
    private void OpenWorkoutStoryWindow(object sender, RoutedEventArgs routedEventArgs)
    {
        var sellHistoryWindow = new WorkoutStoryWindow(database);
        sellHistoryWindow.ShowDialog();
    }

    private void OpenExerciseListWindow(object sender, RoutedEventArgs routedEventArgs)
    {
        var exerciseListWindow = new ExerciseListWindow(database);
        exerciseListWindow.ShowDialog();
    }
}