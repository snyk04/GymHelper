using System.Globalization;
using System.Windows;
using BusinessLogic.Models;

namespace Client.Windows.WorkoutStory;

public partial class AddSetWindow
{
    public event Action<Set> OnSetAdded; 
    
    public AddSetWindow()
    {
        InitializeComponent();
    }
    
    private void HandleSaveButtonClicked(object sender, RoutedEventArgs e)
    {
        var set = new Set
        {
            Reps = int.Parse(RepsTextBox.Text, CultureInfo.InvariantCulture),
            Weight = float.Parse(WeightTextBox.Text, CultureInfo.InvariantCulture)
        };
        
        OnSetAdded?.Invoke(set);
        
        Close();
    }
}