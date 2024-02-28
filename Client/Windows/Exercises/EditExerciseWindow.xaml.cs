using System.Windows;
using BusinessLogic.Interfaces;
using BusinessLogic.Models;

namespace Client.Windows.Exercises;

public partial class EditExerciseWindow : Window
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
        if (database.Exercises.GetList().Any(exercise => exercise.Name == NameTextBox.Text))
        {
            Console.WriteLine("This exercise already exists!");
            return;
        }

        exercise.Name = NameTextBox.Text;
        OnExerciseSaved?.Invoke(exercise);
        Close();
    }
}