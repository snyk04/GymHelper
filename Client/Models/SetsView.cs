using BusinessLogic.Models;

namespace Client.Models;

public class SetsView
{
    public Exercise Exercise { get; set; }
    public List<Set> Sets { get; set; }
}