using System.Collections.ObjectModel;
using System.Windows.Input;
using CounterAW4E.Model;

namespace CounterAW4E.ViewModel;

public class CounterViewModel
{
    public ObservableCollection<Counter> Counters { get; set; } = new ObservableCollection<Counter>();

    public string NewCounterName { get; set; } = string.Empty;

    public ICommand AddCounterCommand { get; }

    private readonly string filePath = Path.Combine(FileSystem.AppDataDirectory, "counters.txt");
    public CounterViewModel()
    {
        AddCounterCommand = new Command(AddCounter);

        LoadCounters();
    }

    private void AddCounter()
    {
        if (string.IsNullOrWhiteSpace(NewCounterName))
        {
            NewCounterName = "Unnamed counter";
        }

        Counters.Add(new Counter
        {
            Name = NewCounterName,
            Value = 0
        });

        var c = Counters[Counters.Count - 1];
        c.PropertyChanged += (_, __) => SaveCounters();

        NewCounterName = string.Empty;
    }

    private void SaveCounters()
    {
        List<string> lines = new List<string>();

        foreach (var counter in Counters)
        {
            lines.Add(counter.Name + "," + counter.Value);
        }

        File.WriteAllLines(filePath, lines);
    }

    private void LoadCounters()
    {
        if (!File.Exists(filePath))
            return;

        var lines = File.ReadAllLines(filePath);

        foreach (var line in lines)
        {
            var parts = line.Split(',');

            if (parts.Length == 2)
            {
                int value = int.Parse(parts[1]);

                Counters.Add(new Counter
                {
                    Name = parts[0],
                    Value = value
                });

                var c = Counters[Counters.Count - 1];
                c.PropertyChanged += (_, __) => SaveCounters();
            }
        }
    }
}