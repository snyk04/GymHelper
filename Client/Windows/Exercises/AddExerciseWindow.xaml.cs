using System.Windows;
using BusinessLogic.Interfaces;
using BusinessLogic.Models;

namespace Client.Windows.Exercises;

public partial class AddExerciseWindow : Window
{
    public event Action<Exercise> OnExerciseSaved;

    private readonly IDatabase database;
    
    public AddExerciseWindow(IDatabase database)
    {
        InitializeComponent();
        this.database = database;
    }

    private void HandleSaveButtonClicked(object sender, RoutedEventArgs e)
    {
        if (database.Exercises.GetList().Any(exercise => exercise.Name == NameTextBox.Text))
        {
            Console.WriteLine("This exercise already exists!");
            return;
        }
        
        var exercise = new Exercise
        {
            Name = NameTextBox.Text
        };
        OnExerciseSaved?.Invoke(exercise);
        Close();
    }
}