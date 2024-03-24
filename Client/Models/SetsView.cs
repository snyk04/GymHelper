using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using BusinessLogic.Models;

namespace Client.New.Models;

public sealed class SetsView : INotifyPropertyChanged
{
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

    private ObservableCollection<Set> sets;
    public ObservableCollection<Set> Sets
    {
        get => sets;
        set
        {
            sets = value;
            OnPropertyChanged();
        }
    }
    
    public SetsView(Exercise exercise, ObservableCollection<Set> sets)
    {
        this.exercise = exercise;
        this.sets = sets;

        foreach (var set in sets)
        {
            set.PropertyChanged += SetPropertyChanged;
        }
        
        sets.CollectionChanged += (_, args) =>
        {
            OnPropertyChanged(nameof(Sets));
            if (args.NewItems == null)
            {
                return;
            }

            foreach (var newItem in args.NewItems)
            {
                var set = (Set)newItem;
                set.PropertyChanged += SetPropertyChanged;
            }
        };
    }

    private void SetPropertyChanged(object sender, PropertyChangedEventArgs args)
    {
        OnPropertyChanged(nameof(Sets));
    }
    
    public event PropertyChangedEventHandler PropertyChanged;

    private void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}