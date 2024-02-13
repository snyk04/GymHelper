using System.Windows;
using BusinessLogic.Interfaces;

namespace Client.Windows;

public partial class AddWorkoutWindow : Window
{
    private readonly IDatabase database;
    
    public AddWorkoutWindow(IDatabase database)
    {
        this.database = database;
        
        InitializeComponent();
    }
}