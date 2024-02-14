using BusinessLogic.Interfaces;
using BusinessLogic.Models;

namespace Database;

public class DatabaseTestFiller
{
    public void Fill(IDatabase database)
    {
        FillWithExercises(database);
        FillWithWorkouts(database);
    }

    private void FillWithExercises(IDatabase database)
    {
        database.Exercises.Add(new Exercise
        {
            Name = "Bench press"
        });
    }

    private void FillWithWorkouts(IDatabase database)
    {

    }
}