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
            Name = "Жим лёжа"
        });
        database.Exercises.Add(new Exercise
        {
            Name = "Выпады с гантелями"
        });
    }

    private void FillWithWorkouts(IDatabase database)
    {

    }
}