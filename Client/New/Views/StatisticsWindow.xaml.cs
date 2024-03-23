using Client.New.ViewModels;

namespace Client.New.Views;

public sealed partial class StatisticsWindow
{
    public StatisticsWindow(StatisticsViewModel statisticsViewModel)
    {
        InitializeComponent();
        DataContext = statisticsViewModel;
    }
}