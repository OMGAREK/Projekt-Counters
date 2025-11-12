using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace CounterAW4E.ViewModels;

public class CounterViewModel : INotifyPropertyChanged
{
	private int _value;
	private string _name;

	public string Name
	{
		get => _name;
        set { _name = value; OnPropertyChanged(); }
	}

    public int Value
    {
        get => _value;
        set { _value = value; OnPropertyChanged(); }
    }

    public ICommand IncrementCommand { get; }
    public ICommand DecrementCommand { get; }

    public event PropertyChangedEventHandler PropertyChanged;

    public CounterViewModel(string name, int value = 0)
    {
        Name = name;
        Value = value;
        IncrementCommand = new Command(() => Value++);
        DecrementCommand = new Command(() => Value--);
    }

    protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}