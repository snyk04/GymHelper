using BusinessLogic;
using BusinessLogic.Interfaces;
using Client.New.ViewModels.Utils;

namespace Client.New.ViewModels;

public sealed class StatisticsViewModel : ViewModel
{
    private readonly IDatabase database;

    private int workoutsCount;
    public int WorkoutsCount
    {
        get => workoutsCount;
        set
        {
            workoutsCount = value;
            OnPropertyChanged();
        }
    }

    private FavouriteExercise favouriteExercise;
    public FavouriteExercise FavouriteExercise
    {
        get => favouriteExercise;
        set
        {
            favouriteExercise = value;
            OnPropertyChanged();
        }
    }

    public StatisticsViewModel(IDatabase database)
    {
        this.database = database;

        database.Workouts.OnChange += HandleWorkoutsChange;

        HandleWorkoutsChange();
    }

    private void HandleWorkoutsChange()
    {
        workoutsCount = database.Workouts.GetList().Count;
        FavouriteExercise = WorkoutAnalyzer.GetFavouriteExercise(database.Workouts.GetList());
    }
}