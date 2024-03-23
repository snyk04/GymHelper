using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace BusinessLogic.Models;

public sealed class Exercise(string name = "") : INotifyPropertyChanged
{
    public int Id { get; set; }

    public string Name
    {
        get => name;
        set
        {
            name = value;
            OnPropertyChanged();
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}