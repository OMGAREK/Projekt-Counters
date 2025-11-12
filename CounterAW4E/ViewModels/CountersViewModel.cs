using CounterAW4E.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Windows.Input;

namespace CounterAW4E.ViewModels;

public class CountersViewModel : INotifyPropertyChanged
{
    private const string StorageKey = "counters_data";

    private string _newCounterName = string.Empty;
    public string NewCounterName
    {
        get => _newCounterName;
        set { _newCounterName = value; OnPropertyChanged(); }
    }

    public ObservableCollection<CounterViewModel> Counters { get; } = new();

    public ICommand AddCounterCommand { get; }

    public CountersViewModel()
    {
        AddCounterCommand = new Command(AddCounter);
        LoadCounters();
    }

    private void AddCounter()
    {
        string name = string.IsNullOrWhiteSpace(NewCounterName) ? "Unnamed Counter" : NewCounterName.Trim();

        var counter = new CounterViewModel(name);

        counter.PropertyChanged += Counter_PropertyChanged;

        Counters.Add(counter);

        NewCounterName = string.Empty;

        SaveCounters();
    }

    private void Counter_PropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(CounterViewModel.Value))
            SaveCounters();
    }

    private void SaveCounters()
    {
        var data = Counters.Select(c => new CounterModel { Name = c.Name, Value = c.Value }).ToList();
        string json = JsonSerializer.Serialize(data);
        Preferences.Set(StorageKey, json);
    }

    private void LoadCounters()
    {
        string json = Preferences.Get(StorageKey, string.Empty);
        if (string.IsNullOrEmpty(json)) return;

        var data = JsonSerializer.Deserialize<List<CounterModel>>(json);
        if (data == null) return;

        foreach (var d in data)
        {
            var counter = new CounterViewModel(d.Name, d.Value);
            counter.PropertyChanged += Counter_PropertyChanged;
            Counters.Add(counter);
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string? name = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
}