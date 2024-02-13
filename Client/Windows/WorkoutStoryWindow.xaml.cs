using System.Windows;
using BusinessLogic.Interfaces;

namespace Client.Windows;

public partial class WorkoutStoryWindow : Window
{
    private readonly IDatabase database;
    
    public WorkoutStoryWindow(IDatabase database)
    {
        this.database = database;
        InitializeComponent();
    }
}