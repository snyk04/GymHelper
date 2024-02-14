using BusinessLogic.Models;
using Microsoft.EntityFrameworkCore;

namespace Database;

public class ApplicationDbContext : DbContext
{
    private readonly string connectionString;
    
    public DbSet<Workout> Workouts { get; set; }
    public DbSet<Exercise> Exercises { get; set; }
    
    public ApplicationDbContext(string connectionString)
    {
        this.connectionString = connectionString;
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);

        optionsBuilder.UseSqlite(connectionString);
    }
}