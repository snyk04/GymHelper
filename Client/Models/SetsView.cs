using BusinessLogic.Models;

namespace Client.Models;

public class SetsView
{
    public Exercise Exercise { get; init; }
    public List<Set> Sets { get; init; }
}