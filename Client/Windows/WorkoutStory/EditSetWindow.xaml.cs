using System.Globalization;
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
        WeightTextBox.Text = set.Weight.ToString(CultureInfo.InvariantCulture);
    }

    private void HandleSaveButtonClicked(object sender, RoutedEventArgs e)
    {
        set.Reps = int.Parse(RepsTextBox.Text, CultureInfo.InvariantCulture);
        set.Weight = float.Parse(WeightTextBox.Text, CultureInfo.InvariantCulture);

        OnSetUpdated?.Invoke(set);

        Close();
    }
}