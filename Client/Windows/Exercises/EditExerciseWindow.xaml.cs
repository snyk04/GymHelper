using System.Windows;
using BusinessLogic.Interfaces;
using BusinessLogic.Models;

namespace Client.Windows.Exercises;

public partial class EditExerciseWindow
{
    public event Action<Exercise> OnExerciseSaved;
    
    private readonly IDatabase database;
    private readonly Exercise exercise;
    
    public EditExerciseWindow(Exercise exercise, IDatabase database)
    {
        InitializeComponent();

        this.exercise = exercise;
        this.database = database;

        NameTextBox.Text = this.exercise.Name;
    }

    private void HandleSaveButtonClicked(object sender, RoutedEventArgs e)
    {
        if (ExerciseExists())
        {
            Console.WriteLine("This exercise already exists!");
            return;
        }

        exercise.Name = NameTextBox.Text;
        OnExerciseSaved?.Invoke(exercise);
        Close();
    }

    private bool ExerciseExists()
    {
        return database.Exercises.GetList().Any(exercise => exercise.Name == NameTextBox.Text);
    }
}