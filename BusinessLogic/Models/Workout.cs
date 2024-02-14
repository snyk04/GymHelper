namespace BusinessLogic.Models;

public class Workout
{
    public int Id { get; set; }
    public List<Set> Sets { get; set; }
    public DateTime DateTime { get; set; }
}