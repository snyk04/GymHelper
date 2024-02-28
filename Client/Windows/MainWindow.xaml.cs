using System.Windows;
using System.Windows.Controls;
using BusinessLogic.Interfaces;
using Client.Windows.Exercises;
using Client.Windows.WorkoutStory;

namespace Client.Windows;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private readonly IDatabase database;
    
    public MainWindow(IDatabase database)
    {
        this.database = database;
        InitializeComponent();
        InitializeWindowsMenu();
    }
    
    private void InitializeWindowsMenu()
    {
        WindowsMenu.Items.Clear();
        AddNavigationMenuItem("История тренировок", OpenWorkoutStoryWindow);
        AddNavigationMenuItem("Упражнения", OpenExerciseListWindow);
    }

    private void AddNavigationMenuItem(string name, RoutedEventHandler clickHandler)
    {
        var workoutStory = new MenuItem
        {
            Header = name
        };
        workoutStory.Click += clickHandler; 
        WindowsMenu.Items.Add(workoutStory);
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