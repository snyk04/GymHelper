using BusinessLogic.Models;
using Microsoft.EntityFrameworkCore;

namespace Database;

public class ApplicationDbContext(string connectionString) : DbContext
{
    public DbSet<Workout> Workouts { get; set; }
    public DbSet<Set> Sets { get; set; }
    public DbSet<Exercise> Exercises { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);

        optionsBuilder.UseSqlite(connectionString);
    }
}