using BusinessLogic.Interfaces;
using BusinessLogic.Models;
using Microsoft.EntityFrameworkCore;

namespace Database;

public class Database : IDatabase
{
    private const string ConnectionString = "Data Source=database.db";

    public IRepository<Workout> Workouts { get; private set; }
    public IRepository<Set> Sets { get; private set; }
    public IRepository<Exercise> Exercises { get; private set; }
    
    public Database()
    {
        var applicationDbContext = new ApplicationDbContext(ConnectionString);
        
        ConfigureApplicationDbContext(applicationDbContext);
        LoadDbSets(applicationDbContext);
        
        Workouts = new Repository<Workout>(applicationDbContext.Workouts, applicationDbContext);
        Sets = new Repository<Set>(applicationDbContext.Sets, applicationDbContext);
        Exercises = new Repository<Exercise>(applicationDbContext.Exercises, applicationDbContext);

        // LoadTestData();
    }

    private void ConfigureApplicationDbContext(DbContext applicationContext)
    {
        applicationContext.Database.EnsureCreated();
    }

    private void LoadDbSets(ApplicationDbContext applicationDbContext)
    {
        applicationDbContext.Exercises.Load();
        applicationDbContext.Sets.Load();
        applicationDbContext.Workouts.Load();
    }

    private void LoadTestData()
    {
        var databaseTestFiller = new DatabaseTestFiller();
        databaseTestFiller.Fill(this);
    }
}