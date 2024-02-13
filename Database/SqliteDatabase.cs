using BusinessLogic.Interfaces;
using BusinessLogic.Models;

namespace Database;

public class SqliteDatabase : IDatabase
{
    public IRepository<Workout> Workouts { get; }

    public SqliteDatabase()
    {
        var applicationContext = new ApplicationContext();
        Workouts = new SqliteRepository<Workout>(applicationContext.Workouts);
    }
}