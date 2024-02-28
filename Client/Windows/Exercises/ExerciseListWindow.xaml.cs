using System.Windows;
using BusinessLogic.Interfaces;
using BusinessLogic.Models;
using Client.Utils.Sorting;

namespace Client.Windows.Exercises;

public partial class ExerciseListWindow
{
    private readonly IDatabase database;
    private readonly ListViewSorter listViewSorter;
    
    public ExerciseListWindow(IDatabase database)
    {
        InitializeComponent();

        this.database = database;
        listViewSorter = new ListViewSorter(ExerciseList);

        UpdateExerciseList();
    }

    private void UpdateExerciseList()
    {
        ExerciseList.ItemsSource = database.Exercises.GetList().OrderBy(exercise => exercise.Name);
    }

    private void HandleAddButtonClicked(object sender, RoutedEventArgs e)
    {
        var addExerciseWindow = new AddExerciseWindow(database);
        addExerciseWindow.OnExerciseAdded += exercise =>
        {
            database.Exercises.Add(exercise);
            UpdateExerciseList();
        };
        addExerciseWindow.ShowDialog();
    }

    private void OnEditClicked(object sender, RoutedEventArgs e)
    {
        var exerciseToEdit = (Exercise)ExerciseList.SelectedItem;
        var editExerciseWindow = new EditExerciseWindow(exerciseToEdit, database);
        editExerciseWindow.OnExerciseSaved += exercise =>
        {
            database.Exercises.Update(exercise);
            UpdateExerciseList();
        };
        editExerciseWindow.ShowDialog();
    }

    private void OnColumnHeaderClick(object sender, RoutedEventArgs e)
    {
        listViewSorter.OnColumnHeaderClick(sender);
    }
}