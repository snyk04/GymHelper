using System.Windows;
using System.Windows.Controls;
using BusinessLogic.Interfaces;

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
            
        var addWorkout = new MenuItem
        {
            Header = "Добавить запись"
        };
        var workoutStory = new MenuItem
        {
            Header = "Посмотреть записи"
        };
            
        addWorkout.Click += OpenAddWorkoutWindow; 
        workoutStory.Click += OpenWorkoutStoryWindow; 
            
        WindowsMenu.Items.Add(addWorkout);
        WindowsMenu.Items.Add(workoutStory);
    }
    
    private void OpenAddWorkoutWindow(object sender, RoutedEventArgs e)
    {
        var productsWindow = new AddWorkoutWindow(database);
        productsWindow.ShowDialog();
    }
        
    private void OpenWorkoutStoryWindow(object sender, RoutedEventArgs routedEventArgs)
    {
        var sellHistoryWindow = new WorkoutStoryWindow(database);
        sellHistoryWindow.ShowDialog();
    }
}