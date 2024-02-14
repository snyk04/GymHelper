using System.Windows;
using BusinessLogic.Interfaces;
using Client.Windows;

namespace Client;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App
{
    private readonly IDatabase database;

    public App()
    {
        database = new Database.Database();
    }
    
    private void OnStartup(object sender, StartupEventArgs e)
    {
        var mainWindow = new MainWindow(database);
        mainWindow.Show();
    }
}