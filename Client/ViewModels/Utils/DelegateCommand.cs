using System.Windows.Input;

namespace Client.New.ViewModels.Utils;

public class DelegateCommand(Action<object> execute, Predicate<object> canExecute = null) : ICommand
{
    public event EventHandler CanExecuteChanged;

    public bool CanExecute(object parameter)
    {
        return canExecute == null || canExecute(parameter);
    }

    public void Execute(object parameter)
    {
        execute(parameter);
    }
}