using System.Windows;
using BusinessLogic.Models;

namespace Client.Windows.WorkoutStory;

public partial class EditSetWindow
{
    public event Action<Set> OnSetUpdated;

    private readonly Set set;
    
    public EditSetWindow(Set set)
    {
        InitializeComponent();

        this.set = set;
        
        RepsTextBox.Text = set.Reps.ToString();
        WeightTextBox.Text = set.Weight.ToString();
    }
    
    private void HandleSaveButtonClicked(object sender, RoutedEventArgs e)
    {
        set.Reps = int.Parse(RepsTextBox.Text);
        set.Weight = float.Parse(WeightTextBox.Text);
        
        OnSetUpdated?.Invoke(set);
        
        Close();
    }
}