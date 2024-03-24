namespace BusinessLogic.Models;

public class Workout
{
    public int Id { get; set; }
    public List<Set> Sets { get; set; }
    public DateTime DateTime { get; set; }

    public Workout()
    {
    }

    public Workout(List<Set> sets, DateTime dateTime)
    {
        Sets = sets;
        DateTime = dateTime;
    }
}