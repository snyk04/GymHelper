using Client.New.ViewModels;

namespace Client.New.Views;

public sealed partial class ExercisesWindow
{
    public ExercisesWindow(ExercisesViewModel exercisesViewModel)
    {
        InitializeComponent();
        DataContext = exercisesViewModel;
    }
}