using BusinessLogic.Models;

namespace BusinessLogic;

public class FavouriteExercise
{
    public Exercise Exercise { get; init; }
    public int AmountOfWorkouts { get; init; }
}

public class WorkoutAnalyzer
{
    public static FavouriteExercise GetFavouriteExercise(List<Workout> workouts)
    {
        var amountOfSetsByExercise = new Dictionary<Exercise, int>();
        foreach (var workout in workouts)
        {
            var exerciseSet = new HashSet<Exercise>();
            foreach (var set in workout.Sets)
            {
                if (exerciseSet.Contains(set.Exercise))
                {
                    continue;
                }

                exerciseSet.Add(set.Exercise);
                amountOfSetsByExercise.TryAdd(set.Exercise, 0);
                amountOfSetsByExercise[set.Exercise]++;
            }
        }

        var maxPair = amountOfSetsByExercise.MaxBy(pair => pair.Value);
        return new FavouriteExercise
        {
            Exercise = maxPair.Key,
            AmountOfWorkouts = maxPair.Value
        };
    }
}