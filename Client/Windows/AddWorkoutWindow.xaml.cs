using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using BusinessLogic.Interfaces;
using Client.Models;

namespace Client.Windows;

public partial class AddWorkoutWindow : Window
{
    private readonly IDatabase database;

    public event Action OnDataUpdate;
    
    public AddWorkoutWindow(IDatabase database)
    {
        this.database = database;
        
        InitializeComponent();
    }
}