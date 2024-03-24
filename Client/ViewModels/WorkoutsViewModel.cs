using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Input;
using BusinessLogic.Interfaces;
using BusinessLogic.Models;
using Client.New.Models;
using Client.New.ViewModels.Utils;

namespace Client.New.ViewModels;

public sealed class WorkoutsViewModel : ViewModel
{
    private readonly IDatabase database;

    private ObservableCollection<SetsView> setsViews;
    public ObservableCollection<SetsView> SetsViews
    {
        get => setsViews;
        set
        {
            setsViews = value;
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

    private ObservableCollection<Exercise> exercises;
    public ObservableCollection<Exercise> Exercises
    {
        get => exercises;
        set
        {
            exercises = value;
            OnPropertyChanged();
        }
    }

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

    private Exercise newExercise;
    public Exercise NewExercise
    {
        get => newExercise;
        set
        {
            newExercise = value;
            OnPropertyChanged();
        }
    }
    
    public SetsView SelectedSetsView { get; set; }
    public Set SelectedSet { get; set; }
    public DateTime SelectedDate { get; set; }
    
    private Workout selectedWorkout;
    
    public ICommand SelectedDatesChanged { get; }
    public ICommand SelectionChanged { get; }
    public ICommand AddSetCommand { get; }
    public ICommand AddExerciseCommand { get; }
    public ICommand DeleteSetsViewCommand { get; }
    public ICommand DeleteSetCommand { get; }
    
    public WorkoutsViewModel(IDatabase database)
    {
        this.database = database;

        SelectedDate = DateTime.Today;
        Exercises = new ObservableCollection<Exercise>(database.Exercises.GetList());

        SelectedDatesChanged = new DelegateCommand(HandleSelectedDatesChanged);
        SelectionChanged = new DelegateCommand(HandleSelectionChanged);
        AddSetCommand = new DelegateCommand(AddSet);
        AddExerciseCommand = new DelegateCommand(AddExercise);
        DeleteSetsViewCommand = new DelegateCommand(DeleteSetsView);
        DeleteSetCommand = new DelegateCommand(DeleteSet);

        PropertyChanged += (_, args) =>
        {
            if (args.PropertyName == nameof(Exercise))
            {
                HandleExerciseChanged();
            }
        };
    }

    private void HandleSelectedDatesChanged(object obj)
    {
        var eventArgs = (SelectionChangedEventArgs)obj;
        var selectedDate = (DateTime)eventArgs.AddedItems[0];
        
        selectedWorkout = database.Workouts.GetList().FirstOrDefault(workout => workout.DateTime.Date == selectedDate.Date);
        if (selectedWorkout == null)
        {
            SetsViews = [];
            return;
        }
        
        SetsViews = new ObservableCollection<SetsView>(GetSetsViews(selectedWorkout));
    }
    
    private List<SetsView> GetSetsViews(Workout workout)
    {
        var setsByExercises = GetSetsByExercises(workout.Sets);
        return setsByExercises.Values.Select(sets => new SetsView(sets.First().Exercise, new ObservableCollection<Set>(sets))).ToList();
    }

    private Dictionary<Exercise, List<Set>> GetSetsByExercises(List<Set> sets)
    {
        var setsByExercises = new Dictionary<Exercise, List<Set>>();
        foreach (var set in sets)
        {
            if (!setsByExercises.ContainsKey(set.Exercise))
            {
                setsByExercises[set.Exercise] = [];
            }

            setsByExercises[set.Exercise].Add(set);
        }
        return setsByExercises;
    }

    private void HandleSelectionChanged(object obj)
    {
        var eventArgs = (SelectionChangedEventArgs)obj;
        var addedItems = eventArgs.AddedItems;

        if (addedItems.Count == 0)
        {
            Sets = [];
            return;
        }

        var setsView = (SetsView)addedItems[0];
        Sets = new ObservableCollection<Set>(setsView.Sets);
        foreach (var set in Sets)
        {
            set.PropertyChanged += (sender, _) =>
            {
                if (sender is not Set updatedSet)
                {
                    return;
                }

                database.Sets.Update(updatedSet);
            };
        }
        Exercise = Sets.First().Exercise;
    }

    private void HandleExerciseChanged()
    {
        SelectedSetsView.Exercise = Exercise;
        foreach (var set in Sets)
        {
            if (set.Exercise == Exercise)
            {
                continue;
            }
            
            set.Exercise = Exercise;
            database.Sets.Update(set);
        }
    }

    private void AddSet(object obj)
    {
        var set = new Set
        {
            Exercise = Exercise
        };
        
        database.Sets.Add(set);
        selectedWorkout.Sets.Add(set);
        database.Workouts.Update(selectedWorkout);
        
        Sets.Add(set);
        SetsViews.First(setsViews => setsViews.Exercise == exercise).Sets.Add(set);
    }
    
    private void AddExercise(object obj)
    {
        var set = new Set
        {
            Exercise = NewExercise
        };

        selectedWorkout ??= new Workout([], SelectedDate);
        
        database.Sets.Add(set);
        selectedWorkout.Sets.Add(set);
        database.Workouts.Update(selectedWorkout);

        SetsViews ??= [];
        SetsViews.Add(new SetsView(NewExercise, [set]));
    }

    private void DeleteSetsView(object obj)
    {
        if (SelectedSetsView == null)
        {
            return;
        }

        selectedWorkout.Sets.RemoveAll(set => set.Exercise == SelectedSetsView.Exercise);
        database.Workouts.Update(selectedWorkout);

        foreach (var set in SelectedSetsView.Sets)
        {
            database.Sets.Remove(set);
        }

        SetsViews.Remove(SelectedSetsView);
    }

    private void DeleteSet(object obj)
    {
        if (SelectedSet == null)
        {
            return;
        }
        
        database.Workouts.Update(selectedWorkout);
        database.Sets.Remove(SelectedSet);

        SelectedSetsView.Sets.Remove(SelectedSet);
        selectedWorkout.Sets.Remove(SelectedSet);
        Sets.Remove(SelectedSet);
    }
}