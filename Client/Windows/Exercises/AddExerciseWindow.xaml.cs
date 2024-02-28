using System.Windows;
using BusinessLogic.Interfaces;
using BusinessLogic.Models;

namespace Client.Windows.Exercises;

public partial class AddExerciseWindow
{
    public event Action<Exercise> OnExerciseAdded;

    private readonly IDatabase database;
    
    public AddExerciseWindow(IDatabase database)
    {
        InitializeComponent();
        
        this.database = database;
    }

    private void HandleSaveButtonClicked(object sender, RoutedEventArgs e)
    {
        if (ExerciseExists())
        {
            Console.WriteLine("This exercise already exists!");
            return;
        }
        
        var exercise = new Exercise
        {
            Name = NameTextBox.Text
        };
        OnExerciseAdded?.Invoke(exercise);
        Close();
    }

    private bool ExerciseExists()
    {
        return database.Exercises.GetList().Any(exercise => exercise.Name == NameTextBox.Text);
    }
}