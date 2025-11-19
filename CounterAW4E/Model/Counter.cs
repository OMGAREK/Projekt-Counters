using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Runtime.CompilerServices;
using System.ComponentModel;

namespace CounterAW4E.Model
{
    public class Counter : INotifyPropertyChanged
    {
        private int value;

        public event PropertyChangedEventHandler? PropertyChanged;

        public string Name { get; set; }
        public int Value
        {
            get => value; 
            set
            {
                if(this.value != value)
                {
                    this.value = value;
                    OnPropertyChanged();
                }
            }
        }

        public ICommand IncreaseCommand { get; }
        public ICommand DecreaseCommand { get; }

        public Counter()
        {
            IncreaseCommand = new Command(Increse);
            DecreaseCommand = new Command(Decrese);
        }

        private void Increse()
        {
            Value++;
        }

        private void Decrese()
        {
            Value--;
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
