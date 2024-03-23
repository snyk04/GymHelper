using BusinessLogic.Models;

namespace Client.New.Models;

public sealed class ExerciseInfoView(DateTime dateTime, Exercise exercise, List<Set> sets)
{
    public DateTime DateTime { get; } = dateTime;
    public Exercise Exercise { get; } = exercise;
    public List<Set> Sets { get; } = sets;
}