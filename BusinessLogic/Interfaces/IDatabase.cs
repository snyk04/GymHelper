using BusinessLogic.Models;

namespace BusinessLogic.Interfaces;

public interface IDatabase
{
    public IRepository<Workout> Workouts { get;  }
    public IRepository<Set> Sets { get;  }
    public IRepository<Exercise> Exercises { get;  }
}