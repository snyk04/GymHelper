using BusinessLogic.Models;
using Microsoft.EntityFrameworkCore;

namespace Database;

public class ApplicationContext : DbContext
{
    private const string DataSource = "database.db";
    
    public DbSet<Workout> Workouts { get; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite($"DataSource={DataSource}");
    }
}