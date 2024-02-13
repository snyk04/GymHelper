using BusinessLogic.Models;

namespace BusinessLogic.Interfaces;

public interface IDatabase
{
    public IRepository<Workout> Workouts { get;  }
}