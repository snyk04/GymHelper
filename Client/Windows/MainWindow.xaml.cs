using System.Windows;
using System.Windows.Controls;
using BusinessLogic.Interfaces;
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
        
        var workoutStory = new MenuItem
        {
            Header = "История тренировок"
        };
            
        workoutStory.Click += OpenWorkoutStoryWindow; 
            
        WindowsMenu.Items.Add(workoutStory);
    }
        
    private void OpenWorkoutStoryWindow(object sender, RoutedEventArgs routedEventArgs)
    {
        var sellHistoryWindow = new WorkoutStoryWindow(database);
        sellHistoryWindow.ShowDialog();
    }
}