using System.Collections;
using System.Windows;
using BusinessLogic;
using BusinessLogic.Interfaces;
using BusinessLogic.Models;

namespace Client.Windows.Statistics;

public partial class StatisticsWindow : Window
{
    public StatisticsWindow(IDatabase database)
    {
        InitializeComponent();

        var workouts = database.Workouts.GetList();
        InitializeTotalWorkoutsLabel(workouts);
        InitializeFavouriteExercisesLabel(workouts);
    }

    private void InitializeTotalWorkoutsLabel(ICollection workouts)
    {
        TotalWorkoutsLabel.Content = $"Всего тренировок: {workouts.Count.ToString()}";
    }
    
    private void InitializeFavouriteExercisesLabel(List<Workout> workouts)
    {
        var favouriteExercise = WorkoutAnalyzer.GetFavouriteExercise(workouts);
        FavouriteExerciseLabel.Content =
            $"Любимое упражнение: {favouriteExercise.Exercise.Name} | {favouriteExercise.AmountOfWorkouts} тренировок";
    }
}