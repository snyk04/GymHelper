using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Input;
using BusinessLogic.Interfaces;
using BusinessLogic.Models;
using Client.New.Models;
using Client.New.ViewModels.Utils;

namespace Client.New.ViewModels;

public sealed class ExercisesViewModel : ViewModel
{
    private readonly IDatabase database;
    
    public ObservableCollection<Exercise> Exercises { get; }
    public ObservableCollection<ExerciseInfoView> ExercisesInfoViews { get; } = [];

    public ICommand SelectionChanged { get; }
    public ICommand AddExerciseCommand { get; }

    public ExercisesViewModel(IDatabase database)
    {
        this.database = database;

        Exercises = new ObservableCollection<Exercise>(database.Exercises.GetList());
        foreach (var exercise in Exercises)
        {
            exercise.PropertyChanged += HandleExercisePropertyChanged;
        }
        
        SelectionChanged = new DelegateCommand(HandleSelectionChanged);
        AddExerciseCommand = new DelegateCommand(AddExercise);
    }

    private void HandleExercisePropertyChanged(object sender, PropertyChangedEventArgs args)
    {
        if (args.PropertyName != nameof(Exercise.Name))
        {
            return;
        }
        
        var changedExercise = (Exercise)sender;
        database.Exercises.Update(changedExercise);
    }
    
    private void HandleSelectionChanged(object obj)
    {
        var eventArgs = (SelectionChangedEventArgs)obj;
        var exercise = (Exercise)eventArgs.AddedItems[0];
        FillExerciseStory(exercise);
    }

    private void FillExerciseStory(Exercise exercise)
    {
        var workouts = database.Workouts.GetList();
        var workoutsWithExercise = workouts.Where(workout => workout.Sets.Any(set => set.Exercise == exercise));
        ExercisesInfoViews.Clear();
        foreach (var workout in workoutsWithExercise)
        {
            var exerciseInfoView = new ExerciseInfoView(workout.DateTime, exercise, []);
            foreach (var set in workout.Sets)
            {
                if (set.Exercise == exercise)
                {
                    exerciseInfoView.Sets.Add(set);
                }
            }
            ExercisesInfoViews.Add(exerciseInfoView);
        }
    }

    private void AddExercise(object _)
    {
        var newExercise = new Exercise();
        newExercise.PropertyChanged += HandleExercisePropertyChanged;
        
        Exercises.Add(newExercise);
        database.Exercises.Add(newExercise);
    }
}