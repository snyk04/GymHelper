using BusinessLogic.Interfaces;
using BusinessLogic.Models;

namespace Database;

public class DatabaseTestFiller
{
    private readonly List<string> exerciseNames = new()
    {
        "Жим штанги лёжа", "Французский жим с гантелями на наклонной скамье"
    };
    public void Fill(IDatabase database)
    {
        FillWithExercises(database);
        FillWithWorkouts(database);
    }

    private void FillWithExercises(IDatabase database)
    {
        foreach (var exercise in exerciseNames)
        {
            database.Exercises.Add(new Exercise
            {
                Name = exercise
            });
        }
    }

    private void FillWithWorkouts(IDatabase database)
    {

    }
}