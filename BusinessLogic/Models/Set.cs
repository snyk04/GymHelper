using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace BusinessLogic.Models;

public sealed class Set : INotifyPropertyChanged
{
    public int Id { get; set; }

    private Exercise exercise;
    public Exercise Exercise
    {
        get => exercise;
        set
        {
            exercise = value;
            OnPropertyChanged();
        }
    }

    private int reps;
    public int Reps
    {
        get => reps;
        set
        {
            reps = value;
            OnPropertyChanged();
        }
    }

    private float weight;
    public float Weight
    {
        get => weight;
        set
        {
            weight = value;
            OnPropertyChanged();
        }
    }

    public Set()
    {
    }

    public Set(Exercise exercise, int reps, float weight)
    {
        this.exercise = exercise;
        this.reps = reps;
        this.weight = weight;
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}