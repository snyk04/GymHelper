using Client.New.ViewModels;

namespace Client.New.Views;

public partial class WorkoutsWindow
{
    public WorkoutsWindow(WorkoutsViewModel workoutsViewModel)
    {
        InitializeComponent();
        DataContext = workoutsViewModel;
    }
}