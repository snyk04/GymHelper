using System.Windows;
using BusinessLogic.Models;

namespace Client.Windows;

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
            Reps = int.Parse(RepsTextBox.Text),
            Weight = float.Parse(WeightTextBox.Text)
        };
        OnSetAdded?.Invoke(set);
        
        Close();
    }
}